using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGreco.Levels
{
    /// <summary>
    /// Information about the movement style of the enemy
    /// </summary>
    public enum EnemyType
    {
        Flying,
        Ghost,
        Ground
        
    }
    /// <summary>
    /// Information about the size of the enemy
    /// Small: 1x1
    /// Medium: 2x2:
    /// Large: 3x3
    /// </summary>
    public enum EnemySize
    {
        Small,
        Medium,
        Large
    }


    /// <summary>
    /// Author: Luna Meier
    /// Purpose: Figures out where to create enemies and what basic kind of enemy is needed at the location.
    /// </summary>
    public class Spawner
    {
        /// <summary>
        /// Contains the basic shape and information about the terrain.
        /// </summary>
        private char[][] terrain;
        /// <summary>
        /// Stores information about enemies to be retrieved later on enemy creation.
        /// </summary>
        private List<TemporaryEnemy> enemyData;

        private List<Sector> sectors;
        private List<Sector> validSectors;
        private List<Sector> spawningSectors;

        private Random rand;

        public List<TemporaryEnemy> EnemyData { get { return enemyData; } }

        /// <summary>
        /// Spawns enemies into a EnemyData list that contains information on where to place each enemy.
        /// </summary>
        /// <param name="terrain">The terrain that's already been generated</param>
        /// <param name="seed">Seed to use, -1 is used for total randomness.</param>
        public Spawner(char[][] terrain, int seed = -1)
        {
            if (seed == -1)
                this.rand = new Random();
            else
                this.rand = new Random(seed);

            this.terrain = terrain;
            this.sectors = new List<Sector>();
            this.validSectors = new List<Sector>();
            this.spawningSectors = new List<Sector>();
            this.enemyData = new List<TemporaryEnemy>();

            // Do everything needed to gather a list of enemies.
            CreateSectors();
            ValidateList();
            SpawnEnemies();

        }
        
        /// <summary>
        /// Creates all of the sectors needed for spawning enemies and also determines which ones are valid to spawn things in.
        /// </summary>
        public void CreateSectors()
        {
            // Create a bunch of sectors
            for (int y = 0; y < terrain[0].Length; y += LevelVariables.SPAWNING_SECTOR_LENGTH)
            {
                for (int x = 8; x < terrain.Length - 8; x += LevelVariables.SPAWNING_SECTOR_LENGTH)
                {
                    sectors.Add(new Sector(x, y));
                }
            }

            // Test for the possibility of spawns in each sector
            for (int i = 0; i < sectors.Count; i++)
            {
                Sector mySector = sectors[i];

                for (int y = mySector.yPos; y < mySector.yPos + LevelVariables.SPAWNING_SECTOR_LENGTH; y++)
                {
                    for (int x = mySector.xPos; x < mySector.xPos + LevelVariables.SPAWNING_SECTOR_LENGTH; x++)
                    {
                        // Make sure that there are edge blocks in the sector, and that they have air in them.
                        if ((terrain[x][y] == 'E' || terrain[x][y] == 'M') && AirCheck(x, y))
                        {
                            sectors[i] = new Sector(mySector.xPos, mySector.yPos, true);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Checks the space around a block to see if there's air
        /// </summary>
        public bool AirCheck(int x, int y)
        {
            if (terrain[x - 1][y] == ' ' || terrain[x - 1][y] == 'C' ||
                terrain[x + 1][y] == ' ' || terrain[x + 1][y] == 'C' ||
                terrain[x][y - 1] == ' ' || terrain[x][y - 1] == 'C' ||
                terrain[x][y + 1] == ' ' || terrain[x][y + 1] == 'C')
            {
                return true;
            }
            else
                return false;
        }
        /// <summary>
        /// Figures out which sectors can have valid spawning.
        /// Then randomly choose which ones will have 
        /// </summary>
        public void ValidateList()
        {
            foreach(Sector i in sectors)
            {
                if (i.spawnValid)
                {
                    validSectors.Add(i);
                }
            }

            // Keep on doing this until we're out of credits
            int creditToSpend = LevelVariables.ENEMY_VALUE_PER_LEVEL;
            while (creditToSpend > 0)
            {
                // Pick a sector
                int sectorIndex = rand.Next(0, validSectors.Count);
                Sector addedSector = validSectors[sectorIndex];
                validSectors.RemoveAt(sectorIndex);
                // Pick how much it is worth
                int valueOfSector = rand.Next(-LevelVariables.SPAWNING_SECTOR_VARIATION + LevelVariables.AVERAGE_SECTOR_VALUE,
                    LevelVariables.SPAWNING_SECTOR_VARIATION + LevelVariables.AVERAGE_SECTOR_VALUE + 1);

                // Account for overspending
                if (creditToSpend - valueOfSector < 0)
                {
                    valueOfSector = creditToSpend;
                }

                // Purchase it
                creditToSpend -= valueOfSector;

                spawningSectors.Add(new Sector(addedSector.xPos, addedSector.yPos, true,  valueOfSector));
            }

        }
        /// <summary>
        /// Figures out where enemies should go.
        /// </summary>
        public void SpawnEnemies()
        {
            foreach(Sector mySector in spawningSectors)
            {
                int creditsToSpend = mySector.enemyCredit;
                while (creditsToSpend > 0)
                {
                    // Choose a random enemy size
                    switch(rand.Next(0,3))
                    {
                        // Large enemy
                        case 0:
                            if (creditsToSpend >= LevelVariables.LARGE_ENEMY_VALUE)
                            {
                                // Find a valid spawning location
                                bool noValidPosition = true;
                                int validX = -1;
                                int validY = -1;
                                List<int[]> possibleSpawns = new List<int[]>();
                                // Create a list of the possible positions so we can be random
                                for (int x = 0; x < (int)Math.Pow(LevelVariables.SPAWNING_SECTOR_LENGTH,2); x++)
                                {
                                    possibleSpawns.Add(new int[2]);
                                    possibleSpawns[x][0] = mySector.xPos + x % LevelVariables.SPAWNING_SECTOR_LENGTH;
                                    possibleSpawns[x][1] = mySector.yPos + x / LevelVariables.SPAWNING_SECTOR_LENGTH;
                                }

                                // Check for specific success of a location, but make sure to not check forever.
                                while(noValidPosition)
                                {
                                    if (possibleSpawns.Count == 0)
                                    {
                                        break;
                                    }
                                    int spawnIndex = rand.Next(0, possibleSpawns.Count);
                                    // If the spot is valid, get out of the loop
                                    if (SpaceCheck(EnemySize.Large, possibleSpawns[spawnIndex][0], possibleSpawns[spawnIndex][1]))
                                    {
                                        noValidPosition = false;
                                        validX = possibleSpawns[spawnIndex][0];
                                        validY = possibleSpawns[spawnIndex][1];
                                        continue;
                                    }
                                    // If the location isn't valid, then remove that spot from the list
                                    possibleSpawns.RemoveAt(spawnIndex);
                                }
                                // In the case that there was no valid spot for a large enemy to spawn, we should make it a medium or small enemy instead.
                                if (noValidPosition)
                                {
                                    goto case 1;
                                }
                                // Otherwise, make the enemy and subtract the credits.
                                enemyData.Add(new TemporaryEnemy(validX, validY, (EnemyType)rand.Next(0,3), EnemySize.Large));
                                creditsToSpend -= LevelVariables.LARGE_ENEMY_VALUE;

                                break;
                            }
                            else // If you don't have enough credits for a big enemy get a medium one instead
                                goto case 1;
                        // Medium enemy
                        case 1:
                            if (creditsToSpend >= LevelVariables.MEDIUM_ENEMY_VALUE)
                            {
                                // Find a valid spawning location
                                bool noValidPosition = true;
                                int validX = -1;
                                int validY = -1;
                                List<int[]> possibleSpawns = new List<int[]>();
                                // Create a list of the possible positions so we can be random
                                for (int x = 0; x < (int)Math.Pow(LevelVariables.SPAWNING_SECTOR_LENGTH, 2); x++)
                                {
                                    possibleSpawns.Add(new int[2]);
                                    possibleSpawns[x][0] = mySector.xPos + x % LevelVariables.SPAWNING_SECTOR_LENGTH;
                                    possibleSpawns[x][1] = mySector.yPos + x / LevelVariables.SPAWNING_SECTOR_LENGTH;
                                }

                                // Check for specific success of a location, but make sure to not check forever.
                                while (noValidPosition)
                                {
                                    if (possibleSpawns.Count == 0)
                                    {
                                        break;
                                    }
                                    int spawnIndex = rand.Next(0, possibleSpawns.Count);
                                    // If the spot is valid, get out of the loop
                                    if (SpaceCheck(EnemySize.Medium, possibleSpawns[spawnIndex][0], possibleSpawns[spawnIndex][1]))
                                    {
                                        noValidPosition = false;
                                        validX = possibleSpawns[spawnIndex][0];
                                        validY = possibleSpawns[spawnIndex][1];
                                        continue;
                                    }
                                    // If the location isn't valid, then remove that spot from the list
                                    possibleSpawns.RemoveAt(spawnIndex);
                                }
                                // In the case that there was no valid spot for a large enemy to spawn, we should make it a medium or small enemy instead.
                                if (noValidPosition)
                                {
                                    goto case 2;
                                }
                                // Otherwise, make the enemy and subtract the credits.
                                enemyData.Add(new TemporaryEnemy(validX, validY, (EnemyType)rand.Next(0, 3), EnemySize.Medium));
                                creditsToSpend -= LevelVariables.MEDIUM_ENEMY_VALUE;
                                break;
                            }
                            else // If you don't have enough credits for a medium enemy get a small one instead
                                goto case 2;
                        // Small enemy
                        case 2:
                            if (creditsToSpend >= LevelVariables.SMALL_ENEMY_VALUE)
                            {
                                // Find a valid spawning location
                                bool noValidPosition = true;
                                int validX = -1;
                                int validY = -1;
                                List<int[]> possibleSpawns = new List<int[]>();
                                // Create a list of the possible positions so we can be random
                                for (int x = 0; x < (int)Math.Pow(LevelVariables.SPAWNING_SECTOR_LENGTH, 2); x++)
                                {
                                    possibleSpawns.Add(new int[2]);
                                    possibleSpawns[x][0] = mySector.xPos + x % LevelVariables.SPAWNING_SECTOR_LENGTH;
                                    possibleSpawns[x][1] = mySector.yPos + x / LevelVariables.SPAWNING_SECTOR_LENGTH;
                                }

                                // Check for specific success of a location, but make sure to not check forever.
                                while (noValidPosition)
                                {
                                    if (possibleSpawns.Count == 0)
                                    {
                                        break;
                                    }
                                    int spawnIndex = rand.Next(0, possibleSpawns.Count);
                                    // If the spot is valid, get out of the loop
                                    if (SpaceCheck(EnemySize.Small, possibleSpawns[spawnIndex][0], possibleSpawns[spawnIndex][1]))
                                    {
                                        noValidPosition = false;
                                        validX = possibleSpawns[spawnIndex][0];
                                        validY = possibleSpawns[spawnIndex][1];
                                        continue;
                                    }
                                    // If the location isn't valid, then remove that spot from the list
                                    possibleSpawns.RemoveAt(spawnIndex);
                                }
                                // In the case that there was no valid spot for a large enemy to spawn, we should make it a medium or small enemy instead.
                                if (noValidPosition)
                                {
                                    creditsToSpend = 0;
                                    break;
                                }
                                // Otherwise, make the enemy and subtract the credits.
                                enemyData.Add(new TemporaryEnemy(validX, validY, (EnemyType)rand.Next(0, 3), EnemySize.Small));
                                creditsToSpend -= LevelVariables.SMALL_ENEMY_VALUE;
                                break;
                            }
                            else // If you don't even have enough credits for a small enemy then set the number of credits you have to 0; there's no reason to check anymore.
                            {
                                creditsToSpend = 0;
                                break; 
                            }
                    }
                }
            }
        }
        /// <summary>
        /// Figures out if you can spawn an enemy of the size asked for
        /// Returns true if you can spawn the enemy
        /// </summary>
        public bool SpaceCheck(EnemySize size, int xPos, int yPos)
        {
            switch(size)
            {
                // Large enemies take up a 2x2 space
                case EnemySize.Large:
                    if ((terrain[xPos][yPos] == ' ' ||  terrain[xPos][yPos] == 'C') &&
                        (terrain[xPos + 1][yPos] == ' ' ||  terrain[xPos + 1][yPos] == 'C') &&
                        (terrain[xPos][yPos + 1] == ' ' ||  terrain[xPos][yPos + 1] == 'C') &&
                        (terrain[xPos + 1][yPos + 1] == ' ' ||  terrain[xPos + 1][yPos + 1] == 'C'))
                    {
                        return true;
                    }
                    return false;

                // Medium and small enemies only take up 1 block worth of space.
                case EnemySize.Medium:
                case EnemySize.Small:
                    if (terrain[xPos][yPos] == ' ' || terrain[xPos][yPos] == 'C')
                        return true;
                    return false;
                default:
                    return false;
            }
        }

    }

    public struct TemporaryEnemy
    {
        /// <summary>
        /// X position of the enemy.
        /// </summary>
        public int xPosition;
        /// <summary>
        /// Y position of the enemy.
        /// </summary>
        public int yPosition;
        /// <summary>
        /// The basic style of the enemy's movement.
        /// </summary>
        public EnemyType movementType;
        /// <summary>
        /// The size of the enemy.
        /// </summary>
        public EnemySize size;

        public TemporaryEnemy(int xPosition, int yPosition, EnemyType movementType, EnemySize size)
        {
            this.xPosition = xPosition;
            this.yPosition = yPosition;
            this.movementType = movementType;
            this.size = size;
        }


    }

    public struct Sector
    {
        public int xPos;
        public int yPos;
        public bool spawnValid;
        public int enemyCredit;
        
        public Sector(int xPos, int yPos, bool spawnValid = false, int enemyCredit = 0)
        {
            this.xPos = xPos;
            this.yPos = yPos;
            this.spawnValid = spawnValid;
            this.enemyCredit = enemyCredit;
        }
    }

    public struct Position
    {
        public int xPos;
        public int yPos;
        public bool spawnValid;

        public Position(int xPos, int yPos, bool spawnValid)
        {
            this.xPos = xPos;
            this.yPos = yPos;
            this.spawnValid = spawnValid;
        }

    }

}

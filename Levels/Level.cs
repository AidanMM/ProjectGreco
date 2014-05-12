using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using ProjectGreco.GameObjects;


namespace ProjectGreco.Levels
{
    

    /// <summary>
    /// Creates a level when information is put in.
    /// </summary>
    public class Level : BaseState
    {
        private bool renderBackground;
        

        public bool RenderBackground { get { return renderBackground; } }
        public LevelName LevelType { get { return levelType; } }

        private string mainTexture;
        private string secondaryTexture;
        private Texture2D mainEdge;
        private Texture2D secondaryEdge;

        private Random myRandom;

        private Player myPlayer;


        public string MainTexture { get { return mainTexture; } }
        public string SecondaryTexture { get { return secondaryTexture; } }
        public Texture2D MainEdge { get { return mainEdge; } }
        public Texture2D SecondaryEdge { get { return secondaryEdge; } }

        private Map myMap;
        private Spawner mySpawner;

        public Map MyMap { get { return myMap; } }
        public Spawner MySpawner { get { return mySpawner; } }

        public Random MyRandom { get { return myRandom; } }

        public Player MyPlayer { get { return myPlayer; } }

        

        /// <summary>
        /// Creates a level based on the leveltype supplied
        /// </summary>
        /// <param name="levelType">The LevelType you want to create</param>
        /// <param name="renderBackground">Bool that determines whether or not we create background blocks</param>
        public Level(LevelName levelType, Player myPlayer = null, bool renderBackground = true) : base()
        {
            

            myRandom = new Random();

            this.renderBackground = renderBackground;
            this.levelType = levelType;
            this.myPlayer = myPlayer;
            currLevel = levelType;

            ChooseTextures();
            CreateMap();
            SetupLevel();

            MediaPlayer.Play(Game1.SONG_LIBRARY["StartMusic"]);   

            
        }

        /// <summary>
        /// Chooses which textures are to be used in the creation of blocks.
        ///         // NEEDS TEXTURES TO BE IMPLEMENTED
        /// </summary>
        public void ChooseTextures()
        {
            switch(levelType)
            {
                case LevelName.Desert:
                    this.mainTexture = "desertFiller";
                    this.secondaryTexture = "caveFillerBlock";
                    this.mainEdge = Game1.IMAGE_DICTIONARY["desertEdge"];
                    this.secondaryEdge = Game1.IMAGE_DICTIONARY["defaultEdge"];
                    break;

                case LevelName.Forest:
                    this.mainTexture = "forestDirtBlock";
                    this.secondaryTexture = "caveFillerBlock";
                    this.mainEdge = Game1.IMAGE_DICTIONARY["forestEdge"];
                    this.secondaryEdge = Game1.IMAGE_DICTIONARY["caveEdge"];
                    break;

                case LevelName.Hills:
                    this.mainTexture = "dirtBlock";
                    this.secondaryTexture = "caveFillerBlock";
                    this.mainEdge = Game1.IMAGE_DICTIONARY["dirtEdge"];
                    this.secondaryEdge = Game1.IMAGE_DICTIONARY["caveEdge"];
                    break;

                case LevelName.Ice:
                    this.mainTexture = "iceFiller";
                    this.secondaryTexture = "caveFillerBlock";
                    this.mainEdge = Game1.IMAGE_DICTIONARY["iceEdge"];
                    this.secondaryEdge = Game1.IMAGE_DICTIONARY["iceEdge"];
                    break;
            }
        }

        /// <summary>
        /// Creates the map based on the level type chosen.
        ///         // NEEDS ALGORITHMS TO BE IMPLEMENTED
        /// </summary>
        public void CreateMap()
        {
            switch(levelType)
            {
                case LevelName.Desert:
                    myMap = new Map(AlgorithmType.Desert);
                    break;
                case LevelName.Forest:
                    myMap = new Map(AlgorithmType.Desert);
                    break;
                case LevelName.Hills:
                    myMap = new Map(AlgorithmType.Hills);
                    break;
                case LevelName.Ice:
                    myMap = new Map(AlgorithmType.HillsDesert);
                    break;
            }
            
        }

        /// <summary>
        /// Positions the player to the correct starting location.
        /// </summary>
        public void PositionPlayer()
        {
            myPlayer.Acceleration = new Vector2(0, 0);
            myPlayer.Velocity = new Vector2(0, 0);
            myPlayer.Position = new Vector2(200, (LevelVariables.HEIGHT - LevelVariables.GROUND_HEIGHT - 3) * 64);
        }

        /// <summary>
        /// Creates the level based on the information supplied.
        /// </summary>
        public void SetupLevel()
        {
            if (myPlayer == null)
            {
                myPlayer = new Player(new Vector2(200, (LevelVariables.HEIGHT - LevelVariables.GROUND_HEIGHT - 3) * 64), Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["WalkRight"], Game1.ANIMATION_DICTIONARY["WalkLeft"]));
            }
            // Create the player
            AddObjectToHandler("Player", myPlayer);
            // Create the cursor
            AddObjectToHandler("Cursor", new Cursor(new Vector2(200, (LevelVariables.HEIGHT - LevelVariables.GROUND_HEIGHT - 3) * 64), Game1.IMAGE_DICTIONARY["cursor"]));
            //AddObjectToHandler("Button", new Button(new Vector2(200, 200), Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["ButtonStates"]), "clickable test", true));
            //AddObjectToHandler("NoButton", new Button(new Vector2(200, 300), Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["ButtonStates"]), "unclickable test", true));

            mySpawner = new Spawner(myMap.Terrain, -1);

            int edgeTiles = 0;
            int backgroundTiles = 0;

            for (int x = 0; x < LevelVariables.WIDTH; x++)
            {
                for (int y = 0; y < LevelVariables.HEIGHT; y++)
                {
                    // Main Edge Tiles
                    if (myMap.Terrain[x][y] == 'E')
                    {
                        AddObjectToHandler("EdgeTile", new EdgeTile(new Vector2(x * 64, (LevelVariables.HEIGHT - y) * 64),
                            mainEdge, myMap.Terrain,
                            Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY[mainTexture])), edgeTiles);
                        edgeTiles++;
                    }
                    // Secondary Edge Tiles
                    if (myMap.Terrain[x][y] == 'M')
                    {
                        AddObjectToHandler("EdgeTile", new EdgeTile(new Vector2(x * 64, (LevelVariables.HEIGHT - y) * 64),
                            secondaryEdge, myMap.Terrain,
                            Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY[secondaryTexture])), edgeTiles);
                        edgeTiles++;
                    }
                   
                    if (renderBackground)
                    {
                        // Main Background Tiles
                        if (myMap.Terrain[x][y] == 'O')
                        {
                            AddObjectToHandler("BackgroundTile", new BackgroundTile(new Vector2(x * 64, (LevelVariables.HEIGHT - y) * 64),
                                Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY[mainTexture])), backgroundTiles);
                            backgroundTiles++;
                        }
                        // Secondary Background Tiles
                        if (myMap.Terrain[x][y] == 'C')
                        {
                            AddObjectToHandler("BackgroundTile", new BackgroundTile(new Vector2(x * 64, (LevelVariables.HEIGHT - y) * 64),
                                Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY[secondaryTexture])), backgroundTiles);
                            backgroundTiles++;
                        }
                    }
                }
            }

            // Look at every enemy
            foreach (TemporaryEnemy myEnemy in mySpawner.EnemyData)
            {
                string enemyType = "";
                // Figure out which enemy type to use, searching by movement type and size.
                switch (myEnemy.movementType)
                {
                    case EnemyType.Flying:
                        if (myEnemy.size == EnemySize.Large)
                        {
                            enemyType = "FlyingEnemyLarge";
                        }
                        if (myEnemy.size == EnemySize.Medium)
                        {
                            enemyType = "FlyingEnemy";
                        }
                        if (myEnemy.size == EnemySize.Small)
                        {
                            enemyType = "FlyingEnemySmall";
                        }
                        break;
                    case EnemyType.Ghost:
                        if (myEnemy.size == EnemySize.Large)
                        {
                            enemyType = "GhostEnemyLarge";
                        }
                        if (myEnemy.size == EnemySize.Medium)
                        {
                            enemyType = "GhostEnemy";
                        }
                        if (myEnemy.size == EnemySize.Small)
                        {
                            enemyType = "GhostEnemySmall";
                        }
                        break;
                    case EnemyType.Ground:
                        if (myEnemy.size == EnemySize.Large)
                        {
                            enemyType = "GroundEnemyLarge";
                        }
                        if (myEnemy.size == EnemySize.Medium)
                        {
                            enemyType = "Onion";
                        }
                        if (myEnemy.size == EnemySize.Small)
                        {
                            enemyType = "GroundEnemySmall";
                        }
                        break;
                }

                switch (myEnemy.size)
                {
                    case EnemySize.Large:
                        AddObjectToHandler("Enemy", new BaseEnemy(Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY[enemyType]),
                            new Vector2(myEnemy.xPosition * 64, (LevelVariables.HEIGHT - myEnemy.yPosition) * 64), myEnemy.movementType,
                            myRandom, myPlayer, EnemySize.Large));
                        break;
                    case EnemySize.Medium:
                        AddObjectToHandler("Enemy", new BaseEnemy(Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY[enemyType]),
                            new Vector2(myEnemy.xPosition * 64, (LevelVariables.HEIGHT - myEnemy.yPosition) * 64), myEnemy.movementType,
                            myRandom, myPlayer, EnemySize.Medium));
                        break;
                    case EnemySize.Small:
                        AddObjectToHandler("Enemy", new BaseEnemy(Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY[enemyType]),
                            new Vector2(myEnemy.xPosition * 64, (LevelVariables.HEIGHT - myEnemy.yPosition) * 64), myEnemy.movementType,
                            myRandom, myPlayer, EnemySize.Small));
                        break;

                }

                
            }

            AddObjectToHandler("Homeworld", new LevelPortal(new Vector2((LevelVariables.WIDTH - 4) * 64, (LevelVariables.HEIGHT - LevelVariables.GROUND_HEIGHT - 1) * 64),
                    Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["ButtonStates"]), new HomeWorld(myPlayer), (LevelObjectDictionary["Player"] as Player)));
            //Sort all of the objects by their zOrder
            SortByZorder();
        }
    }
}

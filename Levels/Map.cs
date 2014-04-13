using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectGreco.Levels.Algorithms;

namespace ProjectGreco.Levels
{
    /// <summary>
    /// Author: Luna Meier
    /// Purpose: Contains code for creating a terrain and then randomly creating differences in it.
    /// </summary>
    class Map
    {
        //
        // Fields
        //

        /// <summary>
        /// Contains the array of characters that defines the terrain
        /// </summary>
        private char[][] terrain;

        private Random rand;

        //
        // Properties
        //

        /// <summary>
        /// Contains the array of characters that defines the terrain
        /// </summary>
        public char[][] Terrain { get { return terrain; } }

        /// <summary>
        /// Contains the code necessary to create a Terrain and stores that terrain.
        /// </summary>
        /// <param name="seed">The seed used to generate the map.  Seed -1 is fully random.</param>
        /// <param name="algorithm">The specific algorithm to use to generate the map.</param>
        public Map(AlgorithmType algorithm = AlgorithmType.Hills, int seed = -1)
        {
            if (seed == -1)
            {
                rand = new Random();
            }
            else
                rand = new Random(seed);

            terrain = new char[LevelVariables.WIDTH][];

            // Create a jagged array with a width and height set via constants.
            for (int i = 0; i < LevelVariables.WIDTH; i++)
            {
                terrain[i] = new char[LevelVariables.HEIGHT];
            }

            // Try to generate a map 1000 times, if it fails 1000 times in a row throw an exception.
            int x = 0;
            while (x < 1000)
            {
                // Attempt to generate a map
                try
                {
                    GenerateMap(algorithm, seed);
                    EdgeDetection();
                    break;
                }
                catch (Exception)
                {
                    x++;
                }
            }
            if (x == 1000)
            {
                throw new Exception("Map Generation Failed."); // Custom exception to be implemented.
            }
        }

        /// <summary>
        /// Creates a map based on the requested algorithm.
        /// </summary>
        public void GenerateMap(AlgorithmType algorithm, int seed)
        {
            switch (algorithm)
            {
                case AlgorithmType.Hills: // Use the case relevant to your algorithm type
                    // Create the appropriate algorithm, inputing your personal variables from LevelVariables as shown in the algorithms information.
                    if (true) // Neccessary to use the same names for variables. 
                    {
                        HillGeneration hillGen = new HillGeneration(terrain, LevelVariables.WIDTH, LevelVariables.HEIGHT, LevelVariables.GROUND_HEIGHT, LevelVariables.SAFE_ZONE_WIDTH,
                            LevelVariables.HILL_MIN_WIDTH, LevelVariables.HILL_MAX_WIDTH, LevelVariables.HILL_MIN_HEIGHT, LevelVariables.HILL_MAX_HEIGHT,
                            LevelVariables.HILL_SMOOTH_WIDTH, LevelVariables.HILL_VARIATION, LevelVariables.HILL_PEAK_AVERAGE_PERCENT, seed);
                        hillGen.Initialize(); // Run the initialization code.
                        hillGen.Shape(); // Run the shaping code
                        this.terrain = hillGen.Terrain; // Save the terrain changes.
                    }
                    // Here I would add the next algorithm if this was a complex algorithm type.
                    break;

                case AlgorithmType.Desert: // Use the case relevant to your algorithm type
                    if (true)
                    {
                        // Create the appropriate algorithm, inputing your personal variables from LevelVariables as shown in the algorithms information.
                        HillGeneration desertGen = new HillGeneration(terrain, LevelVariables.WIDTH, LevelVariables.HEIGHT, LevelVariables.GROUND_HEIGHT, LevelVariables.SAFE_ZONE_WIDTH,
                            LevelVariables.DESERT_MIN_WIDTH, LevelVariables.DESERT_MAX_WIDTH, LevelVariables.DESERT_MIN_HEIGHT, LevelVariables.DESERT_MAX_HEIGHT,
                            LevelVariables.DESERT_SMOOTH_WIDTH, LevelVariables.DESERT_VARIATION, LevelVariables.DESERT_PEAK_AVERAGE_PERCENT, seed);
                        desertGen.Initialize(); // Run the initialization code.
                        desertGen.Shape(); // Run the shaping code
                        this.terrain = desertGen.Terrain; // Save the terrain changes.
                    }
                    // Here I would add the next algorithm if this was a complex algorithm type.
                    break;

                case AlgorithmType.HillsDesert: // Example Complex Algorithm
                    if (true)
                    {
                        // Create the appropriate algorithm, inputing your personal variables from LevelVariables as shown in the algorithms information.
                        HillGeneration hillGen = new HillGeneration(terrain, LevelVariables.WIDTH, LevelVariables.HEIGHT, LevelVariables.GROUND_HEIGHT, LevelVariables.SAFE_ZONE_WIDTH,
                            LevelVariables.HILL_MIN_WIDTH, LevelVariables.HILL_MAX_WIDTH, LevelVariables.HILL_MIN_HEIGHT, LevelVariables.HILL_MAX_HEIGHT,
                            LevelVariables.HILL_SMOOTH_WIDTH, LevelVariables.HILL_VARIATION, LevelVariables.HILL_PEAK_AVERAGE_PERCENT, seed);
                        hillGen.Initialize(0, LevelVariables.WIDTH / 2, 0, LevelVariables.HEIGHT); // Run the initialization code.
                        hillGen.Shape(0, LevelVariables.WIDTH / 2, 0, LevelVariables.HEIGHT); // Run the shaping code
                        this.terrain = hillGen.Terrain; // Save the terrain changes.

                    }
                    if (true)
                    {
                        // Create the appropriate algorithm, inputing your personal variables from LevelVariables as shown in the algorithms information.
                        HillGeneration desertGen = new HillGeneration(terrain, LevelVariables.WIDTH, LevelVariables.HEIGHT, LevelVariables.GROUND_HEIGHT, LevelVariables.SAFE_ZONE_WIDTH,
                            LevelVariables.DESERT_MIN_WIDTH, LevelVariables.DESERT_MAX_WIDTH, LevelVariables.DESERT_MIN_HEIGHT, LevelVariables.DESERT_MAX_HEIGHT,
                            LevelVariables.DESERT_SMOOTH_WIDTH, LevelVariables.DESERT_VARIATION, LevelVariables.DESERT_PEAK_AVERAGE_PERCENT, seed);
                        desertGen.Initialize(LevelVariables.WIDTH / 2, LevelVariables.WIDTH, 0, LevelVariables.HEIGHT); // Run the initialization code.
                        desertGen.Shape(LevelVariables.WIDTH / 2, LevelVariables.WIDTH, 0, LevelVariables.HEIGHT); // Run the shaping code
                        this.terrain = desertGen.Terrain; // Save the terrain changes.
                    }
                    if (true)
                    {
                        // Let's just apply caves EVERYWHERE!
                        int numberOfPoints = rand.Next(LevelVariables.CAVE_MIN_POINTS, LevelVariables.CAVE_MAX_POINTS);
                        List<int[]> points = new List<int[]>();

                        for (int m = 0; m <= numberOfPoints; m++)
                        {
                            // Create points for the caves to go in, but make sure that they don't leave the map.
                            points.Add(new int[3]{
                                rand.Next(LevelVariables.CAVE_MAX_RADIUS + 1, LevelVariables.WIDTH - LevelVariables.CAVE_MAX_RADIUS),
                                rand.Next(LevelVariables.CAVE_MAX_RADIUS + 1, LevelVariables.HEIGHT - LevelVariables.CAVE_MAX_RADIUS),
                                rand.Next(LevelVariables.CAVE_MIN_RADIUS, LevelVariables.CAVE_MAX_RADIUS)});
                        }
                        // Create the caves
                        CaveGeneration caveGen = new CaveGeneration(terrain, points, LevelVariables.WIDTH, LevelVariables.HEIGHT, LevelVariables.GROUND_HEIGHT, LevelVariables.SAFE_ZONE_WIDTH,
                            seed);
                        caveGen.Shape();


                    }

                    break;

                case AlgorithmType.Cave:
                    if (true)
                    {
                        int numberOfPoints = rand.Next(LevelVariables.CAVE_MIN_POINTS, LevelVariables.CAVE_MAX_POINTS);
                        List<int[]> points = new List<int[]>();

                        for (int m = 0; m <= numberOfPoints; m++)
                        {
                            // Create points for the caves to go in, but make sure that they don't leave the map.
                            points.Add(new int[3]{
                                rand.Next(LevelVariables.CAVE_MAX_RADIUS + 1, LevelVariables.WIDTH - LevelVariables.CAVE_MAX_RADIUS),
                                rand.Next(LevelVariables.CAVE_MAX_RADIUS + 1, LevelVariables.HEIGHT - LevelVariables.CAVE_MAX_RADIUS),
                                rand.Next(LevelVariables.CAVE_MIN_RADIUS, LevelVariables.CAVE_MAX_RADIUS)});
                        }
                        // Create the caves
                        CaveGeneration caveGen = new CaveGeneration(terrain, points, LevelVariables.WIDTH, LevelVariables.HEIGHT, LevelVariables.GROUND_HEIGHT, LevelVariables.SAFE_ZONE_WIDTH,
                            seed);
                        caveGen.Initialize();
                        caveGen.Shape();
                    }


                    break;
            }
        }

        /// <summary>
        /// Looks through a map and checks each block to see if it's an edge block.  Replaces as appropriate.
        /// </summary>
        public void EdgeDetection()
        {
            for (int x = 0; x < terrain.Length; x++)
            {
                for (int y = 0; y < terrain[x].Length; y++)
                {
                    char currentBlock = terrain[x][y];

                    // Make sure block isn't air
                    switch (currentBlock)
                    {
                        case ' ':
                        case 'C':
                            continue;
                    }

                    char[] edgeBlocks = new char[8] {'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X'};

                    //
                    // Edge of Screen Detection/Acquiring blocks on the side
                    //

                    if (x > 0)
                        edgeBlocks[0] = terrain[x - 1][y];

                    if (x < terrain.Length - 1)
                        edgeBlocks[1] = terrain[x + 1][y];

                    if (y > 0)
                        edgeBlocks[2] = terrain[x][y - 1];

                    if (y < terrain[x].Length - 1)
                        edgeBlocks[3] = terrain[x][y + 1];

                    if (x > 0 && y > 0)
                        edgeBlocks[4] = terrain[x - 1][y - 1];

                    if (x < terrain.Length - 1 && y > 0)
                        edgeBlocks[5] = terrain[x + 1][y - 1];

                    if (x > 0 && y < terrain[x].Length - 1)
                        edgeBlocks[6] = terrain[x - 1][y + 1];

                    if (x < terrain.Length - 1 && y < terrain[x].Length - 1)
                        edgeBlocks[7] = terrain[x + 1][y + 1];

                    //
                    // Make changes if possible.
                    //

                    foreach (char i in edgeBlocks)
                    {
                        switch (i)
                        {
                            case ' ':
                                terrain[x][y] = 'E';
                                break;

                            case 'C':
                                terrain[x][y] = 'M';
                                break;
                        }
                    }

                }
            }
        }
    }
}

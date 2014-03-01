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
    /// Purpose: Contains all of the basic framework for creating random terrain.
    /// Acts as a hub for the different algorithm types.  If you want to add a new
    /// generation type, do it here under Generate Map.
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
            terrain = new char[LevelVariables.WIDTH][];

            // Create a jagged array with a width and height set via constants.
            for(int i = 0; i < LevelVariables.WIDTH; i++)
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
            switch(algorithm)
            {
                case AlgorithmType.Hills: // Use the case relevant to your algorithm type
                    // Create the appropriate algorithm, inputing your personal variables from LevelVariables as shown in the algorithms information.
                    if (true)
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
                        hillGen.Initialize(0,LevelVariables.WIDTH / 2, 0, LevelVariables.HEIGHT); // Run the initialization code.
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
                    break;

            }


        }


    }
}

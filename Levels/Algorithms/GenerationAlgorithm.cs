using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGreco.Levels.Algorithms
{
    /// <summary>
    /// Author: Luna Meier
    /// Purpose: Base class for the different algorithms that will shape
    /// terrain into unique shapes.
    /// </summary>
    public abstract class GenerationAlgorithm
    {
        //
        // Fields
        //
        
        /// <summary>
        /// Contains the array of characters that defines the terrain
        /// </summary>
        protected char[][] terrain; // Array holding the actual terrain
        /// <summary>
        /// The seed that the Random object uses.
        /// </summary>
        protected int seed; // Seed for the random object
        /// <summary>
        /// The Random object that is used.
        /// </summary>
        private Random rand;

        //
        // Properties
        //

        /// <summary>
        /// Contains the array of characters that defines the terrain
        /// </summary>
        public char[][] Terrain { get { return terrain; } }
        /// <summary>
        /// The seed that the Random object uses.
        /// </summary>
        public int Seed { get { return seed; } }
        /// <summary>
        /// The Random object that is used.
        /// </summary>
        public Random Rand { get { return rand; } }

        /// <summary>
        /// Runs an algorithm on a terrain to shape it into a certain style.
        /// </summary>
        /// <param name="seed">The seed used to generate the map.  Seed -1 is fully random.</param>
        /// <param name="terrain">The array to apply terrain generation to.</param>
        public GenerationAlgorithm(char[][] terrain, int seed = -1)
        {
            this.terrain = terrain;
            this.seed = seed;

            // If seed 0 is chosen, a random map will generate.
            // Otherwise the random number generator will use the specified seed.
            if (seed == -1)
                this.rand = new Random();
            else
                this.rand = new Random(seed);
        }

        /// <summary>
        /// This code should set the framework for your shaping code.
        /// It will plot out a basic shape to carve and sculpt.
        /// </summary>
        abstract public void Initialize();
        
        /// <summary>
        /// This code should set the framework for your shaping code.
        /// It will plot out a basic shape to carve and sculpt.
        /// Allows you to specify part of a map to edit.
        /// </summary>
        /// <param name="startX">The x-coordinate the shaping begins at.</param>
        /// <param name="endX">The x-coordinate the shaping ends at.</param>
        /// <param name="startY">The y-coordinate the shaping begins at.</param>
        /// <param name="endY">The y-coordinate the shaping ends at.</param>
        abstract public void Initialize(int startX, int endX, int startY, int endY);
        
        /// <summary>
        /// Should change the landscape in some iterative manner.
        /// </summary>
        abstract public void Shape(); 

        /// <summary>
        /// Should change the landscape in some iterative manner.
        /// Allows you to specify part of a map to edit.
        /// </summary>
        /// <param name="startX">The x-coordinate the shaping begins at.</param>
        /// <param name="endX">The x-coordinate the shaping ends at.</param>
        /// <param name="startY">The y-coordinate the shaping begins at.</param>
        /// <param name="endY">The y-coordinate the shaping ends at.</param>
        abstract public void Shape(int startX, int endX, int startY, int endY);
    }
}

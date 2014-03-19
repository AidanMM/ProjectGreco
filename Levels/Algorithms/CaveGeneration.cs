using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGreco.Levels.Algorithms
{
    /// <summary>
    /// Contains the methods necessary to implement a cave system in a world.
    /// </summary>
    public class CaveGeneration : GenerationAlgorithm
    {
        //
        // Fields
        //

        private List<int[]> points;

        #region constants

        //
        // Fields
        //


        /// <summary>
        /// Width of the total terrain in a level.
        /// </summary>
        private int WIDTH;
        /// <summary>
        /// Height of the total terrain in a level.
        /// </summary>
        private int HEIGHT;
        /// <summary>
        /// Average height of a "ground level."
        /// </summary>
        private int GROUND_HEIGHT;
        /// <summary>
        /// Width of the safe zone.
        /// </summary>
        private int SAFE_ZONE_WIDTH;

        //
        // Properties
        //

        /// <summary>
        /// Width of the total terrain in a level.
        /// </summary>
        public int Width { get { return WIDTH; } }
        /// <summary>
        /// Height of the total terrain in a level.
        /// </summary>
        public int Height { get { return HEIGHT; } }
        /// <summary>
        /// Average height of a "ground level."
        /// </summary>
        public int Ground_Height { get { return GROUND_HEIGHT; } }
        /// <summary>
        /// Width of the safe zone.
        /// </summary>
        public int Safe_Zone_Width { get { return SAFE_ZONE_WIDTH; } }


        #endregion


        //
        // Properties
        //

        public List<int[]> Points { get { return points; } }

        /// <summary>
        /// An algorithm that applies to a terrain and creates a cavern.
        /// </summary>
        /// <param name="terrain">The map terrain passed in to be edited</param>
        /// <param name="seed">The seed used to generate the map.  Seed -1 is fully random.</param>
        public CaveGeneration(char[][] terrain, List<int[]> points,
            int WIDTH, int HEIGHT, int GROUND_HEIGHT, int SAFE_ZONE_WIDTH,
            int seed = -1)
            : base(terrain, seed)
        {
            this.points = points;
            this.WIDTH = WIDTH;
            this.HEIGHT = HEIGHT;
            this.GROUND_HEIGHT = GROUND_HEIGHT;
            this.SAFE_ZONE_WIDTH = SAFE_ZONE_WIDTH;
        }

        /// <summary>
        /// Fills the area up with land that can be filled with caverns.
        /// </summary>
        public override void Initialize()
        {
            Initialize(0, WIDTH, 0, HEIGHT);
        }

        /// <summary>
        /// Fills the area up with land that can be filled with caverns.
        /// Allows you to specify part of a map to edit.
        /// </summary>
        /// <param name="startX"></param>
        /// <param name="endX"></param>
        /// <param name="startY"></param>
        /// <param name="endY"></param>
        public override void Initialize(int startX, int endX, int startY, int endY)
        {
            for (int i = startX; i < endX; i++)
            {
                for (int j = startY; j < endY; j++)
                {
                    // Create the untouched layer
                    if (j < 1)
                    {
                        terrain[i][j] = 'X';
                    }
                    // Fill up everything else.
                    else
                    {
                        terrain[i][j] = 'O';
                    }
                }
            }
        }
        /// <summary>
        /// Carves caverns into the landscape.
        /// </summary>
        public override void Shape()
        {
            Shape(0, WIDTH, 0, HEIGHT);
        }

        /// <summary>
        /// Carves Caverns into the landscape.
        /// Allows you to specify which part of the map to edit.
        /// </summary>
        /// <param name="startX"></param>
        /// <param name="endX"></param>
        /// <param name="startY"></param>
        /// <param name="endY"></param>
        public override void Shape(int startX, int endX, int startY, int endY)
        {
            foreach (int[] point in points)
            {
                int posX = point[0];
                int posY = point[1];
                int radius = point[2];

                for (int x = posX - radius; x < posX + radius; x++)
                {
                    // Check that the point is within bounds.
                    //if (x - radius < startX || x + radius > endX)
                    //    continue;

                    for (int y = posY - radius; y < posY + radius; y++)
                    {
                        // Continue the check to ensure point is within bounds.
                        //if (y - radius < startY || y + radius > endY)
                        //    continue;

                        int relX = x - posX;// x relative to the center point
                        int relY = y - posY;// y relative to the center point

                        // Turn land into cave where applicable.
                        if (Math.Pow(relX, 2) + Math.Pow(relY, 2) < Math.Pow(radius, 2) &&
                            terrain[x][y] == 'O')
                        {
                            terrain[x][y] = 'C';
                        }



                    }
                }

            }
        }

        /// <summary>
        /// Carves tunnels into the landscape
        /// </summary>
        public void Tunnel()
        {



        }

    }
}

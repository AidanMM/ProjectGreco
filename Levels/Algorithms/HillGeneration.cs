using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGreco.Levels.Algorithms
{
    /// <summary>
    /// Author: Luna Meier
    /// Purpose: Contains code that will shape a map to look like a series of hilly terrain.
    /// </summary>
    public class HillGeneration : GenerationAlgorithm
    {
        //
        // Fields
        //

        /// <summary>
        /// List of sections of the zone to apply an algorithm to.
        /// Each section is used to make the smoothing effect of the mountains.
        /// </summary>
        private List<Sector> mySectors;

        // Contains all the constants used for generating a hill-type terrain.
        #region HILL_ALGORITHM_CONSTANTS

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
        /// <summary>
        /// Minimum width of a hill.
        /// </summary>
        private int HILL_MIN_WIDTH;
        /// <summary>
        /// Maximum width of a hill.
        /// </summary>
        private int HILL_MAX_WIDTH;
        /// <summary>
        /// Minimum height of a hill's peak.
        /// </summary>
        private int HILL_MIN_HEIGHT;
        /// <summary>
        /// Maximum height of a hill's peak.
        /// </summary>
        private int HILL_MAX_HEIGHT;
        /// <summary>
        /// The higher the number the more points in a sector.
        /// Basically a higher number is smoother.
        /// Should not be greater than the minimum width of a hill.
        /// </summary>
        private int HILL_SMOOTH_WIDTH;
        /// <summary>
        /// The level of randomness in hill generation.  Higher number creates more artifacts.
        /// </summary>
        private int HILL_VARIATION;
        /// <summary>
        /// The percent that an averaged peak is lowered.
        /// </summary>
        private double HILL_PEAK_AVERAGE_PERCENT;

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
        /// <summary>
        /// Minimum width of a hill.
        /// </summary>
        public int Hill_Min_Width { get { return HILL_MIN_WIDTH; } }
        /// <summary>
        /// Maximum width of a hill.
        /// </summary>
        public int Hill_Max_Width { get { return HILL_MAX_WIDTH; } }
        /// <summary>
        /// Minimum height of a hill's peak.
        /// </summary>
        public int Hill_Min_Height { get { return HILL_MIN_HEIGHT; } }
        /// <summary>
        /// Maximum height of a hill's peak.
        /// </summary>
        public int Hill_Max_Height { get { return HILL_MAX_HEIGHT; } }
        /// <summary>
        /// The higher the number the more points in a sector.
        /// Basically a higher number is smoother.
        /// Should not be greater than the minimum width of a hill.
        /// </summary>
        public int Hill_Smooth_Width { get { return HILL_SMOOTH_WIDTH; } }
        /// <summary>
        /// The level of randomness in hill generation.  Higher number creates more artifacts.
        /// </summary>
        public int Hill_Variation { get { return HILL_VARIATION; } }
        /// <summary>
        /// The percent that an averaged peak is lowered.
        /// </summary>
        public double Hill_Peak_Average_Percent { get { return HILL_PEAK_AVERAGE_PERCENT; } }

        #endregion

        //
        // Properties
        //

        /// <summary>
        /// List of sections of the zone to apply an algorithm to.
        /// Each section is used to make the smoothing effect of the mountains.
        /// </summary>
        public List<Sector> MySectors { get { return mySectors; } }

        /// <summary>
        /// An algorithm that applies to a terrain and creates a hill-like appearance.
        /// </summary>
        /// <param name="terrain">The map terrain passed in to be edited.</param>
        /// <param name="seed">The seed used to generate the map.  Seed -1 is fully random.</param>
        public HillGeneration(char[][] terrain, int WIDTH, int HEIGHT, int GROUND_HEIGHT, int SAFE_ZONE_WIDTH,
            int HILL_MIN_WIDTH, int HILL_MAX_WIDTH, int HILL_MIN_HEIGHT, int HILL_MAX_HEIGHT,
            int HILL_SMOOTH_WIDTH, int HILL_VARIATION, double HILL_PEAK_AVERAGE_PERCENT,
            int seed = -1)
            : base(terrain, seed)
        {
            // Store the constants.
            this.WIDTH = WIDTH;
            this.HEIGHT = HEIGHT;
            this.GROUND_HEIGHT = GROUND_HEIGHT;
            this.SAFE_ZONE_WIDTH = SAFE_ZONE_WIDTH;
            this.HILL_MIN_WIDTH = HILL_MIN_WIDTH;
            this.HILL_MAX_WIDTH = HILL_MAX_WIDTH;
            this.HILL_MIN_HEIGHT = HILL_MIN_HEIGHT;
            this.HILL_MAX_HEIGHT = HILL_MAX_HEIGHT;
            this.HILL_SMOOTH_WIDTH = HILL_SMOOTH_WIDTH;
            this.HILL_VARIATION = HILL_VARIATION;
            this.HILL_PEAK_AVERAGE_PERCENT = HILL_PEAK_AVERAGE_PERCENT;

            // Initialize the sector list.
            mySectors = new List<Sector>();
        }

        /// <summary>
        /// Creates a box that can be manipulated into a hill with a height at ground level.
        /// </summary>
        public override void Initialize()
        {
            Initialize(0, WIDTH, 0, HEIGHT);
        }

        /// <summary>
        /// Creates a minimum bounding box at the bottom of the world.
        /// Allows you to specify part of a map to edit.
        /// </summary>
        /// <param name="startX">The x-coordinate the box begins at.</param>
        /// <param name="endX">The x-coordinate the box ends at.</param>
        /// <param name="startY">The y-coordinate the box begins at.</param>
        /// <param name="endY">The y-coordinate the box ends at.</param>
        public override void Initialize(int startX, int endX, int startY, int endY)
        {

            for (int i = startX; i < endX; i++)
            {
                for (int j = startY; j < endY; j++)
                {
                    // If the height of the current y coordinate from the bottom is less than the ground height,
                    // the terrain by default is ground, so set it as such.
                    if (j < 1)
                    {
                        terrain[i][j] = 'X';
                    }
                    // Otherwise it is air, so set it as such.
                    else
                    {
                        terrain[i][j] = ' ';
                    }
                }
            }

        }

        /// <summary>
        /// An iterative process that creates hills.
        /// Takes two sector values, creates a hill between the two of them via an iterative smoothing process.
        /// After creating a hill for every sector value, smooths between two hills as appropriate.
        /// </summary>
        public override void Shape()
        {
            Shape(0, WIDTH, 0, HEIGHT);
        }

        /// <summary>
        /// An iterative process that creates hills.
        /// Takes two sector values, creates a hill between the two of them via an iterative smoothing process.
        /// After creating a hill for every sector value, smooths between two hills as appropriate.
        /// Allows you to specify part of a map to edit.
        /// </summary>
        /// <param name="startX">The x-coordinate the shaping begins at.</param>
        /// <param name="endX">The x-coordinate the shaping ends at.</param>
        /// <param name="startY">The y-coordinate the shaping begins at.</param>
        /// <param name="endY">The y-coordinate the shaping ends at.</param>
        public override void Shape(int startX, int endX, int startY, int endY)
        {
            // Begin by preparing the sector list.
            PrepareSectors(startX, endX);

            // Time to change the map.  Oh boy.

            for (int i = 0; i < mySectors.Count; i++)
            {
                for (int j = 0; j < mySectors[i].Points.Length - 1; j++)
                {
                    // Storing this for the sake of readability, we're not going to be changing anything in it.
                    int[][] points = mySectors[i].Points;

                    // Calculate the slope of the line; the change in y divided by the change in x.
                    double slope = (double)(points[j + 1][1] - points[j][1]) /
                        (double)(points[j + 1][0] - points[j][0]);

                    // Calculate the y-intercept of the line
                    double yIntercept = points[j][1] - (slope * points[j][0]);


                    for (int y = startY; y < endY; y++)
                    {
                        for (int x = points[j][0]; // Start x at the beginning x-coordinate of the current sector.
                            x <= points[j + 1][0] - 1; // End x at the second to last x-coordinate of the current sector.
                            x++)
                        {
                            if (terrain[x][y] == ' ' &&
                                y <= slope * x + yIntercept)
                            {
                                terrain[x][y] = 'O';
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Prepares the sector list so it can be properly used.
        /// </summary>
        /// <param name="startX">The x-coordinate the generation starts at.</param>
        /// <param name="endX">The x-coordinate the generation ends at.</param>
        public void PrepareSectors(int startX, int endX)
        {
            // This figures out the length of this sector.
            int sectorEnd = startX + SAFE_ZONE_WIDTH;
            int sectorStart = sectorEnd;

            // Prepare the entering sector, this one will be flat to guarantee a place to spawn.
            mySectors.Add(new Sector(startX, sectorEnd, this, false));

            while (sectorEnd < endX) // Create sectors until you have sectors for the entire area being edited.
            {
                sectorStart = sectorEnd;

                sectorEnd = sectorEnd + Rand.Next(HILL_MIN_WIDTH, HILL_MAX_WIDTH + 1);

                // To ensure that there's always an ending, the last sector will also always be flat.
                // As such we can just leave this loop when that occurs, but first taking advantage of the remaining space.
                if (sectorEnd > endX)
                {
                    sectorEnd = endX - SAFE_ZONE_WIDTH;
                    mySectors.Add(new Sector(sectorStart, sectorEnd, this, true));
                    break;
                }

                // Otherwise we're going to set up our sector and add it to the list.
                mySectors.Add(new Sector(sectorStart, sectorEnd, this, true));
            }

            // Now just create the last sector.
            mySectors.Add(new Sector(sectorStart, endX, this, false));

            // Next we implement the sector smoothing.  For this, we're going to average the peak
            // heights, and then follow up by only taking a portion of the average, to help smoothing.
            // Remember that we're ignoring the first sector and the last sector; they don't even have peaks.
            for (int i = 1; i < mySectors.Count - 1; i++)
            {
                int peakHeight1 = mySectors[i].Points[mySectors[i].PeakIndex][1];
                int peakHeight2 = mySectors[i + 1].Points[mySectors[i + 1].PeakIndex][1];

                // Get the avg height and change the indexes to be correct.
                int avgHeight = mySectors[i].AveragePeaks(peakHeight1, peakHeight2);
                mySectors[i].UpdateSectorEnd(avgHeight);
                mySectors[i + 1].UpdateSectorBeginning(avgHeight);
            }
            // When looking at the second to last sector we don't want the end to be avg height, we want it to be the lowest height possible.
            mySectors[mySectors.Count - 2].UpdateSectorEnd(GROUND_HEIGHT);

            // Now that all the sectors have their points set up, let's smooth them.
            for (int i = 1; i < mySectors.Count - 1; i++)
            {
                mySectors[i].SmoothSector();
            }

            // Since we're not smoothing the 1st and last sectors, we need to ensure all the points have a y-coordinate.
            mySectors[0].Flatten();
            mySectors[mySectors.Count - 1].Flatten();
        }
    }

    /// <summary>
    /// Contains information necessary to create a singular hill of some sort.
    /// </summary>
    public class Sector
    {
        //
        // Fields
        //

        /// <summary>
        /// The array holding each point and its data.
        /// </summary>
        private int[][] points;
        /// <summary>
        /// The index number of the peak.
        /// </summary>
        private int peakIndex;
        /// <summary>
        /// The algorithm that made this sector.
        /// </summary>
        private HillGeneration myAlgorithm;

        //
        // Properties
        //

        /// <summary>
        /// The array holding each point and its data.
        /// </summary>
        public int[][] Points { get { return points; } }
        /// <summary>
        /// The index number of the peak.
        /// </summary>
        public int PeakIndex { get { return peakIndex; } }
        /// <summary>
        /// The algorithm that made this sector.
        /// </summary>
        public HillGeneration MyAlgorithm { get { return myAlgorithm; } }

        /// <summary>
        /// Create a sector that begins at one x-coordinate and ends at another.
        /// </summary>
        /// <param name="startX">The sector's starting x-coordinate.</param>
        /// <param name="endX">The sector's ending x-coordinate.</param>
        /// <param name="myAlgorithm">The algorithm containing the Random to be used.</param>
        /// <param name="hasPeak">Does this sector has a peak?</param>
        public Sector(int startX, int endX, HillGeneration myAlgorithm, bool hasPeak)
        {
            this.myAlgorithm = myAlgorithm;

            int numPoints;
            // The number of points in a sector is equivalent to the width divided by the number of points per sector, rounded up.
            if (hasPeak)
            {
                numPoints = (int)Math.Ceiling((double)(endX - startX) / myAlgorithm.Hill_Smooth_Width);
            }
            else
            {
                numPoints = 2;
            }

            // This array will store the positions of our points.
            points = new int[numPoints][];

            // Set up the points array to contain both that points x and y co-ordinate.
            for (int i = 0; i < points.Length - 1; i++)
            {
                points[i] = new int[2];
                // The x-coordinate of a point is partially made by the width.
                points[i][0] = startX + myAlgorithm.Hill_Smooth_Width * i;
            }
            // We'll have to set up the last point manually, in case of rounding errors.
            points[points.Length - 1] = new int[2];
            points[points.Length - 1][0] = endX;

            // The points by default will be around ground level.  We can work around that later.
            points[0][1] = myAlgorithm.Ground_Height;
            points[points.Length - 1][1] = myAlgorithm.Ground_Height;

            // If hasPeak is true, then we'll give it a peak.
            if (hasPeak)
                CreatePeak();
        }

        /// <summary>
        /// Takes the average of two peaks.
        /// </summary>
        /// <param name="peakHeight1">The height of the first peak.</param>
        /// <param name="peakHeight2">The height of the second peak.</param>
        /// <returns>The number that is the average of the two peaks.</returns>
        public int AveragePeaks(int peakHeight1, int peakHeight2)
        {
            int newHeight = (int)(((peakHeight1 + peakHeight2) / 2.0) * myAlgorithm.Hill_Peak_Average_Percent);

            return newHeight;
        }

        /// <summary>
        /// Creates a peak at a random index and at a random height.
        /// </summary>
        public void CreatePeak()
        {
            // We'll set the index to any random one that isn't an endpoint.
            peakIndex = myAlgorithm.Rand.Next(1, points.Length - 1);

            // The height of a peak lies somewhere between the given minheight and maxheight.
            int peakHeight = myAlgorithm.Ground_Height + myAlgorithm.Rand.Next(myAlgorithm.Hill_Min_Height, myAlgorithm.Hill_Max_Height);

            // And update the index to reflect this.
            points[peakIndex][1] = peakHeight;
        }

        /// <summary>
        /// Gives the vector smoothing between points.
        /// </summary>
        public void SmoothSector()
        {
            // Find the average variation between points.
            int differencePrePeak = (points[peakIndex][1] - points[0][1]) / peakIndex;
            int differencePostPeak = (points[peakIndex][1] - points[points.Length - 1][1]) / (points.Length - peakIndex);

            for (int i = 1; i < points.Length - 1; i++)
            {
                // If it's before the peak, add the pre-peak difference
                // The closer it is to the peak, the more you add.
                if (i < peakIndex)
                {
                    points[i][1] = points[0][1] + i * differencePrePeak;
                    points[i][1] += myAlgorithm.Rand.Next(0, myAlgorithm.Hill_Variation);
                }

                // If it's after the peak, add the post-peak difference.
                // The further it is from the peak, the less you add.
                else if (i > peakIndex)
                {
                    points[i][1] = points[points.Length - 1][1] + (points.Length - i) * differencePostPeak;


                    points[i][1] += myAlgorithm.Rand.Next(0, myAlgorithm.Hill_Variation);
                }
            }
        }

        /// <summary>
        /// Changes the sector's beginning to start at the specified height.
        /// </summary>
        /// <param name="height">The height to set the beginning to.</param>
        public void UpdateSectorBeginning(int height)
        {
            points[0][1] = height;
        }

        /// <summary>
        /// Changes the sector's end to start at the specified height.
        /// </summary>
        /// <param name="height">The height to set the end to.</param>
        public void UpdateSectorEnd(int height)
        {
            points[points.Length - 1][1] = height;
        }

        /// <summary>
        /// Flattens the a terrain that's meant to be flat by setting
        /// all points y-coordinates to the ground height.
        /// </summary>
        public void Flatten()
        {
            for (int i = 0; i < points.Length; i++)
            {
                points[i][1] = myAlgorithm.Ground_Height;
            }
        }
    }
}

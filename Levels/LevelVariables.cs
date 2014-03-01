﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGreco.Levels
{
    // Simple algorithms have classes; they affect the entire map
    enum AlgorithmType
    {
        Hills,
        Desert,
        HillsDesert
    }

    static class LevelVariables
    {
        /// <summary>
        /// Width of the total terrain in a level.
        /// </summary>
        public const int WIDTH = 220;
        /// <summary>
        /// Height of the total terrain in a level.
        /// </summary>
        public const int HEIGHT = 70;
        /// <summary>
        /// Average height of a "ground level."
        /// </summary>
        public const int GROUND_HEIGHT = 30;
        /// <summary>
        /// Width of the safe zone.
        /// </summary>
        public const int SAFE_ZONE_WIDTH = 7;

        #region HILL_ALGORITHM_CONSTANTS

        /// <summary>
        /// Minimum width of a hill.
        /// </summary>
        public const int HILL_MIN_WIDTH = 10;
        /// <summary>
        /// Maximum width of a hill.
        /// </summary>
        public const int HILL_MAX_WIDTH = 170;
        /// <summary>
        /// Minimum height of a hill's peak.
        /// </summary>
        public const int HILL_MIN_HEIGHT = 25;
        /// <summary>
        /// Maximum height of a hill's peak.
        /// </summary>
        public const int HILL_MAX_HEIGHT = 35;
        /// <summary>
        /// The higher the number the more points in a sector.
        /// Basically a higher number is smoother.
        /// Should not be greater than the minimum width of a hill.
        /// </summary>
        public const int HILL_SMOOTH_WIDTH = 10;
        /// <summary>
        /// The level of randomness in hill generation.  Higher number creates more artifacts.
        /// </summary>
        public const int HILL_VARIATION = 7;
        /// <summary>
        /// The percent that an averaged peak is lowered.
        /// </summary>
        public const double HILL_PEAK_AVERAGE_PERCENT = .7;

        #endregion

        #region DESERT_ALGORITHM_CONSTANTS

        /// <summary>
        /// Minimum width of a dune.
        /// </summary>
        public const int DESERT_MIN_WIDTH = 40;
        /// <summary>
        /// Maximum width of a dune.
        /// </summary>
        public const int DESERT_MAX_WIDTH = 90;
        /// <summary>
        /// Minimum height of a dune's peak.
        /// </summary>
        public const int DESERT_MIN_HEIGHT = 2;
        /// <summary>
        /// Maximum height of a dune's peak.
        /// </summary>
        public const int DESERT_MAX_HEIGHT = 7;
        /// <summary>
        /// The higher the number the more points in a sector.
        /// Basically a higher number is smoother.
        /// Should not be greater than the minimum width of a dune.
        /// </summary>
        public const int DESERT_SMOOTH_WIDTH = 10;
        /// <summary>
        /// The level of randomness in dune generation.  Higher number creates more artifacts.
        /// </summary>
        public const int DESERT_VARIATION = 3;
        /// <summary>
        /// The percent that an averaged peak is lowered.
        /// </summary>
        public const double DESERT_PEAK_AVERAGE_PERCENT = .9;

        #endregion

    }
}

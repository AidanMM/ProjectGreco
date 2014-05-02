using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectGreco.Levels;
using ProjectGreco.Levels.Algorithms;

namespace ProjectGreco
{
    public static class PlayerStats
    {
        public static int mula = 0;
        public static bool hillComplete = false;
        public static bool desertComplete = false;
        public static bool forestComplete = false;
        public static bool snowComplete = false;


        public static bool firstTime = true;

        public static Level hill;
        public static Level desert;
        public static Level forest;
        public static Level snow;

    }
}

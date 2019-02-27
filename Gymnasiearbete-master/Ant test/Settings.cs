using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ant_test
{
    class Settings
    {
        /// <summary>
        /// Render
        /// </summary>
        public static bool render_Color = false;
        public static int render_Size = 10;
        public static bool render_Ants = true;
        public static bool render_TrafficLights = true;
        /// <summary>
        /// Ant behavior
        /// </summary>
        public static int ant_max_speed = 3;
        public static bool[] ant_random_directions = new bool[4] { true, true, true, true };
        public static bool ant_stacking = true;
}
}

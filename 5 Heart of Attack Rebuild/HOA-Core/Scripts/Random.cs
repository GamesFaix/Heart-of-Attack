using System;
using System.Collections.Generic;

namespace HOA
{
    /// <summary> Static singleton of System.Random class. </summary>
    public static class Random
    {
        static System.Random r;

        static Random() { r = new System.Random(); }

        /// <summary> Generates Int32 between min (inclusive) and max (inclusive). </summary>
        public static int Range(int min, int max)
        {
            return r.Next(min, max);
        }
    }
}

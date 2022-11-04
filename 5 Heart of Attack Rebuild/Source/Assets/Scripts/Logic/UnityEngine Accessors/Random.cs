using System;
using System.Collections.Generic;
using UnityEngine;

namespace HOA
{
    /// <summary>
    /// Random number generators and such
    /// </summary>
    public static class Random
    {
        /// <summary>
        /// Generates int32 between min (inclusive) and max (inclusive).
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int Range(int min, int max)
        {
            return (int)Mathf.Round(UnityEngine.Random.Range(min, max));
        }
    }
}

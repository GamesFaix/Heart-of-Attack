using UnityEngine;
using System;

namespace HOA
{
    public static class Time
    {
        public static float time { get { return UnityEngine.Time.time; } }

        public static float approx { get { return (float)Math.Round(time, 3); } }

        public static float delta { get { return UnityEngine.Time.deltaTime; } }

        public static float Since(float instant) { return time - instant; }



    }
}
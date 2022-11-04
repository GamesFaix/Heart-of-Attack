using UnityEngine;

namespace HOA
{
    public static class Time
    {
        public static float time { get { return UnityEngine.Time.time; } }

        public static float delta { get { return UnityEngine.Time.deltaTime; } }

        public static float Since(float f) { return time - f; }



    }
}
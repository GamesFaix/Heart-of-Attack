using UnityEngine;
using System.Collections;

namespace HOA
{
    public static class Time
    {
        /// <summary> Accessor for UnityEngine.Time.time</summary>
        public static float time { get { return UnityEngine.Time.time; } }

        /// <summary> Accessor for UnityEngine.Time.deltaTime</summary>
        public static float delta { get { return UnityEngine.Time.deltaTime; } }

        /// <summary> Current time minus input.</summary>
        public static float Since(float f) { return time - f; }

        /// <summary> Wrapper for UnityEngine.WaitForSeconds, but input in ms.</summary>
        public static IEnumerator Wait(float f)
        {
            yield return new UnityEngine.WaitForSeconds(f * 0.001f);
        }

    }
}
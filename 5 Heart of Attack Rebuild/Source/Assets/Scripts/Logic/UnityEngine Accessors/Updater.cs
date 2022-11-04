//#define DEBUG

using System;
using UnityEngine;

namespace HOA
{

    public class Updater : MonoBehaviour
    {
        static float lastStep = 0;
        static float stepLength = 1f;
        static bool StepConditions { get { return Time.Since(lastStep) > stepLength; } }

        static Updater()
        {
            Debug.Log("Updater created.");
        }

        public void Update() {
            UpdatePublish();
            if (StepConditions)
            {
                lastStep = Time.time;
                StepPublish();
            }
        }

        public static event EventHandler<BlankEventArgs> UpdateEvent;
        public static event EventHandler<BlankEventArgs> StepEvent;

        public static void UpdatePublish()
        {
            if (UpdateEvent != null)
                UpdateEvent(null, new BlankEventArgs());
        }
        public static void StepPublish()
        {
            if (StepEvent != null)
            {
                StepEvent(null, new BlankEventArgs());
#if DEBUG
                Debug.Log("Step [{0:f3}s]", Time.time);
#endif
            }
        }

    }

    public class BlankEventArgs : EventArgs { public BlankEventArgs() { } }
}
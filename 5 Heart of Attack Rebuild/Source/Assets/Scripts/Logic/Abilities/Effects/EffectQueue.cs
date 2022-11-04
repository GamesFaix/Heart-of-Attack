using System.Collections.Generic;
using System;

namespace HOA.Ab
{

    public static class EffectQueue
    {
        #region Properties

        static List<IEffect> effects = new List<IEffect>();
        static IEffect Top { get { return effects[0]; } }
        static int Count { get { return effects.Count; } }
        static bool Empty { get { return Count < 1; } }

        static bool active;
        public static bool Active
        {
            get { return active; }
            private set
            {
                active = value;
                string status = (value ? "activated" : "deactivated");
                Log.Debug("EffectQueue {0}.", status);
            }
        }

        static bool pause;
        public static bool Pause 
        {
            get { return pause; }
            private set
            {
                pause = value;
                string status = (value ? "paused" : "unpaused");
                Log.Debug("EffectQueue {0}.", status);
            }
        }
        
        static bool SequenceInProgress { get { 
            return (Top is EffectSequence && (Top as EffectSequence).Count > 0); } }

        #endregion

        public static void Load () 
        {
            Log.Start("EffectQueue created.");
            Updater.StepEvent += OnStep;
        }

        static void OnStep(object sender, BlankEventArgs args)
        {
            if (!Pause)
                if (Active)
                {
                    if (!Empty)
                    {
                        Top.Process();
                        if (!SequenceInProgress)
                            effects.Remove(Top);
                    }
                    else
                        Active = false;
                }
                else if (!Empty)
                    Active = true;
        }

        public static void Add(IEffect e) 
        {
            Log.Debug("{0} added to EffectQueue.", e);
            effects.Add(e); 
        }

        public static void Interrupt(IEffect e) 
        {
            Log.Debug("{0} interrupting EffectQueue.", e);
            effects.Insert(1, e); 
        }
    }
}
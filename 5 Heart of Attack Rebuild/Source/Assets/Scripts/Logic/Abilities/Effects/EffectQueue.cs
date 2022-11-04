using UnityEngine;
using System.Collections.Generic;
using System;

namespace HOA.Abilities
{

    public static class EffectQueue
    {
        #region Properties

        static List<IEffect> effects = new List<IEffect>();
        static IEffect Top { get { return effects[0]; } }
        static int Count { get { return effects.Count; } }
        static bool Empty { get { return Count < 1; } }
        
        public static bool Active { get; private set; }
        public static bool Pause { get; private set; }
        
        static bool SequenceInProgress { get { 
            return (Top is EffectSequence && (Top as EffectSequence).Count > 0); } }

        #endregion

        public static void Load () 
        {
            Debug.Log("EffectQueue created.");
            Updater.StepEvent += OnStep;
        }

        static void OnStep(object sender, BlankEventArgs args)
        {
            if (!Pause)
                if (Active)
                    if (!Empty)
                    {
                        if (!SequenceInProgress)
                            effects.Remove(Top);
                        Top.Process();
                    }
                    else
                        Active = false;
                else if (!Empty)
                    Active = true;
        }

        public static void Add(IEffect e) { effects.Add(e); }

        public static void Interrupt(IEffect e) { effects.Insert(1, e); }
    }
}
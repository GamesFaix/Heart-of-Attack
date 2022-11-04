﻿using HOA.Resources;

namespace HOA.Abilities
{

    public partial class Effect
    {

        public static Effect Advance(IEffectUser user, EffectArgs args)
        {
            Effect e = new Effect("Advance", user, args);
            e.Process = () =>
            {
                Session.Active.Queue.Advance();
                AVEffect.Advance.PlayNonLocal();
            };
            return e;
        }
        
        public static Effect Initialize(IEffectUser user, EffectArgs args)
        {
            Effect e = new Effect("Initialize", user, args);
            e.Process = () => { Session.Active.Queue.Initialize(); };
            return e;
        }
        
        public static Effect Shift(IEffectUser user, EffectArgs args)
        {
            Effect e = new Effect("Shift", user, args);
            e.Process = () =>
            {
                Unit u = args.unit;
                int n = args.value;
                Session.Active.Queue.Shift(u, n);
                if (n < 0)
                {
                    Debug.Log("{0} shifted up {1} slot(s) in the Queue.", u, n);
                    AVEffect.StatUp.Play(u);
                }
                else if (n > 0)
                {
                    Debug.Log("{0} shifted down {1} slot(s) in the Queue.", u, n);
                    AVEffect.StatDown.Play(u);
                }
                Debug.Log("{0} shifted down 0 slots in the Queue\n...or shifted up 0 slots.", u);
            };
            return e;
        }

        public static Effect Shuffle(IEffectUser user, EffectArgs args)
        {
            Effect e = new Effect("Shuffle", user, args);
            e.Process = () =>
            {
                Session.Active.Queue.Shuffle();
                Debug.Log("{0} shuffled the Queue.", user);
                AVEffect.Shuffle.PlayNonLocal();
            };
            return e;
        }

    }
}
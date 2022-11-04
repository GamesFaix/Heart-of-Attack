﻿using HOA.Resources;

namespace HOA.Ef
{

    public partial class Effect
    {

        public static Effect Advance(object source, Args args)
        {
            Effect e = new Effect(source, "Advance", args);
            e.action = (a) =>
            {
                Session.Active.Queue.Advance();
                AVEffect.Advance.PlayNonLocal();
            };
            return e;
        }

        public static Effect Initialize(object source, Args args)
        {
            Effect e = new Effect(source, "Initialize", args);
            e.action = (a) => { Session.Active.Queue.Initialize(); };
            return e;
        }

        public static Effect Shift(object source, Args args)
        {
            Effect e = new Effect(source, "Shift", args);
            e.action = (a) =>
            {
                Unit u = args.unit;
                int n = args.value;
                Session.Active.Queue.Shift(u, n);
                if (n < 0)
                {
                    Log.Game("{0} shifted up {1} slot(s) in the Queue.", u, n);
                    AVEffect.StatUp.Play(u);
                }
                else if (n > 0)
                {
                    Log.Game("{0} shifted down {1} slot(s) in the Queue.", u, n);
                    AVEffect.StatDown.Play(u);
                }
                Log.Debug("{0} shifted down 0 slots in the Queue\n...or shifted up 0 slots.", u);
            };
            return e;
        }

        public static Effect Shuffle(object source, Args args)
        {
            Effect e = new Effect(source, "Shuffle", args);
            e.action = (a) =>
            {
                Session.Active.Queue.Shuffle();
                Log.Game("{0} shuffled the Queue.", e.source.Last<Token>());
                AVEffect.Shuffle.PlayNonLocal();
            };
            return e;
        }

    }
}
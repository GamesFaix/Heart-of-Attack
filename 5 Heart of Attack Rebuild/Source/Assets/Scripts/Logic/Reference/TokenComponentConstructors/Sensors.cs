using System;
using System.Collections.Generic;
using HOA.Abilities;

namespace HOA.Tokens
{

    public partial class Sensor
    {
        //This file is just for specialized constructors and methods.

        public static Sensor Blank(IEffect source, Token thisToken)
        {
            Sensor s = new Sensor(source, thisToken, "Blank Sensor", EntityFilter.None, EntityFilter.None);
            s.Desc = Scribe.Write("Does nothing.");
            return s;
        }

        /// <summary>
        /// INCOMPLETE
        /// </summary>
        /// <param name="thisToken"></param>
        /// <param name="cell"></param>
        /// <returns></returns>
        public static Sensor Aperture(IEffect source, Token thisToken)
        {
            Sensor s = new Sensor(source, thisToken, "Aperture Sensor",
                EntityFilter.Token, EntityFilter.Plane(Plane.Ground | Plane.Air | Plane.Ethereal, true));
            s.Desc = Scribe.Write("Stops ground, air, and ethereal tokens.");
            
            s.OnThisEnter = (c) =>
            {
                s.Subscribe(c);
                TokenSet apertures = Session.Active.tokens - EntityFilter.Species(Species.Aperture, true);
                apertures.Remove(s.ThisToken);

                foreach (Token t in apertures)
                {
                    c.Links.Add(t.Cell);
                    t.Cell.Links.Add(c);
                }
            };

            s.OnThisExit = (c) =>
            {
                TokenSet apertures = Session.Active.tokens - EntityFilter.Species(Species.Aperture, true);
                apertures.Remove(s.ThisToken);

                foreach (Token t in apertures)
                {
                    s.ThisToken.Cell.Links.Remove(t.Cell);
                    t.Cell.Links.Remove(s.ThisToken.Cell);
                }
                s.Unsubscribe(c);
            };

            return s;
        }

        public static Sensor BombingRange(IEffect source, Token thisToken)
        {
            Sensor s = new Sensor(source, thisToken, "Bombing Range Sensor", EntityFilter.Unit);
            s.Desc = Scribe.Write("If a Unit is in Cell at the end of its turn," +
                "\n10 Explosive damage is dealth at Cell.");

            s.OnOtherEnter = (t) =>
            {
                EffectQueue.Add(Effect.AddTimer
                    (s, new EffectArgs(t, Timer.Bombing(Force.Effect, t as Unit))));
            };
            s.OnOtherExit = (t) =>
            {
                EffectQueue.Add(Effect.RemoveTimer
                    (s, new EffectArgs(t, Timer.Bombing(Force.Effect, t as Unit))));
            };
            return s;
        }

        /// <summary> INCOMPLETE! </summary>
        public static Sensor Carapace(IEffect source, Token thisToken)
        {
            Sensor s = new Sensor(source, thisToken, "Carapace Invader Sensor",
                EntityFilter.Species(Species.Carapace, false) 
                + EntityPredicates.Owner(thisToken.Owner, true));
            s.Desc = Scribe.Write("Units in Cell on {0}'s team " +
                "\n(except Carapace Invaders) add n{0}'s Defense to their own.", s.ThisToken);
            return s;
        }

        public static Sensor Curse(IEffect source, Token thisToken)
        {
            Sensor s = new Sensor(source, thisToken, "Curse Sensor", EntityFilter.Unit);
            s.Desc = Scribe.Write("Units entering Cell take 2 damage." +
                "\nUnits take 2 damage if in Cell at the end of their turn.");

            s.OnThisEnter = (c) =>
            {
                foreach (Cell cell in c.NeighborsAndSelf)
                    s.Subscribe(cell);
                EffectSet e = new EffectSet();
                foreach (Unit u in c.Occupants)
                    e.Add(Effect.AddTimer(s, new EffectArgs(u, Timer.Cursed(e,u))));
                EffectQueue.Add(e);
            };

            s.OnOtherEnter = (t) =>
            {
                EffectQueue.Add(Effect.AddTimer(s, new EffectArgs(t, Timer.Cursed(Force.Effect, t as Unit))));
                if (!Session.Active.paused)
                    EffectQueue.Interrupt(Effect.Damage(s, new EffectArgs(t, 2)));
            };

            s.OnOtherExit = (t) =>
            {
                EffectQueue.Add(Effect.RemoveTimer(s, new EffectArgs(t, Timer.Cursed(Force.Effect, t as Unit))));
            };

            s.OnThisExit = (c) =>
            {
                EffectSet e = new EffectSet();
                foreach (Token t in c.Occupants)
                {
                    e.Add(Effect.RemoveTimer(s, new EffectArgs(t, Timer.Cursed(Force.Effect, t as Unit))));
                    s.UnsubscribeAll();
                }
                EffectQueue.Add(e);
            };
            return s;
        }

        public static Sensor Exhaust(IEffect source, Token thisToken)
        {
            Sensor s = new Sensor(source, thisToken, "Exhaust Sensor", 
                EntityFilter.Plane(Plane.Ground|Plane.Air, true) + EntityPredicates.Unit, 
                EntityFilter.Plane(Plane.Ground|Plane.Air, true));
            s.Desc = Scribe.Write("Stops Ground and Air Tokens." +
                "\nGround and Air Units entering Cell take 5 damage." +
                "\nGround and Air Units take 5 damage if in Cell at the end of their turn.");
            s.OnThisEnter = (c) =>
            {
                s.Subscribe(c);
                EffectSet e = new EffectSet();
                foreach (Token t in c.Occupants - s.Trigger)
                    e.Add(Effect.AddTimer(s, new EffectArgs(t, Timer.Exhaust(Force.Effect, t as Unit))));
                EffectQueue.Add(e);
            };

            s.OnOtherEnter = (t) =>
            {
                EffectQueue.Add(Effect.AddTimer(s, new EffectArgs(t, Timer.Exhaust(Force.Effect, t as Unit))));
                if (!Session.Active.paused)
                    EffectQueue.Interrupt(Effect.Damage(s, new EffectArgs(t, 5)));
            };

            s.OnOtherExit = (t) =>
            {
                EffectQueue.Add(Effect.RemoveTimer(s, new EffectArgs(t, Timer.Exhaust(Force.Effect, t as Unit))));
            };
            return s;
        }

        public static Sensor Ice(IEffect source, Token thisToken)
        {
            Sensor s = new Sensor(source, thisToken, "Ice Sensor",
                EntityFilter.Plane(Plane.Ground, true) + EntityPredicates.Token);
            s.Desc = Scribe.Write("When any Ground Token enters Cell," +
                "\nthere is a 25% chance Ice breaks, turning into Water." +
                "\nTokens moving through Cell when Ice breaks do not stop," +
                "\nonly Tokens stopping on Ice that breaks are affected by Water.");
            s.OnOtherEnter = (t) =>
            {
                if (!Session.Active.paused
                    && Random.Range(1, 4) == 1) 
                        EffectQueue.Add(Effect.Replace(s, new EffectArgs(s.ThisToken, Species.Water)));
            };
            s.OnThisEnter = (c) => { s.Subscribe(c); };
            return s;
        }

        public static Sensor Lava(IEffect source, Token thisToken)
        {
            Sensor s = new Sensor(source, thisToken, "Lava Sensor", 
                EntityFilter.Plane(Plane.Ground, true) + EntityPredicates.Unit,
                EntityFilter.Plane(Plane.Ground, true));
            s.Desc = Scribe.Write("Stops Ground Tokens." +
                "\nGround Units entering Cell take 7 damage." +
                "\nGround Units take 7 damage if in Cell at the end of their turn.");

            s.OnThisEnter = (c) =>
            {
                s.Subscribe(c);
                EffectSet e = new EffectSet();
                foreach (Token t in c.Occupants - s.Trigger)
                    e.Add(Effect.AddTimer
                        (s, new EffectArgs(t, Timer.Incineration(Force.Effect, t as Unit))));
                EffectQueue.Add(e);
            };

            s.OnOtherEnter = (t) =>
            {
                EffectQueue.Add(Effect.AddTimer
                    (s, new EffectArgs(t, Timer.Incineration(Force.Effect, t as Unit))));

                if (!Session.Active.paused && EntityFilter.UnitDest.Test(t))
                    EffectQueue.Interrupt(Effect.FireInitial(s, new EffectArgs(t, 7)));
            };

            s.OnOtherExit = (t) =>
            {
                EffectQueue.Add(Effect.RemoveTimer
                    (s, new EffectArgs(t, Timer.Incineration(Force.Effect, t as Unit))));
            };
            return s;
        }

        public static Sensor Mine(IEffect source, Token thisToken)
        {
            Sensor s = new Sensor(source, thisToken, "Mine Sensor", EntityFilter.Token);
            s.Desc = Scribe.Write("If a Token enters Cell, " +
                "\n10 Explosive damage is dealt at {0}'s Cell.", s.ThisToken);
            s.OnOtherEnter = (t) =>
            {
                if (!Session.Active.paused) 
                    EffectQueue.Interrupt(EffectSequence.Detonate(s, new EffectArgs(s.ThisToken)));
            };
            s.OnThisEnter = (c) =>
            {
                foreach (Cell cell in c.NeighborsAndSelf)
                    s.Subscribe(cell);
            };

            s.OnThisExit = (c) => { s.UnsubscribeAll(); };
            return s;
        }

        public static Sensor TimeSink(IEffect source, Token thisToken)
        {
            Sensor s = new Sensor(source, thisToken, "Time Sink Sensor", EntityFilter.Unit);
            s.Desc = Scribe.Write("Units in Cell have -2 Initiative");
            s.OnOtherEnter = (t) =>
            {
                s.ThisToken.trackList.Add(t, 2);
                EffectQueue.Add(Effect.AddStat(s, new EffectArgs(t, -2, Stats.Initiative)));
            };

            s.OnOtherExit = (t) =>
            {
                s.ThisToken.trackList.Remove(t);
                EffectQueue.Add(Effect.AddStat(s, new EffectArgs(t, 2, Stats.Initiative)));
            };
            return s;
        }

        public static Sensor TimeWell(IEffect source, Token thisToken)
        {
            Sensor s = new Sensor(source, thisToken, "Time Well Sensor", EntityFilter.Unit);
            s.Desc = Scribe.Write("Units in Cell have +2 Initiative");
            s.OnOtherEnter = (t) =>
            {
                s.ThisToken.trackList.Add(t, 2);
                EffectQueue.Add(Effect.AddStat(s, new EffectArgs(t, 2, Stats.Initiative)));
            };

            s.OnOtherExit = (t) =>
            {
                s.ThisToken.trackList.Remove(t);
                EffectQueue.Add(Effect.AddStat(s, new EffectArgs(t, -2, Stats.Initiative)));
            };
            return s;
        }

        public static Sensor Water(IEffect source, Token thisToken)
        {
            Sensor s = new Sensor(source, thisToken, "Water Sensor", 
                EntityFilter.Plane(Plane.Ground, true),
                EntityFilter.Plane(Plane.Ground, true));
            s.Desc = Scribe.Write("Stops Ground Tokens." +
                "\nGround Units take 5 damage if in Cell at the end of their turn.");
            s.OnThisEnter = (c) =>
            {
                s.Subscribe(c);
                EffectSet e = new EffectSet();
                foreach (Token t in c.Occupants - s.Trigger)
                    e.Add(Effect.AddTimer(s, new EffectArgs(t, Timer.WaterLogged(e, t as Unit))));
                EffectQueue.Add(e);
            };

            s.OnOtherEnter = (t) =>
            {
                EffectQueue.Add(Effect.AddTimer(
                    s, new EffectArgs(t, Timer.WaterLogged(Force.Effect, t as Unit))));
                if (!Session.Active.paused && EntityFilter.UnitDest.Test(t))
                    EffectQueue.Interrupt(Effect.FireInitial(s, new EffectArgs(t, 7)));
            };

            s.OnOtherExit = (t) =>
            {
                EffectQueue.Add(Effect.RemoveTimer(
                    s, new EffectArgs(t, Timer.WaterLogged(Force.Effect, t as Unit))));
            };
            return s;
        }

        public static Sensor Web(IEffect source, Token thisToken)
        {
            Sensor s = new Sensor(source, thisToken, "Web Sensor", 
                EntityFilter.Plane(Plane.Tall, true) + EntityPredicates.Unit,
                EntityFilter.Plane(Plane.Tall, true));
            s.Desc = Scribe.Write("Stops Ground and Air Tokens." +
                "\nGround and Air Units in Cell have a Move Range of 1.");
            s.OnOtherEnter = (t) => 
            { 
                EffectQueue.Add(Effect.Stick(s, new EffectArgs(t))); 
            };

            s.OnThisExit = (c) =>
            {
                s.Subscribe(c);

                TrackList list = s.ThisToken.trackList;

                foreach (Token t in list)
                {
                    Ability move = (t as Unit).arsenal.Move;
                    if (move != null)
                    {
                        move.Aims[0].range = (Range)list[t];
                        list.Remove(t);
                    }
                }
            };

            s.OnOtherExit = (t) =>
            {
                Ability move = (t as Unit).arsenal.Move;
                if (move != null)
                {
                    TrackList list = s.ThisToken.trackList;
                    move.Aims[0].range = (Range)list[t];
                    list.Remove(t);
                }
            };
            return s;
        }
    }
}

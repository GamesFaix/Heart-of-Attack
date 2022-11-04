using System;
using System.Collections.Generic;
using HOA.Ab;
using HOA.Ef;
using HOA.Fargo;

namespace HOA.To
{

    public partial class Sensor
    {
        //This file is just for specialized constructors and methods.

        public static Sensor Blank(object source, Token thisToken)
        {
            Sensor s = new Sensor(thisToken, "Blank Sensor", Filter.False, Filter.False);
            s.Desc = Scribe.Write("Does nothing.");
            return s;
        }

        /// <summary>
        /// INCOMPLETE
        /// </summary>
         public static Sensor Aperture(Token thisToken)
        {
            Sensor s = new Sensor(thisToken, "Aperture Sensor",
                Filter.Token, Filter.Plane(Plane.Ground | Plane.Air | Plane.Ethereal, true));
            s.Desc = Scribe.Write("Stops ground, air, and ethereal tokens.");
            
            s.OnThisEnter = (c) =>
            {
                s.Subscribe(c);
                Set<IEntity> apertures = 
                    Session.Active.tokens 
                    / Filter.Species(Species.Aperture, true);
                apertures.Remove(s.ThisToken);

                foreach (Token t in apertures)
                {
                    c.Links.Add(t.Cell);
                    t.Cell.Links.Add(c);
                }
            };

            s.OnThisExit = (c) =>
            {
                Set<IEntity> apertures = 
                    Session.Active.tokens 
                    / Filter.Species(Species.Aperture, true);
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

         public static Sensor BombingRange(Token thisToken)
        {
            Sensor s = new Sensor(thisToken, "Bombing Range Sensor", Filter.Unit);
            s.Desc = Scribe.Write("If a Unit is in Cell at the end of its turn," +
                "\n10 Explosive damage is dealth at Cell.");

            s.OnOtherEnter = (t) =>
            {
                Effect e = Effect.AddTimer(s, new EffectArgs(
                    Arg.Target(FT.Unit, t)));
                e.args.component = Timer.Bombing(e, t as Unit);
                Queue.Add(e);
            };
            s.OnOtherExit = (t) =>
            {
                Effect e = Effect.RemoveTimer(s, new EffectArgs(
                    Arg.Target(FT.Unit, t)));
                e.args.component = Timer.Bombing(e, t as Unit);
                Queue.Add(e);
            };
            return s;
        }

        /// <summary> INCOMPLETE! </summary>
         public static Sensor Carapace(Token thisToken)
        {
            Sensor s = new Sensor(thisToken, "Carapace Invader Sensor",
                Filter.Species(Species.Carapace, false) + Filter.Owner(thisToken.Owner, true));
            s.Desc = Scribe.Write("Units in Cell on {0}'s team " +
                "\n(except Carapace Invaders) add n{0}'s Defense to their own.", s.ThisToken);
            return s;
        }

         public static Sensor Curse(Token thisToken)
        {
            Sensor s = new Sensor(thisToken, "Curse Sensor", Filter.Unit);
            s.Desc = Scribe.Write("Units entering Cell take 2 damage." +
                "\nUnits take 2 damage if in Cell at the end of their turn.");

            s.OnThisEnter = (c) =>
            {
                foreach (Cell cell in c.NeighborsAndSelf)
                    s.Subscribe(cell);
                Ef.Set e = new Ef.Set();
                foreach (Unit u in c.occupants)
                    e.Add(Effect.AddTimer(s, new EffectArgs(
                        Arg.Target(FT.Unit, u),
                        Timer.Cursed(e, u))));
                Queue.Add(e);
            };

            s.OnOtherEnter = (t) =>
            {
                Effect e = Effect.AddTimer(s, new EffectArgs(
                    Arg.Target(FT.Unit, t)));
                e.args.component = Timer.Cursed(e, t as Unit);
                Queue.Add(e);
                if (!Session.Active.paused)
                    Queue.Interrupt(Effect.Damage(s, new EffectArgs(
                        Arg.Target(FT.Unit, t),
                        Arg.Num(FN.Damage, 2))));
            };

            s.OnOtherExit = (t) =>
            {
                Effect e = Effect.RemoveTimer(s, new EffectArgs(
                    Arg.Target(FT.Unit, t)));
                e.args.component = Timer.Cursed(e, t as Unit);
                Queue.Add(e);
            };

            s.OnThisExit = (c) =>
            {
                Ef.Set e = new Ef.Set();
                foreach (Token t in c.occupants)
                {
                    e.Add(Effect.RemoveTimer(s, new EffectArgs(
                        Arg.Target(FT.Unit, t), 
                        Timer.Cursed(e, t as Unit))));
                    s.UnsubscribeAll();
                }
                Queue.Add(e);
            };
            return s;
        }

         public static Sensor Exhaust(Token thisToken)
        {
            Sensor s = new Sensor(thisToken, "Exhaust Sensor", 
                Filter.Plane(Plane.Ground|Plane.Air, true) + Filter.Unit, 
                Filter.Plane(Plane.Ground|Plane.Air, true));
            s.Desc = Scribe.Write("Stops Ground and Air To." +
                "\nGround and Air Units entering Cell take 5 damage." +
                "\nGround and Air Units take 5 damage if in Cell at the end of their turn.");
            s.OnThisEnter = (c) =>
            {
                s.Subscribe(c);
                Ef.Set e = new Ef.Set();
                foreach (Token t in c.occupants / s.Trigger)
                    e.Add(Effect.AddTimer(s, new EffectArgs(
                        Arg.Target(FT.Unit, t), 
                        Timer.Exhaust(e, t as Unit))));
                Queue.Add(e);
            };

            s.OnOtherEnter = (t) =>
            {
                Effect e = Effect.AddTimer(s, new EffectArgs(
                    Arg.Target(FT.Unit, t)));
                e.args.component = Timer.Exhaust(e, t as Unit);
                Queue.Add(e);
                if (!Session.Active.paused)
                    Queue.Interrupt(Effect.Damage(s, new EffectArgs(
                        Arg.Target(FT.Unit, t), 
                        Arg.Num(FN.Damage, 5))));
            };

            s.OnOtherExit = (t) =>
            {
                Effect e = Effect.RemoveTimer(s, new EffectArgs(
                    Arg.Target(FT.Unit, t)));
                e.args.component = Timer.Exhaust(e, t as Unit);
                Queue.Add(e);
            };
            return s;
        }

         public static Sensor Ice(Token thisToken)
        {
            Sensor s = new Sensor(thisToken, "Ice Sensor",
                Filter.Plane(Plane.Ground, true));
            s.Desc = Scribe.Write("When any Ground Token enters Cell," +
                "\nthere is a 25% chance Ice breaks, turning into Water." +
                "\nTokens moving through Cell when Ice breaks do not stop," +
                "\nonly Tokens stopping on Ice that breaks are affected by Water.");
            s.OnOtherEnter = (t) =>
            {
                if (!Session.Active.paused
                    && Random.Range(1, 4) == 1) 
                        Queue.Add(Effect.Replace(s, new EffectArgs(
                            Arg.Target(FT.User, s.ThisToken), 
                            Species.Water)));
            };
            s.OnThisEnter = (c) => { s.Subscribe(c); };
            return s;
        }

         public static Sensor Lava(Token thisToken)
        {
            Sensor s = new Sensor(thisToken, "Lava Sensor", 
                Filter.Plane(Plane.Ground, true) + Filter.Unit,
                Filter.Plane(Plane.Ground, true));
            s.Desc = Scribe.Write("Stops Ground To." +
                "\nGround Units entering Cell take 7 damage." +
                "\nGround Units take 7 damage if in Cell at the end of their turn.");

            s.OnThisEnter = (c) =>
            {
                s.Subscribe(c);
                Ef.Set e = new Ef.Set();
                foreach (Token t in c.occupants / s.Trigger)
                    e.Add(Effect.AddTimer(s, new EffectArgs(
                        Arg.Target(FT.Unit, t), 
                        Timer.Incineration(e, t as Unit))));
                Queue.Add(e);
            };

            s.OnOtherEnter = (t) =>
            {
                Effect e = Effect.AddTimer(s, new EffectArgs(
                    Arg.Target(FT.Unit, t)));
                e.args.component = Timer.Incineration(e, t as Unit);
                Queue.Add(e);
                
                if (!Session.Active.paused && Filter.UnitDest.AllTrue(t))
                    Queue.Interrupt(Effect.FireInitial(s, new EffectArgs(
                        Arg.Target(FT.Unit, t), 
                        Arg.Num(FN.Damage, 7))));
            };

            s.OnOtherExit = (t) =>
            {
                Effect e = Effect.RemoveTimer(s, new EffectArgs(
                    Arg.Target(FT.Unit, t)));
                e.args.component = Timer.Incineration(e, t as Unit);
                Queue.Add(e);
            };
            return s;
        }

         public static Sensor Mine(Token thisToken)
        {
            Sensor s = new Sensor(thisToken, "Mine Sensor", Filter.Token);
            s.Desc = Scribe.Write("If a Token enters Cell, " +
                "\n10 Explosive damage is dealt at {0}'s Cell.", s.ThisToken);
            s.OnOtherEnter = (t) =>
            {
                if (!Session.Active.paused) 
                    Queue.Interrupt(Sequence.Detonate(s, new EffectArgs(
                        Arg.Target(FT.Token, s.ThisToken))));
            };
            s.OnThisEnter = (c) =>
            {
                foreach (Cell cell in c.NeighborsAndSelf)
                    s.Subscribe(cell);
            };

            s.OnThisExit = (c) => { s.UnsubscribeAll(); };
            return s;
        }

         public static Sensor TimeSink(Token thisToken)
        {
            Sensor s = new Sensor(thisToken, "Time Sink Sensor", Filter.Unit);
            s.Desc = Scribe.Write("Units in Cell have -2 Initiative");
            s.OnOtherEnter = (t) =>
            {
                s.ThisToken.trackList.Add(t, 2);
                Queue.Add(Effect.AddStat(s, new EffectArgs(
                    Arg.Target(FT.Unit, t),
                    Arg.Num(FN.Damage, -2),
                    Arg.Text(FX.Stat, "Initiative"))));
            };

            s.OnOtherExit = (t) =>
            {
                s.ThisToken.trackList.Remove(t);
                Queue.Add(Effect.AddStat(s, new EffectArgs(
                    Arg.Target(FT.Unit, t),
                    Arg.Num(FN.Damage, 2),
                    Arg.Text(FX.Stat, "Initiative"))));
            };
            return s;
        }

         public static Sensor TimeWell(Token thisToken)
        {
            Sensor s = new Sensor(thisToken, "Time Well Sensor", Filter.Unit);
            s.Desc = Scribe.Write("Units in Cell have +2 Initiative");
            s.OnOtherEnter = (t) =>
            {
                s.ThisToken.trackList.Add(t, 2);
                Queue.Add(Effect.AddStat(s, new EffectArgs(
                    Arg.Target(FT.Unit, t),
                    Arg.Num(FN.Damage, 2),
                    Arg.Text(FX.Stat, "Initiative"))));
            };

            s.OnOtherExit = (t) =>
            {
                s.ThisToken.trackList.Remove(t);
                Queue.Add(Effect.AddStat(s, new EffectArgs(
                    Arg.Target(FT.Unit, t),
                    Arg.Num(FN.Damage, -2),
                    Arg.Text(FX.Stat, "Initiative"))));
            };
            return s;
        }

         public static Sensor Water(Token thisToken)
        {
            Sensor s = new Sensor(thisToken, "Water Sensor", 
                Filter.Plane(Plane.Ground, true),
                Filter.Plane(Plane.Ground, true));
            s.Desc = Scribe.Write("Stops Ground To." +
                "\nGround Units take 5 damage if in Cell at the end of their turn.");
            s.OnThisEnter = (c) =>
            {
                s.Subscribe(c);
                Ef.Set e = new Ef.Set();
                foreach (Token t in c.occupants / s.Trigger)
                    e.Add(Effect.AddTimer(s, new EffectArgs(
                        Arg.Target(FT.Unit, t), 
                        Timer.WaterLogged(e, t as Unit))));
                Queue.Add(e);
            };

            s.OnOtherEnter = (t) =>
            {
                Effect e = Effect.AddTimer(s, new EffectArgs(
                    Arg.Target(FT.Unit, t)));
                e.args.component = Timer.WaterLogged(e, t as Unit);
                Queue.Add(e);
                if (!Session.Active.paused && Filter.UnitDest.AllTrue(t))
                    Queue.Interrupt(Effect.FireInitial(s, new EffectArgs(
                        Arg.Target(FT.Unit, t), 
                        Arg.Num(FN.Damage, 5))));
            };

            s.OnOtherExit = (t) =>
            {
                Effect e = Effect.RemoveTimer(s, new EffectArgs(
                    Arg.Target(FT.Unit, t)));
                e.args.component = Timer.WaterLogged(e, t as Unit);
                Queue.Add(e);
            };
            return s;
        }

         public static Sensor Web(Token thisToken)
        {
            Sensor s = new Sensor(thisToken, "Web Sensor", 
                Filter.Plane(Plane.Tall, true) + Filter.Unit,
                Filter.Plane(Plane.Tall, true));
            s.Desc = Scribe.Write("Stops Ground and Air To." +
                "\nGround and Air Units in Cell have a Move Range of 1.");
            s.OnOtherEnter = (t) => 
            { 
                Queue.Add(Effect.Stick(s, new EffectArgs(
                    Arg.Target(FT.Unit, t)))); 
            };

            s.OnThisExit = (c) =>
            {
                s.Subscribe(c);

                TrackList list = s.ThisToken.trackList;

                foreach (Token t in list)
                {
                    AbilityClosure move = (t as Unit).arsenal.Move;
                    if (move != null)
                    {
                        move.ability.Aims[0].range = (Range<sbyte>)list[t];
                        list.Remove(t);
                    }
                }
            };

            s.OnOtherExit = (t) =>
            {
                AbilityClosure move = (t as Unit).arsenal.Move;
                if (move != null)
                {
                    TrackList list = s.ThisToken.trackList;
                    move.ability.Aims[0].range = (Range<sbyte>)list[t];
                    list.Remove(t);
                }
            };
            return s;
        }
    }
}

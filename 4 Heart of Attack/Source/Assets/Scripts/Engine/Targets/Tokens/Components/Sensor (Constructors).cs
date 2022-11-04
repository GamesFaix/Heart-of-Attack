using UnityEngine;
using System.Collections.Generic;

namespace HOA { 

    public partial class Sensor
    {
        //This file is just for specialized constructors and methods.

        public static Sensor Blank(Token parent)
        {
            Sensor s = new Sensor(parent);
            s.Name = "Blank Sensor";
            s.Desc = () => { return "Does nothing."; };
            s.PlanesToStop = Plane.None;
            s.TriggerTest = NothingTrigger;
            return s;
        }

        /// <summary>
        /// INCOMPLETE
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="cell"></param>
        /// <returns></returns>
        public static Sensor Aperture(Token parent) 
        {
            Sensor s = new Sensor(parent);
            s.Name = "Aperture Sensor";
            s.Desc = () => { return "Stops all Tokens."; };
            s.PlanesToStop = (Plane.Ground|Plane.Air|Plane.Ethereal);
            s.TriggerTest = EverythingTrigger;

            s.OnParentEnter = (c) =>
            {
                s.Subscribe(c);
                TokenSet apertures = TokenRegistry.Tokens - TargetFilter.Species(Species.Aperture, true);
                apertures.Remove(s.Parent);

                foreach (Token t in apertures)
                {
                    Cell otherCell = t.Body.Cell;
                    c.Links.Add(otherCell);
                    otherCell.Links.Add(c);
                }
            };

            s.OnParentExit = (c) =>
                {
                    TokenSet apertures = TokenRegistry.Tokens - TargetFilter.Species(Species.Aperture, true);
                    apertures.Remove(s.Parent);

                    foreach (Token t in apertures)
                    {
                        Cell otherCell = t.Body.Cell;
                        s.Parent.Body.Cell.Links.Remove(otherCell);
                        otherCell.Links.Remove(s.Parent.Body.Cell);
                    }
                    s.Unsubscribe(c);
                };

            return s;
        }

        public static Sensor BombingRange(Token parent)
        {
            Sensor s = new Sensor(parent);
            s.Name = "Bombing Range Sensor";
            s.Desc = () => 
            {
                return "If a Unit is in Cell at the end of its turn," +
                "\n10 Explosive damage is dealth at Cell.";
            };
            s.TriggerTest = UnitTrigger;

            s.OnOtherEnter = (t) =>
            {
                if (t is Unit)
                {
                    Unit u = t as Unit;
                    u.timers.Add(Timer.Bombing(new Source(s.Parent), u));
                }
            };
            s.OnOtherExit = (Token token) => { s.RemoveTimer(token, "Bombing"); };
            return s;
        }

        /// <summary>
        /// INCOMPLETE!
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static Sensor Carapace(Token parent)
        {
            Sensor s = new Sensor(parent);
            s.Name = "Carapace Invader Sensor";
            s.Desc = () =>
            {
                return "Units in Cell on " + s.Parent + "'s team " +
                "\n(except Carapace Invaders) add" +
                "\n" + s.Parent + "'s Defense to their own.";
            };
            s.TriggerTest = (Token token) =>
            {
                return ((token is Unit) 
                    && (token.ID.Species != Species.Carapace) 
                    && (token.Owner == s.Parent.Owner));
            };

            return s;
        }

        public static Sensor Curse(Token parent)
        {
            Sensor s = new Sensor(parent);
            s.Name = "Curse Sensor";
            s.Desc = () => 
            {
                return "Units entering Cell take 2 damage." +
				"\nUnits take 2 damage if in Cell at the end of their turn.";
            };
            s.TriggerTest = UnitTrigger;

            s.OnParentEnter = (c) =>
            {
                foreach (Cell cell in c.NeighborsAndSelf)
                    s.Subscribe(cell);
                foreach (Unit u in c.Occupants)
                    u.timers.Add(Timer.Cursed(new Source(s.Parent), u));
            };

            s.OnOtherEnter = (t) =>
            {
                if (t is Unit)
                {
                    Unit u = t as Unit;
                    u.timers.Add(Timer.Cursed(new Source(s.Parent), u));
                    if (Game.Active)
                        EffectQueue.Interrupt(Effect.Damage(new Source(s.Parent), u, 2));
                }
            };
            
            s.OnOtherExit = (t) => { s.RemoveTimer(t, "Cursed"); };

            s.OnParentExit = (c) =>
            {
                foreach (Token t in c.Occupants)
                {
                    s.RemoveTimer(t, "Cursed"); 
                    s.UnsubscribeAll();
                }
            }; 
            return s;
        }

        public static Sensor Exhaust(Token parent)
        {
            Sensor s = new Sensor(parent);
            s.Name = "Exhaust Sensor";
            s.Desc = () =>
            {
                return "Stops Ground and Air Tokens." +
                "\nGround and Air Units entering Cell take 5 damage." +
                "\nGround and Air Units take 5 damage if in Cell at the end of their turn.";
            };
            s.PlanesToStop = Plane.Tall;
            s.TriggerTest = TallUnitTrigger;

            s.OnParentEnter = (c) =>
            {
                s.Subscribe(c);
                foreach (Unit u in c.Occupants)
                    u.timers.Add(Timer.Exhaust(new Source(s.Parent), u));
            };

            s.OnOtherEnter = (t) =>
            {
                if (t is Unit)
                {
                    Unit u = t as Unit;
                     u.timers.Add(Timer.Exhaust(new Source(s.Parent), u));
                    if (Game.Active)
                        EffectQueue.Interrupt(Effect.Damage(new Source(s.Parent), u, 5));
                }
            };

            s.OnOtherExit = (t) => { s.RemoveTimer(t, "Exhaust"); };
            return s;
        }

        public static Sensor Ice(Token parent)
        {
            Sensor s = new Sensor(parent);
            s.Name = "Ice Sensor";
            s.Desc = () =>
            {
                return "When any Ground Token enters Cell," +
                "\nthere is a 25% chance Ice breaks, turning into Water." +
                "\nTokens moving through Cell when Ice breaks do not stop," +
                "\nonly Tokens stopping on Ice that breaks are affected by Water.";
            };
            s.TriggerTest = GroundTokenTrigger;
            s.OnOtherEnter = (t) =>
            {
                if (Game.Active)
                {
                    int random = DiceCoin.Throw(new Source(s.Parent), EDice.D4);
                    if (random == 1) EffectQueue.Add(Effect.Replace(new Source(s.Parent), s.Parent, Species.Water));
                }
            };
            s.OnParentEnter = (c) => { s.Subscribe(c); };
            return s;
        }
       
        public static Sensor Lava(Token parent)
        {
            Sensor s = new Sensor(parent);
            s.Name = "Lava Sensor";
            s.Desc = () =>
            {
                return "Stops Ground Tokens." +
                "\nGround Units entering Cell take 7 damage." +
                "\nGround Units take 7 damage if in Cell at the end of their turn.";
            };
            s.PlanesToStop = Plane.Ground;
            s.TriggerTest = GroundTokenTrigger;

            s.OnParentEnter = (c) =>
            {
                s.Subscribe(c);
                foreach (Unit u in (c.Occupants - TargetFilter.Unit))
                    u.timers.Add(Timer.Incineration(new Source(s.Parent), u));
            };

            s.OnOtherEnter = (t) =>
            {
                if (t is Unit)
                {
                    Unit u = t as Unit;
                    u.timers.Add(Timer.Incineration(new Source(s.Parent), u));
                    TargetFilter f = TargetFilter.UnitDest;
                    if (Game.Active && f.Test(t))
                        EffectQueue.Interrupt(Effect.FireInitial(new Source(s.Parent), t, 7));
                }
            };

            s.OnOtherExit = (t) => { s.RemoveTimer(t, "Incineration"); };
            return s;
        }
            
        public static Sensor Mine(Token parent)
        {
            Sensor s = new Sensor(parent);
            s.Name = "Mine Sensor";
            s.Desc = () =>
            {
                return "If a Token enters Cell, " +
                "\n10 Explosive damage is dealt at " + s.Parent + "'s Cell.";
            };
            s.TriggerTest = EverythingTrigger;
            s.OnOtherEnter = (t) =>
            {
                if (Game.Active) EffectQueue.Interrupt(Effect.Detonate(new Source(t), s.Parent));
            };
            s.OnParentEnter = (c) =>
            {
                foreach (Cell cell in c.NeighborsAndSelf)
                    s.Subscribe(cell);
            };

            s.OnParentExit = (c) => { s.UnsubscribeAll(); };
            return s;
        }
   
        public static Sensor TimeSink(Token parent)
        {
            Sensor s = new Sensor(parent);
            s.Name = "Time Sink Sensor";
            s.Desc = () => { return "Units in Cell have -2 Initiative"; };
            s.TriggerTest = UnitTrigger;

            s.OnOtherEnter = (t) =>
            {
                if (t is Unit)
                {
                    Unit u = t as Unit;
                    s.Parent.WatchList.Add(new TokenRecord(t));
                    EffectQueue.Add(Effect.AddStat(new Source(s.Parent), u, Stats.Initiative, 2));
                }
            };

            s.OnOtherExit = (t) =>
            {
                if (t is Unit)
                {
                    Unit u = t as Unit;
                    s.Parent.WatchList.Remove(t);
                    EffectQueue.Add(Effect.AddStat(new Source(s.Parent), u, Stats.Initiative, -2));
                }
            };
            return s;
        }

        public static Sensor TimeWell(Token parent)
        {
            Sensor s = new Sensor(parent);
            s.Name = "Time Well Sensor";
            s.Desc = () => { return "Units in Cell have +2 Initiative"; };
            s.TriggerTest = UnitTrigger;

            s.OnOtherEnter = (t) =>
            {
                if (t is Unit)
                {
                    Unit u = t as Unit;
                    s.Parent.WatchList.Add(new TokenRecord(t));
                    EffectQueue.Add(Effect.AddStat(new Source(s.Parent), u, Stats.Initiative, 2));
                }
            };

            s.OnOtherExit = (t) =>
            {
                if (t is Unit)
                {
                    Unit u = t as Unit;
                    s.Parent.WatchList.Remove(t);
                    EffectQueue.Add(Effect.AddStat(new Source(s.Parent), u, Stats.Initiative, -2));
                }
            };
            return s;
        }
   
        public static Sensor Water(Token parent)
        {
            Sensor s = new Sensor(parent);
            s.Name = "Water Sensor";
            s.Desc = () =>
            {
                return "Stops Ground Tokens." +
                "\nGround Units take 5 damage if in Cell at the end of their turn.";
            };
            s.PlanesToStop = Plane.Ground;
            s.TriggerTest = GroundTokenTrigger;

            s.OnParentEnter = (c) =>
            {
                s.Subscribe(c);
                foreach (Unit u in (c.Occupants - TargetFilter.Unit))

                    u.timers.Add(Timer.WaterLogged(new Source(s.Parent), u));
            };
           
            s.OnOtherEnter = (t) =>
            {
                if (t is Unit)
                {
                    Unit u = t as Unit;
                    u.timers.Add(Timer.WaterLogged(new Source(s.Parent), u));
                    TargetFilter f = TargetFilter.UnitDest;
                    if (Game.Active && f.Test(t))
                        EffectQueue.Interrupt(Effect.FireInitial(new Source(s.Parent), t, 7));
                }
            };

            s.OnOtherExit = (t) => { s.RemoveTimer(t, "WaterLogged"); };
            return s;
        }

        public static Sensor Web(Token parent)
        {
            Sensor s = new Sensor(parent);
            s.Name = "Web Sensor";
            s.Desc = () =>
            {
                return "Stops Ground and Air Tokens." +
                "\nGround and Air Units in Cell have a Move Range of 1.";
            };
            s.PlanesToStop = Plane.Tall;
            s.TriggerTest = TallUnitTrigger;

            s.OnOtherEnter = (t) =>
            {
                if (t is Unit)
                {
                    Unit u = t as Unit;
                    EffectQueue.Add(Effect.Stick(new Source(s.Parent), u));
                }
            };

            s.OnParentExit = (c) => 
            {
                s.Subscribe(c);
                foreach (WebRecord record in s.Parent.WatchList) {
                    Unit u = record.Unit;
                    Ability move = u.Arsenal.Move;
                    if (move != null) {
                        move.Aims[0].Range = record.RangeLost;
                        s.Parent.WatchList.Remove(u);
                    }
                }
            };
            
            s.OnOtherExit = (t) => 
            {
                if (t is Unit)
                {
                    Unit u = t as Unit;
                    Ability move = u.Arsenal.Move;
                    if (move != default(Ability))
                    {
                        WebRecord record = (WebRecord)(s.Parent.WatchList.Record(t));
                        move.Aims[0].Range = record.RangeLost;
                        s.Parent.WatchList.Remove(t);
                    }
                }
            };
            return s;
        }
    }
}

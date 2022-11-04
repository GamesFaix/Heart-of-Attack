using UnityEngine;
using System.Collections.Generic;

namespace HOA { 

    public partial class Sensor
    {
        //This file is just for specialized constructors and methods.

        /*INCOMPLETE*/
        public static Sensor Aperture(Token parent, Cell cell) 
        {
            Sensor s = new Sensor(parent, cell);
            s.Name = "Aperture Sensor";
            s.Desc = () => { return "Stops all Tokens."; };
            s.PlanesToStop = (Plane.Ground|Plane.Air|Plane.Ethereal);
            s.TriggerTest = EverythingTrigger;

            s.SNCE = (Token token) =>
            {
                
            };
            s.ONE = s.SNCE;

            s.Enter(s.Cell);
            return s;
        }

        public static Sensor BombingRange(Token parent, Cell cell)
        {
            Sensor s = new Sensor(parent, cell);
            s.Name = "Bombing Range Sensor";
            s.Desc = () => 
            {
                return "If a Unit is in Cell at the end of its turn," +
                "\n10 Explosive damage is dealth at Cell.";
            };
            s.TriggerTest = UnitTrigger;

            s.SNCE = (Token token) =>
            {
                Unit u;
                if (token.IsUnit(out u))
                    u.timers.Add(Timer.Bombing(new Source(s.Parent), u));
            };
            s.ONE = s.SNCE;
            
            s.SXCE = (Token token) => { s.RemoveTimer(token, "Bombing"); };
            s.OXE = s.SXCE;

            s.Enter(s.Cell);
            return s;
        }

        /*INCOMPLETE*/
        public static Sensor Carapace(Token parent, Cell cell)
        {
            Sensor s = new Sensor(parent, cell);
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
                    && (token.ID.Code != EToken.CARA) 
                    && (token.Owner == s.Parent.Owner));
            };

            s.SNCE = (Token token) =>
            {
                
            };
            s.ONE = s.SNCE;

            s.SXCE = (Token token) => {  };
            s.OXE = s.SXCE;

            s.Enter(s.Cell);
            return s;
        }

        public static Sensor Curse(Token parent, Cell cell)
        {
            Sensor s = new Sensor(parent, cell);
            s.Name = "Curse Sensor";
            s.Desc = () => 
            {
                return "Units entering Cell take 2 damage." +
				"\nUnits take 2 damage if in Cell at the end of their turn.";
            };
            s.TriggerTest = UnitTrigger;

            s.SNCE = (Token token) =>
            {
                Unit u;
                if (token.IsUnit(out u))
                    u.timers.Add(Timer.Cursed(new Source(s.Parent), u));
            };
            s.ONE = (Token token) =>
            {
                s.SNCE(token);
                Unit u;
                if (Game.Active && token.IsUnit(out u))
                    EffectQueue.Interrupt(Effect.Damage(new Source(s.Parent), u, 2));
            };
            
            s.SXCE = (Token token) => { s.RemoveTimer(token, "Cursed"); };
            s.OXE = s.SXCE;

            s.Enter(s.Cell);
            return s;
        }

        public static Sensor Exhaust(Token parent, Cell cell)
        {
            Sensor s = new Sensor(parent, cell);
            s.Name = "Exhaust Sensor";
            s.Desc = () =>
            {
                return "Stops Ground and Air Tokens." +
                "\nGround and Air Units entering Cell take 5 damage." +
                "\nGround and Air Units take 5 damage if in Cell at the end of their turn.";
            };
            s.PlanesToStop = Plane.Tall;
            s.TriggerTest = TallUnitTrigger;

            s.SNCE = (Token token) =>
            {
                Unit u;
                if (token.IsUnit(out u))
                    u.timers.Add(Timer.Exhaust(new Source(s.Parent), u));
            };
            s.ONE = (Token token) =>
            {
                s.SNCE(token);
                Unit u;
                if (Game.Active && token.IsUnit(out u))
                    EffectQueue.Interrupt(Effect.Damage(new Source(s.Parent), u, 5));
            };

            s.SXCE = (Token token) => { s.RemoveTimer(token, "Exhaust"); };
            s.OXE = s.SXCE;

            s.Enter(s.Cell);
            return s;
        }

        public static Sensor Ice(Token parent, Cell cell)
        {
            Sensor s = new Sensor(parent, cell);
            s.Name = "Ice Sensor";
            s.Desc = () =>
            {
                return "When any Ground Token enters Cell," +
                "\nthere is a 25% chance Ice breaks, turning into Water." +
                "\nTokens moving through Cell when Ice breaks do not stop," +
                "\nonly Tokens stopping on Ice that breaks are affected by Water.";
            };
            s.TriggerTest = GroundTokenTrigger;
            s.ONE = (Token token) =>
            {
                if (Game.Active)
                {
                    int random = DiceCoin.Throw(new Source(s.Parent), EDice.D4);
                    if (random == 1) EffectQueue.Add(Effect.Replace(new Source(s.Parent), s.Parent, EToken.WATR));
                }
            };
            s.Enter(s.Cell);
            return s;
        }
       
        public static Sensor Lava(Token parent, Cell cell)
        {
            Sensor s = new Sensor(parent, cell);
            s.Name = "Lava Sensor";
            s.Desc = () =>
            {
                return "Stops Ground Tokens." +
                "\nGround Units entering Cell take 7 damage." +
                "\nGround Units take 7 damage if in Cell at the end of their turn.";
            };
            s.PlanesToStop = Plane.Ground;
            s.TriggerTest = GroundTokenTrigger;

            s.SNCE = (Token token) =>
            {
                Unit u;
                if (token.IsUnit(out u))
                    u.timers.Add(Timer.Incineration(new Source(s.Parent), u));
            };
            s.ONE = (Token token) =>
            {
                s.SNCE(token);
                TargetFilter f = TargetFilter.UnitDest;
                if (Game.Active && f.Test(token))
                    EffectQueue.Interrupt(Effect.FireInitial(new Source(s.Parent), token, 7));
            };

            s.SXCE = (Token token) => { s.RemoveTimer(token, "Incineration"); };
            s.OXE = s.SXCE;

            s.Enter(s.Cell);
            return s;
        }
            
        public static Sensor Mine(Token parent, Cell cell)
        {
            Sensor s = new Sensor(parent, cell);
            s.Name = "Mine Sensor";
            s.Desc = () =>
            {
                return "If a Token enters Cell, " +
                "\n10 Explosive damage is dealt at " + s.Parent + "'s Cell.";
            };
            s.TriggerTest = EverythingTrigger;
            s.ONE = (Token token) =>
            {
                if (Game.Active) EffectQueue.Interrupt(Effect.Detonate(new Source(token), s.Parent));
            };
            s.Enter(s.Cell);
            return s;
        }
   
        public static Sensor TimeSink(Token parent, Cell cell)
        {
            Sensor s = new Sensor(parent, cell);
            s.Name = "Time Sink Sensor";
            s.Desc = () => { return "Units in Cell have -2 Initiative"; };
            s.TriggerTest = UnitTrigger;

            s.SNCE = (Token token) => 
            {
               Unit u;
               if (token.IsUnit(out u))
               {
                    s.Parent.WatchList.Add(new TokenRecord(token));
                    EffectQueue.Add(Effect.AddStat(new Source(s.Parent), u, Stats.Initiative, 2));
                }
            };
            s.ONE = s.SNCE;

            s.SXCE = (Token token) => 
            {
               Unit u;
               if (token.IsUnit(out u))
               {
                   s.Parent.WatchList.Remove(new TokenRecord(token));
                   EffectQueue.Add(Effect.AddStat(new Source(s.Parent), u, Stats.Initiative, -2));
               }
            };
            s.OXE = s.SXCE;

            s.Enter(s.Cell);
            return s;
        }

        public static Sensor TimeWell(Token parent, Cell cell)
        {
            Sensor s = new Sensor(parent, cell);
            s.Name = "Time Well Sensor";
            s.Desc = () => { return "Units in Cell have +2 Initiative"; };
            s.TriggerTest = UnitTrigger;

            s.SNCE = (Token token) => 
            {
               Unit u;
               if (token.IsUnit(out u))
               {
                   s.Parent.WatchList.Add(new TokenRecord(token));
                   EffectQueue.Add(Effect.AddStat(new Source(s.Parent), u, Stats.Initiative, 2));
               }
            };
            s.ONE = s.SNCE;

            s.SXCE = (Token token) =>
            {
                Unit u;
                if (token.IsUnit(out u))
                {
                    s.Parent.WatchList.Remove(new TokenRecord(token));
                    EffectQueue.Add(Effect.AddStat(new Source(s.Parent), u, Stats.Initiative, -2));
                }
            };
            s.OXE = s.SXCE;

            s.Enter(s.Cell);
            return s;
        }
   
        public static Sensor Water(Token parent, Cell cell)
        {
            Sensor s = new Sensor(parent, cell);
            s.Name = "Water Sensor";
            s.Desc = () =>
            {
                return "Stops Ground Tokens." +
                "\nGround Units take 5 damage if in Cell at the end of their turn.";
            };
            s.PlanesToStop = Plane.Ground;
            s.TriggerTest = GroundTokenTrigger;

            s.SNCE = (Token token) =>
            {
                Unit u;
                if (token.IsUnit(out u))
                    u.timers.Add(Timer.WaterLogged(new Source(s.Parent), u));
            };
            s.ONE = (Token token) =>
            {
                s.SNCE(token);
                TargetFilter f = TargetFilter.UnitDest;
                if (Game.Active && f.Test(token))
                    EffectQueue.Interrupt(Effect.FireInitial(new Source(s.Parent), token, 7));
            };

            s.SXCE = (Token token) => { s.RemoveTimer(token, "WaterLogged"); };
            s.OXE = s.SXCE;

            s.Enter(s.Cell);
            return s;
        }

        public static Sensor Web(Token parent, Cell cell)
        {
            Sensor s = new Sensor(parent, cell);
            s.Name = "Web Sensor";
            s.Desc = () =>
            {
                return "Stops Ground and Air Tokens." +
                "\nGround and Air Units in Cell have a Move Range of 1.";
            };
            s.PlanesToStop = Plane.Tall;
            s.TriggerTest = TallUnitTrigger;

            s.SNCE = (Token token) =>
            {
                Unit u;
                if (token.IsUnit(out u))
                   EffectQueue.Add(Effect.Stick(new Source(s.Parent), u));
            };
            s.ONE = s.SNCE;

            s.SXCE = (Token token) => 
            {
                foreach (WebRecord record in s.Parent.WatchList) {
                    Unit u;
                    if (record.Token.IsUnit(out u)) 
                    { 
                        Ability move = u.Arsenal.Move;
                        if (move != null) {
                            move.Aims[0].Range = record.RangeLost;
                            s.Parent.WatchList.Remove(token);
                        }
                    }
                }
            };
            
            s.OXE = (Token token) => 
            {
                Unit u;
                if (token.IsUnit(out u))
                {
                    Ability move = u.Arsenal.Move;
                    if (move != default(Ability))
                    {
                        WebRecord record = (WebRecord)(s.Parent.WatchList.Record(token));
                        move.Aims[0].Range = record.RangeLost;
                        s.Parent.WatchList.Remove(token);
                    }
                }
            };
            
            s.Enter(s.Cell);
            return s;
        }
    }
}

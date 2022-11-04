using System;
using System.Collections.Generic;
using HOA.Abilities;
using HOA.Effects;
using HOA.Args;
using Farg = HOA.Args.Arg;

namespace HOA.Tokens
{
    public partial class Timer
    {
        public static Timer ActiveGrenade(IEffect source, Unit parent)
        {
            var t = new Timer(source, parent);
            t.Name = "Active Grenade";
            t.Desc = Scribe.Write("{0} explosive damage at end of {1}'s next turn.", t.Modifier, t.self);
            t.Modifier = 10;
            t.Turns = 1;
            t.Test = t.ParentTurnEndTest;
            t.Activate = () => 
            {
                EffectQueue.Add(Sequence.Explosion(t, new EffectArgs(
                    Arg.Target(RT.Location, t.self.cell),    
                    Farg.Num(RN.Damage, t.Modifier), 
                    Farg.Option(RO.ExcludeSelf, false)))); 
            };
            return t;
        }

        public static Timer Altaration(IEffect source, Unit parent)
        {
            var t = new Timer(source, parent);
            t.Name = "Blood Altaration";
            t.Desc = Scribe.Write("Initiative +{0}. ({1} turns left.)", t.Modifier, t.Turns); 
            t.Turns = 2;
            t.Modifier = 4;
            t.Test = t.ParentTurnEndTest;
            t.Activate = () => 
            {
                EffectQueue.Add(Effect.AddStat(t, new EffectArgs(
                    Arg.Target(RT.User, t.selfUnit),
                    Farg.Num(RN.Damage, (sbyte)(0 - t.Modifier)), 
                    Farg.Text(RL.Stat, "Initiative"))));
            };
            return t;
        }

        public static Timer ArcticGust(IEffect source, Unit parent, sbyte modifier, AbilityClosure ability)
        {
            var t = new Timer(source, parent, modifier, ability);
            t.Name = "Arctic Gusted";
            t.Desc = Scribe.Write("Move range {0}. ({1} turns left.)", (0 - t.Modifier), t.Turns); 
            t.Turns = 1;
            t.Test = t.ParentTurnEndTest;
            t.Activate = () =>
            {
                AbilityClosure c = t.selfUnit.arsenal.Move;
                if (c != null)
                    c.ability.Aims[0].range.max += (sbyte)t.Modifier;
            };
            return t;
        }

        public static Timer Bombing(IEffect source, Unit parent)
        {
            var t = new Timer(source, parent);
            t.Name = "Targeted";
            t.Desc = Scribe.Write("{0} explosive damage at end of {1}'s turn.", t.Modifier, t.self);
            t.Turns = 1;
            t.Modifier = 10;
            t.Test = t.ParentTurnEndTest;
            t.Activate = () =>
            {
                EffectQueue.Add(Sequence.Explosion(t, new EffectArgs(
                    Arg.Target(RT.Location, t.self.cell),
                    Farg.Num(RN.Damage, t.Modifier),
                    Farg.Option(RO.ExcludeSelf, false))));
                t.Turns++;
            };
            return t;
        }

        public static Timer Corrosion(IEffect source, Unit parent, sbyte damage)
        {
            var t = new Timer(source, parent, damage);
            t.Name = "Corrosion";
            t.Desc = Scribe.Write("{0} damage at end of {1}'s turn. (50% next turn.)", t.Modifier, t.self); 
            t.Turns = 1;
            t.Test = t.ParentTurnEndTest;
            t.Activate = () =>
            {
                EffectQueue.Add(Effect.CorrodeResidual(t, new EffectArgs(
                    Arg.Target(RT.Damaged, t.selfUnit), 
                    Farg.Num(RN.Damage, t.Modifier))));
                t.Modifier = (sbyte)Math.Floor(t.Modifier * 0.5f);
                if (t.Modifier > 0) 
                    t.Turns++;
            };
            return t;

        }

        public static Timer Cursed(IEffect source, Unit parent)
        {
            var t = new Timer(source, parent);
            t.Name = "Cursed";
            t.Desc = Scribe.Write("{0} damage at end of {1}'s turn.", t.Modifier, t.self);
            t.Turns = 1;
            t.Modifier = 2;
            t.Test = t.ParentTurnEndTest;
            t.Activate = () =>
            {
                EffectQueue.Add(Effect.Damage(t, new EffectArgs(
                    Arg.Target(RT.Damaged, t.selfUnit), 
                    Farg.Num(RN.Damage, t.Modifier))));
                t.Turns++;
            };
            return t;
        }

        public static Timer Exhaust(IEffect source, Unit parent)
        {
            var t = new Timer(source, parent);
            t.Name = "Exhausted";
            t.Desc = Scribe.Write("{0} damage at end of {1}'s turn.", t.Modifier, t.self); 
            t.Turns = 1;
            t.Modifier = 5;
            t.Test = t.ParentTurnEndTest;
            t.Activate = () =>
            {
                EffectQueue.Add(Effect.Damage(t, new EffectArgs(
                    Arg.Target(RT.Damaged, t.selfUnit), 
                    Farg.Num(RN.Damage, t.Modifier))));
                t.Turns++;
            };
            return t;
        }

        public static Timer IceBlast(IEffect source, Unit parent)
        {
            var t = new Timer(source, parent);
            t.Name = "Ice Blasted";
            t.Desc = Scribe.Write("Initiative {0}. ({1} of {2}'s turns left.)",
                t.Modifier, t.Turns, t.self);
            t.Turns = 2;
            t.Modifier = (-2);
            t.Test = t.ParentTurnEndTest;
            t.Activate = () => 
            {
                EffectQueue.Add(Effect.AddStat(t, new EffectArgs(
                    Arg.Target(RT.Damaged, t.selfUnit), 
                    Farg.Num(RN.Damage, (sbyte)(0 - t.Modifier)),
                    Farg.Text(RL.Stat, "Initiative"))));
            };
            return t;
        }

        public static Timer Incineration(IEffect source, Unit parent)
        {
            var t = new Timer(source, parent);
            t.Name = "Incinerating";
            t.Desc = Scribe.Write("{0} damage at the end of {1}'s turn.", t.Modifier, t.self);
            t.Turns = 1;
            t.Modifier = 7;
            t.Test = t.ParentTurnEndTest;
            t.Activate = () =>
            {
                EffectQueue.Add(Effect.FireInitial(t, new EffectArgs(
                    Arg.Target(RT.Damaged, t.selfUnit), 
                    Farg.Num(RN.Damage, t.Modifier))));
                t.Turns++;
            };
            return t;
        }

        public static Timer Petrified(IEffect source, Unit parent, AbilityClosure ability)
        {
            var t = new Timer(source, parent, ability);
            t.Name = "Petrified";
            t.Desc = Scribe.Write("{0} cannot move. ({1} of {0}'s turns left.)", t.self, t.Turns);
            t.Turns = 1;
            t.Test = t.ParentTurnEndTest;
            t.Activate = () => 
            {
                EffectQueue.Add(Effect.Learn(t, new EffectArgs(
                    Arg.Target(RT.Damaged, t.selfUnit), 
                    t.Ability))); 
            };
            return t;
        }

        public static Timer Stunned(IEffect source, Unit parent, sbyte turns)
        {
            var t = new Timer(source, parent, turns);
            t.Name = "Stunned";
            t.Desc = Scribe.Write("{0} cannot move forward in the Queue for the next {1} turn changes.",
                    t.self, t.Turns);
            t.Turns = turns;
            t.Test = t.EveryTurnTest;
            t.Activate = () => 
            {
                EffectQueue.Add(Effect.Shift(t, new EffectArgs(
                    Arg.Target(RT.Damaged, t.selfUnit), 
                    Farg.Num(RN.Damage, 1)))); 
            };
            return t;
        }

        public static Timer TimeBomb(IEffect source, Unit parent, sbyte modifier)
        {
            var t = new Timer(source, parent, modifier);
            t.Name = "Time Bombed";
            t.Desc = Scribe.Write("Initiative {0}. ({1} of {2}'s turns left.)",
                    t.Modifier, t.Turns, t.self);
            t.Turns = 2;
            t.Test = t.ParentTurnEndTest;
            t.Activate = () => 
            {
                EffectQueue.Add(Effect.AddStat(t,
                    new EffectArgs(
                        Arg.Target(RT.Damaged, t.selfUnit), 
                        Farg.Num(RN.Damage, t.Modifier),
                        Farg.Text(RL.Stat, "Initiative"))));
            };
            return t;
        }

        public static Timer TimeSlam(IEffect source, Unit parent)
        {
            var t = new Timer(source, parent);
            t.Name = "Time Slammed";
            t.Desc = Scribe.Write("Initiative {0}. ({1} of {2}'s turns left.)",
                    t.Modifier, t.Turns, t.self);
            t.Turns = 2;
            t.Modifier = (-2);
            t.Test = t.ParentTurnEndTest;
            t.Activate = () => 
            {
                EffectQueue.Add(Effect.AddStat(t,
                     new EffectArgs(
                         Arg.Target(RT.Damaged, t.selfUnit), 
                         Farg.Num(RN.Damage, t.Modifier),
                         Farg.Text(RL.Stat, "Initiative"))));
            };
            return t;

        }

        public static Timer WaterLogged(IEffect source, Unit parent)
        {
            var t = new Timer(source, parent);
            t.Name = "Waterlogged";
            t.Desc = Scribe.Write("{0} damage at the end of {1}'s turn.", t.Modifier, t.self);
            t.Turns = 1;
            t.Modifier = 5;
            t.Test = t.ParentTurnEndTest;
            t.Activate = () =>
            {
                EffectQueue.Add(Effect.WaterLog(t, new EffectArgs(
                    Arg.Target(RT.Damaged, t.selfUnit), 
                    Farg.Num(RN.Damage, t.Modifier))));
                t.Turns++;
            };
            return t;
        }

	}
}
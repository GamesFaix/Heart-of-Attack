using System;
using System.Collections.Generic;
using HOA.Ab;
using HOA.Ef;
using HOA.Fargo;

namespace HOA.To 
{
    public partial class Timer
    {
        public static Timer ActiveGrenade(IEffect source, Unit parent)
        {
            Timer t = new Timer(source, parent);
            t.Name = "Active Grenade";
            t.Desc = Scribe.Write("{0} explosive damage at end of {1}'s next turn.", t.Modifier, t.ThisToken);
            t.Modifier = 10;
            t.Turns = 1;
            t.Test = t.ParentTurnEndTest;
            t.Activate = () => 
            { 
                Queue.Add(Sequence.Explosion(t, new EffectArgs(
                    Arg.Target(FT.Location, t.ThisToken.Cell),    
                    Arg.Num(FN.Damage, t.Modifier), 
                    Arg.Option(FO.ExcludeSelf, false)))); 
            };
            return t;
        }

        public static Timer Altaration(IEffect source, Unit parent)
        {
            Timer t = new Timer(source, parent);
            t.Name = "Blood Altaration";
            t.Desc = Scribe.Write("Initiative +{0}. ({1} turns left.)", t.Modifier, t.Turns); 
            t.Turns = 2;
            t.Modifier = 4;
            t.Test = t.ParentTurnEndTest;
            t.Activate = () => 
            {
                Queue.Add(Effect.AddStat(t, new EffectArgs(
                    Arg.Target(FT.User, t.ThisUnit),
                    Arg.Num(FN.Damage, (sbyte)(0 - t.Modifier)), 
                    Arg.Text(FX.Stat, "Initiative"))));
            };
            return t;
        }

        public static Timer ArcticGust(IEffect source, Unit parent, sbyte modifier, AbilityClosure ability)
        {
            Timer t = new Timer(source, parent, modifier, ability);
            t.Name = "Arctic Gusted";
            t.Desc = Scribe.Write("Move range {0}. ({1} turns left.)", (0 - t.Modifier), t.Turns); 
            t.Turns = 1;
            t.Test = t.ParentTurnEndTest;
            t.Activate = () =>
            {
                AbilityClosure c = t.ThisUnit.arsenal.Move;
                if (c != null)
                    c.ability.Aims[0].range.max += (sbyte)t.Modifier;
            };
            return t;
        }

        public static Timer Bombing(IEffect source, Unit parent)
        {
            Timer t = new Timer(source, parent);
            t.Name = "Targeted";
            t.Desc = Scribe.Write("{0} explosive damage at end of {1}'s turn.", t.Modifier, t.ThisToken);
            t.Turns = 1;
            t.Modifier = 10;
            t.Test = t.ParentTurnEndTest;
            t.Activate = () =>
            {
                Queue.Add(Sequence.Explosion(t, new EffectArgs(
                    Arg.Target(FT.Location, t.ThisToken.Cell),
                    Arg.Num(FN.Damage, t.Modifier),
                    Arg.Option(FO.ExcludeSelf, false))));
                t.Turns++;
            };
            return t;
        }

        public static Timer Corrosion(IEffect source, Unit parent, sbyte damage)
        {
            Timer t = new Timer(source, parent, damage);
            t.Name = "Corrosion";
            t.Desc = Scribe.Write("{0} damage at end of {1}'s turn. (50% next turn.)", t.Modifier, t.ThisToken); 
            t.Turns = 1;
            t.Test = t.ParentTurnEndTest;
            t.Activate = () =>
            {
                Queue.Add(Effect.CorrodeResidual(t, new EffectArgs(
                    Arg.Target(FT.Damaged, t.ThisUnit), 
                    Arg.Num(FN.Damage, t.Modifier))));
                t.Modifier = (sbyte)Math.Floor(t.Modifier * 0.5f);
                if (t.Modifier > 0) 
                    t.Turns++;
            };
            return t;

        }

        public static Timer Cursed(IEffect source, Unit parent)
        {
            Timer t = new Timer(source, parent);
            t.Name = "Cursed";
            t.Desc = Scribe.Write("{0} damage at end of {1}'s turn.", t.Modifier, t.ThisToken);
            t.Turns = 1;
            t.Modifier = 2;
            t.Test = t.ParentTurnEndTest;
            t.Activate = () =>
            {
                Queue.Add(Effect.Damage(t, new EffectArgs(
                    Arg.Target(FT.Damaged, t.ThisUnit), 
                    Arg.Num(FN.Damage, t.Modifier))));
                t.Turns++;
            };
            return t;
        }

        public static Timer Exhaust(IEffect source, Unit parent)
        {
            Timer t = new Timer(source, parent);
            t.Name = "Exhausted";
            t.Desc = Scribe.Write("{0} damage at end of {1}'s turn.", t.Modifier, t.ThisToken); 
            t.Turns = 1;
            t.Modifier = 5;
            t.Test = t.ParentTurnEndTest;
            t.Activate = () =>
            {
                Queue.Add(Effect.Damage(t, new EffectArgs(
                    Arg.Target(FT.Damaged, t.ThisUnit), 
                    Arg.Num(FN.Damage, t.Modifier))));
                t.Turns++;
            };
            return t;
        }

        public static Timer IceBlast(IEffect source, Unit parent)
        {
            Timer t = new Timer(source, parent);
            t.Name = "Ice Blasted";
            t.Desc = Scribe.Write("Initiative {0}. ({1} of {2}'s turns left.)",
                t.Modifier, t.Turns, t.ThisToken);
            t.Turns = 2;
            t.Modifier = (-2);
            t.Test = t.ParentTurnEndTest;
            t.Activate = () => 
            {
                Queue.Add(Effect.AddStat(t, new EffectArgs(
                    Arg.Target(FT.Damaged, t.ThisUnit), 
                    Arg.Num(FN.Damage, (sbyte)(0 - t.Modifier)),
                    Arg.Text(FX.Stat, "Initiative"))));
            };
            return t;
        }

        public static Timer Incineration(IEffect source, Unit parent)
        {
            Timer t = new Timer(source, parent);
            t.Name = "Incinerating";
            t.Desc = Scribe.Write("{0} damage at the end of {1}'s turn.", t.Modifier, t.ThisToken);
            t.Turns = 1;
            t.Modifier = 7;
            t.Test = t.ParentTurnEndTest;
            t.Activate = () =>
            {
                Queue.Add(Effect.FireInitial(t, new EffectArgs(
                    Arg.Target(FT.Damaged, t.ThisUnit), 
                    Arg.Num(FN.Damage, t.Modifier))));
                t.Turns++;
            };
            return t;
        }

        public static Timer Petrified(IEffect source, Unit parent, AbilityClosure ability)
        {
            Timer t = new Timer(source, parent, ability);
            t.Name = "Petrified";
            t.Desc = Scribe.Write("{0} cannot move. ({1} of {0}'s turns left.)", t.ThisToken, t.Turns);
            t.Turns = 1;
            t.Test = t.ParentTurnEndTest;
            t.Activate = () => 
            { 
                Queue.Add(Effect.Learn(t, new EffectArgs(
                    Arg.Target(FT.Damaged, t.ThisUnit), 
                    t.Ability))); 
            };
            return t;
        }

        public static Timer Stunned(IEffect source, Unit parent, sbyte turns)
        {
            Timer t = new Timer(source, parent, turns);
            t.Name = "Stunned";
            t.Desc = Scribe.Write("{0} cannot move forward in the Queue for the next {1} turn changes.",
                    t.ThisToken, t.Turns);
            t.Turns = turns;
            t.Test = t.EveryTurnTest;
            t.Activate = () => 
            { 
                Queue.Add(Effect.Shift(t, new EffectArgs(
                    Arg.Target(FT.Damaged, t.ThisUnit), 
                    Arg.Num(FN.Damage, 1)))); 
            };
            return t;
        }

        public static Timer TimeBomb(IEffect source, Unit parent, sbyte modifier)
        {
            Timer t = new Timer(source, parent, modifier);
            t.Name = "Time Bombed";
            t.Desc = Scribe.Write("Initiative {0}. ({1} of {2}'s turns left.)",
                    t.Modifier, t.Turns, t.ThisToken);
            t.Turns = 2;
            t.Test = t.ParentTurnEndTest;
            t.Activate = () => 
            {
                Queue.Add(Effect.AddStat(t,
                    new EffectArgs(
                        Arg.Target(FT.Damaged, t.ThisUnit), 
                        Arg.Num(FN.Damage, t.Modifier),
                        Arg.Text(FX.Stat, "Initiative"))));
            };
            return t;
        }

        public static Timer TimeSlam(IEffect source, Unit parent)
        {
            Timer t = new Timer(source, parent);
            t.Name = "Time Slammed";
            t.Desc = Scribe.Write("Initiative {0}. ({1} of {2}'s turns left.)",
                    t.Modifier, t.Turns, t.ThisToken);
            t.Turns = 2;
            t.Modifier = (-2);
            t.Test = t.ParentTurnEndTest;
            t.Activate = () => 
            {
                Queue.Add(Effect.AddStat(t,
                     new EffectArgs(
                         Arg.Target(FT.Damaged, t.ThisUnit), 
                         Arg.Num(FN.Damage, t.Modifier),
                         Arg.Text(FX.Stat, "Initiative"))));
            };
            return t;

        }

        public static Timer WaterLogged(IEffect source, Unit parent)
        {
            Timer t = new Timer(source, parent);
            t.Name = "Waterlogged";
            t.Desc = Scribe.Write("{0} damage at the end of {1}'s turn.", t.Modifier, t.ThisToken);
            t.Turns = 1;
            t.Modifier = 5;
            t.Test = t.ParentTurnEndTest;
            t.Activate = () =>
            {
                Queue.Add(Effect.WaterLog(t, new EffectArgs(
                    Arg.Target(FT.Damaged, t.ThisUnit), 
                    Arg.Num(FN.Damage, t.Modifier))));
                t.Turns++;
            };
            return t;
        }

	}
}
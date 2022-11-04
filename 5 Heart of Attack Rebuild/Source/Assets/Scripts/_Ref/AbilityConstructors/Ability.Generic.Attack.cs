using HOA.Tokens;
using System;
using HOA.Ef;
using HOA.Fargo;

namespace HOA.Abilities
{

    public partial class Ability
    {
        /// <summary>Arguments: price, damage</summary>
        public static Ability AttackNeighbor()
        {
            Ability a = new Ability("Strike", AbilityRank.Attack);
            //a.desc = Scribe.Write("Do {0} damage to target unit.", a.value);
            a.Aims = AimPlan.Melee(a, Filter.Unit);
            a.MainEffects = (arg, tar) =>
                Queue.Add(Effect.Damage(a, new EffectArgs(
                    Arg.Target(FT.Damaged, tar[0, 0]), 
                    Arg.Num(FN.Damage, arg[FS.Damage]))));
            return a;
        }
        
        /// <summary>Arguments: price, range, damage</summary>
        public static Ability AttackLine()
        {
            Ability a = new Ability("Shoot", AbilityRank.Attack);
            //a.desc = Scribe.Write("Do {0} damage to target unit.", a.value);
            a.Aims += AimStage.AttackLine(a.Aims, Filter.Unit, Range.sb(0,1));
            a.MainEffects = (arg, tar) =>
                Queue.Add(Effect.Damage(a, new EffectArgs(
                    Arg.Target(FT.Damaged, tar[0, 0]),
                    Arg.Num(FN.Damage, arg[FS.Damage]))));
            return a;
        }

        /// <summary>Arguments: price, range, damage</summary>
        public static Ability AttackArc()
        {
            Ability a = new Ability("Lob", AbilityRank.Attack);
            //a.desc = Scribe.Write("Do {0} damage to target unit.", a.value);
            a.Aims += AimStage.AttackArc(a.Aims, Filter.Unit, Range.sb(0, 1));
            a.MainEffects = (arg, tar) =>
                Queue.Add(Effect.Damage(a, new EffectArgs(
                    Arg.Target(FT.Damaged, tar[0, 0]),
                    Arg.Num(FN.Damage, arg[FS.Damage]))));
            return a;
        }

        /// <summary>Arguments: price, damage</summary>
        public static Ability LeechNeighbor()
        {
            Ability a = new Ability("Leech", AbilityRank.Attack);
            //a.desc = Scribe.Write("Do {0} damage to target unit." +
            //    "\n{1} gains health equal to the damage successfully dealt.", a.value, a.sourceToken);
            a.Aims = AimPlan.Melee(a, Filter.Unit);
            a.MainEffects = (arg, tar) =>
                Queue.Add(Effect.Leech(a, new EffectArgs(
                    Arg.Target(FT.Damaged, tar[0, 0]),
                    Arg.Num(FN.Damage, arg[FS.Damage]))));
            return a;
        }
        
        /// <summary>Arguments: price, range, damage</summary>
        public static Ability LeechArc()
        {
            Ability a = new Ability("Inhale", AbilityRank.Attack);
            //a.desc = Scribe.Write("Do {0} damage to target unit." +
            //    "\n{1} gains health equal to the damage successfully dealt.", a.value, a.sourceToken);
            a.Aims += AimStage.AttackArc(a.Aims, Filter.Unit, Range.sb(0, 1));
            a.MainEffects = (arg, tar) =>
                Queue.Add(Effect.Leech(a, new EffectArgs(
                    Arg.Target(FT.Damaged, tar[0, 0]),
                    Arg.Num(FN.Damage, arg[FS.Damage]))));
            return a;
        }

        /// <summary>Arguments: price, damage</summary>
        public static Ability DonateNeighbor()
        {
            Ability a = new Ability("Donate", AbilityRank.Attack);
            //a.desc = Scribe.Write("Do {0} damage to {1}." +
            //    "\nTarget unit gains health equal to the damage successfully dealt.", a.value, a.sourceToken);
            a.Aims = AimPlan.Melee(a, Filter.Unit);
            a.MainEffects = (arg, tar) =>
                Queue.Add(Effect.Donate(a, new EffectArgs(
                    Arg.Target(FT.Unit, tar[0, 0]),
                    Arg.Num(FN.Amount, arg[FS.Amount]))));
            return a;
        }

        /// <summary>Arguments: price, damage</summary>
        public static Ability RageNeighbor()
        {
            Ability a = new Ability("Rage", AbilityRank.Attack);
            //a.desc = Scribe.Write("Do {0} damage to target unit and" +
            //    "\n{1} damage to {2}.", a.value, Math.Ceil(a.value/2), a.sourceToken);
            a.Aims = AimPlan.Melee(a, Filter.Unit);
            a.MainEffects = (arg, tar) =>
                Queue.Add(Effect.Rage(a, new EffectArgs(
                    Arg.Target(FT.Damaged, tar[0, 0]),
                    Arg.Num(FN.Damage, arg[FS.Damage]))));
            return a;
        }

        /// <summary>Arguments: price, damage, filter</summary>
        public static Ability HealNeighbor()
        {
            Ability a = new Ability("Heal", AbilityRank.Special);
            a.Aims = AimPlan.Melee(a, Filter.Unit);
           // a.desc = Scribe.Write("Heal target {0} up to {1} health.", a.Aims[0].filter, a.value);
            a.MainEffects = (arg, tar) =>
                Queue.Add(Effect.AddStat(a, new EffectArgs(
                    Arg.Target(FT.Unit, tar[0, 0]),
                    Arg.Num(FN.Amount, arg[FS.Amount]),
                    Arg.Text(FX.Stat, "Health"))));
            return a;
        }
        /// <summary>Arguments: price, range, damage, filter</summary>
        public static Ability HealArc()
        {
            Ability a = new Ability("Restore", AbilityRank.Special);
            a.Aims += AimStage.AttackArc(a.Aims, Filter.Unit, Range.sb(0,1));
            //a.desc = Scribe.Write("Heal target {0} up to {1} health.", a.Aims[0].filter, a.value);
            a.MainEffects = (arg, tar) =>
                Queue.Add(Effect.AddStat(a, new EffectArgs(
                    Arg.Target(FT.Unit, tar[0, 0]),
                    Arg.Num(FN.Amount, arg[FS.Amount]),
                    Arg.Text(FX.Stat, "Health"))));
            return a;
        }

        /// <summary>Arguments: price</summary>
        public static Ability EndTurn()
        {
            Ability a = new Ability("End turn", AbilityRank.None);
           // a.desc = Scribe.Write("End current turn.");
            a.Aims = AimPlan.Self(a);
            a.MainEffects = (arg, tar) =>
                Queue.Add(Effect.Advance(a, new EffectArgs()));
            return a;
        }

        /// <summary>Arguments: price, damage
        public static Ability Focus()
        {
            Ability a = new Ability("Focus", AbilityRank.Focus);
            //a.desc = Scribe.Write("Focus +{0}.", a.value);
            a.Aims = AimPlan.Self(a);
            a.MainEffects = (arg, tar) =>
                Queue.Add(Effect.AddStat(a, new EffectArgs(
                    Arg.Target(FT.User, tar[0, 0]), 
                    Arg.Num(FN.Amount, arg[FS.Amount]),
                    Arg.Text(FX.Stat, "Focus"))));
            return a;
        }
    }
}
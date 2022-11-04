using HOA.To;
using System;

namespace HOA.Ab
{
    


    public partial class Ability
    {
        /// <summary>Arguments: price, damage</summary>
        public static Ability AttackNeighbor()
        {
            Ability a = new Ability("Strike", Rank.Attack);
            //a.desc = Scribe.Write("Do {0} damage to target unit.", a.value);
            a.Aims = AimPlan.Melee(a, Filter.Unit);
            a.MainEffects = (arg, tar) =>
            {
                IEntity unit = tar[0, 0];
                EffectQueue.Add(Effect.Damage(a, new EffectArgs(unit, arg.damage)));
            };
            return a;
        }
        
        /// <summary>Arguments: price, range, damage</summary>
        public static Ability AttackLine()
        {
            Ability a = new Ability("Shoot", Rank.Attack);
            //a.desc = Scribe.Write("Do {0} damage to target unit.", a.value);
            a.Aims += AimStage.AttackLine(a.Aims, Filter.Unit, Range.b(0,1));
            a.Update = Adjustments.Range0;
            a.MainEffects = (arg, tar) =>
            {
                IEntity unit = tar[0, 0];
                EffectQueue.Add(Effect.Damage(a, new EffectArgs(unit, arg.damage)));
            };
            return a;
        }

        /// <summary>Arguments: price, range, damage</summary>
        public static Ability AttackArc()
        {
            Ability a = new Ability("Lob", Rank.Attack);
            //a.desc = Scribe.Write("Do {0} damage to target unit.", a.value);
            a.Aims += AimStage.AttackArc(a.Aims, Filter.Unit, Range.b(0,1));
            a.Update = Adjustments.Range0;
            a.MainEffects = (arg, tar) =>
            {
                IEntity unit = tar[0, 0];
                EffectQueue.Add(Effect.Damage(a, new EffectArgs(unit, arg.damage)));
            };
            return a;
        }

        /// <summary>Arguments: price, damage</summary>
        public static Ability LeechNeighbor()
        {
            Ability a = new Ability("Leech", Rank.Attack);
            //a.desc = Scribe.Write("Do {0} damage to target unit." +
            //    "\n{1} gains health equal to the damage successfully dealt.", a.value, a.sourceToken);
            a.Aims = AimPlan.Melee(a, Filter.Unit);
            a.MainEffects = (arg, tar) =>
            {
                IEntity unit = tar[0, 0];
                EffectQueue.Add(Effect.Leech(a, new EffectArgs(unit, arg.damage)));
            };
            return a;
        }
        
        /// <summary>Arguments: price, range, damage</summary>
        public static Ability LeechArc()
        {
            Ability a = new Ability("Inhale", Rank.Attack);
            //a.desc = Scribe.Write("Do {0} damage to target unit." +
            //    "\n{1} gains health equal to the damage successfully dealt.", a.value, a.sourceToken);
            a.Aims += AimStage.AttackArc(a.Aims, Filter.Unit, Range.b(0,1));
            a.Update = Adjustments.Range0;
            a.MainEffects = (arg, tar) =>
            {
                IEntity unit = tar[0, 0];
                EffectQueue.Add(Effect.Leech(a, new EffectArgs(unit, arg.damage)));
            };
            return a;
        }

        /// <summary>Arguments: price, damage</summary>
        public static Ability DonateNeighbor()
        {
            Ability a = new Ability("Donate", Rank.Attack);
            //a.desc = Scribe.Write("Do {0} damage to {1}." +
            //    "\nTarget unit gains health equal to the damage successfully dealt.", a.value, a.sourceToken);
            a.Aims = AimPlan.Melee(a, Filter.Unit);
            a.MainEffects = (arg, tar) =>
            {
                IEntity unit = tar[0, 0];
                EffectQueue.Add(Effect.Donate(a, new EffectArgs(unit, arg.damage)));
            };
            return a;
        }

        /// <summary>Arguments: price, damage</summary>
        public static Ability RageNeighbor()
        {
            Ability a = new Ability("Rage", Rank.Attack);
            //a.desc = Scribe.Write("Do {0} damage to target unit and" +
            //    "\n{1} damage to {2}.", a.value, Math.Ceil(a.value/2), a.sourceToken);
            a.Aims = AimPlan.Melee(a, Filter.Unit);
            a.MainEffects = (arg, tar) =>
            {
                IEntity unit = tar[0, 0];
                EffectQueue.Add(Effect.Rage(a, new EffectArgs(unit, arg.damage)));
            };
            return a;
        }

        /// <summary>Arguments: price, damage, filter</summary>
        public static Ability HealNeighbor()
        {
            Ability a = new Ability("Heal", Rank.Special);
            a.Aims = AimPlan.Melee(a, Filter.Unit);
           // a.desc = Scribe.Write("Heal target {0} up to {1} health.", a.Aims[0].filter, a.value);
            a.Update = Adjustments.Filter0;
            a.MainEffects = (arg, tar) =>
            {
                IEntity unit = tar[0, 0];
                EffectQueue.Add(Effect.AddStat(a, new EffectArgs(unit, arg.damage, To.Stats.Health)));
            };
            return a;
        }
        /// <summary>Arguments: price, range, damage, filter</summary>
        public static Ability HealArc()
        {
            Ability a = new Ability("Restore", Rank.Special);
            a.Aims += AimStage.AttackArc(a.Aims, Filter.Unit, Range.b(0,1));
            //a.desc = Scribe.Write("Heal target {0} up to {1} health.", a.Aims[0].filter, a.value);
            a.Update = Adjustments.Range0 + Adjustments.Filter0;
            a.MainEffects = (arg, tar) =>
            {
                IEntity unit = tar[0, 0];
                EffectQueue.Add(Effect.AddStat(a, new EffectArgs(unit, arg.damage, To.Stats.Health)));
            };
            return a;
        }

        /// <summary>Arguments: price</summary>
        public static Ability EndTurn()
        {
            Ability a = new Ability("End turn", Rank.None);
           // a.desc = Scribe.Write("End current turn.");
            a.Aims = AimPlan.Self(a);
            a.MainEffects = (arg, tar) =>
                EffectQueue.Add(Effect.Advance(a, new EffectArgs()));
            return a;
        }

        /// <summary>Arguments: price, damage
        public static Ability Focus()
        {
            Ability a = new Ability("Focus", Rank.Focus);
            //a.desc = Scribe.Write("Focus +{0}.", a.value);
            a.Aims = AimPlan.Self(a);
            a.MainEffects = (arg, tar) =>
            {
                IEntity self = tar[0, 0];
                EffectQueue.Add(Effect.AddStat(a, new EffectArgs(self, arg.damage, To.Stats.Focus)));
            };
            return a;
        }
    }
}
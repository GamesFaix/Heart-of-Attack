using HOA.Tokens;
using System;

namespace HOA.Abilities
{

    public partial class Ability
    {
        public static Ability AttackNeighbor(Unit parent, string name, Rank rank, Price price, int damage)
        {
            Ability a = new Ability(parent, new AbilityArgs(name, rank, price, damage));
            a.desc = Scribe.Write("Do {0} damage to target unit.", a.value);
            a.Aims = AimPlan.Melee(a, Filter.Unit);
            a.MainEffects = t => 
                EffectQueue.Add(Effect.Damage(a, new EffectArgs(t[0][0] as Unit, a.value)));
            return a;
        }
        public static Ability Strike(Unit parent, int damage)
        { return AttackNeighbor(parent, "Strike", Rank.Attack, Price.Cheap, damage); }
        
        public static Ability AttackLine(Unit parent, string name, Rank rank, Price price, Range range, int damage)
        {
            Ability a = new Ability(parent, new AbilityArgs(name, rank, price, damage));
            a.desc = Scribe.Write("Do {0} damage to target unit.", a.value);
            a.Aims += AimStage.AttackLine(a.Aims, Filter.Unit, range);
            a.MainEffects = t =>
                EffectQueue.Add(Effect.Damage(a, new EffectArgs(t[0][0] as Unit, a.value)));
            return a;
        }
        public static Ability Shoot(Unit parent, int rangeMax, int damage)
        { return AttackLine(parent, "Shoot", Rank.Attack, Price.Cheap, new Range(0, (short)rangeMax), damage); }

        public static Ability AttackArc(Unit parent, string name, Rank rank, Price price, Range range, int damage)
        {
            Ability a = new Ability(parent, new AbilityArgs(name, rank, price, damage));
            a.desc = Scribe.Write("Do {0} damage to target unit.", a.value);
            a.Aims += AimStage.AttackArc(a.Aims, Filter.Unit, range);
            a.MainEffects = t =>
                EffectQueue.Add(Effect.Damage(a, new EffectArgs(t[0][0] as Unit, a.value)));
            return a;
        }

        public static Ability LeechNeighbor(Unit parent, int damage)
        {
            Ability a = new Ability(parent, new AbilityArgs("Leech", Rank.Attack, Price.Cheap, damage));
            a.desc = Scribe.Write("Do {0} damage to target unit." +
                "\n{1} gains health equal to the damage successfully dealt.", a.value, a.sourceToken);
            a.Aims = AimPlan.Melee(a, Filter.Unit);
            a.MainEffects = t =>
                EffectQueue.Add(Effect.Leech(a, new EffectArgs(t[0][0] as Unit, a.value)));
            return a;
        }
        public static Ability LeechArc(Unit parent, string name, Rank rank, Price price, Range range, int damage)
        {
            Ability a = new Ability(parent, new AbilityArgs(name, rank, price, damage));
            a.desc = Scribe.Write("Do {0} damage to target unit." +
                "\n{1} gains health equal to the damage successfully dealt.", a.value, a.sourceToken);
            a.Aims += AimStage.AttackArc(a.Aims, Filter.Unit, range);
            a.MainEffects = t =>
                EffectQueue.Add(Effect.Leech(a, new EffectArgs(t[0][0] as Unit, a.value)));
            return a;
        }

        public static Ability DonateNeighbor(Unit parent, Price price, int damage)
        {
            Ability a = new Ability(parent, new AbilityArgs("Donate", Rank.Special, price, damage));
            a.desc = Scribe.Write("Do {0} damage to {1}." +
                "\nTarget unit gains health equal to the damage successfully dealt.", a.value, a.sourceToken);
            a.Aims = AimPlan.Melee(a, Filter.Unit);
            a.MainEffects = t =>
                EffectQueue.Add(Effect.Donate(a, new EffectArgs(t[0][0] as Unit, a.value)));
            return a;
        }

        public static Ability RageNeighbor(Unit parent, int damage)
        {
            Ability a = new Ability(parent, new AbilityArgs("Rage", Rank.Attack, Price.Cheap, damage));
            a.desc = Scribe.Write("Do {0} damage to target unit and" +
                "\n{1} damage to {2}.", a.value, Math.Ceil(a.value/2), a.sourceToken);
            a.Aims = AimPlan.Melee(a, Filter.Unit);
            a.MainEffects = t =>
                EffectQueue.Add(Effect.Rage(a, new EffectArgs(t[0][0] as Unit, a.value)));
            return a;
        }

        public static Ability HealNeighbor(Unit parent, string name, Price price, Predicate<IEntity> filter, int hp)
        {
            Ability a = new Ability(parent, new AbilityArgs(name, Rank.Special, price, hp));
            a.Aims = AimPlan.Melee(a, filter);
            a.desc = Scribe.Write("Heal target {0} up to {1} health.", a.Aims[0].filter, a.value);
            a.MainEffects = t =>
                EffectQueue.Add(Effect.AddStat(a, new EffectArgs(t[0][0] as Unit, a.value, Tokens.Stats.Health)));
            return a;
        }
        public static Ability HealArc(Unit parent, string name, Price price, Predicate<IEntity> filter, Range range, int hp)
        {
            Ability a = new Ability(parent, new AbilityArgs(name, Rank.Special, price, hp));
            a.Aims += AimStage.AttackArc(a.Aims, filter, range);
            a.desc = Scribe.Write("Heal target {0} up to {1} health.", a.Aims[0].filter, a.value);
            a.MainEffects = t =>
                EffectQueue.Add(Effect.AddStat(a, new EffectArgs(t[0][0] as Unit, a.value, Tokens.Stats.Health)));
            return a;
        }

        public static Ability End(Unit parent)
        {
            Ability a = new Ability(parent, new AbilityArgs("End turn", Rank.None, Price.Free));
            a.desc = Scribe.Write("End current turn.");
            a.Aims = AimPlan.Self(a);
            a.MainEffects = t =>
                EffectQueue.Add(Effect.Advance(a, new EffectArgs()));
            return a;
        }
        public static Ability Focus(Unit parent)
        {
            Ability a = new Ability(parent, new AbilityArgs("Focus", Rank.Focus, Price.Cheap, 1));
            a.desc = Scribe.Write("Focus +{0}.", a.value);
            a.Aims = AimPlan.Self(a);
            a.MainEffects = t =>
                EffectQueue.Add(Effect.AddStat(a, new EffectArgs(a.sourceUnit, a.value, Tokens.Stats.Focus)));
            return a;
        }
    }
}
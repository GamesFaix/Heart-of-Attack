using HOA.Tokens;
using System;

namespace HOA.Abilities
{

    public partial class Ability
    {
        public static Ability Create(Unit parent, Price price, Species species, EffectConstructor birthEffects = null)
        {
            Ability a = new Ability(parent, new AbilityArgs("Create " + species, Rank.Create, price, species));
            a.desc = Scribe.Write("Create {0} in target cell.", a.args.species);
            a.Aims += AimStage.CreateNeighbor(a.Aims, a.args.species);
            a.MainEffects = t =>
                EffectQueue.Add(Effect.Create(a, new EffectArgs((Cell)t[new index2(0,0)], species, birthEffects)));
            return a;
        }

        public static Ability CreateDrop(Unit parent, Price price, Species species, EffectConstructor birthEffects = null)
        {
            Ability a = new Ability(parent, new AbilityArgs("Drop " + species, Rank.Create, price, species));
            a.desc = Scribe.Write("Create {0} in {1}'s cell.", a.args.species, a.sourceToken);
            a.Aims += AimStage.CreateDrop(a.Aims, a.args.species);
            a.MainEffects = t =>
            {
                if (t.Count > 0)
                    EffectQueue.Add(Effect.Create(a, new EffectArgs(t[0][0], species, birthEffects)));
            };
            return a;
        }
          
        public static Ability CreateArc(Unit parent, Price price, Species species, Range range, EffectConstructor birthEffects = null)
        {
            Ability a = new Ability(parent, new AbilityArgs("Arc Create " + species, Rank.Create, price, species));
            a.desc = Scribe.Write("Create {0} in target cell.", a.args.species);
            a.Aims += AimStage.CreateArc(a.Aims, a.args.species, range);
            a.MainEffects = t =>
                EffectQueue.Add(Effect.Create(a, new EffectArgs((Cell)t[new index2(0, 0)], species, birthEffects)));
            return a;
        }

        public static Ability CreateFree(Unit parent, Price price, Species species, Range range, EffectConstructor birthEffects = null)
        {
            Ability a = new Ability(parent, new AbilityArgs("Free Create " + species, Rank.Create, price, species));
            a.desc = Scribe.Write("Create {0} in any legal cell.", a.args.species);
            a.Aims += AimStage.CreateFree(a.Aims, a.args.species);
            a.MainEffects = t =>
                EffectQueue.Add(Effect.Create(a, new EffectArgs((Cell)t[new index2(0, 0)], species, birthEffects)));
            return a;
        }

        public static Ability CreateMulti(Unit parent, Price price, Species species, Range count)
        {
            string name = String.Format("Create {0}s {1}", species, count);
            Ability a = new Ability(parent, new AbilityArgs(name, Rank.Create, price, species));
            a.desc = Scribe.Write("Create {0}s in {1} target cells.", a.args.species, a.Aims[0].selectionCount);
            a.Aims += AimStage.CreateNeighborMulti(a.Aims, a.args.species, count);
            a.MainEffects = t =>
            {
                EffectSet e = new EffectSet();
                foreach (Cell c in t[0])
                    e.Add(Effect.Create(a, new EffectArgs(c, species)));
                EffectQueue.Add(e);
            };
            return a;
        }

        public static Ability TransformOther(Unit parent, Description name, Abilities.Rank rank, Price price, Species species, Predicate<IEntity> filter)
        {
            Ability a = new Ability(parent, new AbilityArgs(name(), rank, price, species));
            a.desc = Scribe.Write("Replace {0} with {1}.", filter, a.args.species);
            a.Aims += new AimStage(a.Aims, AimPatterns.Neighbor, filter);
            a.MainEffects = t =>
                EffectQueue.Add(Effect.Replace(a, new EffectArgs(t[0][0], a.args.species)));
            return a;
        }

        public static Ability TransformSelf(Unit parent, Description name, Price price, Species species, EffectConstructor birthEffects = null)
        {
            Ability a = new Ability(parent, new AbilityArgs(name(), Rank.Special, price, species));
            a.desc = Scribe.Write("Replace {0} with {1}.", a.sourceToken, a.args.species);
            a.Aims += AimStage.Self(a.Aims);
            a.MainEffects = t =>
                EffectQueue.Add(Effect.Replace(a, new EffectArgs(t[0][0], a.args.species, birthEffects)));
            return a;
        }
        
    }
}
using HOA.Tokens;
using System;

namespace HOA.Abilities
{

    public partial class Ability
    {
        public static Ability Create(Unit parent, Price price, Species species, EffectConstructor birthEffects = null)
        {
            Ability a = new Ability(parent, "Create " + species, Rank.Create, price, new AbilityArgs(species));
            a.Desc = Scribe.Write("Create {0} in target cell.", a.Args.species);
            a.Aims += AimStage.CreateNeighbor(a.Aims, a.Args.species);
            a.MainEffects = Targets =>
                EffectQueue.Add(Effect.Create(a, new EffectArgs((Cell)Targets[new index2(0,0)], species, birthEffects)));
            return a;
        }

        public static Ability CreateDrop(Unit parent, Price price, Species species, EffectConstructor birthEffects = null)
        {
            Ability a = new Ability(parent, "Drop " + species, Rank.Create, price, new AbilityArgs(species));
            a.Desc = Scribe.Write("Create {0} in {1}'s cell.", a.Args.species, a.sourceToken);
            a.Aims += AimStage.CreateDrop(a.Aims, a.Args.species);
            a.MainEffects = Targets =>
            {
                if (Targets.Count > 0)
                    EffectQueue.Add(Effect.Create(a, new EffectArgs(Targets[0][0], species, birthEffects)));
            };
            return a;
        }
          
        public static Ability CreateArc(Unit parent, Price price, Species species, Range range, EffectConstructor birthEffects = null)
        {
            Ability a = new Ability(parent, "Arc Create " + species, Rank.Create, price, new AbilityArgs(species));
            a.Desc = Scribe.Write("Create {0} in target cell.", a.Args.species);
            a.Aims += AimStage.CreateArc(a.Aims, a.Args.species, range);
            a.MainEffects = Targets =>
                EffectQueue.Add(Effect.Create(a, new EffectArgs((Cell)Targets[new index2(0, 0)], species, birthEffects)));
            return a;
        }

        public static Ability CreateFree(Unit parent, Price price, Species species, Range range, EffectConstructor birthEffects = null)
        {
            Ability a = new Ability(parent, "Free Create " + species, Rank.Create, price, new AbilityArgs(species));
            a.Desc = Scribe.Write("Create {0} in any legal cell.", a.Args.species);
            a.Aims += AimStage.CreateFree(a.Aims, a.Args.species);
            a.MainEffects = Targets =>
                EffectQueue.Add(Effect.Create(a, new EffectArgs((Cell)Targets[new index2(0, 0)], species, birthEffects)));
            return a;
        }

        public static Ability CreateMulti(Unit parent, Price price, Species species, Range count)
        {
            string name = String.Format("Create {0}s {1}", species, count);
            Ability a = new Ability(parent, name, Rank.Create, price, new AbilityArgs(species));
            a.Desc = Scribe.Write("Create {0}s in {1} target cells.", a.Args.species, a.Aims[0].selectionCount);
            a.Aims += AimStage.CreateNeighborMulti(a.Aims, a.Args.species, count);
            a.MainEffects = Targets =>
            {
                EffectSet e = new EffectSet();
                foreach (Cell c in Targets[0])
                    e.Add(Effect.Create(a, new EffectArgs(c, species)));
                EffectQueue.Add(e);
            };
            return a;
        }

        public static Ability Transmute(Unit parent, Description name, Price price, Species species, Predicate<IEntity> filter)
        {
            Ability a = new Ability(parent, name(), Rank.Special, price, new AbilityArgs(species));
            a.Desc = Scribe.Write("Replace {0} with {1}.", filter, species);
            a.Aims += new AimStage(a.Aims, AimPatterns.Neighbor, filter);
            a.MainEffects = Targets =>
                EffectQueue.Add(Effect.Replace(a,
                    new EffectArgs(Targets[0][0], a.Args.species)));
            return a;
        }

        public static Ability Evolve(Unit parent, Description name, Price price, Species species, EffectConstructor birthEffects = null)
        {
            Ability a = new Ability(parent, name(), Rank.Special, price, new AbilityArgs(species));
            a.Desc = Scribe.Write("Replace {0} with {1}.", parent, species);
            a.Aims += AimStage.Self(a.Aims);
            a.MainEffects = Targets =>
                EffectQueue.Add(Effect.Replace(a,
                    new EffectArgs(Targets[0][0], a.Args.species, birthEffects)));
            return a;
        }
        
    }
}
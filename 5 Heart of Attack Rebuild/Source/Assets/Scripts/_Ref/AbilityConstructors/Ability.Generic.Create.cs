using HOA.To;
using System;
using HOA.Ab.Aim;
using HOA.Ef;

namespace HOA.Ab
{

    public partial class Ability
    {
        /// <summary>Arguments: price, species, effectConstructor birtheffects </summary>
        public static Ability Create()
        {
            Ability a = new Ability("Create", Rank.Create);
           // a.desc = Scribe.Write("Create {0} in target cell.", a.args.species);
            a.Aims += Stage.CreateNeighbor(a.Aims, Species.None);
            a.Update += Adjustments.Body0 + Adjustments.SpeciesName(a.name);
            a.MainEffects = (arg, tar) =>
            {
                IEntity cell = tar[0, 0];
                Queue.Add(Effect.Create(a, new Ef.Args(cell, arg.species, arg.effect)));
            };
            return a;
        }

        /// <summary>Arguments: price, species, effectConstructor birtheffects </summary>
        public static Ability CreateDrop()
        {
            Ability a = new Ability("Drop", Rank.Create);
            //a.desc = Scribe.Write("Create {0} in {1}'s cell.", a.args.species, a.sourceToken);
            a.Aims += Stage.CreateDrop(a.Aims, Species.None);
            a.Update += Adjustments.Body0 + Adjustments.SpeciesName(a.name);
            a.MainEffects = (arg, tar) =>
            {
                if (tar.Count > 0)
                {
                    IEntity cell = tar[0, 0];
                    Queue.Add(Effect.Create(a, new Ef.Args(cell, arg.species, arg.effect)));
                }
            };
            return a;
        }

        /// <summary>Arguments: price, int rangeMin, int rangeMax, species, effectConstructor birtheffects </summary>
        public static Ability CreateArc()
        {
            Ability a = new Ability("Summon", Rank.Create);
            //a.desc = Scribe.Write("Create {0} in target cell.", a.args.species);
            a.Aims += Stage.CreateArc(a.Aims, Species.None, Range.sb(0,1));
            a.Update += Adjustments.Body0 + Adjustments.SpeciesName(a.name);
            a.MainEffects = (arg, tar) =>
            {
                IEntity cell = tar[0, 0];
                Queue.Add(Effect.Create(a, new Ef.Args(cell, arg.species, arg.effect)));
            };
            return a;
        }

        /// <summary>Arguments: price, species, effectConstructor birtheffects </summary>
        public static Ability CreateFree()
        {
            Ability a = new Ability("Conjure", Rank.Create);
            //a.desc = Scribe.Write("Create {0} in any legal cell.", a.args.species);
            a.Aims += Stage.CreateFree(a.Aims, Species.None);
            a.Update += Adjustments.Body0 + Adjustments.SpeciesName(a.name);
            a.MainEffects = (arg, tar) =>
            {
                IEntity cell = tar[0, 0];
                Queue.Add(Effect.Create(a, new Ef.Args(cell, arg.species, arg.effect)));
            };
            return a;
        }

        /// <summary>Arguments: price, species, int countMin, int CountMax, effectConstructor birtheffects </summary>
        public static Ability CreateMulti()
        {
            //string name = String.Format("Spawn {0}s {1}", species, count);
            Ability a = new Ability("Spawn", Rank.Create);
           // a.desc = Scribe.Write("Spawn {0}s in {1} target cells.", a.args.species, a.Aims[0].selectionCount);
            a.Aims += Stage.CreateNeighborMulti(a.Aims, Species.None, Range.sb(1,1));
            a.Update += Adjustments.Body0 + Adjustments.SpeciesName(a.name);
            a.MainEffects = (arg, tar) =>
            {
                Ef.Set e = new Ef.Set();
                foreach (Cell c in tar[0])
                    e.Add(Effect.Create(a, new Ef.Args(c, arg.species, arg.effect)));
                Queue.Add(e);
            };
            return a;
        }

        /// <summary>Arguments: price, species, filter, birthEffects </summary>
        public static Ability TransformNeighbor()
        {
            Ability a = new Ability("Transmute", Rank.Create);
            //a.desc = Scribe.Write("Replace {0} with {1}.", filter, a.args.species);
            a.Aims += new Stage(a.Aims, Patterns.Neighbor, Filter.Token);
            a.Update += Adjustments.Filter0;
            a.MainEffects = (arg, tar) =>
            {
                IEntity token = tar[0, 0];
                Queue.Add(Effect.Replace(a, new Ef.Args(token, arg.species, arg.effect)));
            };
            return a;
        }

        /// <summary>Arguments: price, species, birthEffects </summary>
        public static Ability TransformSelf()
        {
            Ability a = new Ability("Evolve", Rank.Special);
            //a.desc = Scribe.Write("Replace {0} with {1}.", a.sourceToken, a.args.species);
            a.Aims += Stage.Self(a.Aims);
            a.MainEffects = (arg, tar) =>
            {
                IEntity self = tar[0, 0];
                Queue.Add(Effect.Replace(a, new Ef.Args(self, arg.species, arg.effect)));
            };
            return a;
        }
        
    }
}
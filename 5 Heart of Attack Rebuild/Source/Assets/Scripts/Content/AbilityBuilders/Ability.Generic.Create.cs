using HOA.Tokens;
using System;
using HOA.Effects;
using HOA.Args;
using Cell = HOA.Board.Cell;

namespace HOA.Abilities
{

    public partial class Ability
    {
        /// <summary>Arguments: price, species, effectConstructor birtheffects </summary>
        public static Ability Create()
        {
            var a = new Ability("Create", AbilityRank.Create);
           // a.desc = Scribe.Write("Create {0} in target cell.", a.args.species);
            a.Aims += AimStage.CreateNeighbor(a.Aims, Species.None);
            a.Update += Adjustments.Body0 + Adjustments.SpeciesName(a.name);
            a.MainEffects = (arg, tar) =>
                EffectQueue.Add(Effect.Create(a, new EffectArgs(
                    Arg.Target(RT.Destination, tar[0, 0]),
                    arg.species,
                    Arg.Effect(RE.Birth, arg[RE.Birth]))));
            return a;
        }

        /// <summary>Arguments: price, species, effectConstructor birtheffects </summary>
        public static Ability CreateDrop()
        {
            var a = new Ability("Drop", AbilityRank.Create);
            //a.desc = Scribe.Write("Create {0} in {1}'s cell.", a.args.species, a.sourceToken);
            a.Aims += AimStage.CreateDrop(a.Aims, Species.None);
            a.Update += Adjustments.Body0 + Adjustments.SpeciesName(a.name);
            a.MainEffects = (arg, tar) =>
            {
                if (tar.Count > 0)
                    EffectQueue.Add(Effect.Create(a, new EffectArgs(
                        Arg.Target(RT.Destination,  tar[0, 0]),
                        arg.species,
                        Arg.Effect(RE.Birth, arg[RE.Birth]))));
            };
            return a;
        }

        /// <summary>Arguments: price, int rangeMin, int rangeMax, species, effectConstructor birtheffects </summary>
        public static Ability CreateArc()
        {
            var a = new Ability("Summon", AbilityRank.Create);
            //a.desc = Scribe.Write("Create {0} in target cell.", a.args.species);
            a.Aims += AimStage.CreateArc(a.Aims, Species.None, Range.sb(0,1));
            a.Update += Adjustments.Body0 + Adjustments.SpeciesName(a.name);
            a.MainEffects = (arg, tar) =>
                 EffectQueue.Add(Effect.Create(a, new EffectArgs(
                    Arg.Target(RT.Destination, tar[0, 0]),
                    arg.species,
                    Arg.Effect(RE.Birth, arg[RE.Birth]))));
            return a;
        }

        /// <summary>Arguments: price, species, effectConstructor birtheffects </summary>
        public static Ability CreateFree()
        {
            var a = new Ability("Conjure", AbilityRank.Create);
            //a.desc = Scribe.Write("Create {0} in any legal cell.", a.args.species);
            a.Aims += AimStage.CreateFree(a.Aims, Species.None);
            a.Update += Adjustments.Body0 + Adjustments.SpeciesName(a.name);
            a.MainEffects = (arg, tar) =>
                EffectQueue.Add(Effect.Create(a, new EffectArgs(
                    Arg.Target(RT.Destination, tar[0, 0]),
                    arg.species,
                    Arg.Effect(RE.Birth, arg[RE.Birth]))));
            return a;
        }

        /// <summary>Arguments: price, species, int countMin, int CountMax, effectConstructor birtheffects </summary>
        public static Ability CreateMulti()
        {
            //string name = String.Format("Spawn {0}s {1}", species, count);
            var a = new Ability("Spawn", AbilityRank.Create);
           // a.desc = Scribe.Write("Spawn {0}s in {1} target cells.", a.args.species, a.Aims[0].selectionCount);
            a.Aims += AimStage.CreateNeighborMulti(a.Aims, Species.None, Range.sb(1,1));
            a.Update += Adjustments.Body0 + Adjustments.SpeciesName(a.name);
            a.MainEffects = (arg, tar) =>
            {
                var e = new EffectSet();
                foreach (Cell c in tar[0])
                    e.Add(Effect.Create(a, new EffectArgs(
                        Arg.Target(RT.Destination, c),
                        arg.species,
                        Arg.Effect(RE.Birth, arg[RE.Birth]))));
                EffectQueue.Add(e);
            };
            return a;
        }

        /// <summary>Arguments: price, species, filter, birthEffects </summary>
        public static Ability TransformNeighbor()
        {
            var a = new Ability("Transmute", AbilityRank.Create);
            //a.desc = Scribe.Write("Replace {0} with {1}.", filter, a.args.species);
            a.Aims += new AimStage(a.Aims, AimPatterns.Neighbor, Filter.Token);
            a.MainEffects = (arg, tar) =>
                EffectQueue.Add(Effect.Replace(a, new EffectArgs(
                    Arg.Target(RT.Token, tar[0, 0]), 
                    arg.species, 
                    Arg.Effect(RE.Birth, arg[RE.Birth]))));
            return a;
        }

        /// <summary>Arguments: price, species, birthEffects </summary>
        public static Ability TransformSelf()
        {
            var a = new Ability("Evolve", AbilityRank.Special);
            //a.desc = Scribe.Write("Replace {0} with {1}.", a.sourceToken, a.args.species);
            a.Aims += AimStage.Self(a.Aims);
            a.MainEffects = (arg, tar) =>
                EffectQueue.Add(Effect.Replace(a, new EffectArgs(
                    Arg.Target(RT.User, tar[0, 0]), 
                    arg.species, 
                    Arg.Effect(RE.Birth, arg[RE.Birth]))));
            return a;
        }
        
    }
}
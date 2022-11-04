using HOA.Tokens;
using System;
using HOA.Effects;
using HOA.Stats;
using HOA.Args;
using Cell = HOA.Board.Cell;
using Farg = HOA.Args.Arg;

namespace HOA.Abilities
{

    public partial class Ability
    {
        /// <summary>Args: damage, stat</summary>
        public static Ability ManualAdd()
        {
            Ability a = new Ability("#Add", AbilityRank.Special);
            //a.desc = Scribe.Write ("Increase/Descrease stat of up to 10 units.");
            a.Aims += AimStage.AttackFree(a.Aims, Filter.Unit);
            a.Aims[0].selectionCount = Range.sb(1,12);
            
            a.MainEffects = (arg, tar) =>
            {
                var e = new EffectSet();
                foreach (Unit u in tar[0])
                    e.Add(Effect.AddStat(null, new EffectArgs(
                        Arg.Target(RT.Unit,u), 
                        Farg.Num(RN.Amount, arg[RS.Amount]), 
                        Farg.Text(RL.Stat, arg[RL.Stat]))));
                EffectQueue.Add(e);
            };

            a.Usable = AbilityConditions.AlreadyProcessing;
            return a;
        }

        /// <summary>Args: species</summary>
        public static Ability ManualCreate()
        {
            var a = new Ability("#Create", AbilityRank.Create);
            //a.desc = Scribe.Write("Create {0} in upto 12 cells.", a.args.species);
            a.Aims += AimStage.CreateFreeManual(a.Aims, Species.None, Range.sb(1,12));
            a.Update = Adjustments.SpeciesName("#Create");
            a.MainEffects = (arg, tar) =>
            {
                var e = new EffectSet();
                foreach (Cell c in tar[0])
                    e.Add(Effect.Create(a, new EffectArgs(
                        Arg.Target(RT.Location, c), 
                        arg.species)));
                EffectQueue.Add(e);
            };

            a.Usable = AbilityConditions.AlreadyProcessing;
            return a;
        }

        /// <summary>Args: (none)</summary>
        public static Ability ManualDestroy()
        {
            var a = new Ability("#Destroy", AbilityRank.Special);
            //a.desc = Scribe.Write("Destroy up to 12 tokens.");
            a.Aims += AimStage.AttackFree(a.Aims, Filter.Token);
            a.Aims[0].selectionCount = Range.sb(1, 12);
            a.MainEffects = (arg, tar) =>
            {
                var e = new EffectSet();
                foreach (Token t in tar)
                {
                    if (t is Unit)
                        e.Add(Effect.DestroyUnit(a, new EffectArgs(
                            Arg.Target(RT.Unit, t))));
                    else
                        e.Add(Effect.DestroyObstacle(a, new EffectArgs(
                            Arg.Target(RT.Token, t))));
                }
                EffectQueue.Add(e);
            };

            a.Usable = AbilityConditions.AlreadyProcessing;
            return a;
        }

        /// <summary>Args: (none)</summary>
        public static Ability ManualEnd()
        {
            var a = new Ability("#End", AbilityRank.None);
//            a.desc = Scribe.Write("End current turn.");
            a.Aims = AimPlan.Self(a);
            a.MainEffects = (arg, tar) =>
                EffectQueue.Add(Effect.Advance(a, new EffectArgs()));
            a.Usable = AbilityConditions.AlreadyProcessing;
            return a;
        }

        /// <summary>Args: </summary>
        public static Ability ManualMove()
        {
            var a = new Ability("#Move", AbilityRank.Move);
//            a.desc = Scribe.Write("Move target token to target cell.");
            a.Aims += AimStage.AttackFree(a.Aims, Filter.Token);
            a.Aims += AimStage.MoveFree(a.Aims);
            a.MainEffects = (arg, tar) =>
            {
                EffectQueue.Add(Effect.TeleportStart(a, new EffectArgs(
                    Arg.Target(RT.Mover, tar[0, 0]), 
                    Arg.Target(RT.Destination, tar[1, 0]))));
            };
            a.Usable = AbilityConditions.AlreadyProcessing;
            return a;
        }

        /// <summary>Args: player</summary>
        public static Ability ManualOwner()
        {
            var a = new Ability("#Capture", AbilityRank.Special);
            ///a.desc = Scribe.Write("Set owner of up to 10 units.");
            a.Aims += AimStage.AttackFree(a.Aims, Filter.Token);
            a.Aims[0].selectionCount = Range.sb(1, 12);
            
            a.MainEffects = (arg, tar) =>
            {
                var e = new EffectSet();
                foreach (Token t in tar)
                    e.Add(Effect.SetOwner(a, new EffectArgs(
                        Arg.Target(RT.Token, t),
                        arg.player)));
                EffectQueue.Add(e);
            };

            a.Usable = AbilityConditions.AlreadyProcessing;
            return a;
        }
        /// <summary>Args: damage, stat</summary>
        public static Ability ManualSet()
        {
            var a = new Ability("#Set", AbilityRank.Special);
           // a.desc = Scribe.Write("Set stat of up to 12 units.");
            a.Aims += AimStage.AttackFree(a.Aims, Filter.Unit);
            a.Aims[0].selectionCount = Range.sb(1, 12);

            a.MainEffects = (arg, tar) =>
            {
                var e = new EffectSet();
                foreach (Unit u in tar)
                    e.Add(Effect.SetStat(a, new EffectArgs(
                        Arg.Target(RT.Unit, u),
                        Farg.Num(RN.Amount, arg[RS.Amount]), 
                        Farg.Text(RL.Stat, arg[RL.Stat]))));
                EffectQueue.Add(e);
            };

            a.Usable = AbilityConditions.AlreadyProcessing;
            return a;
        }

        /// <summary>Args: damage</summary>
        public static Ability ManualShift()
        {
            var a = new Ability("#Shift", AbilityRank.Special);
         //   a.desc = Scribe.Write("Move up to 10 units in the queue.");
            a.Aims += AimStage.AttackFree(a.Aims, Filter.Unit);
            a.Aims[0].selectionCount = Range.sb(1, 12);
            a.MainEffects = (arg, tar) =>
            {
                foreach (Unit u in tar)
                    EffectQueue.Add(Effect.Shift(a, new EffectArgs(
                        Arg.Target(RT.Unit, u), 
                        Farg.Num(RN.Amount, arg[RS.Amount]))));
            };

            a.Usable = AbilityConditions.AlreadyProcessing;
            return a;
        }
        


    }
}
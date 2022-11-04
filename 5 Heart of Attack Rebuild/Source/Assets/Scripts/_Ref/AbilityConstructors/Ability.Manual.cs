using HOA.To;
using System;
using HOA.Ab.Aim;
using HOA.Ef;
using HOA.Stats;
using HOA.Fargo;

namespace HOA.Ab
{

    public partial class Ability
    {
        /// <summary>Args: damage, stat</summary>
        public static Ability ManualAdd()
        {
            Ability a = new Ability("#Add", Rank.Special);
            //a.desc = Scribe.Write ("Increase/Descrease stat of up to 10 units.");
            a.Aims += Stage.AttackFree(a.Aims, Filter.Unit);
            a.Aims[0].selectionCount = Range.sb(1,12);
            
            a.MainEffects = (arg, tar) =>
            {
                Ef.Set e = new Ef.Set();
                foreach (Unit u in tar[0])
                    e.Add(Effect.AddStat(null, new EffectArgs(
                        Arg.Target(FT.Unit,u), 
                        Arg.Num(FN.Amount, arg[FS.Amount]), 
                        Arg.Text(FX.Stat, arg[FX.Stat]))));
                Queue.Add(e);
            };

            a.Usable = UseTests.AlreadyProcessing;
            return a;
        }

        /// <summary>Args: species</summary>
        public static Ability ManualCreate()
        {
            Ability a = new Ability("#Create", Rank.Create);
            //a.desc = Scribe.Write("Create {0} in upto 12 cells.", a.args.species);
            a.Aims += Stage.CreateFreeManual(a.Aims, Species.None, Range.sb(1,12));
            a.Update = Adjustments.SpeciesName("#Create");
            a.MainEffects = (arg, tar) =>
            {
                Ef.Set e = new Ef.Set();
                foreach (Cell c in tar[0])
                    e.Add(Effect.Create(a, new EffectArgs(
                        Arg.Target(FT.Location, c), 
                        arg.species)));
                Queue.Add(e);
            };
            
            a.Usable = UseTests.AlreadyProcessing;
            return a;
        }

        /// <summary>Args: (none)</summary>
        public static Ability ManualDestroy()
        {
            Ability a = new Ability("#Destroy", Rank.Special);
            //a.desc = Scribe.Write("Destroy up to 12 tokens.");
            a.Aims += Stage.AttackFree(a.Aims, Filter.Token);
            a.Aims[0].selectionCount = Range.sb(1, 12);
            a.MainEffects = (arg, tar) =>
            {
                Ef.Set e = new Ef.Set();
                foreach (Token t in tar)
                {
                    if (t is Unit)
                        e.Add(Effect.DestroyUnit(a, new EffectArgs(
                            Arg.Target(FT.Unit, t))));
                    else
                        e.Add(Effect.DestroyObstacle(a, new EffectArgs(
                            Arg.Target(FT.Token, t))));
                }
                Queue.Add(e);
            };

            a.Usable = UseTests.AlreadyProcessing;
            return a;
        }

        /// <summary>Args: (none)</summary>
        public static Ability ManualEnd()
        {
            Ability a = new Ability("#End", Rank.None);
//            a.desc = Scribe.Write("End current turn.");
            a.Aims = Plan.Self(a);
            a.MainEffects = (arg, tar) => 
                Queue.Add(Effect.Advance(a, new EffectArgs()));
            a.Usable = UseTests.AlreadyProcessing;
            return a;
        }

        /// <summary>Args: </summary>
        public static Ability ManualMove()
        {
            Ability a = new Ability("#Move", Rank.Move);
//            a.desc = Scribe.Write("Move target token to target cell.");
            a.Aims += Stage.AttackFree(a.Aims, Filter.Token);
            a.Aims += Stage.MoveFree(a.Aims);
            a.MainEffects = (arg, tar) =>
            {
                Queue.Add(Effect.TeleportStart(a, new EffectArgs(
                    Arg.Target(FT.Mover, tar[0, 0]), 
                    Arg.Target(FT.Destination, tar[1, 0]))));
            };
            a.Usable = UseTests.AlreadyProcessing;
            return a;
        }

        /// <summary>Args: player</summary>
        public static Ability ManualOwner()
        {
            Ability a = new Ability("#Capture", Rank.Special);
            ///a.desc = Scribe.Write("Set owner of up to 10 units.");
            a.Aims += Stage.AttackFree(a.Aims, Filter.Token);
            a.Aims[0].selectionCount = Range.sb(1, 12);
            
            a.MainEffects = (arg, tar) =>
            {
                Ef.Set e = new Ef.Set();
                foreach (Token t in tar)
                    e.Add(Effect.SetOwner(a, new EffectArgs(
                        Arg.Target(FT.Token, t),
                        arg.player)));
                Queue.Add(e);
            };

            a.Usable = UseTests.AlreadyProcessing;
            return a;
        }
        /// <summary>Args: damage, stat</summary>
        public static Ability ManualSet()
        {
            Ability a = new Ability("#Set", Rank.Special);
           // a.desc = Scribe.Write("Set stat of up to 12 units.");
            a.Aims += Stage.AttackFree(a.Aims, Filter.Unit);
            a.Aims[0].selectionCount = Range.sb(1, 12);

            a.MainEffects = (arg, tar) =>
            {
                Ef.Set e = new Ef.Set();
                foreach (Unit u in tar)
                    e.Add(Effect.SetStat(a, new EffectArgs(
                        Arg.Target(FT.Unit, u),
                        Arg.Num(FN.Amount, arg[FS.Amount]), 
                        Arg.Text(FX.Stat, arg[FX.Stat]))));
                Queue.Add(e);
            };

            a.Usable = UseTests.AlreadyProcessing;
            return a;
        }

        /// <summary>Args: damage</summary>
        public static Ability ManualShift()
        {
            Ability a = new Ability("#Shift", Rank.Special);
         //   a.desc = Scribe.Write("Move up to 10 units in the queue.");
            a.Aims += Stage.AttackFree(a.Aims, Filter.Unit);
            a.Aims[0].selectionCount = Range.sb(1, 12);
            a.MainEffects = (arg, tar) =>
            {
                foreach (Unit u in tar)
                    Queue.Add(Effect.Shift(a, new EffectArgs(
                        Arg.Target(FT.Unit, u), 
                        Arg.Num(FN.Amount, arg[FS.Amount]))));
            };

            a.Usable = UseTests.AlreadyProcessing;
            return a;
        }
        


    }
}
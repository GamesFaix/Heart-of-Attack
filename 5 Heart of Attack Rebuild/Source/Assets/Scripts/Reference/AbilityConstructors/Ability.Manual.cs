using HOA.To;
using System;

namespace HOA.Ab
{

    public partial class Ability
    {
        /// <summary>Args: damage, stat</summary>
        public static Ability ManualAdd()
        {
            Ability a = new Ability("#Add", Rank.Special);
            //a.desc = Scribe.Write ("Increase/Descrease stat of up to 10 units.");
            a.Aims += AimStage.AttackFree(a.Aims, Filter.Unit);
            a.Aims[0].selectionCount = Range.b(1,12);
            
            a.MainEffects = (arg, tar) =>
            {
                EffectSet e = new EffectSet();
                foreach (Unit u in tar[0])
                    e.Add(Effect.AddStat(null, new EffectArgs(u, arg.damage, arg.stat)));
                EffectQueue.Add(e);
            };

            a.Usable = UsabilityTests.AlreadyProcessing;
            return a;
        }

        /// <summary>Args: species</summary>
        public static Ability ManualCreate()
        {
            Ability a = new Ability("#Create", Rank.Create);
            //a.desc = Scribe.Write("Create {0} in upto 12 cells.", a.args.species);
            a.Aims += AimStage.CreateFreeManual(a.Aims, Species.None, Range.b(1,12));
            a.Update = Adjustments.SpeciesName("#Create");
            a.MainEffects = (arg, tar) =>
            {
                EffectSet e = new EffectSet();
                foreach (Cell c in tar[0])
                    e.Add(Effect.Create(a, new EffectArgs(c, arg.species)));
                EffectQueue.Add(e);
            };
            
            a.Usable = UsabilityTests.AlreadyProcessing;
            return a;
        }

        /// <summary>Args: (none)</summary>
        public static Ability ManualDestroy()
        {
            Ability a = new Ability("#Destroy", Rank.Special);
            //a.desc = Scribe.Write("Destroy up to 12 tokens.");
            a.Aims += AimStage.AttackFree(a.Aims, Filter.Token);
            a.Aims[0].selectionCount = Range.b(1, 12);
            a.MainEffects = (arg, tar) =>
            {
                EffectSet e = new EffectSet();
                foreach (Token t in tar)
                {
                    if (t is Unit)
                        e.Add(Effect.DestroyUnit(a, new EffectArgs(t)));
                    else
                        e.Add(Effect.DestroyObstacle(a, new EffectArgs(t)));
                }
                EffectQueue.Add(e);
            };

            a.Usable = UsabilityTests.AlreadyProcessing;
            return a;
        }

        /// <summary>Args: (none)</summary>
        public static Ability ManualEnd()
        {
            Ability a = new Ability("#End", Rank.None);
//            a.desc = Scribe.Write("End current turn.");
            a.Aims = AimPlan.Self(a);
            a.MainEffects = (arg, tar) => EffectQueue.Add(Effect.Advance(a, new EffectArgs()));
            a.Usable = UsabilityTests.AlreadyProcessing;
            return a;
        }

        /// <summary>Args: </summary>
        public static Ability ManualMove()
        {
            Ability a = new Ability("#Move", Rank.Move);
//            a.desc = Scribe.Write("Move target token to target cell.");
            a.Aims += AimStage.AttackFree(a.Aims, Filter.Token);
            a.Aims += AimStage.MoveFree(a.Aims);
            a.MainEffects = (arg, tar) =>
            {
                IEntity mover = tar[0, 0];
                IEntity cell = tar[1, 0];
                EffectQueue.Add(Effect.TeleportStart(a, new EffectArgs(mover, cell)));
            };
            a.Usable = UsabilityTests.AlreadyProcessing;
            return a;
        }

        /// <summary>Args: player</summary>
        public static Ability ManualOwner()
        {
            Ability a = new Ability("#Capture", Rank.Special);
            ///a.desc = Scribe.Write("Set owner of up to 10 units.");
            a.Aims += AimStage.AttackFree(a.Aims, Filter.Token);
            a.Aims[0].selectionCount = Range.b(1, 12);
            
            a.MainEffects = (arg, tar) =>
            {
                EffectSet e = new EffectSet();
                foreach (Token t in tar)
                    e.Add(Effect.SetOwner(a, new EffectArgs(t, arg.player)));
                EffectQueue.Add(e);
            };

            a.Usable = UsabilityTests.AlreadyProcessing;
            return a;
        }
        /// <summary>Args: damage, stat</summary>
        public static Ability ManualSet()
        {
            Ability a = new Ability("#Set", Rank.Special);
           // a.desc = Scribe.Write("Set stat of up to 12 units.");
            a.Aims += AimStage.AttackFree(a.Aims, Filter.Unit);
            a.Aims[0].selectionCount = Range.b(1, 12);

            a.MainEffects = (arg, tar) =>
            {
                EffectSet e = new EffectSet();
                foreach (Unit u in tar)
                    e.Add(Effect.SetStat(a, new EffectArgs(u, arg.damage, arg.stat)));
                EffectQueue.Add(e);
            };

            a.Usable = UsabilityTests.AlreadyProcessing;
            return a;
        }

        /// <summary>Args: damage</summary>
        public static Ability ManualShift()
        {
            Ability a = new Ability("#Shift", Rank.Special);
         //   a.desc = Scribe.Write("Move up to 10 units in the queue.");
            a.Aims += AimStage.AttackFree(a.Aims, Filter.Unit);
            a.Aims[0].selectionCount = Range.b(1, 12);
            a.MainEffects = (arg, tar) =>
            {
                foreach (Unit u in tar)
                    EffectQueue.Add(Effect.Shift(a, new EffectArgs(u, arg.damage)));
            };

            a.Usable = UsabilityTests.AlreadyProcessing;
            return a;
        }
        


    }
}
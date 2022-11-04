using HOA.Tokens;
using System;

namespace HOA.Abilities
{

    public partial class Ability
    {
        public static Ability ManualAdd(Stats stat, int change)
        {
            Ability a = new Ability(Source.Force, new AbilityArgs("Manual Add Stat", Rank.Special, Price.Free, stat, change));
            a.desc = Scribe.Write ("Increase/Descrease stat of up to 10 units.");
            a.Aims += AimStage.AttackFree(a.Aims, Filter.Unit);
            a.Aims[0].selectionCount = new Range(1,12);
            
            a.PreEffects = () => { };
            a.MainEffects = t =>
            {
                EffectSet e = new EffectSet();
                foreach (Unit u in t[0])
                    e.Add(Effect.AddStat(null, new EffectArgs(u, a.value, a.args.stat)));
                EffectQueue.Add(e);
            };

            a.Usable = a.AlreadyProcessing;
            return a;
        }

        public static Ability ManualCreate(Species species)
        {
            Ability a = new Ability(Source.Force, new AbilityArgs("Manual Create " + species, Rank.Create, Price.Free, species));
            a.desc = Scribe.Write("Create {0} in upto 12 cells.", a.args.species);
            a.Aims += AimStage.CreateFreeManual(a.Aims, species, new Range(1,12));
            
            a.PreEffects = () => { };
            a.MainEffects = t =>
            {
                EffectSet e = new EffectSet();
                foreach (Cell c in t[0])
                    e.Add(Effect.Create(a, new EffectArgs(c, a.args.species)));
                EffectQueue.Add(e);
            };
            
            a.Usable = a.AlreadyProcessing;
            return a;
        }

        public static Ability ManualDestroy()
        {
            Ability a = new Ability(Source.Force, new AbilityArgs("Manual Destroy", Rank.Special, Price.Free));
            a.desc = Scribe.Write("Destroy up to 12 tokens.");
            a.Aims += AimStage.AttackFree(a.Aims, Filter.Token);
            a.Aims[0].selectionCount = new Range(1, 12);

            a.PreEffects = () => { };
            a.MainEffects = t =>
            {
                EffectSet e = new EffectSet();
                foreach (Token x in t)
                {
                    if (x is Unit)
                        e.Add(Effect.DestroyUnit(a, new EffectArgs(x)));
                    else
                        e.Add(Effect.DestroyObstacle(a, new EffectArgs(x)));
                }
                EffectQueue.Add(e);
            };

            a.Usable = a.AlreadyProcessing;
            return a;
        }

        public static Ability ManualEnd()
        {
            Ability a = new Ability(Source.Force, new AbilityArgs("Manual End Turn", Rank.None, Price.Free));
            a.desc = Scribe.Write("End current turn.");
            a.Aims = AimPlan.Self(a);
            a.PreEffects = () => { };
            a.MainEffects = t => EffectQueue.Add(Effect.Advance(a, new EffectArgs()));
            a.Usable = a.AlreadyProcessing;
            return a;
        }

        public static Ability ManualMove()
        {
            Ability a = new Ability(Source.Force, new AbilityArgs("Manual Move", Rank.Move, Price.Free));
            a.desc = Scribe.Write("Move target token to target cell.");
            a.Aims += AimStage.AttackFree(a.Aims, Filter.Token);
            a.Aims += AimStage.MoveFree(a.Aims);
            a.PreEffects = () => { };
            a.MainEffects = t => EffectQueue.Add(Effect.TeleportStart(a, new EffectArgs(t[0][0], t[1][0])));
            a.Usable = a.AlreadyProcessing;
            return a;
        }

        public static Ability ManualOwner(Player owner)
        {
            Ability a = new Ability(Source.Force, new AbilityArgs("Manual Set Owner: " + owner, Rank.Special, Price.Free));
            a.desc = Scribe.Write("Set owner of up to 10 units.");
            a.Aims += AimStage.AttackFree(a.Aims, Filter.Token);
            a.Aims[0].selectionCount = new Range(1, 12);
            
            a.PreEffects = () => { };
            a.MainEffects = t =>
            {
                EffectSet e = new EffectSet();
                foreach (Token x in t)
                    e.Add(Effect.SetOwner(a, new EffectArgs(x, owner)));
                EffectQueue.Add(e);
            };

            a.Usable = a.AlreadyProcessing;
            return a;
        }

        public static Ability ManualSet(Stats stat, int newValue)
        {
            Ability a = new Ability(Source.Force, 
                new AbilityArgs(
                    String.Format("Manual Set Stat: {0} = {1}", stat, newValue), 
                    Rank.Special, Price.Free, stat, newValue));
            a.desc = Scribe.Write("Set stat of up to 12 units.");
            a.Aims += AimStage.AttackFree(a.Aims, Filter.Unit);
            a.Aims[0].selectionCount = new Range(1, 12);

            a.PreEffects = () => { };
            a.MainEffects = t =>
            {
                EffectSet e = new EffectSet();
                foreach (Unit u in t)
                    e.Add(Effect.SetStat(a, new EffectArgs(u, a.value, a.args.stat)));
                EffectQueue.Add(e);
            };

            a.Usable = a.AlreadyProcessing;
            return a;
        }

        public static Ability ManualShift(int change)
        {
            Ability a = new Ability(Source.Force, new AbilityArgs("Manual Shift: " + change, Rank.Special, Price.Free, change));
            a.desc = Scribe.Write("Move up to 10 units in the queue.");
            a.Aims += AimStage.AttackFree(a.Aims, Filter.Unit);
            a.Aims[0].selectionCount = new Range(1, 12);

            a.PreEffects = () => { };
            a.MainEffects = t =>
            {
                foreach (Unit u in t)
                    EffectQueue.Add(Effect.Shift(a, new EffectArgs(u, a.value)));
            };

            a.Usable = a.AlreadyProcessing;
            return a;
        }
        


    }
}
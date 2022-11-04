using HOA.Tokens;
using System;
using HOA.Effects;
using HOA.Args;
using HOA.Collections;
using Farg = HOA.Args.Arg;

namespace HOA.Abilities
{

    public partial class Ability
    {
        /// <summary>Args: price, range, rangeBoostPerFocus (damage) </summary>
        public static Ability Sprint()
        {
            var a = MovePathFocusBoost();
           // a.desc = Scribe.Write("Move {0} to target cell. (+{1} range per Focus.)\nLose all Focus.", a.sourceToken, a.values[2]);
            a.rank = AbilityRank.Special;

            Action<AbilityArgs, NestedList<IEntity>> extra = (arg, tar) =>
                EffectQueue.Add(Effect.SetStat(a, new EffectArgs(
                    Arg.Target(RT.User, arg[RT.User]),
                    Farg.Num(RN.Boost, 0), 
                    Farg.Text(RL.Stat, "Focus"))));
            a.MainEffects += extra;
            return a;
        }

        /// <summary>Args: price, range </summary>
        public static Ability Burrow()
        {
            var a = new Ability("Burrow", AbilityRank.Move);
           // a.desc = Scribe.Write("Move {0} to Target cell.", a.sourceToken);
            a.Aims += AimStage.MoveArc(a.Aims, Range.sb(0, 1));
            a.MainEffects = (arg, tar) =>
                EffectQueue.Add(Effect.BurrowStart(a, new EffectArgs(
                    Arg.Target(RT.Mover, arg[RT.User]), 
                    Arg.Target(RT.Destination, tar[0, 0]))));
            return a;
        }
    }
}
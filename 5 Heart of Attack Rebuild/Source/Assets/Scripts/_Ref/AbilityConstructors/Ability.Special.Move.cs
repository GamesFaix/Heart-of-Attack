using HOA.To;
using System;
using HOA.Ab.Aim;
using HOA.Ef;
using HOA.Fargo;

namespace HOA.Ab
{

    public partial class Ability
    {
        /// <summary>Args: price, range, rangeBoostPerFocus (damage) </summary>
        public static Ability Sprint()
        {
            Ability a = MovePathFocusBoost();
           // a.desc = Scribe.Write("Move {0} to target cell. (+{1} range per Focus.)\nLose all Focus.", a.sourceToken, a.values[2]);
            a.rank = Rank.Special;

            Action<AbilityArgs, NestedList<IEntity>> extra = (arg, tar) =>
                Queue.Add(Effect.SetStat(a, new EffectArgs(
                    Arg.Target(FT.User, arg[FT.User]),
                    Arg.Num(FN.Boost, 0), 
                    Arg.Text(FX.Stat, "Focus"))));
            a.MainEffects += extra;
            return a;
        }

        /// <summary>Args: price, range </summary>
        public static Ability Burrow()
        {
            Ability a = new Ability("Burrow", Rank.Move);
           // a.desc = Scribe.Write("Move {0} to Target cell.", a.sourceToken);
            a.Aims += Stage.MoveArc(a.Aims, Range.sb(0, 1));
            a.MainEffects = (arg, tar) =>
                Queue.Add(Effect.BurrowStart(a, new EffectArgs(
                    Arg.Target(FT.Mover, arg[FT.User]), 
                    Arg.Target(FT.Destination, tar[0, 0]))));
            return a;
        }
    }
}
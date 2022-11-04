using HOA.To;
using System;
using HOA.Ab.Aim;
using HOA.Ef;

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

            Action<Args, NestedList<IEntity>> extra = 
                (arg, tar) =>
                   Queue.Add(Effect.SetStat(a, new Ef.Args(arg.user, 0, Stats.Focus)));
            a.MainEffects += extra;
            return a;
        }

        /// <summary>Args: price, range </summary>
        public static Ability Burrow()
        {
            Ability a = new Ability("Burrow", Rank.Move);
           // a.desc = Scribe.Write("Move {0} to Target cell.", a.sourceToken);
            a.Aims += Stage.MoveArc(a.Aims, Range.b(0, 1));
            a.Update = Adjustments.Range0;
            a.MainEffects = (arg, tar) =>
            {
                IEntity cell = tar[0, 0];
                Queue.Add(Effect.BurrowStart(a, new Ef.Args(arg.user, cell)));
            };
            return a;
        }
    }
}
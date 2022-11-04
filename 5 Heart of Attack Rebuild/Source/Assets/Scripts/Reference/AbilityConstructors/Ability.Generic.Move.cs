using HOA.To;
using System;
using System.Collections.Generic;
using HOA.Ab.Aim;
using HOA.Ef;

namespace HOA.Ab
{

    public partial class Ability
    {
        /// <summary>Arguments: price, range </summary>
        public static Ability MovePath()
        {
            Ability a = new Ability("Move", Rank.Move);
            //a.desc = Scribe.Write("Move {0} to target cell.", a.sourceToken);
            a.Aims = Plan.MovePath(a, Range.b(0,1));
            a.Update = Adjustments.Range0;
            a.MainEffects = (arg, tar) =>
            {
                Token mover = tar[0,0] as Token;
                for (byte i = 1; i < tar.Count; i++)
                {
                    Cell c = tar[i,0] as Cell;
                    Queue.Add(Effect.Move(a, new Ef.Args(mover, c)));
                }
            };
            return a;
        }

        /// <summary>Arguments: price, range, rangeBoostPerFocus </summary>
        public static Ability MovePathFocusBoost()
        {
            Ability a = MovePath();
            a.name = "Embark";
            a.Update = Adjustments.FocusRangeBoost0;
            return a;
        }

        /// <summary>Args: price, range</summary>
        public static Ability MoveLine()
        {
            Ability a = new Ability("Dart", Rank.Move);
//            a.desc = Scribe.Write("Move {0} to Target cell.", a.sourceToken);
            a.Aims += Stage.MoveLine(a.Aims, Range.sb(0,1));
            a.Update = Adjustments.Range0;
            a.MainEffects = (arg, tar) =>
            {
                Cell start = arg.user.Cell;
                Cell finish = tar[0,0] as Cell;
                Set<Cell> line = new Set<Cell>();
                int2 dir = Direction.FromCells(start, finish);
                int length = Math.Max(
                    Math.Abs(start.x - finish.x),
                    Math.Abs(start.y - finish.y));
                for (int i = 0; i < length; i++)
                {
                    index2 index = start.Index + dir;
                    start = Session.Active.board[index];
                    line.Add(start);
                }
                foreach (Cell c in line)
                    Queue.Add(Effect.Move(a, new Ef.Args(arg.user, c)));
            };
            return a;
        }

        /// <summary>Args: price, range, filter, int range2min, range2max </summary>
        public static Ability Teleport()
        {
            Ability a = new Ability("Teleport", Rank.Special);
            a.Aims += Stage.AttackArc(a.Aims, Filter.Token, Range.sb(0,1));
            a.Aims += Stage.MoveArcOther(a.Aims, () => Ab.Processor.targets[0, 0] as Token, Range.sb(0, 1));
            a.Update = Adjustments.Range0 + Adjustments.Filter0 + Adjustments.Range1;
            //a.desc = Scribe.Write("Move {0} to Target cell.", a.Aims[0].filter);
            a.MainEffects = (arg, tar) =>
            {
                IEntity mover = tar[0, 0];
                IEntity destination = tar[1, 0];
                Queue.Add(Effect.TeleportStart(a, new Ef.Args(mover, destination)));
            };
            return a;
        }
    }
}
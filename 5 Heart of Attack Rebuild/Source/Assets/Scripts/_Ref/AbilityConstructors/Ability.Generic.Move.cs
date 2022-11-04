﻿using HOA.To;
using System;
using System.Collections.Generic;
using HOA.Ab.Aim;
using HOA.Ef;
using HOA.Fargo;

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
            a.MainEffects = (arg, tar) =>
            {
                for (byte i = 1; i < tar.Count; i++)
                    Queue.Add(Effect.Move(a, new EffectArgs(
                        Arg.Target(FT.Mover, tar[0, 0]),
                        Arg.Target(FT.Destination, tar[i, 0]))));
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
            a.MainEffects = (arg, tar) =>
            {
                Cell start = (arg[FT.User] as Token).Cell;
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
                    Queue.Add(Effect.Move(a, new EffectArgs(
                        Arg.Target(FT.Mover, arg[FT.User]), 
                        Arg.Target(FT.Destination, c))));
            };
            return a;
        }

        /// <summary>Args: price, range, filter, int range2min, range2max </summary>
        public static Ability Teleport()
        {
            Ability a = new Ability("Teleport", Rank.Special);
            a.Aims += Stage.AttackArc(a.Aims, Filter.Token, Range.sb(0,1));
            a.Aims += Stage.MoveArcOther(a.Aims, () => Ab.Processor.targets[0, 0] as Token, Range.sb(0, 1));
            //a.desc = Scribe.Write("Move {0} to Target cell.", a.Aims[0].filter);
            a.MainEffects = (arg, tar) =>
                Queue.Add(Effect.TeleportStart(a, new EffectArgs(
                    Arg.Target(FT.Mover, tar[0, 0]), 
                    Arg.Target(FT.Destination, tar[1, 0]))));
            return a;
        }
    }
}
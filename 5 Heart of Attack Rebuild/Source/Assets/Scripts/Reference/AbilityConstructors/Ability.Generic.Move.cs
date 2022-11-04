using HOA.Tokens;
using System;

namespace HOA.Abilities
{

    public partial class Ability
    {
        public static Ability Move(Unit parent, string name, Price price, Range range)
        {
            Ability a = new Ability(parent, new AbilityArgs(name, Rank.Move, price));
            a.desc = Scribe.Write("Move {0} to target cell.", a.sourceToken);
            a.Aims = AimPlan.MovePath(a, range);
            a.MainEffects = t =>
            {
                foreach (Set<IEntity> s in t)
                    EffectQueue.Add(Effect.Move(a, new EffectArgs(a.sourceToken, s[0] as Cell)));
            };
            return a;
        }
        public static Ability Move(Unit parent, int rangeMax)
        { return Move(parent, "Move", Price.Cheap, new Range(0, (short)rangeMax)); }


        public static Ability MoveFocusBoost(Unit parent, string name, Range baseRange, int rangeBoostPerFocus, Rank rank = Rank.Move)
        {
            Ability a = new Ability(parent, new AbilityArgs(name, rank, Price.Cheap, baseRange.min, baseRange.max, rangeBoostPerFocus));
            a.desc = Scribe.Write("Move {0} to target cell. (+{1} range per Focus.)", a.sourceToken, a.values[2]);
            a.Aims = AimPlan.MovePath(a, baseRange);
            a.MainEffects = t =>
            {
                foreach (Set<IEntity> s in t)
                    EffectQueue.Add(Effect.Move(a, new EffectArgs(a.sourceToken, s[0] as Cell)));
            };
            a.Adjust = () => a.Aims = AimPlan.MovePath(a, 
                new Range((short)a.values[0], (short)(a.values[1] + a.sourceUnit.Focus)  ));
            a.Unadjust = () => a.Aims = AimPlan.MovePath(a, new Range((short)a.values[0], (short)(a.values[1])));
            return a;
        }


        public static Ability Dart(Unit parent, Range range)
        {
            Ability a = new Ability(parent, new AbilityArgs("Dart", Rank.Move, Price.Cheap));
            a.desc = Scribe.Write("Move {0} to Target cell.", a.sourceToken);
            a.Aims += AimStage.MoveLine(a.Aims, range);
            a.MainEffects = t =>
            {
                Cell start = a.sourceToken.Cell;
                Cell finish = (Cell)t[0][0];
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
                    EffectQueue.Add(Effect.Move(a, new EffectArgs(a.sourceToken, c)));
            };
            return a;
        }

        public static Ability Teleport(Unit parent, string name, Price price, Predicate<IEntity> tokenFilter, Range toToken, Range fromToken)
        {
            Ability a = new Ability(parent, new AbilityArgs(name, Rank.Special, price));
            a.Aims += AimStage.AttackArc(a.Aims, tokenFilter, toToken);
            a.Aims += AimStage.MoveArcOther(a.Aims, () => AbilityProcessor.targets[0][0] as Token, fromToken);
            a.desc = Scribe.Write("Move {0} to Target cell.", a.Aims[0].filter);
            a.MainEffects = t =>
                EffectQueue.Add(Effect.TeleportStart(a, new EffectArgs(t[0][0] as Token, t[1][0] as Cell)));
            return a;
        }
    }
}
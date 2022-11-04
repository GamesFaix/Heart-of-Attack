using System;
using System.Collections.Generic;

namespace HOA.Abilities
{
	

    public partial class AimStage
    {
        public static AimStage Self(AimPlan plan)
        {
            AimStage a = new AimStage(plan, AimPatterns.Self, Filter.identity(plan.source.Last<Unit>(), true));
            a.autoSelect = true;
            return a;
        }

        public static AimStage CreateNeighbor(AimPlan plan, Tokens.Species species)
        {
            return new AimStage(plan, AimPatterns.Neighbor, Filter.Cell, () => Reference.Tokens.templates[species]);
        }

        public static AimStage CreateNeighborMulti(AimPlan plan, Tokens.Species species, Range count)
        {
            AimStage a = new AimStage(plan, AimPatterns.Neighbor, Filter.Cell, () => Reference.Tokens.templates[species]);
            a.selectionCount = count;
            return a;
        }

        public static AimStage CreateArc(AimPlan plan, Tokens.Species species, Range range)
        {
            return new AimStage(plan, AimPatterns.Arc, Filter.Cell, () => Reference.Tokens.templates[species], range);
        }

        public static AimStage CreateDrop(AimPlan plan, Tokens.Species species)
        {
            Predicate<IEntity> p = Filter.identity(plan.source.Last<Token>().Cell, true);
            return new AimStage(plan, AimPatterns.Neighbor, p, () => Reference.Tokens.templates[species]);
        }

        public static AimStage CreateFree(AimPlan plan, Tokens.Species species, Range count)
        {
            AimStage a = new AimStage(plan, AimPatterns.Free, Filter.Cell, () => Reference.Tokens.templates[species]);
            a.selectionCount = count;
            return a;
        }

        public static AimStage CreateFree(AimPlan plan, Tokens.Species species) 
        { return CreateFree(plan, species, new Range(1, 1)); }

        public static AimStage CreateFreeManual(AimPlan plan, Tokens.Species species, Range count)
        {
            AimStage a = new AimStage(plan, AimPatterns.Free, Filter.Cell, () => Reference.Tokens.templates[species], () => null);
            a.selectionCount = count;
            return a;
        }


        public static AimStage MoveNeighbor(AimPlan plan) 
        { return new AimStage(plan, AimPatterns.Neighbor, Filter.Cell); }

        public static AimStage MoveNeighborFromLast(AimPlan plan)
        {
            AimStage a = MoveNeighbor(plan);
            int i = a.plan.Count - 1;
            a.center = () => AbilityProcessor.targets[i][0] as Cell;
            return a;
        }

        public static AimStage MoveLine(AimPlan plan, Range range)
        {
            AimStage a = new AimStage(plan, AimPatterns.Line, Filter.Cell);
            a.range = range;
            return a;
        }
        public static AimStage MoveArc(AimPlan plan, Range range)
        {
            AimStage a = new AimStage(plan, AimPatterns.Arc, Filter.Cell);
            a.range = range;
            return a;
        }


        public static AimStage MoveArcOther(AimPlan plan, Func<Token> body, Range range)
        {
            AimStage a = new AimStage(plan, AimPatterns.Arc, Filter.Cell, body, () => body().Cell);
            a.range = range;
            return a;
        }

        public static AimStage MoveFree(AimPlan plan)
        {
            return new AimStage(plan, AimPatterns.Free, Filter.Cell);
        }

        public static AimStage AttackNeighbor(AimPlan plan, Predicate<IEntity> filter)
        { return new AimStage(plan, AimPatterns.Neighbor, filter); }

        public static AimStage AttackLine(AimPlan plan, Predicate<IEntity> filter, Range range)
        {
            AimStage a = new AimStage(plan, AimPatterns.Line, filter);
            a.range = range;
            return a;
        }

        public static AimStage AttackArc(AimPlan plan, Predicate<IEntity> filter, Range range)
        {
            AimStage a = new AimStage(plan, AimPatterns.Arc, filter);
            a.range = range;
            return a;
        }

        public static AimStage AttackFree(AimPlan plan, Predicate<IEntity> filter)
        { return new AimStage(plan, AimPatterns.Free, filter); }
    }
}
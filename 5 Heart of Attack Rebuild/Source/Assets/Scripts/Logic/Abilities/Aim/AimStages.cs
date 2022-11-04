using System;
using System.Collections.Generic;

namespace HOA.Ab
{
	

    public partial class AimStage
    {
        public static AimStage Self(AimPlan plan)
        {
            AimStage a = new AimStage(plan, AimPatterns.Self, Filter.identity(plan.source.Last<Unit>(), true));
            a.autoSelect = true;
            return a;
        }

        public static AimStage CreateNeighbor(AimPlan plan, To.Species species)
        {
            return new AimStage(plan, AimPatterns.Neighbor, Filter.Cell, () => Ref.Tokens.templates[species]);
        }

        public static AimStage CreateNeighborMulti(AimPlan plan, To.Species species, Range<byte> count)
        {
            AimStage a = new AimStage(plan, AimPatterns.Neighbor, Filter.Cell, () => Ref.Tokens.templates[species]);
            a.selectionCount = count;
            return a;
        }

        public static AimStage CreateArc(AimPlan plan, To.Species species, Range<byte> range)
        {
            return new AimStage(plan, AimPatterns.Arc, Filter.Cell, () => Ref.Tokens.templates[species], range);
        }

        public static AimStage CreateDrop(AimPlan plan, To.Species species)
        {
            Token user = plan.source.Last<Token>();
            Predicate<IEntity> p = (user != null ? Filter.identity(user.Cell, true) : Filter.False);
            return new AimStage(plan, AimPatterns.Neighbor, p, () => Ref.Tokens.templates[species]);
        }

        public static AimStage CreateFree(AimPlan plan, To.Species species, Range<byte> count)
        {
            AimStage a = new AimStage(plan, AimPatterns.Free, Filter.Cell, () => Ref.Tokens.templates[species]);
            a.selectionCount = count;
            return a;
        }

        public static AimStage CreateFree(AimPlan plan, To.Species species) 
        { return CreateFree(plan, species, Range.b(1, 1)); }

        public static AimStage CreateFreeManual(AimPlan plan, To.Species species, Range<byte> count)
        {
            AimStage a = new AimStage(plan, AimPatterns.Free, Filter.Cell, () => Ref.Tokens.templates[species], () => null);
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

        public static AimStage MoveLine(AimPlan plan, Range<byte> range)
        {
            AimStage a = new AimStage(plan, AimPatterns.Line, Filter.Cell);
            a.range = range;
            return a;
        }
        public static AimStage MoveArc(AimPlan plan, Range<byte> range)
        {
            AimStage a = new AimStage(plan, AimPatterns.Arc, Filter.Cell);
            a.range = range;
            return a;
        }


        public static AimStage MoveArcOther(AimPlan plan, Func<Token> body, Range<byte> range)
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

        public static AimStage AttackLine(AimPlan plan, Predicate<IEntity> filter, Range<byte> range)
        {
            AimStage a = new AimStage(plan, AimPatterns.Line, filter);
            a.range = range;
            return a;
        }

        public static AimStage AttackArc(AimPlan plan, Predicate<IEntity> filter, Range<byte> range)
        {
            AimStage a = new AimStage(plan, AimPatterns.Arc, filter);
            a.range = range;
            return a;
        }

        public static AimStage AttackFree(AimPlan plan, Predicate<IEntity> filter)
        { return new AimStage(plan, AimPatterns.Free, filter); }
    }
}
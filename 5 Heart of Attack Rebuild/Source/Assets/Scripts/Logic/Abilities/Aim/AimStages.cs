using System;
using System.Collections.Generic;

namespace HOA.Abilities
{
	

    public partial class AimStage
    {
        public static AimStage Self(AimPlan plan)
        {
            return new AimStage(plan, AimPatterns.Self, Filter.identity(plan.source.Last<Unit>(), true));
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

        public static AimStage CreateFree(AimPlan plan, Tokens.Species species)
        {
            return new AimStage(plan, AimPatterns.Free, Filter.Cell, () => Reference.Tokens.templates[species]);
        }

	}
}
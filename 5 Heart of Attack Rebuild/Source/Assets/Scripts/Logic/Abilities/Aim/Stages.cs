using System;
using System.Collections.Generic;

namespace HOA.Ab.Aim
{

    public partial class Stage
    {
        public static Stage Self(Plan plan)
        {
            Stage a = new Stage(plan, Patterns.Self, Filter.identity(plan.source.Last<Unit>(), true));
            a.autoSelect = true;
            return a;
        }

        public static Stage CreateNeighbor(Plan plan, To.Species species)
        {
            return new Stage(plan, Patterns.Neighbor, Filter.Cell, () => Ref.Tokens.templates[species]);
        }

        public static Stage CreateNeighborMulti(Plan plan, To.Species species, Range<byte> count)
        {
            Stage a = new Stage(plan, Patterns.Neighbor, Filter.Cell, () => Ref.Tokens.templates[species]);
            a.selectionCount = count;
            return a;
        }

        public static Stage CreateArc(Plan plan, To.Species species, Range<byte> range)
        {
            return new Stage(plan, Patterns.Arc, Filter.Cell, () => Ref.Tokens.templates[species], range);
        }

        public static Stage CreateDrop(Plan plan, To.Species species)
        {
            Token user = plan.source.Last<Token>();
            Predicate<IEntity> p = (user != null ? Filter.identity(user.Cell, true) : Filter.False);
            return new Stage(plan, Patterns.Neighbor, p, () => Ref.Tokens.templates[species]);
        }

        public static Stage CreateFree(Plan plan, To.Species species, Range<byte> count)
        {
            Stage a = new Stage(plan, Patterns.Free, Filter.Cell, () => Ref.Tokens.templates[species]);
            a.selectionCount = count;
            return a;
        }

        public static Stage CreateFree(Plan plan, To.Species species) 
        { return CreateFree(plan, species, Range.b(1, 1)); }

        public static Stage CreateFreeManual(Plan plan, To.Species species, Range<byte> count)
        {
            Stage a = new Stage(plan, Patterns.Free, Filter.Cell, () => Ref.Tokens.templates[species], () => null);
            a.selectionCount = count;
            return a;
        }


        public static Stage MoveNeighbor(Plan plan) 
        { return new Stage(plan, Patterns.Neighbor, Filter.Cell); }

        public static Stage MoveNeighborFromLast(Plan plan)
        {
            Stage a = MoveNeighbor(plan);
            int i = a.plan.Count - 1;
            a.center = () => Ab.Processor.targets[i][0] as Cell;
            return a;
        }

        public static Stage MoveLine(Plan plan, Range<byte> range)
        {
            Stage a = new Stage(plan, Patterns.Line, Filter.Cell);
            a.range = range;
            return a;
        }
        public static Stage MoveArc(Plan plan, Range<byte> range)
        {
            Stage a = new Stage(plan, Patterns.Arc, Filter.Cell);
            a.range = range;
            return a;
        }


        public static Stage MoveArcOther(Plan plan, Func<Token> body, Range<byte> range)
        {
            Stage a = new Stage(plan, Patterns.Arc, Filter.Cell, body, () => body().Cell);
            a.range = range;
            return a;
        }

        public static Stage MoveFree(Plan plan)
        {
            return new Stage(plan, Patterns.Free, Filter.Cell);
        }

        public static Stage AttackNeighbor(Plan plan, Predicate<IEntity> filter)
        { return new Stage(plan, Patterns.Neighbor, filter); }

        public static Stage AttackLine(Plan plan, Predicate<IEntity> filter, Range<byte> range)
        {
            Stage a = new Stage(plan, Patterns.Line, filter);
            a.range = range;
            return a;
        }

        public static Stage AttackArc(Plan plan, Predicate<IEntity> filter, Range<byte> range)
        {
            Stage a = new Stage(plan, Patterns.Arc, filter);
            a.range = range;
            return a;
        }

        public static Stage AttackFree(Plan plan, Predicate<IEntity> filter)
        { return new Stage(plan, Patterns.Free, filter); }
    }
}
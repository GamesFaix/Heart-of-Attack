using System;
using System.Linq;
using Cell = HOA.Board.Cell;
using Direction = HOA.Board.Direction;
using Session = HOA.Sessions.Session;
using Token = HOA.Tokens.Token;

namespace HOA.Abilities
{
    public delegate Set<IEntity> AimPattern(AimPatternArgs args);

    public static class AimPatterns
    {
        public static Set<IEntity> Arc(AimPatternArgs args)
        {
            Set<IEntity> square = Square(args.center, args.range);
            return (square + Cell.Occupants) / args.filter;
        }
        static Set<IEntity> Square(Cell start, Range<sbyte> range)
        {
            Set<IEntity> square = new Set<IEntity>();
            Cell c;
            for (int x = (start.x - range.max); x <= (start.x + range.max); x++)
                for (int y = (start.y - range.max); y <= (start.y + range.max); y++)
                    if (start.Board.HasCell(new index2(x, y), out c))
                        square.Add(c);

            if (range.min > 0)
                square = RemoveMin(square, start, range.min);
            return square;
        }
        static Set<IEntity> RemoveMin(Set<IEntity> square, Cell start, int min)
        {
            Set<IEntity> ring = new Set<IEntity>();
            foreach (Cell c in square)
                if ((Math.Abs(c.x - start.x) >= min)
                    || (Math.Abs(c.y - start.y) >= min))
                    ring.Add(c);
            return ring;
        }

        public static Set<IEntity> Free (AimPatternArgs args)
        {
            return (Session.Active.cells.Base<Cell, IEntity>() + Session.Active.tokens) / args.filter;
        }

        public static Set<IEntity> Line (AimPatternArgs args)
        {
            Set<IEntity> cells = new Set<IEntity>(args.center);
            Set<Set<IEntity>> star = Star(args.center, args.range);
            foreach (Set<IEntity> line in star)
            {
                Set<IEntity> lineTrimmed = Trim(line, args.body, args.inclusive);
                cells.Add(lineTrimmed);
            }
            return (cells + Cell.Occupants) / args.filter;
        }
        static Set<Set<IEntity>> Star(Cell center, Range<sbyte> range)
        {
            Set<Set<IEntity>> star = new Set<Set<IEntity>>();

            foreach (int2 dir in Direction.Directions)
            {
                Set<IEntity> line = new Set<IEntity>();
                Cell last = center;
                for (short i = range.min; i <= range.max; i++)
                {
                    Cell next;
                    try
                    {
                        index2 index = (index2)(((int2)last.Index) + dir);
                        if (center.Board.HasCell(index, out next))
                        {
                            line.Add(next);
                            last = next;
                        }
                    }
                    catch (ArgumentOutOfRangeException) { }
                }
                star.Add(line);
            }
            return star;
        }
        static Set<IEntity> Trim(Set<IEntity> line, Token body, bool inclusive = false)
        {
            Set<IEntity> legal = new Set<IEntity>();
            foreach (Cell c in line)
            {
                if (body.CanAimThru(c))
                    legal.Add(c);
                else
                {
                    if (inclusive)
                        legal.Add(c);
                    break;
                }
                if (c.CanStop(body))
                    break;
            }
            return legal;
        }
        
        public static Set<IEntity> Neighbor (AimPatternArgs args)
        {
            return (args.center.NeighborsAndSelf + Cell.Occupants) / args.filter;
        }

        public static Set<IEntity> Path (AimPatternArgs args)
        {
            Log.Debug("Aim.Path does not allow custom paths.");
            Set<IEntity> cells = new Set<IEntity>(args.center);
            Set<IEntity> thisRad = args.center.Neighbors;
            Set<IEntity> nextRad = new Set<IEntity>();

            for (int i = 1; i <= args.range.max; i++)
            {
                foreach (Cell c in thisRad)
                {
                    if (args.body.CanAimThru(c))
                        cells.Add(c);
                    else
                    {
                        if (args.inclusive)
                            cells.Add(c);
                        break;
                    }
                    if (c.CanStop(args.body))
                        break;
                }
                thisRad = nextRad;
                nextRad = new Set<IEntity>();
            }
            return (cells + Cell.Occupants) / args.filter;
        }

        public static Set<IEntity> Radial (AimPatternArgs args)
        {
            Set<IEntity> set = new Set<IEntity>();
            NeighborMatrix neighbors = new NeighborMatrix(args.center);
            Cell c;
            if (neighbors.CellClockwise(args.center, out c))
                set.Add(c);
            if (neighbors.CellCounter(args.center, out c))
                set.Add(c);
            return set;
        }

        public static Set<IEntity> Self (AimPatternArgs args)
        {
            return new Set<IEntity>(args.user);
        }
    }
}

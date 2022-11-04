using System;
using HOA.Collections;

namespace HOA.Abilities
{
    public delegate EntitySet AimPattern(AimPatternArgs args);

    public static class AimPatterns
    {
        public static EntitySet Arc (AimPatternArgs args)
        {
            CellSet square = Square(args.center, args.range);
            return
                (EntitySet)square
                + (EntitySet)(square.Occupants)
                - args.filter;
        }
        static CellSet Square(Cell start, Tokens.Stat range)
        {
            CellSet square = new CellSet();
            Cell c;
            for (int x = (start.x - range); x <= (start.x + range); x++)
                for (int y = (start.y - range); y <= (start.y + range); y++)
                    if (start.Board.HasCell(new index2(x, y), out c))
                        square.Add(c);

            if (range.Min > 0)
                square = RemoveMin(square, start, range.Min);
            return square;
        }
        static CellSet RemoveMin(CellSet square, Cell start, int min)
        {
            CellSet ring = new CellSet();
            foreach (Cell c in square)
                if ((Math.Abs(c.x - start.x) >= min)
                    || (Math.Abs(c.y - start.y) >= min))
                    ring.Add(c);
            return ring;
        }

        public static EntitySet Free (AimPatternArgs args)
        {
            return
            (EntitySet)(args.center.Board.Cells)
            + (EntitySet)(Session.Active.tokens)
            - args.filter;
        }

        public static EntitySet Line (AimPatternArgs args)
        {
            CellSet cells = new CellSet(args.center);
            ListSet<CellSet> star = Star(args.center, args.range);
            foreach (CellSet line in star)
            {
                CellSet lineTrimmed = Trim(line, args.body, args.inclusive);
                cells.Add(lineTrimmed);
            }
            return
                (EntitySet)cells
                + (EntitySet)(cells.Occupants)
                - args.filter;
        }
        static ListSet<CellSet> Star(Cell center, int range)
        {
            ListSet<CellSet> star = new ListSet<CellSet>();

            foreach (int2 dir in Direction.Directions)
            {
                CellSet line = new CellSet();
                Cell last = center;
                for (byte i = 1; i <= range; i++)
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
        static CellSet Trim(CellSet line, Token body, bool inclusive = false)
        {
            CellSet legal = new CellSet();
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
        
        public static EntitySet Neighbor (AimPatternArgs args)
        {
            return
            (EntitySet)(args.center.NeighborsAndSelf)
            + (EntitySet)(args.center.NeighborsAndSelf.Occupants)
            - args.filter;
        }

        public static EntitySet Path (AimPatternArgs args)
        {
            Debug.Log("Aim.Path does not allow custom paths.");
            CellSet cells = new CellSet(args.center);
            CellSet thisRad = args.center.Neighbors;
            CellSet nextRad = new CellSet();

            for (int i = 1; i <= args.range; i++)
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
                nextRad = new CellSet();
            }
            return
                (EntitySet)cells
                + (EntitySet)(cells.Occupants)
                - args.filter;
        }

        public static EntitySet Radial (AimPatternArgs args)
        {
            EntitySet set = new EntitySet();
            NeighborMatrix neighbors = new NeighborMatrix(args.center);
            Cell c;
            if (neighbors.CellClockwise(args.center, out c))
                set.Add(c);
            if (neighbors.CellCounter(args.center, out c))
                set.Add(c);
            return set;
        }

        public static EntitySet Self (AimPatternArgs args)
        {
            return new EntitySet(args.user);
        }
    }
}

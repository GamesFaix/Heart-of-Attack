using System;
using Cell = HOA.Board.Cell;
using HOA.Collections;

namespace HOA.Abilities
{

    public class NeighborMatrix : HOA.Collections.Matrix<Cell>
    {

        public NeighborMatrix(Cell center)
            : base (new size2(3,3))
        {
            index2 startIndex = center.Index - new int2(1, 1);
            foreach (index2 index in Size)
            {
                Cell cell;
                if (center.Board.HasCell(startIndex + (int2)index, out cell))
                    this[index] = cell;
            }
        }

        bool IndexClockwise(index2 start, out index2 next)
        {
            int x = start.x;
            int y = start.y;
            next = new index2(0, 0);

            if (!Size.Contains(start))
            {
                Log.Debug("NeighborMatrix.IndexClockwise: Start cell not in matrix.");
                return false;
            }
            else if (x == 1 && y == 1)
            {
                Log.Debug("NeighborMatrix.IndexClockwise: Cannot start at center.");
                return false;
            }
            else
            {
                if (x == 0 && y == 0)      next = new index2(1, 0);
                else if (x == 1 && y == 0) next = new index2(2, 0);
                else if (x == 2 && y == 0) next = new index2(2, 1);
                else if (x == 2 && y == 1) next = new index2(2, 2);
                else if (x == 2 && y == 2) next = new index2(1, 2);
                else if (x == 1 && y == 2) next = new index2(0, 2);
                else if (x == 0 && y == 2) next = new index2(0, 1);
                else if (x == 0 && y == 1) next = new index2(0, 0);
                return true;
            }
        }

        bool IndexCounter(index2 start, out index2 next)
        {
            int x = start.x;
            int y = start.y;
            next = new index2(0, 0);

            if (!Size.Contains(start))
            {
                Log.Debug("NeighborMatrix.IndexCounter: Start cell not in matrix.");
                return false;
            }
            else if (x == 1 && y == 1)
            {
                Log.Debug("NeighborMatrix.IndexCounter: Cannot start at center.");
                return false;
            }
            else
            {
                if (x == 0 && y == 0)      next = new index2(0, 1);  
                else if (x == 0 && y == 1) next = new index2(0, 2); 
                else if (x == 0 && y == 2) next = new index2(1, 2); 
                else if (x == 1 && y == 2) next = new index2(2, 2); 
                else if (x == 2 && y == 2) next = new index2(2, 1); 
                else if (x == 2 && y == 1) next = new index2(2, 0); 
                else if (x == 2 && y == 0) next = new index2(1, 0);
                else if (x == 1 && y == 0) next = new index2(0, 0);
                return true;
            }
        }

        public bool CellClockwise(Cell start, out Cell next)
        {
            next = null;

            if (!Contains(start))
            {
                Log.Debug("NeighborMatrix.CellClockwise: Start cell not in Matrix.");
                return false;
            }
            else if (start == this[new index2(1, 1)])
            {
                Log.Debug("NeighborMatrix.CellClockwise: Cannot start at center.");
                return false;
            }
            else
            {
                index2 startIndex = IndexOf(start);
                index2 nextIndex;
                if (IndexClockwise(startIndex, out nextIndex))
                {
                    next = this[nextIndex];
                    return true;
                }
                else 
                    return false; 
            }
        }

        public bool CellCounter(Cell start, out Cell next)
        {
            next = null;

            if (!Contains(start))
            {
                Log.Debug("NeighborMatrix.CellCounter: Start cell not in Matrix.");
                return false;
            }
            else if (start == this[new index2(1, 1)])
            {
                Log.Debug("NeighborMatrix.CellCounter: Cannot start at center.");
                return false;
            }
            else
            {
                index2 startIndex = IndexOf(start);
                index2 nextIndex;
                if (IndexCounter(startIndex, out nextIndex))
                {
                    next = this[nextIndex];
                    return true;
                }
                else 
                    return false;
            }
        }

        public Set<IEntity> Ring(Cell first, Cell second)
        {
            Set<IEntity> ring = new Set<IEntity>(first);
            bool clockwise;

            Cell nextClockwise;
            Cell nextCounter;

            if (CellClockwise(first, out nextClockwise)
                && second == nextClockwise)
                    clockwise = true;
            else if (CellCounter(first, out nextCounter)
                && second == nextCounter)
                    clockwise = false;
            else
            {
                Log.Debug("NeighborMatrix.Ring: Second cell invalid.");
                return ring;
            }

            Cell last = first;
            Cell next;

            for (int i = 0; i < 8; i++)
            {
                if (clockwise)
                    if (CellClockwise(last, out next))
                    {
                        ring.Add(next);
                        last = next;
                    }
                    else 
                        return ring; 
                else
                    if (CellCounter(last, out next))
                    {
                        ring.Add(next);
                        last = next;
                    }
                    else 
                        return ring;
            }
            return ring;
        }
    }
}

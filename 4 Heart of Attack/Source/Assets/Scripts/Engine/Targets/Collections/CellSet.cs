using UnityEngine;
using System.Collections.Generic;

namespace HOA 
{ 

    public class CellSet : ListSet<Cell>, IEnumerable<Cell>
    {
        public CellSet()
        {
            list = new List<Cell>();
        }

        public CellSet(Cell c) : this () { Add(c); }

        public TokenSet Occupants
        {
            get
            {
                TokenSet occupants = new TokenSet();
                foreach (Cell c in list)
                    occupants.Add(c.Occupants);
                 return occupants;
            }
        }

        public CellSet Copy()
        {
            CellSet copy = new CellSet();
            foreach (Cell c in this) copy.Add(c);
            return copy;
        }

        private CellSet Filter(TargetFilter filter)
        {
            CellSet rejected = Copy();
            CellSet accepted = new CellSet();

            foreach (FilterTest test in filter.Tests)
            {
                foreach (Cell c in rejected)
                {
                    if (test(c))
                    {
                        accepted.Add(c);
                        rejected.Mark(c);
                    }
                }
                rejected.RemoveMarked();
            }
            return accepted;
        }

        public static CellSet operator -(CellSet a, TargetFilter b) { return a.Filter(b); }

        public Cell[] ToArray()
        {
            Cell[] array = new Cell[Count];
            for (int i = 0; i < Count; i++)
                array[i] = this[i];
            return array;
        }

    }
}

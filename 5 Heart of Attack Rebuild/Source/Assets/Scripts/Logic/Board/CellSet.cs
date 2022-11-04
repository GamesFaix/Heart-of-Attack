using UnityEngine;
using System;
using System.Collections.Generic;
using HOA.Collections;

namespace HOA
{
    /// <summary>
    /// Extended ListSet for holding Cells
    /// </summary>
    public class CellSet : ListSet<Cell>, IEnumerable<Cell>
    {
        /// <summary>
        /// Create an empty set
        /// </summary>
        /// <param name="capacity">Default to 4</param>
        public CellSet(int capacity = 4)
        {
            list = new List<Cell>(capacity);
        }

        /// <summary>
        /// Create a set with one cell
        /// </summary>
        /// <param name="c"></param>
        public CellSet(Cell c) : this() { Add(c); }

        /// <summary>
        /// Create a set from a collection of cells
        /// </summary>
        /// <param name="collection"></param>
        public CellSet(IEnumerable<Cell> collection)
        {
            list = new List<Cell>(collection);
        }

        /// <summary>
        /// Returns TokenSet of all tokens currently in cells
        /// </summary>
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

        /// <summary>
        /// Returns duplicate CellSet
        /// </summary>
        /// <returns></returns>
        public CellSet Copy()
        {
            CellSet copy = new CellSet();
            foreach (Cell c in this) copy.Add(c);
            return copy;
        }

        private CellSet Filter(EntityFilter filter)
        {
            CellSet rejected = Copy();
            CellSet accepted = new CellSet();

            foreach (Predicate<IEntity> test in filter.Tests)
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

        /// <summary>
        /// Duplicate set, then filter it
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static CellSet operator -(CellSet a, EntityFilter b) { return a.Filter(b); }

        /// <summary>
        /// Copys to array
        /// </summary>
        /// <returns></returns>
        public Cell[] ToArray()
        {
            Cell[] array = new Cell[Count];
            for (int i = 0; i < Count; i++)
                array[i] = this[i];
            return array;
        }

        public static explicit operator EntitySet(CellSet cells) {
            EntitySet e = new EntitySet();
            foreach (Cell c in cells)
                e.Add(c);
            return e;
        }

        public void Legalize(bool b = false)
        {
            foreach (Cell c in this)
                c.Legal = b;
        }
    }
}

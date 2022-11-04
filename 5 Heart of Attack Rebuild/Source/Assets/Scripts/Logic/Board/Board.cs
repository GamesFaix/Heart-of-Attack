#define DEBUG

using System;
using System.Collections.Generic;
using HOA.Collections;

namespace HOA 
{ 
    /// <summary>
    /// Instance of game board
    /// </summary>
	public class Board : SessionComponent
	{
        /// <summary>
        /// Smallest possible board size
        /// </summary>
        public static size2 minSize = new size2(6, 6);
        /// <summary>
        /// Largest possible board size
        /// </summary>
        public static size2 maxSize = new size2(24, 24);

        private Matrix<Cell> cells;
        /// <summary>
        /// Accessor for internal Matrix of cells.
        /// </summary>
        public CellSet Cells { get { return new CellSet(cells); } }

        /// <summary>
        /// Two-dimensional size of board
        /// </summary>
        public size2 Size { get { return cells.Size; } }
        /// <summary>
        /// Count of cells in board
        /// </summary>
        public int Count { get { return Size.Product; } }

        /// <summary>
        /// Creates new empty matrix.
        /// Throws exception if illegal size.
        /// </summary>
        /// <param name="size"></param>
        public Board(Session session, size2 size) : base(session)
        {
            if (!LegalSize(size))
                throw new ArgumentOutOfRangeException(
                    "Board size must be between "+minSize+" and "+maxSize+".");
            cells = new Matrix<Cell>(size);
#if DEBUG
            int cellCount = 0;
#endif
            foreach (index2 index in Size)
            {
                cells[index] = new Cell(this, index);
#if DEBUG
                cellCount++;
#endif
            }
#if DEBUG
            Debug.Log(Size + " Board created (" + cellCount + " cells)");
#endif       
        }

        static bool LegalSize(size2 size)
        {
            return (size.SupersetOf(minSize) && size.SubsetOf(maxSize));
        }

        /// <summary>
        /// Accessor for internal matrix
        /// Matrix throws exception if out of range.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Cell this[index2 index] { get { return cells[index]; } }

        /// <summary>
        /// Returns true if index exists in matrix, outputs item at index
        /// </summary>
        /// <param name="index"></param>
        /// <param name="cell"></param>
        /// <returns></returns>
        public bool HasCell(index2 index, out Cell cell)
        {
            cell = null;
            return cells.Contains(index, out cell);
        }

        /// <summary>
        /// Returns true if there are any cells legal for token to enter,
        /// outputs random example
        /// </summary>
        /// <param name="t"></param>
        /// <param name="outCell"></param>
        /// <returns></returns>
        public bool RandomLegalCell(Token t, out Cell outCell)
        {
            CellSet remainingCells = Cells;
            while (remainingCells.Count > 0)
            {
                Cell cell = (Cell)(remainingCells.Random());
                if (!t.CanEnter(cell)) 
                    remainingCells.Remove(cell);
                else 
                {
                    outCell = cell; 
                    return true; 
                }
            }
            outCell = null;
            return false;	
        }
	}

}

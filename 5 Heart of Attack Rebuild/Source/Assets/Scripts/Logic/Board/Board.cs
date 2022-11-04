#define DEBUG

using System;
using HOA.Sessions;
using HOA.Collections;

namespace HOA.Board
{ 
    /// <summary> Instance of game board </summary>
	public class Board : SessionComponent
	{
        /// <summary> Smallest possible board size </summary>
        public static readonly size2 minSize = new size2(6, 6);
        /// <summary>  Largest possible board size </summary>
        public static readonly size2 maxSize = new size2(24, 24);

        private Matrix<Cell> cells;
        /// <summary> Accessor for internal Matrix of cells. </summary>
        public Set<Cell> Cells { get { return new Set<Cell>(cells); } }

        /// <summary> Two-dimensional size of board </summary>
        public size2 Size { get { return cells.Size; } }
        /// <summary> Count of cells in board </summary>
        public int Count { get { return Size.Product; } }

        /// <summary> Creates new empty matrix.
        /// Throws exception if illegal size. </summary>
        public Board(Session session, size2 size) : base(session)
        {
            if (!LegalSize(size))
                throw new ArgumentOutOfRangeException(
                    String.Format("Board size must be between {0} and {1}.", minSize, maxSize));
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
            Log.Session(Size + " Board created (" + cellCount + " cells)");
#endif       
        }

        static bool LegalSize(size2 size)
        {
            return (size.SupersetOf(minSize) && size.SubsetOf(maxSize));
        }

        /// <summary> Accessor for internal matrix
        /// Matrix throws exception if out of range.  </summary>
        public Cell this[index2 index] { get { return cells[index]; } }

        /// <summary> Returns true if index exists in matrix, outputs item at index </summary>
        public bool HasCell(index2 index, out Cell cell)
        {
            cell = null;
            return cells.Contains(index, out cell);
        }

        /// <summary> Returns true if there are any cells legal for token to enter,
        /// outputs random example </summary>
        public bool RandomLegalCell(Tokens.Token t, out Cell outCell)
        {
            Set<Cell> remainingCells = Cells;
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

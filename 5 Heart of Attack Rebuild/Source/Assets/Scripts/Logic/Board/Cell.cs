using System;
using System.Collections.Generic;
using HOA.Tokens;
using HOA.Collections;

namespace HOA
{

    public class Cell : IEntity
    {
        /// <summary>
        /// Can cell currently be selected?
        /// </summary>
        public bool Legal { get; set; }


        /// <summary>
        /// Board this Cell belongs to.
        /// </summary>
        public Board Board { get; private set; }

        /// <summary>
        /// Board coordinates
        /// </summary>
        public index2 Index { get; private set; }
        /// <summary>
        /// X component of board coordinates
        /// </summary>
        public int x { get { return Index.x; } }
        /// <summary>
        /// Y component of board coordinates
        /// </summary>
        public int y { get { return Index.y; } }

        public Tokens.Plane Stop { get; set; }
        public bool CanStop(Token t) { return (Stop.ContainsAny(t.plane)); }

        /// <summary>
        /// Non-adjacent cells to be treated as neighbors.
        /// </summary>
        public CellSet Links { get; set; }


        /// <summary>
        /// Tokens currently in this cell
        /// </summary>
        public TokenSet Occupants { get; private set; }

        public Cell(Board board, index2 i)
        {
            Board = board;
            Index = i;
            Links = new CellSet(0);
            Stop = Plane.None;
            Occupants = new TokenSet(4);
            Subscribers = new ListSet<Sensor>(0);
        }

        /// <summary>
        /// Set of all adjacent and linked Cells.  Does not include self.
        /// </summary>
        public CellSet Neighbors
        {
            get
            {
                CellSet neighbors = new CellSet();

                foreach (int2 dir in Direction.Directions)
                {
                    Cell neighbor;
                    index2 index = (index2)((int2)Index + dir);
                    if (Board.HasCell(index, out neighbor))
                        neighbors.Add(neighbor);
                }
                neighbors.Add(Links);
                return neighbors;
            }
        }

        /// <summary>
        /// Neighbors with self added.
        /// </summary>
        public CellSet NeighborsAndSelf
        {
            get
            {
                CellSet set = Neighbors;
                set.Add(this);
                return set;
            }
        }

        /// <summary>
        /// Return "C([x],[y])"
        /// </summary>
        /// <returns></returns>
        public override string ToString() { return "C(" + x + "," + y + ")"; }

        public void Enter(Token t)
        {
            if (!t.CanEnter(this))
                throw new Exception("Illegal cell entrance.");
            Occupants.Add(t);
            OccupationPublish(this, t, true);
        }

        public void Exit(Token t)
        {
            if (!Occupants.Contains(t))
                throw new Exception("Illegal cell exit.");
            Occupants.Remove(t);
            OccupationPublish(this, t, false);
        }

        #region //EventHandler

        public event EventHandler<OccupationEventArgs> OccupationEvent;

        public void OccupationPublish(Cell cell, Token token, bool enter)
        {
            if (OccupationEvent != null)
            {
                OccupationEvent(null, new OccupationEventArgs(cell, token, enter));
                //Debug.Log("Unfinished code: Cell Occupation Event sender null.");
            }
        }

        public ListSet<Sensor> Subscribers { get; private set; }

    }

    /// <summary>
    /// Arguments for Cell enter or exit events.
    /// </summary>
    public class OccupationEventArgs : EventArgs
    {
        /// <summary>
        /// Cell being entered or exited.
        /// </summary>
        public Cell Cell { get; private set; }
        /// <summary>
        /// Token entering or exiting.
        /// </summary>
        public Token Token { get; private set; }
        /// <summary>
        /// True if event is Enter event.
        /// </summary>
        public bool Enter { get; private set; }
        /// <summary>
        /// True if event is Exit event.
        /// </summary>
        public bool Exit { get { return !Enter; } }

        /// <summary>
        /// Assigns parameters to fields.
        /// </summary>
        /// <param name="cell">Cell being entered or exited.</param>
        /// <param name="token">Token entering or exiting.</param>
        /// <param name="enter">Is token entering?</param>
        public OccupationEventArgs(Cell cell, Token token, bool enter)
        {
            Cell = cell;
            Token = token;
            Enter = enter;
        }
    }
}
#endregion
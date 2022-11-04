using System;
using System.Collections.Generic;
using UnityEngine;

namespace HOA
{
    /// <summary>
    /// Contains Tokens, publishes events on Token enter and exit.
    /// </summary>
    public class Cell : Target
    {

        #region //Properties

        /// <summary>
        /// Board that Cell is a part of.
        /// </summary>
        public Board Board { get; protected set; }

        /// <summary>
        /// Tokens currently in this cell.
        /// </summary>
        public TokenSet Occupants { get; protected set; }

        /// <summary>
        /// Cell number (x,y)
        /// </summary>
        public index2 Index { get; protected set; }

        /// <summary>
        /// Index.x
        /// </summary>
        public int X { get { return Index.x; } }
        
        /// <summary>
        /// Index.y
        /// </summary>
        public int Y { get { return Index.y; } }

        /// <summary>
        /// Tokens in any of these planes can enter but not travel through this Cell.
        /// </summary>
        public Plane Stop { get; set; }

        /// <summary>
        /// Physical location of cell model in Unity coordinates.
        /// </summary>
        public Vector3 Location { get { return Display.gameObject.transform.position; } }
        
        /// <summary>
        /// Cells to be treated as if neighboring.  Primarily for Aperture.
        /// </summary>
        public CellSet Links { get; set; }

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
                    index2 index;
                    if (index2.Safe((int2)Index + dir, out index))
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
        /// Set of Tokens subscribing to this Cell's enter and exit events.
        /// </summary>
        public ListSet<Sensor> Subscribers
        { get; set; }

        #endregion

        public override string ToString() { return "(" + X + "," + Y + ")"; }

        protected Cell() { }

        public Cell(Board board, index2 index)
        {
            Board = board;
            Index = index;
            CellDisplay.Attach(this);
            Links = new CellSet();
            Stop = Plane.None;
            Occupants = new TokenSet();
            Subscribers = new ListSet<Sensor>();
        }

        public Plane Occupied
        {
            get
            {
                Plane p = Plane.None;
                foreach (Token t in Occupants)
                    p = p | t.Plane;
                return p;
            }
        }

        public virtual void Enter(Token t)
        {
            Occupants.Add(t);
            OccupationPublish(this, t, true);
        }

        public virtual void Exit(Token t)
        {
            Occupants.Remove(t);
            OccupationPublish(this, t, true);
        }

        public virtual bool StopToken(Token t)
        {
            if ((t.Plane & Plane.All) != Plane.None) return true;
            if (t.Body.CanTrample(this)) return true;
            return false;
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
        public bool Enter {get; private set;}
        /// <summary>
        /// True if event is Exit event.
        /// </summary>
        public bool Exit {get {return !Enter;} }

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

#endregion
}
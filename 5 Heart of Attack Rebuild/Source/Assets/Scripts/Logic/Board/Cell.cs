using System;
using System.Collections.Generic;
using HOA.Tokens;
using HOA.Collections;


namespace HOA.Board
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

        public Plane Stop { get; set; }
        public bool CanStop(Token t) { return (Stop.ContainsAny(t.plane)); }

        /// <summary>
        /// Non-adjacent cells to be treated as neighbors.
        /// </summary>
        public Set<IEntity> Links { get; set; }


        /// <summary>
        /// Tokens currently in this cell
        /// </summary>
        public Set<IEntity> occupants { get; private set; }
        public static Set<IEntity> Occupants(IEntity e) { return (e as Cell).occupants; }
        
        public Cell(Board board, index2 i)
        {
            Board = board;
            Index = i;
            Links = new Set<IEntity>(0);
            Stop = Plane.None;
            occupants = new Set<IEntity>(4);
            Subscribers = new Set<Sensor>(0);
        }

        /// <summary>
        /// Set of all adjacent and linked Cells.  Does not include self.
        /// </summary>
        public Set<IEntity> Neighbors
        {
            get
            {
                Set<IEntity> neighbors = new Set<IEntity>();

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
        public Set<IEntity> NeighborsAndSelf
        {
            get
            {
                Set<IEntity> set = Neighbors;
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
            occupants.Add(t);
            Log.Game("{0} has entered {1}.", t, this);
            OccupationPublish(this, t, true);
        }

        public void Exit(Token t)
        {
            if (!occupants.Contains(t))
                throw new Exception("Illegal cell exit.");
            occupants.Remove(t);
            Log.Game("{0} has exited {1}.", t, this); 
            OccupationPublish(this, t, false);
        }

        #region //EventHandler

        public event EventHandler<OccupationEventArgs> OccupationEvent;

        public void OccupationPublish(Cell cell, Token token, bool enter)
        {
            if (OccupationEvent != null)
                OccupationEvent(this, new OccupationEventArgs(cell, token, enter));
        }

        public Set<Sensor> Subscribers { get; private set; }

    }

    
}
#endregion
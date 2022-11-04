using System;
using System.Collections.Generic;

namespace HOA.Tokens
{ 
    /// <summary>
    /// Allows tokens to interact with the board
    /// </summary>
	public class Body : TokenComponent
    {

        #region Properties
        /// <summary>
        /// (Destructible, trample, corpse, heart)
        /// </summary>
        public TokenFlags Flags { get; set; }
        /// <summary>
        /// (Terrain, Sunken, Ground, Air, Ethereal)
        /// </summary>
        public Plane Plane { get; set; }
        
        /// <summary>
        /// Cell currently occupied by token
        /// </summary>
        public Cell Cell { get; private set; }

        public List<Sensor> Sensors { get; set; }
        public List<Timer> Timers { get; set; }

        protected Action<Cell> EnterSpecial;
        public Action Exit;

        #endregion

        /// <summary>
        /// Basic constructor
        /// </summary>
        /// <param name="thisToken">Assigns to TokenComponent.ThisToken</param>
        /// <param name="flags">Assigns to Flags</param>
        /// <param name="plane">Assigns to Plane</param>
        public Body(Token thisToken, Plane plane, TokenFlags flags = TokenFlags.None)
            : base (thisToken)
        {
            Flags = flags;
            Plane = plane;
            Sensors = new List<Sensor>(0);
            EnterSpecial = (c) => { }; 
            Exit = DefaultExit;
        }

        public Set<IEntity> Cellmates { get { return Cell.occupants - ThisToken; } }
        public Set<IEntity> Neighbors
        {
            get
            {
                return Cell.Neighbors
                    .Map<IEntity, Set<IEntity>>(Cell.Occupants)
                    .Merge<Set<IEntity>, IEntity>();
            }
        }
        public Set<IEntity> NeighborsAndCellmates
        {
            get
            {
                return Cell.NeighborsAndSelf
                    .Map<IEntity, Set<IEntity>>(Cell.Occupants)
                    .Merge<Set<IEntity>, IEntity>();
            }
        }
    
        public void Enter(Cell cell)
        {
            if (!CanEnter(cell))
                throw new Exception("Illegal cell entrance!");
            cell.Enter(ThisToken);
        }

        public void Swap(Token other)
        {
            if (!CanSwapWith(other))
                throw new Exception("Illegal cell swap!");

            Cell c = Cell;
            Cell otherC = other.Cell;
            Exit();
            other.Enter(c);
            Enter(otherC);
        }

        public void DefaultExit()
        {
            foreach (Sensor s in Sensors)
                s.OnThisExit(Cell);
            Cell.Exit(ThisToken);
        }


        #region //Occupation status checks

        /// <summary>
        /// Determines if token can enter cell
        /// </summary>
        /// <param name="c"></param>
        /// <returns>False if any untramplable token is already 
        /// in the cell at any of this token's planes, true otherwise.</returns>
        public virtual bool CanEnter(Cell c)
        {
            Set<IEntity> set = c.occupants;
            foreach (Token t in set)
                if (CoplanarWith(t) && !CanTrample(t))
                    return false;
            return true;
        }

        public virtual bool CanAimThru(Cell c)
        {
            Set<IEntity> set = c.occupants;
            foreach (Token t in set)
                if (CoplanarWith(t))
                    return false;
            return true;
        }

        /// <summary>
        /// Do the two tokens share any planes?
        /// </summary>
        /// <param name="t"></param>
        /// <returns>True if any plane shared</returns>
        public bool CoplanarWith(Token t) { return Plane.ContainsAny(t.plane); }
        /// <summary>
        /// Could this token trample the other?
        /// </summary>
        /// <param name="t"></param>
        /// <returns>True if This is Terrain, 
        /// or This is King and other is Heart, 
        /// or This is Trample and other is Destructible.</returns>
        public bool CanTrample(Token t)
        {
            return ((ThisToken is Terrain) 
                || (ThisToken.king && t.heart)
                || (ThisToken.trample && t.destructible));
        }
        /// <summary>
        /// Checks if This token could move into the other's Cell,
        /// if the other weren't there.
        /// </summary>
        /// <param name="t"></param>
        /// <returns>False if any of Other's cellmates are 
        /// coplanar with This and untramplable.</returns>
        public bool CanTakePlaceOf(Token t)
        {
            Set<IEntity> set = t.Cell.occupants - t;
            foreach (Token s in set)
                if (CoplanarWith(s) && !CanTrample(s))
                    return false;
            return true;
        }
        /// <summary>
        /// Checks if two tokens can swap cells.
        /// </summary>
        /// <param name="t"></param>
        /// <returns>True if this.CanTakePlaceOf(other) and other.CanTakePlaceOf(this)</returns>
        public bool CanSwapWith(Token t)
        {
            return (CanTakePlaceOf(t) && t.CanTakePlaceOf(ThisToken));
        }

        #endregion

        /// <summary>
        /// Returns "[ThisToken]'s body"
        /// </summary>
        /// <returns></returns>
        public override string ToString() { return ThisToken + "'s body"; }
	}
}

using System;
using System.Collections.Generic;
using HOA.Collections;
using Cell = HOA.Board.Cell;

namespace HOA.Tokens
{
    public abstract partial class Token
    {
        
        #region Flags

        protected TokenFlags flags { get { return body.flags; } }
        
        public bool destructible { get { return flags.ContainsAny(TokenFlags.Destructible); } }
        public bool trample { get { return flags.ContainsAny(TokenFlags.Trample); } }
        public bool corpse { get { return flags.ContainsAny(TokenFlags.Corpse); } }
        public bool heart { get { return flags.ContainsAny(TokenFlags.Heart); } }
        public bool king { get { return (this is Unit && (this as Unit).rank == UnitRank.King); } }

        public void SetFlags(Effects.IEffect source, TokenFlags flags, bool b)
        {
            if (b)
                this.flags.Add(flags);
            else
                this.flags.Remove(flags);
        }

        #endregion

        public Cell cell { get { return body.cell; } }
        public Set<IEntity> neighbors { get { return body.neighbors; } }
        public Set<IEntity> cellmates { get { return body.cellmates; } }
        public Set<IEntity> neighborsAndCellmates { get { return body.neighborsAndCellmates; } }

        #region Methods

        public bool CanEnter(Cell cell) { return body.CanEnter(cell); }
        public void Enter(Cell cell) { body.Enter(cell); }
        public bool CanAimThru(Cell cell) { return body.CanAimThru(cell); }
        public bool CanTakePlaceOf(Token token) { return body.CanTakePlaceOf(token); }
        public void RefreshCell() { Enter(cell); }
        public void Swap(Token other) { body.Swap(other); }

        #endregion

        public List<Sensor> sensors { get { return body.sensors; } }
        public List<Timer> timers { get { return body.timers; } }


        public Plane plane { get { return body.plane; } }
        public void SetPlane(Effects.IEffect effect, Plane plane)
        {
            body.plane = plane;
            RefreshCell();
        }

        protected internal class Body
        {
            #region Properties

            public readonly Token self;
            
            public TokenFlags flags { get; set; }
            public Plane plane { get; set; }

            public Cell cell { get; private set; }
            public Set<IEntity> cellmates { get { return cell.occupants - self; } }
            public Set<IEntity> neighbors
            {
                get
                {
                    return cell.Neighbors
                        .Map<IEntity, Set<IEntity>>(Cell.Occupants)
                        .Merge<Set<IEntity>, IEntity>();
                }
            }
            public Set<IEntity> neighborsAndCellmates
            {
                get
                {
                    return cell.NeighborsAndSelf
                        .Map<IEntity, Set<IEntity>>(Cell.Occupants)
                        .Merge<Set<IEntity>, IEntity>();
                }
            }
            public List<Sensor> sensors { get; set; }
            public List<Timer> timers { get; set; }

            protected Action<Cell> EnterSpecial;
            public Action Exit;

            #endregion

            public Body(Token self, Plane plane, TokenFlags flags = TokenFlags.None)
            {
                this.self = self;
                this.flags = flags;
                this.plane = plane;
                sensors = new List<Sensor>(0);
                timers = new List<Timer>(0);
                EnterSpecial = (c) => { };
                Exit = DefaultExit;
            }

            public void Enter(Cell cell)
            {
                if (!CanEnter(cell))
                    throw new Exception("Illegal cell entrance!");
                cell.Enter(self);
            }

            public void Swap(Token other)
            {
                if (!CanSwapWith(other))
                    throw new Exception("Illegal cell swap!");

                var c1 = cell;
                var c2 = other.cell;
                Exit();
                other.Enter(c1);
                Enter(c2);
            }

            public void DefaultExit()
            {
                foreach (Sensor s in sensors)
                    s.OnThisExit(cell);
                cell.Exit(self);
            }


            #region //Occupation status checks

            /// <summary>  Determines if token can enter cell </summary>
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

            /// <summary>  Do the two tokens share any planes? </summary>
            /// <returns>True if any plane shared</returns>
            public bool CoplanarWith(Token t) { return plane.ContainsAny(t.plane); }
            /// <summary> Could this token trample the other? </summary>
            /// <returns>True if This is Terrain, 
            /// or This is King and other is Heart, 
            /// or This is Trample and other is Destructible.</returns>
            public bool CanTrample(Token t)
            {
                return ((self is Terrain)
                    || (self.king && t.heart)
                    || (self.trample && t.destructible));
            }
            /// <summary>  Checks if This token could move into the other's Cell,
            /// if the other weren't there. </summary>
            /// <returns>False if any of Other's cellmates are 
            /// coplanar with This and untramplable.</returns>
            public bool CanTakePlaceOf(Token t)
            {
                Set<IEntity> set = t.cell.occupants - t;
                foreach (Token s in set)
                    if (CoplanarWith(s) && !CanTrample(s))
                        return false;
                return true;
            }
            /// <summary>  Checks if two tokens can swap cells.  </summary>
            /// <returns>True if this.CanTakePlaceOf(other) and other.CanTakePlaceOf(this)</returns>
            public bool CanSwapWith(Token t)
            {
                return (CanTakePlaceOf(t) && t.CanTakePlaceOf(self));
            }

            #endregion

            /// <summary> Returns "[self]'s body" </summary>
            public override string ToString() { return self + "'s body"; }
        }

    }
}
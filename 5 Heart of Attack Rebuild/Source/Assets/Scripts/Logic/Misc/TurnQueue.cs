using UnityEngine;
using System;
using System.Collections.Generic;

namespace HOA
{
    /// <summary>
    /// Maintains Unit turn order
    /// </summary>
    public class TurnQueue : SessionComponent
    {
        #region Properties

        private TokenSet units;

        /// <summary>Number of units</summary>
        public int Count { get { return units.Count; } }

        /// <summary>Returns first unit</summary>
        public Unit Top { get { return (units.Count > 0 ? units[0] as Unit : null); } }
        
        /// <summary>Returns last unit</summary>
        public Unit Bottom { get { return (units.Count > 0 ? units[Count - 1] as Unit : null); } }

        #endregion

        #region Constructors

        public TurnQueue(Session session) : base(session)
        {
            units = new TokenSet();
        }

        public TurnQueue(Session session, IEnumerable<Token> tokens) : this (session)
        {
            foreach (Token t in tokens)
                if (t is Unit)
                    units.Add(t);
        }

        #endregion

        #region Methods

        /// <summary>Add a Unit</summary>
        public void Add(Unit u)
        {
            units.Add(u);
            Skip(u);
        }
        
        /// <summary>Remove a Unit</summary>
        public void Remove(Unit u)
        {
            bool flag = false;
            if (u == Top)
                flag = true;
            units.Remove(u);
            if (flag)
                Initialize();
        }
        
        /// <summary>Is Unit in Queue?</summary>
        public bool Contains(Unit u) { return units.Contains(u); }
        
        /// <summary>Basic indexer</summary>
        public Unit this[int i] { get { return units[i] as Unit; } }
        /// <summary>Unit's place in Queue</summary>
        public int IndexOf(Unit u) { return units.IndexOf(u); }

        /// <summary>Shuffles queue</summary>
        public void Shuffle() { units.Shuffle(); }
        
        /// <summary>Shifts Unit up or down in queue</summary>
        /// <param name="distance">Negative values move unit up (turn sooner), positive move down</param>
        public void Shift(Unit u, int distance)
        {
            if (Contains(u))
            {
                int magnitude = Math.Abs(distance);
                for (int i = 0; i < magnitude; i++)
                {
                    int index = IndexOf(u);

                    if (index > 0 && distance < 0)
                    {
                        Unit temp = this[index - 1];
                        Remove(temp);
                        units.Insert(index, temp);
                    }

                    else if (index < Count - 1 && distance > 0)
                    {
                        Unit temp = this[index + 1];
                        Remove(temp);
                        units.Insert(index, temp);
                    }
                }
            }
            else
                throw new Exception("Queue does not contain " + u + ".");

        }

        /// <summary>End current turn, start new turn</summary>
        public void Advance()
        {
            Unit oldTop = Top;
            oldTop.OnTurnEnd();
            units.Remove(oldTop);
            Unit newTop = Top;

            if (oldTop.Health > 0)
            {
                units.Add(oldTop);
                Skip(oldTop);
            }

            TurnChangePublish(oldTop, newTop);

            Initialize();
            if (!Top.Owner.Alive)
                Advance();
        }


        void Skip(Unit oldTop)
        {
            int i = oldTop.Initiative;

            while (i > 1)
            {
                int index = units.IndexOf(oldTop);
                if (index > 1)
                {
                    Unit other = units[index - 1] as Unit;
                    int oldTopRandom = UnityEngine.Random.Range(0, i);
                    int otherRandom = UnityEngine.Random.Range(0, other.Initiative);
                    if (oldTopRandom > otherRandom)
                    {
                        Shift(oldTop, -1);
                        i--;
                    }
                    else i = 0;
                }
                else i = 0;
            }
        }

        /// <summary>Call Top.OnTurnStart</summary>
        public void Initialize() { if (Top != null) Top.OnTurnStart(); }

        #endregion

        #region Events

        /// <summary>
        /// Fires when turn changes, args include last unit and new unit
        /// </summary>
        public event EventHandler<TurnChangeEventArgs> TurnChangeEvent;

        /// <summary>Publish TurnChangeEvent</summary>
        /// <param name="lastUnit">Unit who's turn ended</param>
        /// <param name="newUnit">Unit who's turn is starting</param>
        public void TurnChangePublish(Unit lastUnit, Unit newUnit)
        {
            if (TurnChangeEvent != null)
                TurnChangeEvent(this, new TurnChangeEventArgs(lastUnit, newUnit));
        }
    }

    /// <summary>Arguments for TurnQueue.TurnChangeEvent</summary>
    public class TurnChangeEventArgs : EventArgs
    {
        /// <summary>Unit who's turn just ended.</summary>
        public Unit LastUnit { get; private set; }
        /// <summary>Unit who's turn is starting.</summary>
        public Unit NewUnit { get; private set; }

        /// <summary>Basic constructor</summary>
        public TurnChangeEventArgs(Unit lastUnit, Unit newUnit)
        {
            LastUnit = lastUnit;
            NewUnit = newUnit;
        }
    }


#endregion
}
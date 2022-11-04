using System;
using System.Collections.Generic;
using UnityEngine;

namespace HOA {
    /// <summary>
    /// A Token's Body component allows it to occupy Cells and repsond to Cell events.
    /// </summary>
	public partial class Body : TokenComponent, IInspectable
    {
        /// <summary>
        /// Function that creates a Sensor controlled by Parent in Cell.
        /// </summary>
        /// <param name="parent">Token in control of Sensor.</param>
        /// <param name="cell">Cell to create Sensor in.</param>
        /// <returns>New Sensor</returns>
        public delegate Sensor SensorConstructor(Token parent);
        
        #region //Properties & Fields

        /// <summary>
        /// Can Token be trampled, burned, blown up?
        /// </summary>
        public bool Destructible { get; set; }

        /// <summary>
        /// Can Token move over Destructible Tokens?
        /// </summary>
        public bool Trample { get; set; }

        /// <summary>
        /// Can Token be targeted by Abilities requiring a Corpse?
        /// </summary>
        public bool Corpse { get; set; }

        /// <summary>
        /// The Cell this Token is currently occupying.
        /// </summary>
        public Cell Cell { get; protected set; }

        /// <summary>
        /// Other Tokens in this Token's Cell.
        /// </summary>
        public virtual TokenSet Cellmates
        {
            get
            {
                TokenSet tokens = new TokenSet(Cell.Occupants);
                tokens.Remove(Parent);
                return tokens;
            }
        }

        /// <summary>
        /// Tokens in Cells neighboring this Token's Cell.
        /// </summary>
        public TokenSet Neighbors { get { return Cell.Neighbors.Occupants; } }

        /// <summary>
        /// Set of Neighbors and Cellmates. (Does not include self.)
        /// </summary>
        public TokenSet NeighborsAndCellmates
        {
            get
            {
                TokenSet tokens = Neighbors;
                tokens.Add(Cellmates);
                return tokens;
            }
        }

        /// <summary>
        /// Set of Aliases referencing Body's Parent.  Typically empty.
        /// </summary>
        public ListSet<Alias> Aliases { get; protected set; }

        public ListSet<Sensor> Sensors { get; protected set; }

        protected Action<Cell> EnterSpecial;
        public Action Exit;

        #endregion

        /// <summary>
        /// Extends TokenComponent constructor.  
        /// Creates Sensor and Alias list (blank normally).
        /// Sets default EnterSpecial and Exit methods.
        /// </summary>
        /// <param name="Parent"></param>
        public Body(Token parent, SensorConstructor constructor = null) 
            : base (parent) 
        {
            Aliases = new ListSet<Alias>(0);
            Sensors = new ListSet<Sensor>(0);
            if (constructor != null)
                Sensors.Add(constructor(Parent));
            EnterSpecial = (c) => { };
            Exit = DefaultExit;
        }

        public void DefaultExit()
        {
            foreach (Sensor s in Sensors)
                s.OnParentExit(Cell);
            Cell.Exit(Parent);
        }

        #region //Occupation status checks

        public virtual bool CanEnter (Cell c) {
            if (c is ExoCell) return false;
            TokenSet set = c.Occupants - TargetFilter.Plane(Parent.Plane, true);
            if (set.Count == 0) return true;
            if (CanTrample(c)) return true;
            return false;
		}

        public bool CanTrample(Cell c)
        {
            return TrampleVsDest(c) || KingVsHeart(c);
        }

        protected bool TrampleVsDest(Cell c)
        {
            if (Trample)
                foreach (Token t in c.Occupants)
                    if (t.Body.Destructible && CanTakePlaceOf(t))
                        return true;
            return false;
        }

        protected bool KingVsHeart(Cell c)
        {
            if (Parent is King)
                foreach (Token t in c.Occupants)
                    if (t is Heart && CanTakePlaceOf(t))
                        return true;
            return false;
        }

        public bool CanTakePlaceOf(Token t)
        {
            return Parent.Plane.SubsetOf(t.Plane);
        }

        public bool CanSwapWith(Token t)
        {
            return (CanTakePlaceOf(t) && t.Body.CanTakePlaceOf(Parent));
        }

        #endregion

        #region //Entering

        public bool TryEnter(Cell c, bool smooth)
        {
            if (!CanEnter(c))
                return false;
            Enter(c, smooth);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newCell">Cell to enter.</param>
        /// <param name="smooth">If true, Token model moves slowly to destination.</param>
        /// <returns>Returns false if CanEnter fails, true otherwise.</returns>
		public void Enter (Cell c, bool smooth) {
            if (!CanEnter(c))
                throw new Exception("Illegal occupation!");
    
            if (Cell != null) 
                Exit();
			Cell = c;
			TryTrample(c);
			c.Enter(Parent);
            foreach (Sensor s in Sensors)
                s.OnParentEnter(c);
            EnterSpecial(c);
            if (Parent.Display != null) 
            {
                if (!smooth)
                    ((TokenDisplay)Parent.Display).Enter(Cell);
                else
                    ((TokenDisplay)Parent.Display).MoveTo(Cell);
            }
		}

        protected bool TryTrample(Cell c)
        {
            if (!CanTrample(c))
                return false;
            TrampleCell(c);
            return true;
        }

        protected void TrampleCell(Cell c)
        {
            TokenSet occupants = c.Occupants;
            EffectSet e = new EffectSet();

            if (TrampleVsDest(c))
            {
                TokenSet destructibles = occupants - TargetFilter.Dest;
                for (int i = destructibles.Count - 1; i >= 0; i--)
                    e.Add(Effect.DestroyObstacle(new Source(Parent), (Token)(destructibles[i])));
            }
            if (KingVsHeart(c))
            {
                TokenSet hearts = occupants - TargetFilter.Heart;
                if (hearts.Count > 0)
                    e.Add(Effect.GetHeart(new Source(Parent.Owner), (Token)(hearts[0])));
            }
            EffectQueue.Add(e);
        }

        public bool TrySwapWith(Token t)
        {
            if (!CanSwapWith(t))
                return false;
            CanSwapWith(t);
            return true;

        }

		public void Swap (Token t) {
			if (!CanSwapWith(t)) 
                throw new Exception ("Illegal token swap!");
			Cell oldCell = Cell;
			Cell newCell = t.Body.Cell;

			Exit();
			t.Body.Exit();

			Cell = newCell;
			newCell.Enter(Parent);
			if (Parent.Display != null) 
                ((TokenDisplay)Parent.Display).MoveTo(newCell);

			t.Body.Cell = oldCell;
			oldCell.Enter(t);
			if (t.Display != null) 
                ((TokenDisplay)t.Display).MoveTo(oldCell);
		}

        #endregion

        /// <summary>
        /// Calls InspectorInfo.Body on this.
        /// </summary>
        /// <param name="p">Panel to draw in.</param>
        public override void Draw(Panel p) { InspectorInfo.Body(this, p); }

        /// <summary>
        /// Returns "[Parent]'s Body".
        /// </summary>
        /// <returns></returns>
        public override string ToString() { return Parent + "'s Body"; }

	}
}
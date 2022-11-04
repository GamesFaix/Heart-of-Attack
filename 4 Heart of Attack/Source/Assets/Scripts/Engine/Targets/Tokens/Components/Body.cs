using System;
using System.Collections.Generic;
using UnityEngine;

namespace HOA {

	public partial class Body : TokenComponent, IDeepCopyToken<Body>, IInspectable{

		public bool Destructible { get; set; }
        public bool Trample { get; set; }
        public bool Corpse { get; set; }

		public Body(Token Parent) 
            : base (Parent) { }

		public Body DeepCopy (Token parent) {return new Body(parent);}

		public Cell Cell {get; set;}
        public virtual void DestroySensors() { }

		public TokenSet Neighbors (bool cellMates = false) {
            TokenSet neighbors = Cell.Neighbors().Occupants;
			if (cellMates) {neighbors.Add(CellMates);}
			return neighbors;
		}

        public virtual TokenSet CellMates
        {
			get {
                TokenSet cellMates = Cell.Occupants;
				cellMates.Remove(Parent);
				return cellMates;
			}
		}

		public virtual bool CanEnter (Cell newCell) {
            if (newCell is ExoCell) return false;
            TokenSet set = newCell.Occupants;
            set -= TargetFilter.Plane(Parent.Plane, true);
            if (set.Count == 0 
                || CanTrample(Parent, newCell)) 
            	return true;
            return false;
		}

		public bool Enter (Cell newCell) {
			if (CanEnter(newCell)) {
				if (Cell != null) {Exit();}
				Cell = newCell;
				TrampleCell(Parent, newCell);
				newCell.Enter(Parent);
				EnterSpecial(newCell);
				if (Parent.Display != null) {((TokenDisplay)Parent.Display).Enter(Cell);}
				return true;
			}	
			return false;
		}

		public bool MoveTo (Cell newCell) {
			if (CanEnter(newCell)) {
				if (Cell != null) {Exit();}
				Cell = newCell;
				TrampleCell(Parent, newCell);
				newCell.Enter(Parent);
				EnterSpecial(newCell);
				if (Parent.Display != null) {((TokenDisplay)Parent.Display).MoveTo(Cell);}
				return true;
			}	
			return false;
		}

		protected virtual void EnterSpecial (Cell newCell) {}

		public bool Swap (Token other) {
			if (CanSwap(Parent, other)) {
				Cell oldCell = Cell;
				Cell newCell = other.Body.Cell;

				Exit();
				other.Body.Exit();

				Cell = newCell;
				newCell.Enter(Parent);
				if (Parent.Display != null) {((TokenDisplay)Parent.Display).MoveTo(newCell);}

				other.Body.Cell = oldCell;
				oldCell.Enter(other);
				if (other.Display != null) {((TokenDisplay)other.Display).MoveTo(oldCell);}

				return true;
			}	
			return false;
		}

        public override void Draw(Panel p) { InspectorInfo.Body(this, p); }

        public override string ToString()
        {
            return Parent + "'s Body";
        }

		public virtual void Exit () {Cell.Exit(Parent);}
	}
}
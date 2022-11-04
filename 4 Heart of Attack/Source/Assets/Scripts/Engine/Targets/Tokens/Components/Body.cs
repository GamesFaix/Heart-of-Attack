using System;
using System.Collections.Generic;
using UnityEngine;

namespace HOA {

	public partial class Body : IDeepCopyToken<Body>{

		protected Token parent;

		public Body() {}
		public Body(Token t) {parent = t;}

		public Body DeepCopy (Token parent) {return new Body(parent);}

		public Cell Cell {get; set;}
		
		public TargetGroup Neighbors (bool cellMates = false) {
			TargetGroup neighbors = Cell.Neighbors().Occupants;
			if (cellMates) {neighbors.Add(CellMates);}
			return neighbors;
		}
		
		public virtual TargetGroup CellMates {
			get {
				TargetGroup cellMates = Cell.Occupants;
				cellMates.Remove(parent);
				return cellMates;
			}
		}

		public virtual bool CanEnter (Cell newCell) {
			if (!(newCell is ExoCell)) {
				if (!newCell.Occupied(parent.Plane) 
				    || CanTrample(parent, newCell)) {
					return true;
				}
			}
			return false;
		}

		public bool Enter (Cell newCell) {
			if (CanEnter(newCell)) {
				if (Cell != null) {Exit();}
				Cell = newCell;
				Trample(parent, newCell);
				newCell.Enter(parent);
				EnterSpecial(newCell);
				if (parent.Display != null) {((TokenDisplay)parent.Display).Enter(Cell);}
				return true;
			}	
			return false;
		}

		public bool MoveTo (Cell newCell) {
			if (CanEnter(newCell)) {
				if (Cell != null) {Exit();}
				Cell = newCell;
				Trample(parent, newCell);
				newCell.Enter(parent);
				EnterSpecial(newCell);
				if (parent.Display != null) {((TokenDisplay)parent.Display).MoveTo(Cell);}
				return true;
			}	
			return false;
		}

		protected virtual void EnterSpecial (Cell newCell) {}

		public bool Swap (Token other) {
			if (CanSwap(parent, other)) {
				Cell oldCell = Cell;
				Cell newCell = other.Body.Cell;

				Exit();
				other.Body.Exit();

				Cell = newCell;
				newCell.Enter(parent);
				if (parent.Display != null) {((TokenDisplay)parent.Display).MoveTo(newCell);}

				other.Body.Cell = oldCell;
				oldCell.Enter(other);
				if (other.Display != null) {((TokenDisplay)other.Display).MoveTo(oldCell);}

				return true;
			}	
			return false;
		}

		public virtual void Exit () {Cell.Exit(parent);}
	}
}
using System;
using System.Collections.Generic;
using UnityEngine;

namespace HOA {

	public class Body{
		protected Token parent;

		public Body() {}
		public Body(Token t) {parent = t;}

		protected Cell cell;
		public Cell Cell {
			get {return cell;} 
			set {cell = value;}
		}
		
		public virtual TokenGroup Neighbors(bool cellMates = false) {
			TokenGroup neighbors = cell.Neighbors().Occupants;
			if (cellMates) {
				foreach (Token t in CellMates) {neighbors.Add(t);}
			}
			return neighbors;
		}
		
		public virtual TokenGroup CellMates {
			get {
				TokenGroup cellMates = cell.Occupants;
				cellMates.Remove(parent);
				return cellMates;
			}
		}

		public virtual bool CanEnter (Cell newCell) {
			if (!newCell.Occupied(parent.Plane.Value) || CanTrample(newCell)) {return true;}
			return false;
		}
		
		bool CanTakePlaceOf (Token t) {
			Cell otherCell = t.Body.Cell;
			Token blocker;
			
			foreach (EPlane p in parent.Plane.Value) {
				if (otherCell.Contains(p, out blocker)) {
					if (blocker != t) {return false;}
				}
			}
			return true;
		}

		public bool CanSwap (Token other) {
			if (CanTakePlaceOf(other) && other.Body.CanTakePlaceOf(parent)) {return true;}
			return false;
		}

		public bool CanTrample (Cell newCell) {
			if (parent.Type.Is(EType.TRAM)) {
				foreach (Token t in newCell.Occupants) {
					if (t.Type.Is(EType.DEST) && CanTakePlaceOf(t)) {
						return true;
					}
				}
			}
			if (parent.Type.Is(EType.KING)) {
				foreach (Token t in newCell.Occupants) {
					if (t.Type.Is(EType.HEART) && CanTakePlaceOf(t)) {
						return true;
					}
				}
			}
			
			return false;
		}

		public virtual bool Enter (Cell newCell) {
			if (CanEnter(newCell)) {
				if (cell != default(Cell)) {Exit();}
				cell = newCell;
				Trample(newCell);
				newCell.Enter(parent);
				if (parent.Display != null) {parent.Display.MoveTo(cell);}
				return true;
			}	
			if (newCell == TemplateFactory.c) {
				cell = newCell;
				return true;	
			}
			return false;
		}

		public bool Swap (Token other) {
			if (CanSwap(other)) {
				Cell oldCell = cell;
				Cell newCell = other.Body.Cell;

				Exit();
				other.Body.Exit();

				cell = newCell;
				newCell.Enter(parent);
				if (parent.Display != null) {parent.Display.MoveTo(newCell);}

				other.Body.Cell = oldCell;
				oldCell.Enter(other);
				if (other.Display != null) {other.Display.MoveTo(oldCell);}

				return true;
			}	
			return false;
		}

		protected void Trample (Cell newCell) {
			TokenGroup tokens = newCell.Occupants;

			if (parent.Type.Is(EType.TRAM)) {
				TokenGroup dest = tokens.OnlyType(EType.DEST);
				for (int i=dest.Count-1; i>=0; i--) {
					EffectQueue.Add(new EDestruct(new Source(parent), dest[i]));
				}
			}
			if (parent.Type.Is(EType.KING)) {
				TokenGroup heart = tokens.OnlyType(EType.HEART);
				if (heart.Count>0) {
					EffectQueue.Add(new EGetHeart(Source.ActivePlayer, heart[0]));
				}
			}
		}
		
		public virtual void Exit () {
			cell.Exit(parent);
		}
	}
}
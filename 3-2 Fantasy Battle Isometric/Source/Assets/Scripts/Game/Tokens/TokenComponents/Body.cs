using System;
using System.Collections.Generic;
using UnityEngine;

namespace HOA {

	public class Body{
		protected Token parent;
		protected Cell cell;

		protected static int planeCount = Enum.GetNames(typeof(EPlane)).Length;
		protected static int classCount = Enum.GetNames(typeof(EClass)).Length;

		protected List<EPlane> planes;
		protected List<EClass> classes;

		protected EToken deathCode;
				
		public Body() {}
		
		public Body(Token t, EPlane p, EClass c){
			parent = t;
			planes = new List<EPlane> {p};
			classes = new List<EClass> {c};
			OnDeath = EToken.NONE;
		}

		public Body(Token t, EPlane p, List<EClass> c){
			parent = t;
			planes = new List<EPlane> {p};
			classes = c;			
			OnDeath = EToken.NONE;
		}

		public Body(Token t, List<EPlane> p, EClass c){
			parent = t;
			planes = p;
			classes = new List<EClass> {c};
			OnDeath = EToken.NONE;
		}

		public Body(Token t, List<EPlane> p, List<EClass> c){
			parent = t;
			planes = p;
			classes = c;
			OnDeath = EToken.NONE;
		}

		public EToken OnDeath {
			get {return deathCode;}
			set {deathCode = value;}
		}

		public void SetPlane (EPlane p) {planes = new List<EPlane> {p};}
		public void SetPlane (List<EPlane> p) {planes = p;}

		public List<EPlane> Plane {get {return planes;} }

		public void SetClass (EClass c) {classes = new List<EClass> {c};}
		public void SetClass (List<EClass> c) {classes = c;}

		public List<EClass> Class {get {return classes;} }

		public bool IsClass (EClass c){
			if (classes.Contains(c)) {return true;}
			return false;
		}
		
		public void AddClass (EClass c) {if (!classes.Contains(c)) {classes.Add(c);} }
		public void RemoveClass (EClass c) {if (classes.Contains(c)) {classes.Remove(c);} }

		public bool IsPlane (EPlane p){
			if (planes.Contains(p)) {return true;}
			return false;
		}

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
			if (!newCell.Occupied(Plane) || CanTrample(newCell)) {return true;}
			return false;
		}
		
		bool CanTakePlaceOf (Token t) {
			Cell otherCell = t.Cell;
			Token blocker;
			
			foreach (EPlane p in Plane) {
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
			if (IsClass(EClass.TRAM)) {
				foreach (Token t in newCell.Occupants) {
					if (t.IsClass(EClass.DEST) && CanTakePlaceOf(t)) {
						return true;
					}
				}
			}
			if (IsClass(EClass.KING)) {
				foreach (Token t in newCell.Occupants) {
					if (t.IsClass(EClass.HEART) && CanTakePlaceOf(t)) {
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
				Exit();
				cell = other.Cell;
				other.Cell.Enter(parent);
				
				other.Exit();
				other.Body.Cell = oldCell;
				oldCell.Enter(other);
				
				return true;
			}	
			return false;
		}

		protected void Trample (Cell newCell) {
			TokenGroup tokens = newCell.Occupants;

			if (IsClass(EClass.TRAM)) {
				TokenGroup dest = tokens.OnlyClass(EClass.DEST);
				for (int i=dest.Count-1; i>=0; i--) {
					EffectQueue.Add(new EDestruct(new Source(parent), dest[i]));
				}
			}
			if (IsClass(EClass.KING)) {
				TokenGroup heart = tokens.OnlyClass(EClass.HEART);
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
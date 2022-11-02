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

		public virtual bool Enter (Cell newCell) {
			if (CanEnter(newCell)) {
				if (cell != default(Cell)) {Exit();}
				cell = newCell;
				if (CanTrample(newCell)) {Trample(newCell);}
				if (CanGetHeart(newCell)) {GetHeart(newCell);}
				newCell.Enter(parent);
				return true;
			}	
			if (newCell == TemplateFactory.c) {
				cell = newCell;
				return true;	
			}
			return false;
		}
		
		public bool CanEnter (Cell newCell) {
			if (!newCell.Occupied(Plane)) {return true;}
			
			if (CanTrample (newCell)) {return true;}

			if (CanGetHeart (newCell)) {return true;}
			
			return false;
		}
		
		protected bool CanTrample (Cell newCell) {
			if (IsClass(EClass.TRAM) && newCell.Occupied(EPlane.GND)){
				Token t = newCell.Occupant(EPlane.GND);
				if (t.IsClass(EClass.DEST)) {
					return true;
				}
			}
			return false;
		}

		protected bool CanGetHeart (Cell newCell) {
			if (IsClass(EClass.KING) && newCell.Contains(EClass.HEART)){
				return true;
			}
			return false;
		}

		protected void Trample (Cell newCell) {
			if (CanTrample(newCell)) {
				Token dest = newCell.Occupant(EPlane.GND);
				InputBuffer.Submit(new RKill(Source.ActivePlayer, dest));
			}
		}

		protected void GetHeart (Cell newCell) {
			if (CanGetHeart(newCell)) {
				Token heart = newCell.Occupant(EPlane.GND);
				InputBuffer.Submit(new RGetHeart(Source.ActivePlayer, heart));
			}
		}
		
		public virtual void Exit () {
			cell.Exit(parent);
		}
		
		public Cell Cell {get {return cell;} }
		
		public TokenGroup Neighbors(bool cellMates = false) {
			TokenGroup neighbors = cell.Neighbors().Occupants;
			if (cellMates) {
				foreach (Token t in CellMates) {neighbors.Add(t);}
			}
			return neighbors;
		}
		
		public TokenGroup CellMates {
			get {
				TokenGroup cellMates = cell.Occupants;
				cellMates.Remove(parent);
				return cellMates;
			}
		}

		public bool CanSwap (Token other) {
			Cell cell = parent.Cell;
			Cell otherCell = other.Cell;
			Token blocker;

			foreach (EPlane p in Plane) {
				if (otherCell.Contains(p, out blocker)) {
					if (blocker != other) {return false;}
				}
			}
			foreach (EPlane p in other.Plane) {
				if (cell.Contains(p, out blocker)) {
					if (blocker != parent) {return false;}
				}
			}
			return true;
		}

		public bool Swap (Token other) {
			if (CanSwap(other)) {
				Cell oldCell = cell;
				other.Cell.Enter(parent);

				oldCell.Enter(other);

				return true;
			}	
			return false;
		}



	}
}
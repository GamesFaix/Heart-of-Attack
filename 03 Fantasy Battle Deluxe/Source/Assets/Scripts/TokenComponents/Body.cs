using System;
using System.Collections.Generic;
using HOA.Map;
using UnityEngine;

namespace HOA.Tokens.Components {

	public class Body{
		protected Token parent;
		protected Cell cell;

		protected static int planes = Enum.GetNames(typeof(PLANE)).Length;
		protected static int specials = Enum.GetNames(typeof(SPECIAL)).Length;

		protected bool[] plane = new bool[planes];
		protected bool[] special = new bool[specials];

		protected TTYPE deathCode;
				
		public Body() {}
		
		public Body(Token t, PLANE p = PLANE.GND, SPECIAL s=SPECIAL.NONE){
			parent = t;
			SetPlane(p, false);
			SetSpecial(s, false);
			OnDeath = TTYPE.NONE;
		}

		public Body(Token t, PLANE p, SPECIAL[] s){
			parent = t;
			SetPlane(p, false);
			SetSpecial(s, false);			
			OnDeath = TTYPE.NONE;
		}

		public Body(Token t, PLANE[] p, SPECIAL s=SPECIAL.NONE){
			parent = t;
			SetPlane(p, false);
			SetSpecial(s, false);
			OnDeath = TTYPE.NONE;
		}

		public Body(Token t, PLANE[] p, SPECIAL[] s){
			parent = t;
			SetPlane(p, false);
			SetSpecial(s, false);
			OnDeath = TTYPE.NONE;
		}

		public TTYPE OnDeath {
			get {return deathCode;}
			set {deathCode = value;}
		}

		public void SetPlane (PLANE p, bool log=true){
			for (int i=0; i<planes; i++) {plane[i] = false;}
			switch (p){
			case PLANE.SUNK: plane[0]=true; break;
			case PLANE.GND:  plane[1]=true; break;
			case PLANE.AIR:  plane[2]=true; break;
			case PLANE.ETH:  plane[3]=true; break;
			default:
				GameLog.Debug("Attempt to assign invalid plane.");
				break;
			}
			if (log) {GameLog.Out(parent+" plane set to "+PlaneString+".");}
		}
		public void SetPlane (PLANE[] ps, bool log=true){
			for (int i=0; i<planes; i++) {plane[i] = false;}
			foreach (PLANE p in ps){
				switch (p){
				case PLANE.SUNK: plane[0]=true; break;
				case PLANE.GND:  plane[1]=true; break;
				case PLANE.AIR:  plane[2]=true; break;
				case PLANE.ETH:  plane[3]=true; break;
				default:
					GameLog.Debug("Attempt to assign invalid plane.");
					break;
				}
			}
			if (log) {GameLog.Out(parent+" plane set to "+PlaneString+".");}

		}
		public List<PLANE> Plane {
			get {
				List<PLANE> ps = new List<PLANE>();
				if (plane[0]){ps.Add(PLANE.SUNK);}
				if (plane[1]){ps.Add(PLANE.GND);}
				if (plane[2]){ps.Add(PLANE.AIR);}
				if (plane[3]){ps.Add(PLANE.ETH);}
				return ps;
			}
		}
		public void SetSpecial (SPECIAL tc, bool log=true){
			for (int i=0; i<specials; i++) {special[i] = false;}
			switch (tc){
			case SPECIAL.KING: special[1]=true; break;
			case SPECIAL.TRAM: special[2]=true; break;
			case SPECIAL.DEST: special[3]=true; break;
			case SPECIAL.REM:  special[4]=true; break;
			case SPECIAL.HOA:  special[5]=true; break;
			default:
				break;
			}
			if (log) {GameLog.Out(parent+" specials set to "+SpecialString+".");}

		}

		public void SetSpecial (SPECIAL[] ss, bool log=true){
			for (int i=0; i<specials; i++) {special[i] = false;}
			foreach (SPECIAL s in ss){
				switch (s){
				case SPECIAL.KING: special[1]=true; break;
				case SPECIAL.TRAM: special[2]=true; break;
				case SPECIAL.DEST: special[3]=true; break;
				case SPECIAL.REM:  special[4]=true; break;
				case SPECIAL.HOA:  special[5]=true; break;
				default:
					break;
				}
			}
			if (log) {GameLog.Out(parent+" specials set to "+SpecialString+".");}
		}
		public List<SPECIAL> Special(){
			List<SPECIAL> ss = new List<SPECIAL>();
			if (special[1]){ss.Add(SPECIAL.KING);}
			if (special[2]){ss.Add(SPECIAL.TRAM);}
			if (special[3]){ss.Add(SPECIAL.DEST);}
			if (special[4]){ss.Add(SPECIAL.REM);}
			if (special[5]){ss.Add(SPECIAL.HOA);}
			if (!special[1] && !special[2] && !special[3] && !special[4] && !special[5]){ss.Add(SPECIAL.NONE);}
			return ss;
		}
		public bool IsSpecial(SPECIAL s){
			if (s==SPECIAL.KING && special[1]){return true;}
			if (s==SPECIAL.TRAM && special[2]){return true;}
			if (s==SPECIAL.DEST && special[3]){return true;}
			if (s==SPECIAL.REM && special[4]) {return true;}
			if (s==SPECIAL.HOA && special[5]) {return true;}

			return false;
		}
		
		public void AddSpecial(SPECIAL s){
			if (s==SPECIAL.KING){special[1] = true;}
			if (s==SPECIAL.TRAM){special[2] = true;}
			if (s==SPECIAL.DEST){special[3] = true;}
			if (s==SPECIAL.REM) {special[4] = true;}
			if (s==SPECIAL.HOA) {special[5] = true;}
		}
		
		public void RemoveSpecial(SPECIAL s){
			if (s==SPECIAL.KING){special[1] = false;}
			if (s==SPECIAL.TRAM){special[2] = false;}
			if (s==SPECIAL.DEST){special[3] = false;}
			if (s==SPECIAL.REM) {special[4] = false;}
			if (s==SPECIAL.HOA) {special[5] = false;}
		}

		public bool IsPlane(PLANE p){
			if (p==PLANE.SUNK && plane[0]){return true;}
			if (p==PLANE.GND && plane[1]){return true;}
			if (p==PLANE.AIR && plane[2]){return true;}
			if (p==PLANE.ETH && plane[3]) {return true;}
			return false;
		}

		public string PlaneString {
			get {
				string str = "";
				foreach (PLANE p in Plane){
					if (p == PLANE.SUNK){str += "Sunken, ";}
					if (p == PLANE.GND) {str += "Ground, ";}
					if (p == PLANE.AIR) {str += "Air, ";}
					if (p == PLANE.ETH) {str += "Ethereal, ";}
				}
				char[] trim = new char[2]{' ',','};
				return str.Trim(trim);
			}
		}
		public string SpecialString {
			get {
				string str = "";
				foreach (SPECIAL s in Special()){
					if (s == SPECIAL.KING){str += "Attack King, ";}
					if (s == SPECIAL.TRAM){str += "Trample, ";}
					if (s == SPECIAL.DEST){str += "Destructible, ";}
					if (s == SPECIAL.REM) {str += "Remains, ";}
					if (s == SPECIAL.HOA) {str += "Heart of Attack, ";}
				}
				char[] trim = new char[2]{' ',','};
				return str.Trim(trim);
			}
		}
		
		public virtual bool Enter (Cell newCell) {
			if (CanEnter(newCell)) {
				if (cell != default(Cell)) {Exit();}
				cell = newCell;
				if (CanTrample(newCell)) {Trample(newCell);}
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
			
			return false;
		}
		
		protected bool CanTrample (Cell newCell) {
			if (IsSpecial(SPECIAL.TRAM) && newCell.Occupied(PLANE.GND)){
				Token t = newCell.Occupant(PLANE.GND);
				if (t.IsSpecial(SPECIAL.DEST)) {
					return true;
				}
			}
			return false;
		}
		
		protected void Trample (Cell newCell) {
			if (CanTrample(newCell)) {
				Token t = newCell.Occupant(PLANE.GND);
				InputBuffer.Submit(new RKill(Source.ActivePlayer, t));
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
	}
}
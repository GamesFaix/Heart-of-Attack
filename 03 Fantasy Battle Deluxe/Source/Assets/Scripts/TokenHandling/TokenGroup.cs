using System;
using System.Collections.Generic;
using UnityEngine;
using HOA.Players;
using HOA.Map;
using HOA.Actions;

namespace HOA.Tokens {

	public class TokenGroup : Group<Token> {
		public TokenGroup () {list = new List<Token>();}
		public TokenGroup (Token t) {list = new List<Token>{t};}
		public TokenGroup (List<Token> t) {list = t;}

		//filters
		public TokenGroup FilterOwner(Player p){
			TokenGroup filtered = new TokenGroup();
			foreach (Token t in list) {
				if (t.Owner != p) {filtered.Add(t);}
			}
			return filtered;
		}
		public TokenGroup FilterPlane(PLANE p){
			TokenGroup filtered = new TokenGroup();
			foreach (Token t in list) {
				if (t.IsPlane(p)) {filtered.Add(t);}
			}
			return filtered;
		}
		public TokenGroup FilterSpecial(SPECIAL s){
			TokenGroup filtered = new TokenGroup();
			foreach (Token t in list) {
				if (t.IsSpecial(s)) {filtered.Add(t);}
			}
			return filtered;
		}
		public TokenGroup FilterUnit {
			get {
				TokenGroup filtered = new TokenGroup();
				foreach (Token t in list) {
					if (t is Unit) {filtered.Add(t);}
				}
				return filtered;
			}
		}
	
		public TokenGroup FilterObstacle {
			get {
				TokenGroup filtered = new TokenGroup();
				foreach (Token t in list) {
					if (t is Obstacle) {filtered.Add(t);}
				}
				return filtered;
			}
		}
		
		public TokenGroup FilterDest (bool rem=false) {
			TokenGroup filtered = new TokenGroup();
			foreach (Token t in list) {
				if (t.IsSpecial(SPECIAL.DEST)) {filtered.Add(t);}
				if (!rem) {
					if (t.IsSpecial(SPECIAL.REM)) {filtered.Remove(t);}
				}
			}
			return filtered;
		}			
			
		public TokenGroup FilterUnitDest {
			get {
				TokenGroup filtered = new TokenGroup () ;
				foreach (Token t in list) {
					if (t is Unit || t.IsSpecial(SPECIAL.DEST)) {
						filtered.Add(t);
					}
				}
				return filtered;
			}
		}
		
		public TokenGroup FilterRem {
			get {
				TokenGroup filtered = new TokenGroup ();
				foreach (Token t in list) {
					if (t.IsSpecial(SPECIAL.DEST)) {
						filtered.Add(t);
					}
				}
				return filtered;
			}
		}
			
		public TokenGroup FilterTarget (TTAR ttar) {
			switch (ttar) {
				case TTAR.UNIT:
					return FilterUnit;
				case TTAR.UNITDEST:
					return FilterUnitDest;
				case TTAR.DEST:
					return FilterDest(false);
				case TTAR.DESTREM:
					return FilterDest(true);
				case TTAR.REM:
					return FilterRem;
				default:
					return new TokenGroup();
			}
		}
		
		public CellGroup Cells {
			get {
				CellGroup cells = new CellGroup();
				foreach (Token t in list) {
					cells.Add(t.Cell);
				}
				return cells;
			}
		}
	}
}
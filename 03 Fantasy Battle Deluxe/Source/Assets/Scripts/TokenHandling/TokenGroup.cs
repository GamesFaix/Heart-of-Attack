using System;
using System.Collections.Generic;
using UnityEngine;
using HOA.Players;
using HOA.Map;
using HOA.Actions;

namespace HOA.Tokens {


	public class TokenGroup {
		List<Token> list;
		//constructors
		public TokenGroup () {list = new List<Token>();}
		public TokenGroup (Token t) {list = new List<Token>{t};}
		public TokenGroup (List<Token> tokens) {list = tokens;}
	
		//list stuff
		public void Add (Token t, bool duplicate=false) {
			if (!duplicate && list.Contains(t)) {GameLog.Debug("TokenGroup already contains token.");}
			else {list.Add(t);}
		}
		public void Remove (Token t) {
			if (list.Contains(t)) {list.Remove(t);}
			else {GameLog.Debug("TokenGroup cannot remove token.");}
		}
		public bool Contains (Token t) {
			if (list.Contains(t)) {return true;}
			return false;
		}
	
		public int Count {get {return list.Count;} }

		public Token this[int i] {get { return (Token)this.list[i];} }
		
		public Token Random () {
			int rand = RandomSync.Range(0, Count);	
			return list[rand];
		}
		
	
		public MyEnumerator GetEnumerator() {return new MyEnumerator(this);}
	
		public class MyEnumerator {
			int n;
			TokenGroup bufferGroup;
	
			public MyEnumerator(TokenGroup inputGroup) {
				bufferGroup = inputGroup; 
				n = -1;
			}
	
			public bool MoveNext() {
				n++;
				return (n < bufferGroup.Count);
			}
			public Token Current {
				get {return bufferGroup[n];} 
			}
		}
	
	
	
		public override string ToString () {
			string[] strings = new string[list.Count];
			for (int i=0; i<list.Count; i++) {
				strings[i] = list[i].CodeInst();
			}
			return String.Join(" ",strings);
		}

		//filters
		public TokenGroup FilterOwner(Player p){
			TokenGroup filtered = new TokenGroup();
			foreach (Token t in list) {
				if (t.Owner() != p) {filtered.Add(t);}
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
		public TokenGroup FilterUnit(){
			TokenGroup filtered = new TokenGroup();
			foreach (Token t in list) {
				if (t is Unit) {filtered.Add(t);}
			}
			return filtered;
		}
	
		public TokenGroup FilterObstacle(){
			TokenGroup filtered = new TokenGroup();
			foreach (Token t in list) {
				if (t is Obstacle) {filtered.Add(t);}
			}
			return filtered;
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
			
		public TokenGroup FilterUnitDest () {
			TokenGroup filtered = new TokenGroup () ;
			foreach (Token t in list) {
				if (t is Unit || t.IsSpecial(SPECIAL.DEST)) {
					filtered.Add(t);
				}
			}
			return filtered;
		}
		
		public TokenGroup FilterRem () {
			TokenGroup filtered = new TokenGroup ();
			foreach (Token t in list) {
				if (t.IsSpecial(SPECIAL.DEST)) {
					filtered.Add(t);
				}
			}
			return filtered;
		}
			
		public TokenGroup FilterTarget (TTAR ttar) {
			switch (ttar) {
				case TTAR.UNIT:
					return FilterUnit();
				case TTAR.UNITDEST:
					return FilterUnitDest();
				case TTAR.DEST:
					return FilterDest(false);
				case TTAR.DESTREM:
					return FilterDest(true);
				case TTAR.REM:
					return FilterRem();
				default:
					return new TokenGroup();
			}
		}
		
		public CellGroup Cells(){
			CellGroup cells = new CellGroup();
			foreach (Token t in list) {
				cells.Add(t.Cell());
			}
			return cells;
		}
	}
}
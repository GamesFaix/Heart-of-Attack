using System;
using System.Collections.Generic;
using UnityEngine;

namespace HOA {

	public class TokenGroup : Group<Token> {
		public TokenGroup (int capacity=4) {list = new List<Token>(capacity);}
		public TokenGroup (Token t, int capacity=4) {list = new List<Token>(capacity){t};}
		public TokenGroup (IEnumerable<Token> t) {list = new List<Token>(t);}

		//filters
		public TokenGroup OnlyOwner(Player p){
			TokenGroup filtered = new TokenGroup();
			foreach (Token t in list) {
				if (t.Owner == p) {filtered.Add(t);}
			}
			return filtered;
		}
		public TokenGroup RemoveOwner(Player p){
			TokenGroup filtered = new TokenGroup();
			foreach (Token t in list) {
				if (t.Owner != p) {filtered.Add(t);}
			}
			return filtered;
		}

		public TokenGroup OnlyPlane(EPlane p){
			TokenGroup filtered = new TokenGroup();
			foreach (Token t in list) {
				if (t.Plane.Is(p)) {filtered.Add(t);}
			}
			return filtered;
		}
		public TokenGroup RemovePlane(EPlane p){
			TokenGroup filtered = new TokenGroup();
			foreach (Token t in list) {
				if (!t.Plane.Is(p)) {filtered.Add(t);}
			}
			return filtered;
		}

		public TokenGroup OnlyType(ESpecial c){
			TokenGroup filtered = new TokenGroup();
			foreach (Token t in list) {
				if (t.Special.Is(c)) {filtered.Add(t);}
			}
			return filtered;
		}
		public TokenGroup OnlyType(Special cs){
			TokenGroup filtered = new TokenGroup();
			foreach (Token t in list) {
				foreach (ESpecial c in cs) {
					if (t.Special.Is(c)) {filtered.Add(t);}
				}
			}
			return filtered;
		}


		public TokenGroup RemoveType(ESpecial c){
			TokenGroup filtered = new TokenGroup();
			foreach (Token t in list) {
				if (!t.Special.Is(c)) {filtered.Add(t);}
			}
			return filtered;
		}
		public TokenGroup RemoveType(Special cs){
			TokenGroup filtered = new TokenGroup();
			foreach (Token t in list) {filtered.Add(t);}

			for (int i=filtered.Count-1; i>=0; i--) {
				Token t = filtered[i];
				foreach (ESpecial c in cs) {
					if (t.Special.Is(c)) {filtered.Remove(t);}
				}
			}
			return filtered;
		}

		public TokenGroup OnlyToken (EToken code) {
			TokenGroup filtered = new TokenGroup();
			foreach (Token t in list) {
				if (t.ID.Code == code) {filtered.Add(t);}
			}
			return filtered;
		}


		public static TokenGroup operator / (TokenGroup g, Player p) {return g.RemoveOwner(p);}
		public static TokenGroup operator % (TokenGroup g, Player p) {return g.OnlyOwner(p);}
		public static TokenGroup operator / (TokenGroup g, EPlane p) {return g.RemovePlane(p);}
		public static TokenGroup operator % (TokenGroup g, EPlane p) {return g.OnlyPlane(p);}
		public static TokenGroup operator / (TokenGroup g, Special c) {return g.RemoveType(c);}
		public static TokenGroup operator % (TokenGroup g, Special c) {return g.OnlyType(c);}
		public static TokenGroup operator / (TokenGroup g, ESpecial c) {return g.RemoveType(c);}
		public static TokenGroup operator % (TokenGroup g, ESpecial c) {return g.OnlyType(c);}

		public TokenGroup Restrict (Token Parent, Aim a) {
			TokenGroup restricted = new TokenGroup (this);
			restricted = restricted.OnlyType(a.Special);
			if (a.TeamOnly) {restricted = restricted.OnlyOwner(Parent.Owner);}
			if (a.EnemyOnly) {restricted = restricted.RemoveOwner(Parent.Owner);}
			if (a.NoKings) {restricted = restricted.RemoveType(ESpecial.KING);}
			if (!a.IncludeSelf) {restricted.Remove(Parent);}
			return restricted;
		}

		public CellGroup Cells {
			get {
				CellGroup cells = new CellGroup();
				foreach (Token t in list) {
					cells.Add(t.Body.Cell);
				}
				return cells;
			}
		}

		public Group<Unit> Units {
			get {
				Group<Unit> units = new Group<Unit>();
				foreach (Token t in list) {
					if (t is Unit) {units.Add((Unit)t);}
				}
				return units;
			}
		}

		public void Legalize (bool b=true) {
			foreach (Token t in list) {t.Legal = b;}	
		}
	}
}
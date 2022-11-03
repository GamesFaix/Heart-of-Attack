using System;
using System.Collections.Generic;
using UnityEngine;

namespace HOA {

	public class TokenGroup : Group<Token> {
		public TokenGroup () {list = new List<Token>();}
		public TokenGroup (Token t) {list = new List<Token>{t};}
		public TokenGroup (List<Token> t) {list = t;}
		public TokenGroup (TokenGroup tg) {
			list = new List<Token>();
			foreach (Token t in tg) {list.Add(t);}
		}


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

		public TokenGroup OnlyClass(EType c){
			TokenGroup filtered = new TokenGroup();
			foreach (Token t in list) {
				if (t.Type.Is(c)) {filtered.Add(t);}
			}
			return filtered;
		}
		public TokenGroup OnlyClass(List<EType> cs){
			TokenGroup filtered = new TokenGroup();
			foreach (Token t in list) {
				foreach (EType c in cs) {
					if (t.Type.Is(c)) {filtered.Add(t);}
				}
			}
			return filtered;
		}


		public TokenGroup RemoveClass(EType c){
			TokenGroup filtered = new TokenGroup();
			foreach (Token t in list) {
				if (!t.Type.Is(c)) {filtered.Add(t);}
			}
			return filtered;
		}
		public TokenGroup RemoveClass(List<EType> cs){
			TokenGroup filtered = new TokenGroup();
			foreach (Token t in list) {filtered.Add(t);}

			for (int i=filtered.Count-1; i>=0; i--) {
				Token t = filtered[i];
				foreach (EType c in cs) {
					if (t.Type.Is(c)) {filtered.Remove(t);}
				}
			}
			return filtered;
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

		public void Legalize (bool b=true) {
			foreach (Token t in list) {t.Legalize(b);}	
		}
	}
}
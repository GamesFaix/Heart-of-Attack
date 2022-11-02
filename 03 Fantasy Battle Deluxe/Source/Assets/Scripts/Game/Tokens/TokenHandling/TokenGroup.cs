using System;
using System.Collections.Generic;
using UnityEngine;

namespace HOA {

	public class TokenGroup : Group<Token> {
		public TokenGroup () {list = new List<Token>();}
		public TokenGroup (Token t) {list = new List<Token>{t};}
		public TokenGroup (List<Token> t) {list = t;}

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
				if (t.IsPlane(p)) {filtered.Add(t);}
			}
			return filtered;
		}
		public TokenGroup RemovePlane(EPlane p){
			TokenGroup filtered = new TokenGroup();
			foreach (Token t in list) {
				if (!t.IsPlane(p)) {filtered.Add(t);}
			}
			return filtered;
		}

		public TokenGroup OnlyClass(EClass c){
			TokenGroup filtered = new TokenGroup();
			foreach (Token t in list) {
				if (t.IsClass(c)) {filtered.Add(t);}
			}
			return filtered;
		}
		public TokenGroup OnlyClass(List<EClass> cs){
			TokenGroup filtered = new TokenGroup();
			foreach (Token t in list) {
				foreach (EClass c in cs) {
					if (t.IsClass(c)) {filtered.Add(t);}
				}
			}
			return filtered;
		}


		public TokenGroup RemoveClass(EClass c){
			TokenGroup filtered = new TokenGroup();
			foreach (Token t in list) {
				if (!t.IsClass(c)) {filtered.Add(t);}
			}
			return filtered;
		}
		public TokenGroup RemoveClass(List<EClass> cs){
			TokenGroup filtered = new TokenGroup();
			foreach (Token t in list) {filtered.Add(t);}

			for (int i=filtered.Count-1; i>=0; i--) {
				Token t = filtered[i];
				foreach (EClass c in cs) {
					if (t.IsClass(c)) {filtered.Remove(t);}
				}
			}
			return filtered;
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
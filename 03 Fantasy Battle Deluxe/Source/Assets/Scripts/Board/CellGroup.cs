using System.Collections.Generic;
using HOA.Tokens;

namespace HOA.Map {

	public class CellGroup : Group<Cell> {
		public CellGroup () {list = new List<Cell>();}
		public CellGroup (Cell c) {list = new List<Cell>{c};}
		public CellGroup (List<Cell> c) {list = c;}

		public TokenGroup Occupants {
			get {
				TokenGroup occupants = new TokenGroup();
				foreach (Cell c in list) {
					foreach (Token t in c.Occupants) {occupants.Add(t);}
				}
				return occupants;
			}
		}
			
		public void Legalize (bool b=true) {
			foreach (Cell c in list) {c.Legal = b;}	
		}
		
		public CellGroup Legal () {
			CellGroup legal = new CellGroup();
			foreach (Cell c in list) {
				if (c.Legal) {legal.Add(c);}
			}
			return legal;
		}
		
		public CellGroup Illegal () {
			CellGroup illegal = new CellGroup();
			foreach (Cell c in list) {
				if (!c.Legal) {illegal.Add(c);}
			}
			return illegal;
		}
	}
}

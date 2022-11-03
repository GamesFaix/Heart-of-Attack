using System.Collections.Generic;

namespace HOA {

	public class CellGroup : Group<Cell> {
		public CellGroup (int capacity=8) {list = new List<Cell>(capacity);}
		public CellGroup (Cell c, int capacity=8) {list = new List<Cell>(capacity){c};}
		public CellGroup (IEnumerable<Cell> c) {list = new List<Cell>(c);}

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

		public CellGroup Occupiable (Token t) {
			CellGroup occupiable = new CellGroup();
			foreach (Cell c in list) {
				if (t.Body.CanEnter(c)) {
					occupiable.Add(c);
				}
			}
			return occupiable;
		}

		public static CellGroup operator + (CellGroup a, CellGroup b) {a.Add(b); return a;}
		public static CellGroup operator + (CellGroup a, Cell b) {a.Add(b); return a;}

		public static CellGroup operator - (CellGroup a, CellGroup b) {a.Remove(b); return a;}
		public static CellGroup operator - (CellGroup a, Cell b) {a.Remove(b); return a;}

		public static CellGroup operator / (CellGroup cells, Token t) {return cells.Occupiable(t);}
		public static CellGroup operator - (CellGroup cells, Token t) {return new CellGroup(cells) - cells.Occupiable(t);}

		public static implicit operator TargetGroup(CellGroup cells) {
			TargetGroup targets = new TargetGroup();
			targets.Add(cells);
			return targets;
		}

	}
}

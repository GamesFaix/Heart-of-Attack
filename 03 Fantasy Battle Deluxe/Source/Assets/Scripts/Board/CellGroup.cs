using System.Collections.Generic;
using HOA.Tokens;

namespace HOA.Map {

	public class CellGroup {
		
		List<Cell> cells;
		
		public CellGroup () {
			cells = new List<Cell>();
		}
		
		public CellGroup (Cell c) {
			cells = new List<Cell>{c};
		}
		
		
		public bool Contains (Cell c) {
			if (cells.Contains(c)) {return true;}
			return false;
		}
		
		public void Add (Cell c) {if (!Contains(c)) {cells.Add(c);}}
		
		public void Remove (Cell c) {if (Contains(c)) {cells.Remove(c);}}
		
		public int Count {get {return cells.Count;} }

		public Cell this[int i] {get { return (Cell)this.cells[i];} }
		
		public TokenGroup Occupants () {
			TokenGroup occupants = new TokenGroup();
			foreach (Cell c in cells) {
				foreach (Token t in c.Occupants()) {
					if (!occupants.Contains(t)) {occupants.Add(t);}
				}
			}
			return occupants;
		}
			
		public void Legalize (bool b=true) {
			foreach (Cell c in cells) {c.Legalize(b);}	
		}
		
		public CellGroup Legal () {
			CellGroup legal = new CellGroup();
			foreach (Cell c in cells) {
				if (c.IsLegal()) {legal.Add(c);}
			}
			return legal;
		}
		
		public CellGroup Illegal () {
			CellGroup illegal = new CellGroup();
			foreach (Cell c in cells) {
				if (!c.IsLegal()) {illegal.Add(c);}
			}
			return illegal;
		}
		
		public MyEnumerator GetEnumerator() {return new MyEnumerator(this);}
	
		public class MyEnumerator {
			int n;
			CellGroup buffer;
	
			public MyEnumerator(CellGroup input) {
				buffer = input; 
				n = -1;
			}
	
			public bool MoveNext() {
				n++;
				return (n < buffer.Count);
			}
			public Cell Current {
				get {return buffer[n];} 
			}
		}
	
		
	}
}

using System.Collections.Generic;

namespace HOA {
	
	public class TargetGroup : Group<Target> {
		public TargetGroup (int capacity=8) {list = new List<Target>(capacity);}
		public TargetGroup (Target t, int capacity=8) {list = new List<Target>(capacity){t};}
		public TargetGroup (IEnumerable<Target> t) {list = new List<Target>(t);}

		public void Legalize (bool b=true) {
			foreach (Target t in list) {t.Legal = b;}	
		}
		
		public TargetGroup Legal () {
			TargetGroup legal = new TargetGroup();
			foreach (Target t in list) {
				if (t.Legal) {legal.Add(t);}
			}
			return legal;
		}
		
		public TargetGroup Illegal () {
			TargetGroup illegal = new TargetGroup();
			foreach (Target t in list) {
				if (!t.Legal) {illegal.Add(t);}
			}
			return illegal;
		}

		public TargetGroup Cells () {
			TargetGroup cells = new TargetGroup();
			foreach (Target t in list) {
				if (t is Cell) {cells.Add(t);}
			}
			return cells;
		}

		public TargetGroup Tokens () {
			TargetGroup tokens = new TargetGroup();
			foreach (Target t in list) {
				if (t is Token) {tokens.Add(t);}
			}
			return tokens;
		}

		public void Add (Cell c) {
			if (!list.Contains(c)) {list.Add(c);} 
		}
		public void Add (IEnumerable<Cell> cg) {
			foreach (Cell c in cg) {
				if (!list.Contains(c)) {list.Add(c);}
			}
		}
		public void Add (Token t) {
			if (!list.Contains(t)) {list.Add(t);}
		}
		public void Add (IEnumerable<Token> tg) {
			foreach (Token t in tg) {
				if (!list.Contains(t)) {list.Add(t);}
			}
		}

		public void Remove (Cell c) {
			if (list.Contains(c)) {list.Remove(c);}
		}
		public void Remove (IEnumerable<Cell> cg) {
			foreach (Cell c in cg) {
				if (list.Contains(c)) {list.Remove(c);}
			}
		}

		public void Remove (Token t) {
			if (list.Contains(t)) {list.Remove(t);}
		}
		public void Remove (IEnumerable<Token> tg) {
			foreach (Token t in tg) {
				if (list.Contains(t)) {list.Remove(t);}
			}
		}


	}
}

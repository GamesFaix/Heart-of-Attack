using System.Collections.Generic;

namespace HOA {
	
	public class TargetGroup : Group<ITarget> {
		public TargetGroup (int capacity=8) {list = new List<ITarget>(capacity);}
		public TargetGroup (ITarget t, int capacity=8) {list = new List<ITarget>(capacity){t};}
		public TargetGroup (IEnumerable<ITarget> t) {list = new List<ITarget>(t);}

		public void Legalize (bool b=true) {
			foreach (ITarget t in list) {t.Legalize(b);}	
		}
		
		public TargetGroup Legal () {
			TargetGroup legal = new TargetGroup();
			foreach (ITarget t in list) {
				if (t.IsLegal()) {legal.Add(t);}
			}
			return legal;
		}
		
		public TargetGroup Illegal () {
			TargetGroup illegal = new TargetGroup();
			foreach (ITarget t in list) {
				if (!t.IsLegal()) {illegal.Add(t);}
			}
			return illegal;
		}

		public TargetGroup Cells () {
			TargetGroup cells = new TargetGroup();
			foreach (ITarget t in list) {
				if (t is Cell) {cells.Add(t);}
			}
			return cells;
		}

		public TargetGroup Tokens () {
			TargetGroup tokens = new TargetGroup();
			foreach (ITarget t in list) {
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

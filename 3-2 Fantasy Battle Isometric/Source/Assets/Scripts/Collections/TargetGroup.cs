using System.Collections.Generic;

namespace HOA {
	
	public class TargetGroup : Group<ITarget> {
		public TargetGroup () {list = new List<ITarget>();}
		public TargetGroup (ITarget t) {list = new List<ITarget>{t};}
		public TargetGroup (List<ITarget> t) {list = t;}

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

		public void Add (Cell c) {
			if (!list.Contains(c)) {list.Add(c);}
		}
		public void Add (CellGroup cg) {
			foreach (Cell c in cg) {
				if (!list.Contains(c)) {list.Add(c);}
			}
		}
		public void Add (Token t) {
			if (!list.Contains(t)) {list.Add(t);}
		}
		public void Add (TokenGroup tg) {
			foreach (Token t in tg) {
				if (!list.Contains(t)) {list.Add(t);}
			}
		}

		public void Remove (Cell c) {
			if (list.Contains(c)) {list.Remove(c);}
		}

		public void Remove (CellGroup cg) {
			foreach (Cell c in cg) {
				if (list.Contains(c)) {list.Remove(c);}
			}
		}

		public void Remove (Token t) {
			if (list.Contains(t)) {list.Remove(t);}
		}

		public void Remove (TokenGroup tg) {
			foreach (Token t in tg) {
				if (list.Contains(t)) {list.Remove(t);}
			}
		}
	}
}

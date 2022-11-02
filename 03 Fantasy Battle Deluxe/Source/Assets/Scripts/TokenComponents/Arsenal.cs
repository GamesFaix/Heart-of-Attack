using System.Collections.Generic;
using HOA.Actions;

namespace HOA.Tokens.Components {
	public class Arsenal {
		
		//Unit parent;
		
		List<Action> actions;
		
		public Arsenal (Unit unit) {
			//parent = unit;
			actions = new List<Action>();	
		}
		
		public bool Contains (Action a) {
			if (actions.Contains(a)) {return true;}
			return false;
		}
		public void Add (Action a) {if (!Contains(a)) {actions.Add(a);}}		
		public void Remove (Action a) {if (Contains(a)) {actions.Remove(a);}}
	
		public int Count () {return actions.Count;}
		
		public Action Action (int i) {
			if (i < Count()) {return actions[i];}
			else {return default(Action);}
		}
		
		public bool HasMove (out AMove move) {
			move = default(AMove);
			foreach (Action a in actions) {
				if (a is AMove) {move = (AMove)a; return true;}
			}
			return false;
		}
		
		public void Move () {
			AMove move;
			if (HasMove (out move)) {move.Perform();}
		}
		
		public bool HasFocus (out AFocus focus) {
			focus = default(AFocus);
			foreach (Action a in actions) {
				if (a is AFocus) {focus = (AFocus)a; return true;}
			}
			return false;
		}
		
		public void Focus () {
			AFocus focus;
			if (HasFocus (out focus)) {focus.Perform();}
		}
		
		public void Reset () {
			foreach (Action a in actions) {a.Reset();}
		}
		
		public MyEnumerator GetEnumerator() {return new MyEnumerator(this);}
	
		public class MyEnumerator {
			int n;
			Arsenal buffer;
	
			public MyEnumerator(Arsenal input) {
				buffer = input; 
				n = -1;
			}
	
			public bool MoveNext() {
				n++;
				return (n < buffer.Count());
			}
			public Action Current {
				get {return buffer.Action(n);} 
			}
		}
		
	}
}
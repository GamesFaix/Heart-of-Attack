using System.Collections.Generic;
using HOA.Actions;

namespace HOA.Tokens.Components {
	public class Arsenal : Group<Action> {

		List<Action> actions;
		
		public Arsenal (Unit unit) {
			actions = new List<Action>();	
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
	}
}
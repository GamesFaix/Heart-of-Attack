using System.Collections.Generic;
using UnityEngine;
using System;

namespace HOA {
	public class Arsenal : Group<Action> {
		//Unit parent;
		//List<Action> actions;
		
		public Arsenal (Unit unit) {
		//	parent = unit;
			list = new List<Action>();	
		}

		public void Sort () {list.Sort();}

		public void Reset () {
			foreach (Action a in list) {a.Reset();}
		}

		public Action Move {
			get {
				foreach (Action a in list) {
					if (a.Weight == 1) {return a;}
				}
				return default(Action);
			}
		}

		public Action Action (string name) {
			foreach (Action a in list) {
				if (a.Name == name) {return a;}
			}
			return default(Action);
		}

		public bool Replace (Action oldAct, Action newAct) {
			if (oldAct == null || newAct == null) {throw new Exception("Arsenal.Replace: Null argument.");}
			else if (!list.Contains(oldAct)) {
				throw new Exception ("Arsenal cannot replace action.  Does not contain action '"+oldAct.Name+"'.");
			}
			else {
				list.Remove(oldAct);
				list.Add(newAct);
				Sort();
				return true;
			}
		}

		public bool Replace (string name, Action newAct) {
			Action oldAct = Action(name);
			if (oldAct == null) {throw new Exception("Arsenal.Replace: Does not contain action '"+name+"'.");}
			else {return Replace (oldAct, newAct);}
		}

		public bool Remove (string name) {
			Action act = Action(name);
			if (act == null) {throw new Exception("Arsenal.Remove: Does not contain action '"+name+"'.");}
			else {
				list.Remove(act);
				return true;
			}
		}
	}
}
using System.Collections.Generic;
using UnityEngine;
using System;

namespace HOA {
	public class Arsenal : ListSet<Ability>{
		public Unit Parent {get; private set;}

        public Arsenal (Unit parent) {
			Parent = parent;
		}

        public void Add(IEnumerable<Ability> collection)
        {
            foreach (Ability a in collection)
                Add(a);
        }

		public void Reset () {foreach (Ability a in list) {a.Reset();} }

		public Ability Move {
			get {
				foreach (Ability a in list) 
					if (a.Weight == 1) return a;
				return default(Ability);
			}
		}

		public Ability Ability (string name) {
			foreach (Ability a in list) 
				if (a.Name == name) return a;
			return default(Ability);
		}

		public bool Replace (Ability oldAb, Ability newAb) {
			if (oldAb == null || newAb == null) {
				throw new ArgumentException("Arsenal.Replace: Null argument.");
			}
			else if (!list.Contains(oldAb)) {
				throw new Exception ("Arsenal cannot replace action.  Does not contain action '"+oldAb.Name+"'.");
			}
			else {
				list.Remove(oldAb);
				list.Add(newAb);
				Sort();
				return true;
			}
		}

		public bool Replace (string name, Ability newAb) {
			Ability oldAb = Ability(name);
			if (oldAb == null) {throw new Exception("Arsenal.Replace: Does not contain action '"+name+"'.");}
			else {return Replace (oldAb, newAb);}
		}

		public bool Remove (string name) {
			Ability a = Ability(name);
			if (a == null) {throw new Exception("Arsenal.Remove: Does not contain action '"+name+"'.");}
			else {
				list.Remove(a);
				return true;
			}
		}
	}
}
using System.Collections.Generic;
using UnityEngine;

namespace HOA {
	public class Arsenal : Group<Action> {
		//Unit parent;
		//List<Action> actions;
		
		public Arsenal (Unit unit) {
		//	parent = unit;
			list = new List<Action>();	
		}

		public void Sort () {
			List<Action> sorted = new List<Action>();
			List<Action> temp;

			for (int i=1; i<6; i++) {
				temp = ActionsOfWeight(i);
				temp = SortByPrice(temp);
				foreach (Action a in temp) {sorted.Add(a);}
			}
			list = sorted;
		}

		List<Action> ActionsOfWeight (int weight) {
			List<Action> temp = new List<Action>();
			foreach (Action a in list) {if (a.Weight == weight) {temp.Add(a);} }
			return temp;
		}

		List<Action> SortByPrice (List<Action> actions) {
			return actions;


		}

		public void Reset () {
			foreach (Action a in list) {a.Reset();}
		}
	}
}
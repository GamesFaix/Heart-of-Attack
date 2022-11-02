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

		public bool HasMove (out AMove move) {
			move = default(AMove);
			foreach (Action a in list) {
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
			foreach (Action a in list) {
				if (a is AFocus) {focus = (AFocus)a; return true;}
			}
			return false;
		}
		
		public void Focus () {
			AFocus focus;
			if (HasFocus (out focus)) {focus.Perform();}
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
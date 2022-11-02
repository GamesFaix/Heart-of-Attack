using System.Collections.Generic;
using System.Collections;
using System;

namespace HOA {
	
	
	public abstract class Action {
		protected int weight;
		public int Weight {get {return weight;} }

		protected string name;
		protected string desc;
		
		protected Aim aim;
		public Aim Aim {get {return aim;}}
		
		protected Price price = new Price(1,0);
		
		public string Name {get {return name;} }
		public string Desc () {return desc;}
		public void DrawPrice (Panel p) {price.Draw(p);}
		public void DrawAim (Panel p) {aim.Draw(p);}

		protected bool used = false;
		public void Reset () {used = false;}
		
		protected Unit actor;
		
		public abstract void Perform();
		
		public bool Charge () {
			if (actor.CanAfford(price) && !used) {
				used = true;
				actor.Charge(price);
				return true;
			}
			else if (used) {GameLog.Out("Action has already been used this turn.");}
			else if (!actor.CanAfford(price)) {GameLog.Out("Not enough resources.");}
			return false;
		}
	}
}

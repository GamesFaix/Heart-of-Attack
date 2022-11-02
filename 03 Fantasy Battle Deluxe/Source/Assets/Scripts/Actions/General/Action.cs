using HOA.Tokens;
using System.Collections.Generic;
using System.Collections;
using System;

namespace HOA.Actions {
	
	
	public abstract class Action {

		protected string name;
		protected string desc;
		
		protected Aim aim;
		public Aim Aim {get {return aim;}}
		
		protected Price price = new Price(1,0);
		
		public string Name {get {return name;} }
		public override string ToString () {return price.ToString()+"\t"+"\t"+"\t"+aim.ToString()+"\n"+desc;}
	
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

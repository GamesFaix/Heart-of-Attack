using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HOA.Tokens;
using HOA.Map;

namespace HOA.Actions {
	
	public class APrisRefract : Action {
		
		int damage;
		
		public APrisRefract (Price p, Unit u, Aim a, int d) {
			weight = 4;
			actor = u;
			price = p;
			aim = a;
			damage = d;
			
			name = "Refract";
			desc = "50% chance of missing target.\nDo "+d+" damage to all units in target cell.\nIf there are no obstacles in target cell, do reduce damage 50% (rounded up) and damage all units in the next occupied cell in the same direction.  Repeat until damage is 1 or an obstacle is hit.";
		}
		
		public override void Perform () {
			if (Charge()) {
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new RPrisRefract(new Source(actor), default(Token), damage));
			}
		}
	}
}

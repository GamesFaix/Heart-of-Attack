using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HOA.Tokens;
using HOA.Map;

namespace HOA.Actions {
	
	public class ALaser : Action {
		
		int damage;
		
		public ALaser (string n, Price p, Unit u, Aim a, int d) {
			weight = 3;
			actor = u;
			price = p;
			aim = a;
			damage = d;
			
			name = n;
			desc = "Do "+d+" damage to all units in target cell.\nIf there are no obstacles in target cell, do reduce damage 50% (rounded up) and damage all units in the next occupied cell in the same direction.  Repeat until damage is 1 or an obstacle is hit.";
		}
		
		public override void Perform () {
			if (Charge()) {
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new RDamageLaser(new Source(actor), default(Token), damage));
			}
		}
	}
}

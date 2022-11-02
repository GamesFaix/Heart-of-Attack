using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HOA.Tokens;
using HOA.Map;

namespace HOA.Actions {
	
	public class ANecrTouch : Action {
		
		int damage;
		
		public ANecrTouch (Price p, Unit u, Aim a, int d) {
			weight = 3;
			actor = u;
			price = p;
			aim = a;
			damage = d;
			
			name = "Touch of Death";
			desc = "Do "+d+" damage to target unit.\nIf target has less than 10 health after damage is dealt, destroy target.\nIf target is destroyed and is not an Attack King, it leaves no remains and you may place a Corpse in any cell.";
		}
		
		public override void Perform () {
			if (Charge()) {
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new RNecrTouch(new Source(actor), default(Token), damage));
			}
		}
	}
}

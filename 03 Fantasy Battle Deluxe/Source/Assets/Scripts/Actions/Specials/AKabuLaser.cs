using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HOA.Tokens;
using HOA.Map;

namespace HOA.Actions {
	
	public class AKabuLaser : Action {
		
		int damage;
		
		public AKabuLaser (Price p, Unit u, Aim a, int d) {
			weight = 4;
			actor = u;
			price = p;
			aim = a;
			damage = d;
			
			name = "Gamma Burst";
			desc = "Do "+d+" damage to all units in target direction";
		}
		
		public override void Perform () {
			if (Charge()) {
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new RKabuLaser(new Source(actor), default(Token), damage));
			}
		}
	}
}

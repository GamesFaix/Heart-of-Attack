using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HOA.Tokens;
using HOA.Map;

namespace HOA.Actions {

	public class ABeesDeathSting : Action {
		int range;
		int damage;
		
		public ABeesDeathSting (Price p, Unit u, Aim a, int d) {
			weight = 4;
			price = p;
			actor = u;
			aim = a;
			damage = d;
			int cor = (int)Mathf.Floor(d*0.5f);
			name = "Fatal Blow";
			desc = "Do "+d+" damage to target unit. \nTarget takes "+cor+" corrosion counters. \nDestroy "+actor+".";
			
		}
		
		public override void Perform () {
			if (Charge()) {
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new RDeathSting(new Source(actor), default(Token), damage));
			}
		}
	}
}

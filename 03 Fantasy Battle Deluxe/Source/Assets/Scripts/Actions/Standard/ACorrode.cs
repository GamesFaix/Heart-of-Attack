using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HOA.Tokens;
using HOA.Map;

namespace HOA.Actions {

	public class ACorrode : Action {
		int damage;
		
		public ACorrode (Price p, Unit u, Aim a, int d) {
			price = p;
			aim = a;
			damage = d;
			int cor = (int)Mathf.Floor(d*0.5f);
			actor = u;
			name = "Corrode";
			desc = "Do "+d+" damage to target unit. \nTarget takes "+cor+" corrosion counters.";
		}
		
		public override void Perform () {
			if (Charge()) {
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new RCorrode(new Source(actor), default(Token), damage));
			}
		}
	}
}

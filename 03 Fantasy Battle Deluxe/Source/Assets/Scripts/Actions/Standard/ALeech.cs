using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HOA {

	public class ALeech : Action {

		int damage;
		
		public ALeech (Price p, Unit u, Aim a, int d) {
			weight = 3;
			price = p;
			aim = a;
			damage = d;
			actor = u;
			name = "Leech Life";
			desc = "Do "+d+" damage to target unit. \nGain health equal to damage successfully dealt.";
		}
		
		public override void Perform () {
			if (Charge()) {
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new RLeech(new Source(actor), default(Token), damage));
			}
		}
	}
}

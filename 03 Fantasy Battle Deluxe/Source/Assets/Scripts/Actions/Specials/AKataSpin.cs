using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HOA.Tokens;
using HOA.Map;

namespace HOA.Actions {

	public class AKataSpin : Action {
		int damage;
		
		public AKataSpin (Price p, Unit u, int d) {
			price = p;
			actor = u;
			aim = new Aim (AIMTYPE.NEIGHBOR, TARGET.TOKEN, TTAR.UNIT);
			
			damage = d;
			name = "Laser Spin";
			desc = "Do "+d+" damage to target unit, then damage all of target's cellmates and all units clockwise or counterclockwise, reducing damage 50% each time.";
			
		}
		
		public override void Perform () {
			if (Charge()) {
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new RLaserSpin(new Source(actor), default(Token), damage));
			}
		}
	}
}

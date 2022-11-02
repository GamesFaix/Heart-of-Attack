using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HOA.Tokens;
using HOA.Map;

namespace HOA.Actions {
	
	public class AUltrThrow : Action {
		int damage;
		Aim aim2;

		public AUltrThrow (Price p, Unit u, int range, int dmg) {
			weight = 4;
			actor = u;
			price = p;
			aim = new Aim(AIMTYPE.NEIGHBOR, TARGET.TOKEN, TTAR.DEST);
			aim2 = Aim.Shoot(range);
			damage = dmg;

			name = "Throw Terrain";
			desc = "Destroy target non-Remains destructible.\n"+aim2.ToString()+"\nDo "+damage+" damage to target unit.";
		}
		
		public override void Perform () {
			if (Charge()) {
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new RUltrThrow(new Source(actor), default(Token), aim2, damage));
			}
		}
	}
}

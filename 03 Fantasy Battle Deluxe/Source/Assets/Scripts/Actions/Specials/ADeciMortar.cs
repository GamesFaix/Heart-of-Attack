using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HOA.Tokens;
using HOA.Map;

namespace HOA.Actions {
	
	public class ADeciMortar : Action {
		int minRange, range, damage;

		public ADeciMortar (Price p, Unit u, int mr, int r, int d) {
			weight = 4;
			
			price = p;
			actor = u;
			aim = new Aim (AIMTYPE.ARC, TARGET.CELL, CTAR.ATTACK, r, mr);
			damage = d;
			
			name = "Mortar";
			desc = "Do "+d+" damage to all units in target cell. \nAll units in neighboring cells take 50% damage (rounded down). \nDamage continues to spread outward with 50% reduction until 1. \nDestroy all destructible tokens that would take damage.";
		}
		
		public override void Perform () {
			if (Charge()) {
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithCell(new RExplosion(new Source(actor), default(Cell), damage));
			}
		}
	}
}

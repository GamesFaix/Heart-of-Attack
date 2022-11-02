using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HOA {

	public class AGrenade : Action {

		int range;
		int damage;
		
		public AGrenade (string n, Price p, Unit u, int r, int d) {
			weight = 3;
			price = p;
			actor = u;
			aim = new Aim (AIMTYPE.ARC, TARGET.CELL, CTAR.ATTACK, r);
			damage = d;
			
			name = n;
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

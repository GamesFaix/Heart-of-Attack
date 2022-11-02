using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HOA.Tokens;
using HOA.Map;

namespace HOA.Actions {

	public class AMonoFlame : Action {
		int damage;
		
		public AMonoFlame (Price p, Unit u, Aim a, int d) {
			price = p;
			actor = u;
			
			aim = a;
			damage = d;
			
			name = "Eternal Flame";
			desc = "Do "+d+" damage to target unit. \nTarget's neighbors and cellmates take 50% damage (rounded down). \nDamage continues spreading until less than 1. \nDestroy all destructible tokens that would take damage.";
		}
		
		public override void Perform () {
			if (Charge()) {
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new RDamageFirMax(new Source(actor), default(Token), damage));
			}
		}
	}
}

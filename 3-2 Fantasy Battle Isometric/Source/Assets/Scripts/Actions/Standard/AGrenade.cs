using UnityEngine;
using System.Collections.Generic;

namespace HOA {

	public class AGrenade : Action {

		int range;
		int damage;
		
		public AGrenade (string n, Price p, Unit u, int r, int d) {
			weight = 3;
			price = p;
			actor = u;
			AddAim(new Aim (ETraj.ARC, EType.CELL, EPurp.ATTACK, r));
			damage = d;
			
			name = n;
			desc = "Do "+d+" damage to all units in target cell. \nAll units in neighboring cells take 50% damage (rounded down). \nDamage continues to spread outward with 50% reduction until 1. \nDestroy all destructible tokens that would take damage.";
		}
		
		public override void Execute (List<ITarget> targets) {
			Charge();
			EffectQueue.Add(new EExplosion(new Source(actor), (Cell)targets[0], damage));
			//AEffects.Explosion(new Source(actor), (Cell)targets[0], damage);
			Targeter.Reset();
		}
	}
}

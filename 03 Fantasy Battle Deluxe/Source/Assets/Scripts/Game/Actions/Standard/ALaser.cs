using UnityEngine;
using System.Collections.Generic;

namespace HOA {
	
	public class ALaser : Action {
		
		int damage;
		
		public ALaser (string n, Price p, Unit u, Aim a, int d) {
			weight = 3;
			actor = u;
			price = p;
			AddAim(a);
			damage = d;
			
			name = n;
			desc = "Do "+d+" damage to all units in target cell.\nIf there are no obstacles in target cell, do reduce damage 50% (rounded up) and damage all units in the next occupied cell in the same direction.  Repeat until damage is 1 or an obstacle is hit.";
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();
			EffectQueue.Add(new ELaser(new Source(actor), (Unit)targets[0], damage));
			Targeter.Reset();
		}
	}
}

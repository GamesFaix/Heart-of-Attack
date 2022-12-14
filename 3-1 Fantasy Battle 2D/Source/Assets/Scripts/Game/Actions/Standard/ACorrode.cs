using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HOA {

	public class ACorrode : Action {
		int damage;
		
		public ACorrode (string n, Price p, Unit u, Aim a, int d) {
			weight = 3;
			
			price = p;
			AddAim(a);
			damage = d;
			int cor = (int)Mathf.Floor(d*0.5f);
			actor = u;
			name = n;
			desc = "Do "+d+" damage to target unit. \nTarget recieves "+cor+" corrosion counters.\n(If a unit has corrosion counters, at the beginning of its turn it takes damage equal to the number of counters, then removes half the counters (rounded up).)";
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();
			//Debug.Log("action corrode");
			EffectQueue.Add(new ECorrode(new Source(actor), (Unit)targets[0], damage));
			Targeter.Reset();
		}
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HOA {

	public class ASting : Task {
		public override string Desc {get {return "Do "+damage+" damage to target unit. " +
				"\nTarget recieves "+cor+" corrosion counters." +
					"\n(If a unit has corrosion counters, at the beginning of its turn " +
						"it takes damage equal to the number of counters, " +
						"then removes half the counters (rounded up).)";} }

		int damage;
		int cor;
		
		public ASting (Unit parent, int d) {
			Parent = parent;
			Name = "Sting";
			damage = d;
			cor = (int)Mathf.Floor(d*0.5f);
			Weight = 3;
			Price = Price.Cheap;
			AddAim(HOA.Aim.Melee());
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new ECorrode(new Source(Parent), (Unit)targets[0], damage));
		}
	}
}

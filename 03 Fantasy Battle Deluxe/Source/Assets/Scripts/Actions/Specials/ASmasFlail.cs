using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HOA.Tokens;
using HOA.Map;

namespace HOA.Actions {

	public class ASmasFlail : Action {
		int damage;
		
		public ASmasFlail (Price p, Unit u, Aim a, int d) {
			weight = 3;
			price = p;
			actor = u;
			
			aim = a;
			damage = d;
			
			name = "Flail";
			desc = "Do "+d+" damage to target unit.  \nRange +1 per FP, up to 3.  \nRemove all FP.";
			
		}
		
		public override void Perform () {
			if (Charge()) {
				int bonus = Mathf.Min(actor.FP, 3);
				actor.SetStat(new Source(actor), STAT.FP, 0, false);
				aim = new Aim (aim.AimType, aim.Target, aim.TTar, aim.Range+bonus);
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new RDamage(new Source(actor), default(Token), damage));
			}
		}
	}
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HOA.Tokens;
using HOA.Map;

namespace HOA.Actions {

	public class ASmasSlam : Action {
		int range;
		int damage;
		
		public ASmasSlam (Price p, Unit u, Aim a, int d) {
			price = p;
			actor = u;
			aim = a;
			damage = d;
			
			name = "Slam";
			desc = "Do "+d+" damage to target unit and each of its neighbors and cellmates.  \n+1 Range per FP, up to 3.  \nRemove all FP.";
			
		}
		
		public override void Perform () {
			if (Charge()) {
				int bonus = Mathf.Min(actor.FP, 3);
				actor.SetStat(new Source(actor), STAT.FP, 0, false);
				aim = new Aim (aim.AimType, aim.Target, aim.TTar, aim.Range+bonus);
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new RSmasSlam(new Source(actor), default(Token), damage));
			}
		}
	}
}
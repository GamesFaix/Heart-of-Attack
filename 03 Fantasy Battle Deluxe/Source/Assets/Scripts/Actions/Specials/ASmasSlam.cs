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
			weight = 4;
			
			price = p;
			actor = u;
			aim = a;
			damage = d;
			
			name = "Slam";
			desc = "Do "+d+" damage to target unit and each of its neighbors and cellmates.  \nRange +1 per focus (up to +3).  \n"+actor+" loses all focus.";
			
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
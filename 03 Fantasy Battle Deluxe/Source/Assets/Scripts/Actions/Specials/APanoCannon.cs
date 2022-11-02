using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HOA.Tokens;
using HOA.Map;

namespace HOA.Actions {
	
	public class APanoCannon : Action {
		int damage;
		
		public APanoCannon (Price p, Unit u, Aim a, int d) {
			weight = 3;
			price = p;
			actor = u;
			
			aim = a;
			damage = d;
			
			name = "Cannon";
			desc = "Do "+d+" damage to target unit.  \nMax range +1 per focus (up to +3).";
			
		}
		
		public override void Perform () {
			if (Charge()) {
				int bonus = Mathf.Min(actor.FP, 3);
				//actor.SetStat(new Source(actor), STAT.FP, 0, false);
				aim = new Aim (aim.AimType, aim.Target, aim.TTar, aim.Range+bonus);
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new RDamage(new Source(actor), default(Token), damage));
			}
		}
	}
	public class APanoPierce : Action {
		int damage;
		
		public APanoPierce (Price p, Unit u, Aim a, int d) {
			weight = 4;
			price = p;
			actor = u;
			
			aim = a;
			damage = d;
			
			name = "Armor Pierce";
			desc = "Do "+d+" damage to target unit (ignore defense).  \nMax range +1 per focus (up to +3).";
		}
		
		public override void Perform () {
			if (Charge()) {
				int bonus = Mathf.Min(actor.FP, 3);
				aim = new Aim (aim.AimType, aim.Target, aim.TTar, aim.Range+bonus);
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new RDamagePierce(new Source(actor), default(Token), damage));
			}
		}
	}
}
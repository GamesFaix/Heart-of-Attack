using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HOA.Tokens;
using HOA.Map;

namespace HOA.Actions {
	
	public class ARookVolley : Action {
		
		int damage;
		
		public ARookVolley (Price p, Unit u, Aim a, int d) {
			weight = 3;
			actor = u;
			price = p;
			aim = a;
			damage = d;
			
			name = "Volley";
			desc = "Do "+d+" damage to target unit.\nMay only be used if neighboring or sharing cell with non-Rook teammate.";
		}
		
		public override void Perform () {
			if (NeighborTeammate()) {
				if (Charge()) {
					Legalizer.Find(actor, aim);
					GUISelectors.DoWithInstance(new RDamage(new Source(actor), default(Token), damage));
				}
			}
			else {GameLog.Out(actor+" must neighbor a teammate to Volley.");}
		}

		bool NeighborTeammate () {
			TokenGroup neighbors = actor.Neighbors(true);
			for (int i=neighbors.Count-1; i>=0; i--) {
				Token t = neighbors[i];
				if (t.Owner == actor.Owner 
				    && t.Code != TTYPE.ROOK) {
					return true;
				}
			}
			return false;
		}
	}
}

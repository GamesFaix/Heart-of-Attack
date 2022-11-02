using System.Collections.Generic;
using UnityEngine;

namespace HOA{
	public class Rook : Unit {
		public Rook(Source s, bool template=false){
			NewLabel(EToken.ROOK, s, false, template);
			BuildGround();
			OnDeath = EToken.ROCK;
			
			NewHealth(20,3);
			NewWatch(3);

			Aim aim = new Aim(EAim.ARC, EClass.UNIT, EPurpose.ATTACK, 2, 2);
			arsenal.Add(new ARookVolley(Price.Cheap, this, aim, 12));
			arsenal.Add(new ARookMove(this));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}

	public class ARookVolley : Action {
		
		int damage;
		
		public ARookVolley (Price p, Unit u, Aim a, int d) {
			weight = 3;
			actor = u;
			price = p;
			AddAim(a);
			damage = d;
			
			name = "Volley";
			desc = "Do "+d+" damage to target unit.\nMay only be used if neighboring or sharing cell with non-Rook teammate.\nRange +1 per focus (up to 3).";
		}

		public override void Adjust () {
			int bonus = Mathf.Min(actor.FP, 3);
			aim[0] = new Aim (aim[0].AimType, aim[0].TargetClass, aim[0].Range+bonus);
		}
		
		public override void UnAdjust () {
			aim[0] = new Aim(EAim.ARC, EClass.UNIT, EPurpose.ATTACK, 2, 2);
		}

		public override bool Restrict () {
			TokenGroup neighbors = actor.Neighbors(true);
			for (int i=neighbors.Count-1; i>=0; i--) {
				Token t = neighbors[i];
				if (t.Owner == actor.Owner 
				    && t.Code != EToken.ROOK) {
					return false;
				}
			}
			return true;
		}

		public override void Execute (List<ITargetable> targets) {
			Charge();

			AEffects.Damage(new Source(actor), (Unit)targets[0], damage);
			Targeter.Reset();
		}

	}

	public class ARookMove : Action {
		
		Cell target;
		
		public ARookMove (Unit u) {
			weight = 1;
			AddAim(HOA.Aim.MovePath(2));
			actor = u;
			price = new Price(0,2);

			name = "Move";
			desc = "Move "+actor+" to target cell.";
			
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();
			AEffects.Move(new Source(actor), actor, (Cell)targets[0]);
			Targeter.Reset();
		}
	}
}

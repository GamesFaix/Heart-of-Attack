using System.Collections.Generic;

namespace HOA{
	public class Rook : Unit {
		public Rook(Source s, bool template=false){
			NewLabel(EToken.ROOK, s, false, template);
			BuildGround();
			OnDeath = EToken.ROCK;
			
			NewHealth(20,3);
			NewWatch(3);
			
			arsenal.Add(new ARookVolley(Price.Cheap, this, Aim.Arc(3), 12));
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
			desc = "Do "+d+" damage to target unit.\nMay only be used if neighboring or sharing cell with non-Rook teammate.";
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();
			if (NeighborTeammate()) {
			
					Legalizer.Find(actor, aim[0]);
					GUISelectors.DoWithInstance(new RDamage(new Source(actor), default(Token), damage));

			}
			else {GameLog.Out(actor+" must neighbor a teammate to Volley.");}
		}
		
		bool NeighborTeammate () {
			TokenGroup neighbors = actor.Neighbors(true);
			for (int i=neighbors.Count-1; i>=0; i--) {
				Token t = neighbors[i];
				if (t.Owner == actor.Owner 
				    && t.Code != EToken.ROOK) {
					return true;
				}
			}
			return false;
		}
	}
}

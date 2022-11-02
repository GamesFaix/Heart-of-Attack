using UnityEngine;

namespace HOA{
	public class MeinSchutz : Unit {
		public MeinSchutz(Source s, bool template=false){
			NewLabel(TTYPE.MEIN, s, false, template);
			BuildGround();
			
			NewHealth(40);
			NewWatch(4);
			
			arsenal.Add(new AMove(this, Aim.MovePath(5)));
			arsenal.Add(new AAttack("Shoot", Price.Cheap, this, Aim.Shoot(2), 12));
			arsenal.Add(new ACreate(new Price(1,1), this, TTYPE.MINE));
			arsenal.Add(new AMeinDetonate(new Price(1,1), this));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}

	public class AMeinDetonate : Action {
		
		public AMeinDetonate (Price p, Unit u) {
			weight = 4;
			actor = u;
			price = p;
			aim = new Aim(AIMTYPE.GLOBAL, TARGET.TOKEN, TTAR.NA);
			
			name = "Detonate";
			desc = "Destroy all mines on team.";
		}
		
		public override void Perform () {
			if (Charge()) {
				TokenGroup mines = actor.Owner.OwnedUnits;
				for (int i=mines.Count-1; i>=0; i--) {
					Token t = mines[i];
					if (t.Code != TTYPE.MINE) {mines.Remove(t);}
				}
				
				foreach (Token t in mines) {t.Die(new Source(actor));}
			}
		}
	}
}


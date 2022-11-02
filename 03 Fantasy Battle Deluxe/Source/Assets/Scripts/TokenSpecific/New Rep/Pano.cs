using UnityEngine;

namespace HOA{
	public class Panopticannon : Unit {
		public Panopticannon(Source s, bool template=false){
			NewLabel(TTYPE.PANO, s, false, template);
			BuildTrample();
			
			health = new HealthPano(this, 65);
			NewWatch(1);
			
			arsenal.Add(new AMove(this, Aim.MovePath(1)));
			
			Aim aim = new Aim(AIMTYPE.ARC, TARGET.TOKEN, TTAR.UNIT, 3, 2);
			arsenal.Add(new APanoCannon(Price.Cheap, this, aim, 17));
			
			aim = new Aim(AIMTYPE.ARC, TARGET.TOKEN, TTAR.UNIT, 4, 3);
			arsenal.Add(new APanoPierce(new Price(1,2), this, aim, 20));
			arsenal.Sort();
		}		
		public override string Notes () {return "Defense +1 per Focus (up to 2).";}
	}	

	public class HealthPano : Health{
		public HealthPano (Unit u, int n=0, int d=0){
			parent = u; max = n; Fill(); def = d;
		}
		public override int DEF {
			get {return def + Mathf.Min(2, parent.FP);}
		}
	}

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
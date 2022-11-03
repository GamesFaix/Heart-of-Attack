using UnityEngine;
using System.Collections.Generic;

namespace HOA{
	public class Beesassin : Unit {
		public Beesassin(Source s, bool template=false){
			id = new ID(this, EToken.BEES, s, false, template);
			plane = Plane.Air;
			ScaleSmall();
			NewHealth(25);
			NewWatch(5);
			AddStat(new Source(this), EStat.COR, 12, false);
			
			arsenal.Add(new AMoveLine(this, 5));;
			arsenal.Add(new ACorrode("Sting", Price.Cheap, this, Aim.Melee(), 8));
			arsenal.Add(new ABeesDeathSting(new Price(1,1), this, Aim.Melee(), 15));
			arsenal.Sort();
			
		}		
		public override string Notes () {return "";}
	}
	
	public class ABeesDeathSting : Action {
		int range;
		int damage;
		
		public ABeesDeathSting (Price p, Unit u, Aim a, int d) {
			weight = 4;
			price = p;
			actor = u;
			AddAim(a);
			damage = d;
			int cor = (int)Mathf.Floor(d*0.5f);
			name = "Fatal Blow";
			desc = "Destroy "+actor+".\nDo "+d+" damage to target unit. \nTarget takes "+cor+" corrosion counters. \n(If a unit has corrosion counters, at the beginning of its turn it takes damage equal to the number of counters, then removes half the counters (rounded up).)";
		}
		
		public override void Execute (List<ITarget> targets) {
			Charge();


			Unit u = (Unit)targets[0];
			EffectQueue.Add(new ECorrode (new Source(actor), u, damage));

			EffectQueue.Add(new EKill (new Source(actor), actor));
			Targeter.Reset();
		}
	}
}
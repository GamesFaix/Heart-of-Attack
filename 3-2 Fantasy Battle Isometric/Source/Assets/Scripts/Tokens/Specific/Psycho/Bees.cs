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
			BuildArsenal();
		}	

		protected override void BuildArsenal () {
			base.BuildArsenal();
			arsenal.Add(new Task[]{
				new AMoveLine(this, 5),
				new ASting(this, 8),
				new ABeesFatalBlow(this)
			});
			arsenal.Sort();
		}

		public override string Notes () {return "";}
	}
	
	public class ABeesFatalBlow : Task {
		int damage = 15;
		int Cor {get {return (int)Mathf.Floor(damage*0.5f);} }

		public override string Desc {get {return "Destroy "+Parent+"." +
				"\nDo "+damage+" damage to target unit. " +
					"\nTarget takes "+Cor+" corrosion counters. " +
						"\n(If a unit has corrosion counters, at the beginning of its turn " +
						"it takes damage equal to the number of counters, " +
						"then removes half the counters (rounded up).)";} }

		public ABeesFatalBlow (Unit u) {
			Name = "Fatal Blow";
			Weight = 4;

			Price = new Price(1,1);
			Parent = u;
			AddAim(HOA.Aim.Melee());
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Unit u = (Unit)targets[0];
			EffectQueue.Add(new ECorrode (new Source(Parent), u, damage));

			EffectQueue.Add(new EKill (new Source(Parent), Parent));
		}
	}
}
using System.Collections.Generic;
using UnityEngine;

namespace HOA{
	public class GrizzlyElder : Unit {
		public GrizzlyElder(Source s, bool template=false){
			id = new ID(this, EToken.GRIZ, s, false, template);
			plane = Plane.Gnd;
			ScaleSmall();

			NewHealth(25);
			NewWatch(3);
			BuildArsenal();
		}	

		protected override void BuildArsenal () {
			base.BuildArsenal();
			arsenal.Add(new Task[]{
				new AMovePath(this, 3),
				new AStrike(this, 9),
				new ACreate(this, new Price(0,1), EToken.TREE),
				new AGrizHeal(this)
			});
			arsenal.Sort();
		}

		public override string Notes () {return "";}
	}

	public class AGrizHeal : Task {

		public override string Desc {get {return "Target teammate gains "+magnitude+" health." +
				"\n(Cannot target self.)";} }

		int magnitude = 10;
		
		public AGrizHeal (Unit parent) {
			Name = "Heal";
			Weight = 4;
			Parent = parent;
			Price = new Price(1,1);
			AddAim (new Aim(ETraj.NEIGHBOR, EType.UNIT));
			aim[0].TeamOnly = true;
			aim[0].IncludeSelf = false;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new EAddStat(new Source(Parent), (Unit)targets[0], EStat.HP, magnitude));
		}
	}
}
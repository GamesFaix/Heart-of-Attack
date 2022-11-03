using UnityEngine;
using System.Collections.Generic;

namespace HOA{
	public class Panopticannon : Unit {
		public Panopticannon(Source s, bool template=false){
			id = new ID(this, EToken.PANO, s, false, template);
			plane = Plane.Gnd;
			type.Add(EType.TRAM);
			ScaleLarge();
			health = new HealthPano(this, 65);
			NewWatch(1);
			BuildArsenal();
		}	

		protected override void BuildArsenal () {
			base.BuildArsenal();
			arsenal.Add(new Task[] {
				new AMovePath(this, 1),
				new APanoCannon(this, Price.Cheap, 12),
				new APanoPierce(this, new Price(1,2), 20),
			});
			arsenal.Sort();
		}

		public override string Notes () {return "Defense +1 per Focus (up to +2).";}
	}	

	public class HealthPano : Health{
		public HealthPano (Unit u, int n=0, int d=0){
			parent = u; max = n; Fill(); def = d;
		}
		public override int DEF {
			get {return def + Mathf.Min(4, parent.FP);}
		}
	}

	public class APanoCannon : Task {

		int damage = 12;

		public override string Desc {get {return "Do "+damage+" damage to target unit.  " +
				"\nMax range +1 per focus (up to +3).";} }

		public APanoCannon (Unit u, Price p, int damage) {
			Name = "Cannon";
			Weight = 3;

			Price = p;
			Parent = u;
			
			AddAim(new Aim(ETraj.ARC, EType.UNIT, 3, 2));
			this.damage = damage;
		}

		public override void Adjust () {
			int bonus = Mathf.Min(Parent.FP, 3);
			aim[0] = new Aim (aim[0].Trajectory, aim[0].Special, aim[0].Range+bonus, aim[0].MinRange);
		}

		public override void UnAdjust () {
			aim[0] = new Aim(ETraj.ARC, EType.UNIT, 3, 2);
		}

		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new EDamage(new Source(Parent), (Unit)targets[0], damage));
		}
	}
	public class APanoPierce : Task {

		int damage = 12;

		public override string Desc {get {return "Do "+damage+" damage to target unit (ignore defense).  " +
				"\nMax range +1 per focus (up to +3)."; } }

		public APanoPierce (Unit u, Price p, int damage) {
			Name = "Armor Pierce";
			Weight = 4;

			Price = p;
			Parent = u;
			
			AddAim(new Aim(ETraj.ARC, EType.UNIT, 3, 2));
			this.damage = damage;
		}

		public override void Adjust () {
			int bonus = Mathf.Min(Parent.FP, 3);
			aim[0] = new Aim (aim[0].Trajectory, aim[0].Special, aim[0].Range+bonus, aim[0].MinRange);
		}

		public override void UnAdjust () {
			aim[0] = new Aim(ETraj.ARC, EType.UNIT, 4, 3);
		}

		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new EPierce (new Source(Parent), (Unit)targets[0], damage));
		}
	}
}
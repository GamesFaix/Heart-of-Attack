using System.Collections.Generic;
using UnityEngine;

namespace HOA{
	
	public class RevolvingTom : Unit {
		public RevolvingTom(Source s, bool template=false){
			id = new ID(this, EToken.REVO, s, false, template);
			plane = Plane.Gnd;
			ScaleSmall();

			NewHealth(30);
			NewWatch(4);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			arsenal.Add(new Task[] {
				new AMovePath(this, 3),
				new AShoot(this, 2, 8),
				new ARevoQuick(this)
			});
			arsenal.Sort();
		}
		public override string Notes () {return "";}
	}
	public class ARevoQuick : Task, IMultiTarget {

		public override string Desc {get {return "Once per Focus, select and deal "+damage+" damage to target unit (up to 5 times)." +
				"\n(You may choose the same target multiple times.)" +
					"\nLose all Focus.";} }

		int damage = 6;
		public int Optional () {return 1;}
		
		public ARevoQuick (Unit parent) {
			Name = "Quickdraw";
			Weight = 4;
			Parent = parent;
			Price = new Price(0,1);
			ResetAim();
		}

		public override void Adjust () {
			int shots = Mathf.Min(Parent.FP, 5);
			Debug.Log("shots "+shots);
			for (int i=2; i<=shots; i++) {
				AddAim(HOA.Aim.Shoot(3));
			}
			Debug.Log("target slots "+aim.Count);
		}

		public override void UnAdjust () {
			ResetAim();
		}

		void ResetAim () {
			aim = new List<HOA.Aim>();
			AddAim(HOA.Aim.Shoot(3));
		}

		protected override void ExecuteMain (TargetGroup targets) {
			for (int i=0; i<targets.Count; i++) {
				EffectQueue.Add(new EDamage (new Source(Parent), (Unit)targets[i], damage));
			}
			Parent.SetStat(new Source(Parent), EStat.FP, 0);
		}
	} 
}
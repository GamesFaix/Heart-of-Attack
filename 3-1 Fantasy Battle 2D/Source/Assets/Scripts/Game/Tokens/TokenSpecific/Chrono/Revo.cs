using System.Collections.Generic;
using UnityEngine;

namespace HOA{
	
	public class RevolvingTom : Unit {
		public RevolvingTom(Source s, bool template=false){
			NewLabel(EToken.REVO, s, false, template);
			BuildGround();
			
			NewHealth(30);
			NewWatch(4);
			arsenal.Add(new AMovePath(this, 3));
			arsenal.Add(new AAttack("Shoot", Price.Cheap, this, Aim.Shoot(2), 8));
			arsenal.Add(new ARevoQuick(this));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}

		public class ARevoQuick : Action, IMultiTarget {
			
			int damage;
			public int Optional () {return 1;}
			
			public ARevoQuick (Unit u) {
				weight = 4;
				actor = u;
				price = new Price(0,1);
				AddAim(HOA.Aim.Shoot(3));
				damage = 6;
				
				name = "Quickdraw";
				desc = "Once per Focus, select and deal "+damage+" damage to target unit (up to 5 times).\n(You may choose the same target multiple times.)\nLose all Focus.";
			}

			public override void Adjust () {
				int shots = Mathf.Min(actor.FP, 5);
				for (int i=1; i<shots; i++) {
					AddAim(HOA.Aim.Shoot(3));
				}
			}

			public override void UnAdjust () {
				aim = new List<HOA.Aim>();
				AddAim(HOA.Aim.Shoot(3));
			}

			public override void Execute (List<ITargetable> targets) {
				Charge();
				for (int i=0; i<targets.Count; i++) {
					EffectQueue.Add(new EDamage (new Source(actor), (Unit)targets[i], damage));
				}
				actor.SetStat(new Source(actor), EStat.FP, 0);
				Targeter.Reset();
			}
		} 
	}
}
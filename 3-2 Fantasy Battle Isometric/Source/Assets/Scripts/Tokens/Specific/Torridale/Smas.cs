using UnityEngine;
using System.Collections.Generic;

namespace HOA{
	public class Smashbuckler : Unit {
		public Smashbuckler(Source s, bool template=false){
			id = new ID(this, EToken.SMAS, s, false, template);
			plane = Plane.Gnd;
			ScaleSmall();
			NewHealth(30);
			NewWatch(3);
			BuildArsenal();	
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			arsenal.Add(new Task[]{
				new AMovePath(this, 3),
				new ASmasFlail(this),
				new ASmasSlam(this)
			});
			arsenal.Sort();
		}
		
		public override string Notes () {return "";}
	}	

	public class ASmasFlail : Task {
		int damage = 8;

		public override string Desc {get {return "Do "+damage+" damage to target unit.  " +
				"\nRange +1 per focus (Up to +3).  " +
				"\n"+Parent+" loses all focus.";} }

		public ASmasFlail (Unit u) {
			Name = "Flail";
			Weight = 3;
			Price = Price.Cheap;
			Parent = u;
			ResetAim();
		}

		public override void Adjust () {
			Debug.Log("adjusting");
			int bonus = Mathf.Min(Parent.FP, 3);
			aim = new List<HOA.Aim>();
			AddAim(new Aim(ETraj.PATH, EType.UNIT, 1+bonus));
		}

		public override void UnAdjust () {
			ResetAim();
		}

		public void ResetAim () {
			aim = new List<HOA.Aim>();
			AddAim(new Aim(ETraj.PATH, EType.UNIT, 1));
		}

		protected override void ExecuteMain (TargetGroup targets) {
			Parent.SetStat(new Source(Parent), EStat.FP, 0, false);
			EffectQueue.Add(new EDamage(new Source(Parent), (Unit)targets[0], damage));
			UnAdjust();
		}
	}

	public class ASmasSlam : Task {
		int damage = 8;

		public override string Desc {get {return "Do "+damage+" damage to target unit and each of its neighbors and cellmates.  " +
				"\nRange +1 per focus (up to +3).  " +
				"\n"+Parent+" loses all focus.";} }

		public ASmasSlam (Unit u) {
			Name = "Slam";
			Weight = 4;
			Price = new Price(2,0);
			Parent = u;
			ResetAim();
		}

		public override void Adjust () {
			Debug.Log("adjusting");
			int bonus = Mathf.Min(Parent.FP, 3);
			aim = new List<HOA.Aim>();
			AddAim(new Aim(ETraj.PATH, EType.UNIT, 1+bonus));
		}
		
		public override void UnAdjust () {
			ResetAim();
		}
		
		public void ResetAim () {
			aim = new List<HOA.Aim>();
			AddAim(new Aim(ETraj.PATH, EType.UNIT, 1));
		}

		protected override void ExecuteMain (TargetGroup targets) {
			Parent.SetStat(new Source(Parent), EStat.FP, 0, false);
		
			Unit u = (Unit)targets[0];
			EffectQueue.Add(new EDamage(new Source(Parent), u, damage));

			TokenGroup neighbors = u.Body.Neighbors(true).OnlyType(EType.UNIT);
			EffectGroup nextEffects = new EffectGroup();
			foreach (Unit u2 in neighbors) {
				nextEffects.Add(new EDamage(new Source(Parent), u2, damage));
			}
			EffectQueue.Add(nextEffects);
			UnAdjust();
		}
	}
}
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
			
			arsenal.Add(new AMovePath(this, 3));
			arsenal.Add(new ASmasFlail(Price.Cheap, this));
			arsenal.Add(new ASmasSlam(new Price(2,0), this));
			arsenal.Sort();
		}		
		
		public override string Notes () {return "";}
	}	

	public class ASmasFlail : Action {
		int damage;
		
		public ASmasFlail (Price p, Unit u) {
			weight = 3;
			price = p;
			actor = u;
			
			ResetAim();
			damage = 8;
			
			name = "Flail";
			desc = "Do "+damage+" damage to target unit.  " +
				"\nRange +1 per focus (Up to +3).  " +
				"\n"+actor+" loses all focus.";
			
		}

		public override void Adjust () {
			Debug.Log("adjusting");
			int bonus = Mathf.Min(actor.FP, 3);
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

		public override void Execute (List<ITarget> targets) {
			Charge();
			actor.SetStat(new Source(actor), EStat.FP, 0, false);
			EffectQueue.Add(new EDamage(new Source(actor), (Unit)targets[0], damage));
			UnAdjust();
			Targeter.Reset();
		}
	}

	public class ASmasSlam : Action {
		int range;
		int damage;
		
		public ASmasSlam (Price p, Unit u) {
			weight = 4;
			
			price = p;
			actor = u;
			ResetAim();
			damage = 8;
			
			name = "Slam";
			desc = "Do "+damage+" damage to target unit and each of its neighbors and cellmates.  " +
				"\nRange +1 per focus (up to +3).  " +
				"\n"+actor+" loses all focus.";
			
		}

		public override void Adjust () {
			Debug.Log("adjusting");
			int bonus = Mathf.Min(actor.FP, 3);
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

		public override void Execute (List<ITarget> targets) {
			Charge();
			actor.SetStat(new Source(actor), EStat.FP, 0, false);
		
			Unit u = (Unit)targets[0];
			EffectQueue.Add(new EDamage(new Source(actor), u, damage));

			TokenGroup neighbors = u.Body.Neighbors(true).OnlyType(EType.UNIT);
			EffectGroup nextEffects = new EffectGroup();
			foreach (Unit u2 in neighbors) {
				nextEffects.Add(new EDamage(new Source(actor), u2, damage));
			}
			EffectQueue.Add(nextEffects);
			UnAdjust();
			Targeter.Reset();
		}
	}
}
	
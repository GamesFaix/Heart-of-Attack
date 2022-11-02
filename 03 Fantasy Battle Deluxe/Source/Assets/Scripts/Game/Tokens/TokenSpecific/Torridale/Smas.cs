using UnityEngine;
using System.Collections.Generic;

namespace HOA{
	public class Smashbuckler : Unit {
		public Smashbuckler(Source s, bool template=false){
			NewLabel(EToken.SMAS, s, false, template);
			BuildGround();
			
			NewHealth(30);
			NewWatch(3);
			
			arsenal.Add(new AMove(this, Aim.MovePath(3)));
			arsenal.Add(new ASmasFlail(Price.Cheap, this));
			arsenal.Add(new ASmasSlam(new Price(1,1), this));
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
			
			AddAim(new Aim(EAim.PATH, EClass.UNIT, 1));
			damage = 8;
			
			name = "Flail";
			desc = "Do "+damage+" damage to target unit.  \nRange +1 per focus (Up to +3).  \n"+actor+" loses all focus.";
			
		}

		public override void Adjust () {
			Debug.Log("adjusting");
			int bonus = Mathf.Min(actor.FP, 3);
			aim[0] = new Aim (aim[0].AimType, aim[0].TargetClass, aim[0].Range+bonus);
		}

		public override void UnAdjust () {
			aim[0] = new Aim(EAim.PATH, EClass.UNIT, 1);
		}

		public override void Execute (List<ITargetable> targets) {
			Charge();
			actor.SetStat(new Source(actor), EStat.FP, 0, false);
			InputBuffer.Submit(new RDamage(new Source(actor), (Unit)targets[0], damage));
			UnAdjust();
		}
	}

	public class ASmasSlam : Action {
		int range;
		int damage;
		
		public ASmasSlam (Price p, Unit u) {
			weight = 4;
			
			price = p;
			actor = u;
			AddAim(new Aim(EAim.PATH, EClass.UNIT, 1));
			damage = 8;
			
			name = "Slam";
			desc = "Do "+damage+" damage to target unit and each of its neighbors and cellmates.  \nRange +1 per focus (up to +3).  \n"+actor+" loses all focus.";
			
		}

		public override void Adjust () {
			int bonus = Mathf.Min(actor.FP, 3);
			aim[0] = new Aim (aim[0].AimType, aim[0].TargetClass, aim[0].Range+bonus);
		}

		public override void UnAdjust () {
			aim[0] = new Aim(EAim.PATH, EClass.UNIT, 1);
		}

		public override void Execute (List<ITargetable> targets) {
			Charge();
			actor.SetStat(new Source(actor), EStat.FP, 0, false);
		
			Unit u = (Unit)targets[0];
			u.Damage(new Source(actor), damage);
			u.SpriteEffect(EEffect.DMG);
			TokenGroup neighbors = u.Neighbors(true).OnlyClass(EClass.UNIT);
			foreach (Unit u2 in neighbors) {
				u2.Damage(new Source(actor), damage);
				u2.SpriteEffect(EEffect.DMG);
			}
			UnAdjust();
		}
	}
}
	
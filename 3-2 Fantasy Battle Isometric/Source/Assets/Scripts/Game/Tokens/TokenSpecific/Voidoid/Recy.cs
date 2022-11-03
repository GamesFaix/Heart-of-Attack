using UnityEngine;
using System.Collections.Generic;

namespace HOA{
	public class Recyclops : Unit {
		public Recyclops(Source s, bool template=false){
			NewLabel(EToken.RECY, s, false, template);
			BuildGround();
			AddRem();
			ScaleSmall();
			NewHealth(15);
			NewWatch(4);
			
			arsenal.Add(new AMovePath(this, 2));
			arsenal.Add(new ARage(new Price(1,0), this, Aim.Melee(), 12));
			arsenal.Add(new ARecyExplode(this));
			arsenal.Add(new ARecyCannibal(this));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}

	public class ARecyCannibal : Action {
		
		Cell cell;
		
		public ARecyCannibal (Unit par) {
			weight = 4;
			price = new Price(1,0);
			actor = par;
			AddAim(new Aim (EAim.NEIGHBOR, EClass.REM));
			
			name = "Cannibalize";
			desc = "Destroy target remains.\nHealth +10/10";
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();
			Token t = (Token)targets[0];

			t.Die(new Source(actor));
			actor.AddStat(new Source(actor), EStat.MHP, 10);
			actor.AddStat(new Source(actor), EStat.HP, 10);
			Targeter.Reset();
		}
	}

	public class ARecyExplode : Action {
		int damage;
		
		public ARecyExplode (Unit u) {
			weight = 4;
			price = new Price(1,1);
			AddAim(HOA.Aim.Self);
			actor = u;
			
			damage = 12;
			int cor = (int)Mathf.Floor(damage*0.5f);
			name = "Burst";
			desc = "Destroy "+actor+".\nDo "+damage+" damage to cellmates and neighbors. \nDamaged units take "+cor+" corrosion counters. \n(If a unit has corrosion counters, at the beginning of its turn it takes damage equal to the number of counters, then removes half the counters (rounded up).)";
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();
			TokenGroup victims = actor.Neighbors(true).OnlyClass(EClass.UNIT);
			EffectGroup nextEffects = new EffectGroup();
			nextEffects.Add(new EKill(new Source(actor), actor));
			foreach (Token t in victims) {
				nextEffects.Add(new ECorrode(new Source(actor), (Unit)t, damage));	
			}
			EffectQueue.Add(nextEffects);
			Targeter.Reset();
		}
	}
}
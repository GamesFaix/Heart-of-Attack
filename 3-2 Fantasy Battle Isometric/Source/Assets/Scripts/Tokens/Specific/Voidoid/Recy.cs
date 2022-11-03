using UnityEngine;
using System.Collections.Generic;

namespace HOA{
	public class Recyclops : Unit {
		public Recyclops(Source s, bool template=false){
			id = new ID(this, EToken.RECY, s, false, template);
			plane = Plane.Gnd;
			type.Add(EType.DEST);
			type.Add(EType.REM);
			ScaleSmall();
			NewHealth(15);
			NewWatch(4);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			arsenal.Add(new Task[]{
				new AMovePath(this, 2),
				new ARage(this, 12),
				new ARecyExplode(this),
				new ARecyCannibal(this)
			});
			arsenal.Sort();
		}

		public override string Notes () {return "";}
	}

	public class ARecyCannibal : Task {

		public override string Desc {get {return "Destroy target remains." +
				"\nHealth +10/10";} } 

		public ARecyCannibal (Unit par) {
			Name = "Cannibalize";
			Weight = 4;
			Price = new Price(1,0);
			Parent = par;
			AddAim(new Aim (ETraj.NEIGHBOR, EType.REM));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Token t = (Token)targets[0];

			t.Die(new Source(Parent));
			Parent.AddStat(new Source(Parent), EStat.MHP, 10);
			Parent.AddStat(new Source(Parent), EStat.HP, 10);
		}
	}

	public class ARecyExplode : Task {
		int damage = 12;

		int Cor {get {return (int)Mathf.Floor(damage*0.5f);} }

		public override string Desc {get {return "Destroy "+Parent+"." +
			"\nDo "+damage+" damage to cellmates and neighbors. " +
			"\nDamaged units take "+Cor+" corrosion counters. " +
			"\n(If a unit has corrosion counters, at the beginning of its turn " +
				"it takes damage equal to the number of counters, " +
				"then removes half the counters (rounded up).)";
		} } 
		
		public ARecyExplode (Unit u) {
			Name = "Burst";
			Weight = 4;

			Price = new Price(1,1);
			AddAim(HOA.Aim.Self());
			Parent = u;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			TokenGroup victims = Parent.Body.Neighbors(true).OnlyType(EType.UNIT);
			EffectGroup nextEffects = new EffectGroup();
			nextEffects.Add(new EKill(new Source(Parent), Parent));
			foreach (Token t in victims) {
				nextEffects.Add(new ECorrode(new Source(Parent), (Unit)t, damage));	
			}
			EffectQueue.Add(nextEffects);
		}
	}
}
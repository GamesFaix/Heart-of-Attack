using UnityEngine; 

namespace HOA { 


	public class AMycoSpore : Task {
		int damage = 12;
		
		int Cor {get {return (int)Mathf.Floor(damage*0.5f);} }
		
		public override string Desc {get {return "Do "+damage+" damage to target unit. " +
				"\nTarget recieves "+Cor+" corrosion counters." +
					"\n(If a unit has corrosion counters, at the beginning of its turn " +
						"it takes damage equal to the number of counters, " +
						"then removes half the counters (rounded up).)";} }
		
		public AMycoSpore (Unit u) {
			Name = "Sporatic Emission";
			Weight = 3;
			
			Price = Price.Cheap;
			NewAim(HOA.Aim.Arc(2));
			Parent = u;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new ECorrode(new Source(Parent), (Unit)targets[0], damage));
		}
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
			NewAim(HOA.Aim.Melee());
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Unit u = (Unit)targets[0];
			EffectQueue.Add(new ECorrode (new Source(Parent), u, damage));
			
			EffectQueue.Add(new EKill (new Source(Parent), Parent));
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
			NewAim(HOA.Aim.Self());
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

using UnityEngine; 

namespace HOA { 

	public class AMycoDonate : Task {
		
		int damage = 6;
		
		public override string Desc {get {return "Target unit gains "+damage+" health. " +
				"\n"+Parent+" takes damage equal to health successfully gained.";} }
		
		public AMycoDonate (Unit u) {
			Name = "Donate Life";
			Weight = 4;
			
			Price = Price.Cheap;
			NewAim(HOA.Aim.Melee());
			Parent = u;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new EDonate(new Source(Parent), (Unit)targets[0], damage));
		}
	}

	public class APiecHeal : Task {
		
		public override string Desc {get {return "Target unit gains "+magnitude+" health." +
				"\n(Can target self.)";} }
		
		int magnitude = 10;
		
		public APiecHeal (Unit u) {
			Name = "Heal";
			Weight = 4;
			
			Parent = u;
			Price = new Price(0,2);
			NewAim(new Aim(ETraj.ARC, EType.UNIT, 2));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new EAddStat(new Source(Parent), (Unit)targets[0], EStat.HP, magnitude));
		}
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
			NewAim (new Aim(ETraj.NEIGHBOR, EType.UNIT));
			Aim[0].TeamOnly = true;
			Aim[0].IncludeSelf = false;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new EAddStat(new Source(Parent), (Unit)targets[0], EStat.HP, magnitude));
		}
	}

	public class AMetaConsume : Task {
		
		public override string Desc {get {return "Destroy neighboring non-Remains destructible." +
				"\n"+Parent+" gains 12 health.";} }
		
		public AMetaConsume (Unit parent) {
			Name = "Consume Terrain";
			Weight = 4;
			Parent = parent;
			Price = new Price(1,1);
			NewAim(new Aim(ETraj.NEIGHBOR, EType.DEST));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Token t = (Token)targets[0];
			t.Die(new Source(Parent));
			Parent.AddStat(new Source(Parent), EStat.HP, 12);
			Parent.Display.Effect(EEffect.STATUP);
		}
	}

}

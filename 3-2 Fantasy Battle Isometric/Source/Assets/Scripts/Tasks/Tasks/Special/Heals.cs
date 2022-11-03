using UnityEngine; 

namespace HOA.Actions { 

	public class Donate : Task {
		
		int damage = 6;
		
		public override string Desc {get {return "Target unit gains "+damage+" health. " +
				"\n"+Parent+" takes damage equal to health successfully gained.";} }
		
		public Donate (Unit u) {
			Name = "Donate Life";
			Weight = 4;
			
			Price = Price.Cheap;
			NewAim(Aim.AttackNeighbor(Special.Unit));
			Parent = u;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Donate(new Source(Parent), (Unit)targets[0], damage));
		}
	}

	public class Repair : Task {
		
		public override string Desc {get {return "Target unit gains "+magnitude+" health." +
				"\n(Can target self.)";} }
		
		int magnitude = 10;
		
		public Repair (Unit u) {
			Name = "Repair";
			Weight = 4;
			
			Parent = u;
			Price = new Price(0,2);
			NewAim(Aim.AttackArc(Special.Unit, 0, 2));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.AddStat(new Source(Parent), (Unit)targets[0], EStat.HP, magnitude));
		}
	}

	public class Sooth : Task {
		
		public override string Desc {get {return "Target teammate gains "+magnitude+" health." +
				"\n(Cannot target self.)";} }
		
		int magnitude = 10;
		
		public Sooth (Unit parent) {
			Name = "Sooth";
			Weight = 4;
			Parent = parent;
			Price = new Price(1,1);
			NewAim (Aim.AttackNeighbor(Special.Unit));
			Aims[0].TeamOnly = true;
			Aims[0].IncludeSelf = false;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.AddStat(new Source(Parent), (Unit)targets[0], EStat.HP, magnitude));
		}
	}

	public class Engorge : Task {
		
		public override string Desc {get {return "Destroy neighboring non-Remains destructible." +
				"\n"+Parent+" gains 12 health.";} }
		
		public Engorge (Unit parent) {
			Name = "Engorge";
			Weight = 4;
			Parent = parent;
			Price = new Price(1,1);
			NewAim(Aim.AttackNeighbor(Special.Dest));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Token t = (Token)targets[0];
			t.Die(new Source(Parent));
			Parent.AddStat(new Source(Parent), EStat.HP, 12);
			Parent.Display.Effect(EEffect.STATUP);
		}
	}

	public class Oasis : Task {
		
		int damage = 7;
		
		public override string Desc {get {return  "All friendly cellmates +"+damage+" health. " +
				"\nLose health equal to health successfully given.";} }
		
		public Oasis (Unit u) {
			Name = "Donate life";
			Weight = 3;
			
			Price = new Price(1,0);
			NewAim(Aim.Self());
			Parent = u;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			TokenGroup tokens = Parent.Body.CellMates;
			tokens = tokens.OnlyType(ESpecial.UNIT);
			tokens = tokens.OnlyOwner(Parent.Owner);
			EffectGroup effects = new EffectGroup();
			foreach (Token t in tokens) {
				effects.Add(new Effects.Donate(new Source(Parent), (Unit)t, damage));
			}
			EffectQueue.Add(effects);
		}
	}

}

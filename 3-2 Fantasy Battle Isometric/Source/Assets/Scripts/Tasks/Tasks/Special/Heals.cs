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
			NewAim(HOA.Aim.Melee());
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
			NewAim(new Aim(ETraj.ARC, ESpecial.UNIT, 2));
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
			NewAim (new Aim(ETraj.NEIGHBOR, ESpecial.UNIT));
			Aim[0].TeamOnly = true;
			Aim[0].IncludeSelf = false;
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
			NewAim(new Aim(ETraj.NEIGHBOR, ESpecial.DEST));
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
			NewAim(HOA.Aim.Self());
			Parent = u;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			TokenGroup tokens = Parent.Body.CellMates;
			tokens = tokens.OnlyType(ESpecial.UNIT);
			tokens = tokens.OnlyOwner(Parent.Owner);
			foreach (Token t in tokens) {
				EffectQueue.Add(new Effects.Donate(new Source(Parent), (Unit)t, damage));
			}
		}
	}

}

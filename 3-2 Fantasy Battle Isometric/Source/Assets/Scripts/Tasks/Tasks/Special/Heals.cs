using UnityEngine; 

namespace HOA.Actions { 

	public class Donate : Task {
		
		int damage = 6;
		
		public override string desc {get {return "Target unit gains "+damage+" health. " +
				"\n"+parent+" takes damage equal to health successfully gained.";} }
		
		public Donate (Unit parent) : base(parent) {
			name = "Donate Life";
			weight = 4;	
			aims += Aim.AttackNeighbor(Filters.Units);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Donate(source, (Unit)targets[0], damage));
		}
	}

	public class Repair : Task {
		
		public override string desc {get {return "Target unit gains "+magnitude+" health." +
				"\n(Can target self.)";} }
		
		int magnitude = 10;
		
		public Repair (Unit parent) : base(parent) {
			name = "Repair";
			weight = 4;
			price = new Price(0,2);
			aims += Aim.AttackArc(Filters.Units, 0, 2);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.AddStat(source, (Unit)targets[0], EStat.HP, magnitude));
		}
	}

	public class Sooth : Task {
		
		public override string desc {get {return "Target teammate gains "+magnitude+" health." +
				"\n(Cannot target self.)";} }
		
		int magnitude = 10;
		
		public Sooth (Unit parent) : base(parent) {
			name = "Sooth";
			weight = 4;
			price = new Price(1,1);
			aims += Aim.AttackNeighbor(MyFilter);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.AddStat(source, (Unit)targets[0], EStat.HP, magnitude));
		}

		public static TargetGroup MyFilter (TargetGroup targets, Token actor) {
			TokenGroup output = (targets.tokens).units;
			output /= actor.Owner;
			output -= actor;
			return output;
		}
	}

	public class Engorge : Task {
		
		public override string desc {get {return "Destroy neighboring non-Remains destructible." +
				"\n"+parent+" gains 12 health.";} }
		
		public Engorge (Unit parent) : base(parent) {
			name = "Engorge";
			weight = 4;
			price = new Price(1,1);
			aims += Aim.AttackNeighbor(Filters.DestNoCorpse);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Token t = (Token)targets[0];
			t.Die(source);
			parent.AddStat(source, EStat.HP, 12);
			parent.Display.Effect(EEffect.STATUP);
		}
	}

	public class Oasis : Task {
		
		int damage = 7;
		
		public override string desc {get {return  "All friendly cellmates +"+damage+" health. " +
				"\nLose health equal to health successfully given.";} }
		
		public Oasis (Unit parent) : base(parent) {
			name = "Donate life";
			weight = 3;
			aims += Aim.Self();
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			TokenGroup tokens = parent.Body.CellMates;
			tokens = (tokens.units) / parent.Owner;
			EffectGroup effects = new EffectGroup();
			foreach (Token t in tokens) {
				effects.Add(new Effects.Donate(source, (Unit)t, damage));
			}
			EffectQueue.Add(effects);
		}
	}

}

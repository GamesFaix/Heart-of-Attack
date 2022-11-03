using UnityEngine; 

namespace HOA.Actions { 

	public class TimeMine : Task {
		
		public override string desc {get {return "Destroy neighboring destructible." +
				"\nIf initative is less than 6, initiative +1.";} }
		
		public TimeMine (Unit parent) : base(parent) {
			name = "Time Mine";
			weight = 4;
			aims += Aim.AttackNeighbor(Filters.Destructible);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Token t = (Token)targets[0];
			Cell c = t.Body.Cell;
			
			EffectQueue.Add(new Effects.Destruct(source, t));
			
			EffectGroup nextEffects = new EffectGroup();
			
			if (parent.IN < 7) {
				nextEffects.Add(new Effects.AddStat(source, parent, EStat.IN, 1));
			}
			if (parent.Body.CanEnter(c)) {
				nextEffects.Add(new Effects.Move(source, parent, c));
			}
			
			if (nextEffects.Count > 0) {EffectQueue.Add(nextEffects);}
		}
	}

	public class Cannibalize : Task {
		
		public override string desc {get {return "Destroy target remains." +
				"\nHealth +10/10";} } 
		
		public Cannibalize (Unit parent) : base(parent) {
			name = "Cannibalize";
			weight = 4;
			aims += Aim.AttackNeighbor(Filters.Corpses);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Token t = (Token)targets[0];
			
			t.Die(source);
			parent.AddStat(source, EStat.MHP, 10);
			parent.AddStat(source, EStat.HP, 10);
		}
	}




}

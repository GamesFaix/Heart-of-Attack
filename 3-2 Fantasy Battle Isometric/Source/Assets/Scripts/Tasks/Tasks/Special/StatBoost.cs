using UnityEngine; 

namespace HOA.Actions { 

	public class TimeMine : Task {
		
		public override string Desc {get {return "Destroy neighboring destructible." +
				"\nIf initative is less than 6, initiative +1.";} }
		
		public TimeMine (Unit parent) {
			Name = "Time Mine";
			Weight = 4;
			Parent = parent;
			Price = Price.Cheap;
			NewAim(new Aim(ETraj.NEIGHBOR, Special.DestRem));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Token t = (Token)targets[0];
			Cell c = t.Body.Cell;
			
			EffectQueue.Add(new Effects.Destruct(new Source(Parent), t));
			
			EffectGroup nextEffects = new EffectGroup();
			
			if (Parent.IN < 7) {
				nextEffects.Add(new Effects.AddStat(new Source(Parent), Parent, EStat.IN, 1));
			}
			if (Parent.Body.CanEnter(c)) {
				nextEffects.Add(new Effects.Move(new Source(Parent), Parent, c));
			}
			
			if (nextEffects.Count > 0) {EffectQueue.Add(nextEffects);}
		}
	}

	public class Cannibalize : Task {
		
		public override string Desc {get {return "Destroy target remains." +
				"\nHealth +10/10";} } 
		
		public Cannibalize (Unit par) {
			Name = "Cannibalize";
			Weight = 4;
			Price = new Price(1,0);
			Parent = par;
			NewAim(new Aim (ETraj.NEIGHBOR, ESpecial.REM));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Token t = (Token)targets[0];
			
			t.Die(new Source(Parent));
			Parent.AddStat(new Source(Parent), EStat.MHP, 10);
			Parent.AddStat(new Source(Parent), EStat.HP, 10);
		}
	}




}

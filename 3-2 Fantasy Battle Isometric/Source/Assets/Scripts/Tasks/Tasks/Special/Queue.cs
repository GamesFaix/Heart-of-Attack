using UnityEngine; 

namespace HOA.Actions { 

	public class HourSaviour : Task {
		
		public override string Desc {get {return "Target Unit shifts to the bottom of the Queue";} }
		
		public HourSaviour (Unit parent) {
			Parent = parent;
			Name = "Hour Saviour";
			Weight = 4;
			Price = new Price(0,2);
			NewAim(Aim.Free(Special.Unit, EPurp.ATTACK));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Unit u = (Unit)targets[0];
			
			int last = TurnQueue.Count-1;
			int current = TurnQueue.IndexOf(u);
			int magnitude = 0-(last-current);
			
			EffectQueue.Add(new Effects.Shift(new Source(Parent), u, magnitude));
		}
	}
	
	public class MinuteWaltz : Task {
		public override string Desc {get {return "Shuffle the Queue." +
				"\n(End "+Parent.ID.Name+"'s turn.)";} }
		
		public MinuteWaltz (Unit parent) {
			Parent = parent;
			Name = "Minute Waltz";
			Weight = 4;
			
			Price = new Price(1,1);
			NewAim(Aim.Self());
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Shuffle(new Source(Parent)));
			EffectQueue.Add(new Effects.Advance(new Source(Parent), false));
		}
	}
	
	public class SecondInCommand : Task {
		public override string Desc {get {return "Target unit takes the next turn." +
				"\n(Cannot target self.)";} }
		
		public SecondInCommand (Unit parent) {
			Parent = parent;
			Name = "Second in Command";
			Weight = 4;
			Price = new Price(0,2);
			NewAim(Aim.Free(Special.Unit, EPurp.ATTACK));

			Aims[0].IncludeSelf = false;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Unit u = (Unit)targets[0];
			int magnitude = TurnQueue.IndexOf(u) - 1;
			EffectQueue.Add(new Effects.Shift (new Source(Parent), u, magnitude));
		}
	}




}

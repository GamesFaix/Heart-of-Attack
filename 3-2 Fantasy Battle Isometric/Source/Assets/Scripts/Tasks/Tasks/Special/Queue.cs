using UnityEngine; 

namespace HOA.Actions { 

	public class HourSaviour : Task {
		
		public override string desc {get {return "Target Unit shifts to the bottom of the Queue";} }
		
		public HourSaviour (Unit parent) : base(parent) {
			name = "Hour Saviour";
			weight = 4;
			price = new Price(0,2);
			aims += Aim.Free(Filters.Units);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Unit u = (Unit)targets[0];
			
			int last = TurnQueue.Count-1;
			int current = TurnQueue.IndexOf(u);
			int magnitude = 0-(last-current);
			
			EffectQueue.Add(new Effects.Shift(source, u, magnitude));
		}
	}
	
	public class MinuteWaltz : Task {
		public override string desc {get {return "Shuffle the Queue." +
				"\n(End "+parent.ID.Name+"'s turn.)";} }
		
		public MinuteWaltz (Unit parent) : base(parent) {
			name = "Minute Waltz";
			weight = 4;
			price = new Price(1,1);
			aims += Aim.Self();
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Shuffle(source));
			EffectQueue.Add(new Effects.Advance(source, false));
		}
	}
	
	public class SecondInCommand : Task {
		public override string desc {get {return "Target unit takes the next turn." +
				"\n(Cannot target self.)";} }
		
		public SecondInCommand (Unit parent) : base(parent) {
			name = "Second in Command";
			weight = 4;
			price = new Price(0,2);
			aims += Aim.Free(Filters.UnitsNoSelf);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Unit u = (Unit)targets[0];
			int magnitude = TurnQueue.IndexOf(u) - 1;
			EffectQueue.Add(new Effects.Shift (source, u, magnitude));
		}
	}




}

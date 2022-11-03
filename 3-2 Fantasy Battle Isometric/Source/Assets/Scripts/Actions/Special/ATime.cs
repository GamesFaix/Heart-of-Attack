using UnityEngine; 

namespace HOA { 

	public class AOldtHour : Task {
		
		public override string Desc {get {return "Target Unit shifts to the bottom of the Queue";} }
		
		public AOldtHour (Unit parent) {
			Parent = parent;
			Name = "Hour Saviour";
			Weight = 4;
			Price = new Price(0,2);
			NewAim(new Aim(ETraj.FREE, EType.UNIT));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Unit u = (Unit)targets[0];
			
			int last = TurnQueue.Count-1;
			int current = TurnQueue.IndexOf(u);
			int magnitude = 0-(last-current);
			
			EffectQueue.Add(new EShift(new Source(Parent), u, magnitude));
		}
	}
	
	public class AOldMinute : Task {
		public override string Desc {get {return "Shuffle the Queue." +
				"\n(End "+Parent.ID.Name+"'s turn.)";} }
		
		public AOldMinute (Unit parent) {
			Parent = parent;
			Name = "Minute Waltz";
			Weight = 4;
			
			Price = new Price(1,1);
			NewAim(HOA.Aim.Self());
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new EShuffle(new Source(Parent)));
			EffectQueue.Add(new EAdvance(new Source(Parent), false));
		}
	}
	
	public class AOldtSecond : Task {
		public override string Desc {get {return "Target unit takes the next turn." +
				"\n(Cannot target self.)";} }
		
		public AOldtSecond (Unit parent) {
			Parent = parent;
			Name = "Second in Command";
			Weight = 4;
			Price = new Price(0,2);
			NewAim(new Aim(ETraj.FREE, EType.UNIT));

			Aim[0].IncludeSelf = false;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Unit u = (Unit)targets[0];
			int magnitude = TurnQueue.IndexOf(u) - 1;
			EffectQueue.Add(new EShift (new Source(Parent), u, magnitude));
		}
	}




}

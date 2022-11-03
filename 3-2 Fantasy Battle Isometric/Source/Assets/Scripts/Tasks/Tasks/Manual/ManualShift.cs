using UnityEngine; 

namespace HOA.Actions { 

	public class ManualShift : Task, IManualFree, IMultiTarget{
		
		public override string desc {get {return "Move up to 10 units in the queue.";} }
		
		int change;
		
		public ManualShift (int change) {
			parent = null;
			name = "Manual Queue Shift: "+change;
			weight = 0;
			price = Price.Free;
			
			this.change = change;
			
			for (byte i=0; i<10; i++) {
				aims += Aim.Free(Filters.Units);
			}
		}
		
		protected override void ExecuteStart () {}
		protected override void ExecuteMain (TargetGroup targets) {
			foreach (Target target in targets) {
				EffectQueue.Add(new Effects.Shift(Source.Neutral, (Unit)target, change));
			}
		}
		
		public override bool Legal(out string message) {
			message = name+" currently legal.";
			if (EffectQueue.Processing) {
				message = "Another action is currently in progress.";
				return false;
			}
			return true;
		}
	}
}

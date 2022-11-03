using UnityEngine; 

namespace HOA.Actions { 

	public class ManualShift : Task, IManualFree, IMultiTarget{
		
		public override string Desc {get {return "Move up to 10 units in the queue.";} }
		
		int change;
		
		public ManualShift (int change) {
			Parent = null;
			Name = "Manual Queue Shift: "+change;
			Weight = 0;
			Price = Price.Free;
			
			this.change = change;
			
			NewAim(Aim.Free(Special.Unit, EPurp.ATTACK));
			for (int i=2; i<=10; i++) {
				Aims.Add(Aim.Free(Special.Unit, EPurp.ATTACK));
			}
		}
		
		protected override void ExecuteStart () {}
		protected override void ExecuteMain (TargetGroup targets) {
			foreach (Target target in targets) {
				EffectQueue.Add(new Effects.Shift(new Source(Roster.Neutral), (Unit)target, change));
			}
		}
		
		public override bool Legal(out string message) {
			message = Name+" currently legal.";
			if (EffectQueue.Processing) {
				message = "Another action is currently in progress.";
				return false;
			}
			return true;
		}
	}
}

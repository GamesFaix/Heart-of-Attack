using UnityEngine; 

namespace HOA.Actions { 
	
	public class ManualSet : Task, IManualFree, IMultiTarget{
		
		public override string desc {get {return "Set stat of up to 10 units.";} }
		
		EStat stat;
		int newValue;
		
		public ManualSet (EStat stat, int newValue) {
			parent = null;
			name = "Manual "+stat+" Set to "+newValue;
			weight = 0;
			price = Price.Free;
			
			this.stat = stat;
			this.newValue = newValue;
			
			for (byte i=0; i<10; i++) {
				aims += Aim.Free(Filters.Units);
			}
		}
		protected override void ExecuteStart () {}
		protected override void ExecuteMain (TargetGroup targets) {
			foreach (Target target in targets) {
				EffectQueue.Add(new Effects.SetStat(Source.Neutral, (Unit)target, stat, newValue));
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

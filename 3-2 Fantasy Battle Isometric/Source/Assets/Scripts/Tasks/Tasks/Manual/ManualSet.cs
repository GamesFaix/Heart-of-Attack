using UnityEngine; 

namespace HOA.Actions { 
	
	public class ManualSet : Task, IManualFree, IMultiTarget{
		
		public override string Desc {get {return "Set stat of up to 10 units.";} }
		
		EStat stat;
		int newValue;
		
		public ManualSet (EStat stat, int newValue) {
			Parent = null;
			Name = "Manual "+stat+" Set to "+newValue;
			Weight = 0;
			Price = Price.Free;
			
			this.stat = stat;
			this.newValue = newValue;
			
			NewAim(Aim.Free(Special.Unit, EPurp.ATTACK));
			for (int i=2; i<=10; i++) {
				Aims.Add(Aim.Free(Special.Unit, EPurp.ATTACK));
			}
		}
		protected override void ExecuteStart () {}
		protected override void ExecuteMain (TargetGroup targets) {
			foreach (Target target in targets) {
				EffectQueue.Add(new Effects.SetStat(new Source(Roster.Neutral), (Unit)target, stat, newValue));
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

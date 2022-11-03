using UnityEngine; 

namespace HOA.Actions { 

	public class ManualAdd : Task, IManualFree, IMultiTarget{
		
		public override string desc {get {return "Increase/Descrease stat of up to 10 units.";} }

		EStat stat;
		int change;

		public ManualAdd (EStat stat, int change) {
			parent = null;
			name = "Manual "+stat+" Add "+change;
			weight = 0;
			price = Price.Free;

			this.stat = stat;
			this.change = change;

			for (int i=0; i<10; i++) {
				aims += Aim.Free(Filters.Units);
			}
		}
		protected override void ExecuteStart () {}
		protected override void ExecuteMain (TargetGroup targets) {
			foreach (Target target in targets) {
				EffectQueue.Add(new Effects.AddStat(Source.Neutral, (Unit)target, stat, change));
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

using UnityEngine; 

namespace HOA.Actions { 

	public class ManualKill : Task, IManualFree, IMultiTarget{

		public override string desc {get {return "Kill up to 10 tokens.";} }
		
		public ManualKill () {
			parent = null;
			name = "Manual Kill";
			
			weight = 0;
			price = Price.Free;
			
			for (byte i=0; i<10; i++) {
				aims += Aim.Free(Filters.Tokens);
			}
		}
		protected override void ExecuteStart () {}
		protected override void ExecuteMain (TargetGroup targets) {
			foreach (Target target in targets) {
				if (target is Unit) {EffectQueue.Add(new Effects.Kill(Source.Neutral, (Token)target));}
				else {EffectQueue.Add(new Effects.Destruct(Source.Neutral, (Token)target));}
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

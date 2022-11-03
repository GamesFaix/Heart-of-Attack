using System.Collections.Generic;

namespace HOA.Actions {
	public class ManualEnd : Task, IManualFree {
		
		public override string desc {get {return "End current turn.";} }
		
		public ManualEnd () {
			parent = null;
			name = "Manual End Turn";
			weight = 0;
			price = Price.Free;
			aims += Aim.Self();
		}
		protected override void ExecuteStart () {}
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Advance(Source.Neutral));
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

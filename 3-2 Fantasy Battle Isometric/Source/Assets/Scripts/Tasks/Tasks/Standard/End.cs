using System.Collections.Generic;

namespace HOA.Actions {
	public class End : Task {

		public override string desc {get {return "";} }

		public End (Unit parent) : base(parent) {
			name = "End turn";
			weight = 0;
			price = Price.Free;
			aims += Aim.Self();
		}

		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Advance(source));
		}
	}
}

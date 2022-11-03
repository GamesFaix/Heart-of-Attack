using System.Collections.Generic;

namespace HOA.Actions {
	public class End : Task {

		public override string Desc {get {return "";} }

		public End (Unit parent) {
			Parent = parent;
			Name = "End turn";
			Weight = 0;
			Price = Price.Free;
			NewAim(Aim.Self());
		}

		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Advance(new Source(Parent)));
		}
	}
}

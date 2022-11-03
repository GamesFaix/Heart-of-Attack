using System.Collections.Generic;

namespace HOA {
	public class AEnd : Task {

		public override string Desc {get {return "";} }

		public AEnd (Unit parent) {
			Parent = parent;
			Name = "End turn";
			Weight = 0;
			Price = Price.Free;
			AddAim(HOA.Aim.Self());
		}

		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new EAdvance(new Source(Parent)));
		}
	}
}

using System.Collections.Generic;

namespace HOA {
	public class AFocus : Task {

		public override string Desc {get {return "Focus +1.";} }

		public AFocus (Unit parent) {
			Parent = parent;
			Name = "Focus";
			Weight = 2;
			Price = Price.Cheap;
			NewAim(HOA.Aim.Self());
		}

		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new EAddStat(new Source(Parent), Parent, EStat.FP, 1));
		}
	}
}

using System.Collections.Generic;

namespace HOA.Actions {
	public class ManualEnd : Task, IManualFree {
		
		public override string Desc {get {return "End current turn.";} }
		
		public ManualEnd () {
			Parent = null;
			Name = "Manual End Turn";
			Weight = 0;
			Price = Price.Free;
			NewAim(HOA.Aim.Self());
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Advance(new Source(Roster.Neutral)));
		}

		public override bool Legal {get {return true;} }
		public override void Charge () {}
	}
}

using UnityEngine; 

namespace HOA.Actions { 

	public class ManualKill : Task, IManualFree, IMultiTarget{

		public override string Desc {get {return "Kill up to 10 tokens.";} }
		
		public ManualKill () {
			Parent = null;
			Name = "Manual Kill";
			
			Weight = 0;
			Price = Price.Free;
			
			NewAim(Aim.Free(Special.Token, EPurp.ATTACK));
			for (int i=2; i<=10; i++) {
				Aims.Add(Aim.Free(Special.Token, EPurp.ATTACK));
			}
		}
		protected override void ExecuteStart () {}
		protected override void ExecuteMain (TargetGroup targets) {
			foreach (Target target in targets) {
				if (target is Unit) {EffectQueue.Add(new Effects.Kill(new Source(Roster.Neutral), (Token)target));}
				else {EffectQueue.Add(new Effects.Destruct(new Source(Roster.Neutral), (Token)target));}
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

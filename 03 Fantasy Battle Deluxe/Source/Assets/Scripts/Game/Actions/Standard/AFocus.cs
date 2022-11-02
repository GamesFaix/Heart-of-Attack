using System.Collections.Generic;

namespace HOA {
	public class AFocus : Action {

		int magnitude;
		
		public AFocus (Unit u, int n=1) {
			weight = 2;
			actor = u;
			price = Price.Cheap;
			AddAim(HOA.Aim.Self);
			
			name = "Focus";
			desc = "Focus +1";
			magnitude = n;
			
		}
		
		
		public override void Execute (List<ITargetable> targets) {
			Charge();
			InputBuffer.Submit(new RAddStat(new Source(actor), (Token)actor, EStat.FP, magnitude));
			actor.SpriteEffect(EEffect.STATUP);
		}
	}
}

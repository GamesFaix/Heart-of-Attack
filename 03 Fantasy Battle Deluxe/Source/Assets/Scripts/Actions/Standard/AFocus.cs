using System.Collections;

namespace HOA {
	public class AFocus : Action {

		int magnitude;
		
		public AFocus (Unit u, int n=1) {
			weight = 2;
			actor = u;
			price = Price.Cheap;
			aim = new Aim (AIMTYPE.SELF, TARGET.SELF, CTAR.NA);
			
			name = "Focus";
			desc = "Focus +1";
			magnitude = n;
			
		}
		
		
		public override void Perform () {
			if (Charge()) {
				InputBuffer.Submit(new RAddStat(new Source(actor), (Token)actor, STAT.FP, magnitude));
				actor.SpriteEffect(EFFECT.STATUP);
			}
		}
	}
}

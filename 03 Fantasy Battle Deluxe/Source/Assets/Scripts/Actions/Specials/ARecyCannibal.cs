using HOA.Tokens;
using HOA.Map;

namespace HOA.Actions {
	
	public class ARecyCannibal : Action {
		
		Cell cell;

		public ARecyCannibal (Price p, Unit par) {
			weight = 4;
			price = p;
			actor = par;
			aim = new Aim (AIMTYPE.NEIGHBOR, TARGET.TOKEN, TTAR.REM);

			name = "Cannibalize";
			desc = "Destroy target remains.\nHP +10/10";
		}
		
		public override void Perform () {
			if (Charge()) {
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new RRecyCannibal(new Source(actor), default(Token)));
			}
		}
	}
}
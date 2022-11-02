using HOA.Tokens;
using HOA.Map;

namespace HOA.Actions {
	
	public class AMycoSeed : Action {
		
		Cell cell;
		Token template;
		
		public AMycoSeed (Price p, Unit par) {
			weight = 5;
			price = p;
			actor = par;
			aim = new Aim (AIMTYPE.ARC, TARGET.TOKEN, TTAR.DEST, 2);

			name = "Seed";
			desc = "Replace target non-Remains destructible with Lichenthrope.";
		}
		
		public override void Perform () {
			if (Charge()) {
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new RReplace(new Source(actor), default(Token), TTYPE.LICH));
			}
		}
	}
}
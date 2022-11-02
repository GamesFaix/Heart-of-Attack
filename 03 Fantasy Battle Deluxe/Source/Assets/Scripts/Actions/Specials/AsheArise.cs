using HOA.Tokens;
using HOA.Map;

namespace HOA.Actions {

	public class AsheArise : Action {
		
		TTYPE child;
		Token chiTemplate;
		
		public AsheArise (Price p, Unit par, TTYPE chi) {
			price = p;
			aim = new Aim (AIMTYPE.SELF, TARGET.SELF, TTAR.NA);

			actor = par;
			child = chi;
			chiTemplate = TemplateFactory.Template(child);

			name = chiTemplate.Name;
			desc = "Transform "+actor+" into a "+name+".\nNew "+name+" starts with "+actor+"'s HP.";
		}
		
		public override void Perform () {
			if (Charge()) {
				InputBuffer.Submit(new RAsheArise(new Source(actor), actor, child));
			}
		}
	}
}

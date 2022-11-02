using HOA.Tokens;
using HOA.Map;

namespace HOA.Actions {

	public class AsheArise : Action {
		
		TTYPE child;
		Token chiTemplate;
		
		public AsheArise (Price p, Unit par, TTYPE chi) {
			weight = 4;
			price = p;
			aim = new Aim (AIMTYPE.SELF, TARGET.SELF, TTAR.NA);

			actor = par;
			child = chi;
			chiTemplate = TemplateFactory.Template(child);

			name = chiTemplate.Name;
			desc = "Transform "+actor+" into a "+name+".\n(New "+name+" starts with "+actor+"'s health.)";
		}
		
		public override void Perform () {
			if (Charge()) {
				InputBuffer.Submit(new RAsheArise(new Source(actor), actor, child));
			}
		}
	}
}
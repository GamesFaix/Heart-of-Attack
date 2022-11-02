using HOA.Tokens;
using HOA.Map;

namespace HOA.Actions {

	public class AEvolve : Action {

		TTYPE child;
		Token chiTemplate;
		
		public AEvolve (Price p, Unit par, TTYPE chi) {
			weight = 4;
			price = p;
			aim = new Aim (AIMTYPE.SELF, TARGET.SELF, TTAR.NA);

			actor = par;
			child = chi;
			chiTemplate = TemplateFactory.Template(child);

			name = chiTemplate.Name;
			desc = "Transform "+actor+" into a "+name+".  \n(New "+name+" is added to the end of the Queue and does not retain any of "+actor+"'s attributes.)";
		}
		
		public override void Perform () {
			if (Charge()) {
				InputBuffer.Submit(new RReplace(new Source(actor), actor, child));
			}
		}
	}
}

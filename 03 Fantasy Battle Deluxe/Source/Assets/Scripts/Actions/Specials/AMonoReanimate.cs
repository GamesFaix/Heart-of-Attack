using HOA.Tokens;
using HOA.Map;

namespace HOA.Actions {

	public class AMonoReanimate : Action {
		
		Cell cell;
		TTYPE child;
		Token chiTemplate;
		
		public AMonoReanimate (Price p, Unit par, TTYPE chi) {
			price = p;
			actor = par;
			aim = new Aim (AIMTYPE.NEIGHBOR, TARGET.TOKEN, TTAR.REM);
			
			child = chi;
			chiTemplate = TemplateFactory.Template(child);
			
			name = "Reanimate "+chiTemplate.Name();
			desc = "Replace target remains with "+name+".";
		}
		
		public override void Perform () {
			if (Charge()) {
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new RReplace(new Source(actor), default(Token), child));
			}
		}
	}
}
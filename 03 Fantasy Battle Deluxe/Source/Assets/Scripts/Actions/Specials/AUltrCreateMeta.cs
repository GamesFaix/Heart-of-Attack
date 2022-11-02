using HOA.Tokens;
using HOA.Map;

namespace HOA.Actions {

	public class AUltrCreateMeta : Action {
		
		Cell cell;
		TTYPE child;
		Token chiTemplate;
		
		public AUltrCreateMeta (Price p, Unit par, TTYPE chi) {
			weight = 5;
			price = p;
			actor = par;
			aim = new Aim (AIMTYPE.NEIGHBOR, TARGET.TOKEN, TTAR.DEST);
			
			child = chi;
			chiTemplate = TemplateFactory.Template(child);
			
			name = chiTemplate.Name;
			desc = "Replace target non-remains destructible with "+name+".";
		}
		
		public override void Perform () {
			if (Charge()) {
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new RReplace(new Source(actor), default(Token), child));
			}
		}
	}
}
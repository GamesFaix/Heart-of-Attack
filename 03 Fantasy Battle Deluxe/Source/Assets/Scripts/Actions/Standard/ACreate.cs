
namespace HOA {

	public class ACreate : Action {


		Cell cell;
		TTYPE child;
		Token chiTemplate;
		
		public ACreate (Price p, Unit par, TTYPE chi, Aim a=default(Aim)) {
			weight = 5;
			actor = par;
			child = chi;
			chiTemplate = TemplateFactory.Template(child);
			price = p;
			
			aim = new Aim(AIMTYPE.NEIGHBOR, TARGET.CELL, CTAR.CREATE);
			if (a != default(Aim)) {aim = a;}
			
			name = "Create "+chiTemplate.Name;
			desc = "Create "+chiTemplate.Name+" in target cell.";
		}
		
		public override void Perform () {
			if (Charge()) {
				Legalizer.Find(actor, aim, chiTemplate);
				GUISelectors.DoWithCell(new RCreate(new Source(actor), child, default(Cell)));
			}
		}
	}
}

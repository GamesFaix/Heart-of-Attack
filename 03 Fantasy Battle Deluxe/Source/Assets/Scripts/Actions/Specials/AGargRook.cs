using HOA.Tokens;
using HOA.Map;

namespace HOA.Actions {
	
	public class AGargRook : Action {
		
		Token template;
		
		public AGargRook (Price p, Unit par) {
			weight = 5;
			actor = par;
			template = TemplateFactory.Template(TTYPE.ROOK);
			price = p;
			
			aim = new Aim(AIMTYPE.SELF, TARGET.CELL, CTAR.CREATE);

			name = template.Name;
			desc = "Create "+name+" in "+actor+"'s cell.";
		}
		
		public override void Perform () {
			Cell c = actor.Cell;
			if (Charge() && !c.Occupied(PLANE.GND)) {
				TokenFactory.Add(TTYPE.ROOK, new Source(actor), c);
			}
		}
	}
}

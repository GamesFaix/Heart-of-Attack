using System.Collections.Generic;

namespace HOA {
	
	public class ACreateManual : Action {
		
		
		Cell cell;
		EToken child;
		
		public ACreateManual (Unit par, EToken chi) {
			weight = 5;
			actor = par;
			child = chi;
			childTemplate = TemplateFactory.Template(child);
			price = new Price(0,0);
			
			AddAim(new Aim(EAim.FREE, EClass.CELL, EPurpose.CREATE));
			
			name = "Create "+childTemplate.Name;
			desc = "Create "+childTemplate.Name+" in target cell.";
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();
			InputBuffer.Submit(new RCreate(new Source(actor), child, (Cell)targets[0]));
		}
	}
}

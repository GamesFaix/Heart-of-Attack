using System.Collections.Generic;
using UnityEngine;

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
			
			AddAim(new Aim(ETraj.FREE, EType.CELL, EPurp.CREATE));
			
			name = "Manual Create "+childTemplate.ID.Name;
			desc = "Create "+childTemplate.ID.Name+" in any cell.";
		}
		
		public override void Execute (List<ITarget> targets) {
			Charge();
			EffectQueue.Add(new ECreate(new Source(actor), child, (Cell)targets[0]));
			Targeter.Reset();
		}
	}
}

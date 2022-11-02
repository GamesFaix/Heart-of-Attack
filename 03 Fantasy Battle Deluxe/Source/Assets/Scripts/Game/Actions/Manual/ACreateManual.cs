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
			
			AddAim(new Aim(EAim.FREE, EClass.CELL, EPurpose.CREATE));
			
			name = "Manual Create "+childTemplate.Name;
			desc = "Create "+childTemplate.Name+" in any cell.";
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();
			EffectQueue.Add(new ECreate(new Source(actor), child, (Cell)targets[0]));
			Targeter.Reset();
		}
	}
}

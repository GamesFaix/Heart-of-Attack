using System.Collections.Generic;
using UnityEngine;

namespace HOA {
	
	public class ACreateManual : Task {

		public override string Desc {get {return "Create "+Template.ID.Name+" in any cell.";} }

		Cell cell;
		EToken child;
		
		public ACreateManual (Unit parent, EToken child) {
			Parent = parent;
			this.child = child;
			Template = TemplateFactory.Template(child);
			Name = "Manual Create "+Template.ID.Name;

			Weight = 5;
			Price = Price.Free;
			
			NewAim(new Aim(ETraj.FREE, EType.CELL, EPurp.CREATE));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new ECreate(new Source(Parent), child, (Cell)targets[0]));
		}
	}
}

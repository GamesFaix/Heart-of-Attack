using System.Collections.Generic;
using UnityEngine;

namespace HOA.Actions {
	
	public class ManualCreate : Task, IMultiTarget {

		public override string Desc {get {return "Create "+Template.ID.Name+" in upto 10 cells.";} }

		EToken child;
		
		public ManualCreate (Unit parent, EToken child) {
			Parent = parent;
			this.child = child;
			Template = TokenFactory.Template(child);
			Name = "Manual Create "+Template.ID.Name;

			Weight = 5;
			Price = Price.Free;
			
			NewAim(Aim.Free(Special.Cell, EPurp.CREATE));
			for (int i=2; i<=10; i++) {
				Aims.Add(Aim.Free(Special.Cell, EPurp.CREATE));
			}
		}
		protected override void ExecuteStart () {}
		protected override void ExecuteMain (TargetGroup targets) {
			foreach (Target target in targets) {
				EffectQueue.Add(new Effects.Create(new Source(Parent), child, (Cell)target));
			}
		}

		public override bool Legal(out string message) {
			message = Name+" currently legal.";
			if (EffectQueue.Processing) {
				message = "Another action is currently in progress.";
				return false;
			}
			return true;
		}
	}
}

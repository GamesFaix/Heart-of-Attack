using System.Collections.Generic;
using UnityEngine;

namespace HOA.Actions {
	
	public class ManualCreate : Task, IMultiTarget {

		public override string desc {get {return "Create "+template.ID.Name+" in upto 10 cells.";} }

		EToken child;
		
		public ManualCreate (Unit parent, EToken child) : base(parent) {
			this.child = child;
			template = TokenFactory.Template(child);
			name = "Manual Create "+template.ID.Name;

			weight = 5;
			price = Price.Free;
			
			for (int i=0; i<10; i++) {
				aims += Aim.Free(Filters.Create);
			}
		}
		protected override void ExecuteStart () {}
		protected override void ExecuteMain (TargetGroup targets) {
			foreach (Target target in targets) {
				EffectQueue.Add(new Effects.Create(source, child, (Cell)target));
			}
		}

		public override bool Legal(out string message) {
			message = name+" currently legal.";
			if (EffectQueue.Processing) {
				message = "Another action is currently in progress.";
				return false;
			}
			return true;
		}
	}
}

using UnityEngine;
using System.Collections.Generic;

namespace HOA.Actions {
	
	public class Move : Task {

		public override string desc {get {return "Move "+parent+" to target cell.";} }

		public Move (Unit parent, int range) : base(parent) {
			name = "Move";
			weight = 1;
			aims += Aim.MovePath(range);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			foreach (Target target in targets) {
				EffectQueue.Add(new Effects.Move(source, parent, (Cell)target));
			}
		}
	}
}

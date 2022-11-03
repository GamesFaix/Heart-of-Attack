using UnityEngine;
using System.Collections.Generic;

namespace HOA {
	
	public class AMovePath : Task, IMultiMove {

		public override string Desc {get {return "Move "+Parent+" to target cell.";} }

		public AMovePath (Unit parent, int r) {
			Parent = parent;
			Name = "Move";
			Weight = 1;
			NewAim(HOA.Aim.MovePath(r));
			Price = Price.Cheap;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			foreach (Target target in targets) {
				EffectQueue.Add(new EMove(new Source(Parent), Parent, (Cell)target));
			}
		}
	}
}

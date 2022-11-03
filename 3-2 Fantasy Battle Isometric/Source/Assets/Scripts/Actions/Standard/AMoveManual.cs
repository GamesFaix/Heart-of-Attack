using UnityEngine;
using System.Collections.Generic;

namespace HOA {
	
	public class AMoveManual : Task {

		public override string Desc {get {return "Move "+Parent+" to target cell.";} }

		Token mover;

		public AMoveManual (Unit parent, Token mover) {
			Parent = parent;
			this.mover = mover;
			Name = "Manual Move";
			Weight = 1;
			Price = Price.Free;
			NewAim( new Aim (ETraj.FREE, EType.CELL, EPurp.MOVE));
		}

		public override bool Legal {get {return true;} }
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new EMove(new Source(Parent), mover, (Cell)targets[0]));
		}
	}
}

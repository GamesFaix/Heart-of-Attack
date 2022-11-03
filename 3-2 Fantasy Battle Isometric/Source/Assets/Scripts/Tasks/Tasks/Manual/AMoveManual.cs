using UnityEngine;
using System.Collections.Generic;

namespace HOA {
	
	public class AMoveManual : Task, IManualFree, ITeleport {

		public override string Desc {get {return "Move target token to target cell.";} }

		public AMoveManual () {
			Parent = null;
			Name = "Manual Move";
			Weight = 0;
			Price = Price.Free;
			NewAim(new Aim (ETraj.FREE, EType.TOKEN, EPurp.ATTACK));
			Aim.Add(new Aim (ETraj.FREE, EType.CELL, EPurp.MOVE));
		}

		public override bool Legal {get {return true;} }
		public override void Charge () {}

		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new ETeleport(new Source(Roster.Neutral), (Token)targets[0], (Cell)targets[1]));
		}
	}
}

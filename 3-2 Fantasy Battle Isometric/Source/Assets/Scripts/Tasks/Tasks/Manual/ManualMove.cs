using UnityEngine;
using System.Collections.Generic;

namespace HOA.Actions {
	
	public class ManualMove : Task, IManualFree, ITeleport {

		public override string Desc {get {return "Move target token to target cell.";} }

		public ManualMove () {
			Parent = null;
			Name = "Manual Move";
			Weight = 0;
			Price = Price.Free;
			NewAim(Aim.Free(Special.Token, EPurp.ATTACK));
			Aims.Add(Aim.Free(Special.Cell, EPurp.MOVE));
		}
		protected override void ExecuteStart () {}
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Teleport(new Source(Roster.Neutral), (Token)targets[0], (Cell)targets[1]));
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

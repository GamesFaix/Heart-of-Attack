using UnityEngine;
using System.Collections.Generic;

namespace HOA.Actions {
	
	public class ManualMove : Task, IManualFree, ITeleport {

		public override string desc {get {return "Move target token to target cell.";} }

		public ManualMove () {
			parent = null;
			name = "Manual Move";
			weight = 0;
			price = Price.Free;
			aims += Aim.Free(Filters.Tokens);
			aims += Aim.Free(Filters.Move);
		}
		protected override void ExecuteStart () {}
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Teleport(new Source(Roster.Neutral), (Token)targets[0], (Cell)targets[1]));
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

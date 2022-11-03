﻿using UnityEngine; 

namespace HOA.Actions { 

	public class ManualShift : Task, IManualFree, IMultiTarget{
		
		public override string Desc {get {return "Move up to 10 units in the queue.";} }
		
		int change;
		
		public ManualShift (int change) {
			Parent = null;
			Name = "Manual Queue Shift: "+change;
			Weight = 0;
			Price = Price.Free;
			
			this.change = change;
			
			NewAim(new Aim(ETraj.FREE, ESpecial.UNIT, EPurp.ATTACK));
			for (int i=2; i<=10; i++) {
				Aim.Add(new Aim(ETraj.FREE, ESpecial.UNIT, EPurp.ATTACK));
			}
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			foreach (Target target in targets) {
				EffectQueue.Add(new Effects.Shift(new Source(Roster.Neutral), (Unit)target, change));
			}
		}
		
		public override bool Legal {get {return true;} }
		public override void Charge () {}
	}
}
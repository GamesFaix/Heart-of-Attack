using UnityEngine; 

namespace HOA.Actions { 
	
	public class ManualOwner : Task, IManualFree, IMultiTarget{
		
		public override string desc {get {return "Set owner of up to 10 units.";} }
		
		Player owner;

		public ManualOwner (Player owner) {
			parent = null;
			name = "Manual Set Owner: "+owner.ToString();
			weight = 0;
			price = Price.Free;
			this.owner = owner;

			for (byte i=0; i<10; i++) {
				aims += Aim.Free(Filters.Tokens);
			}
		}
		protected override void ExecuteStart () {}
		protected override void ExecuteMain (TargetGroup targets) {
			foreach (Target target in targets) {
				EffectQueue.Add(new Effects.SetOwner(new Source(Roster.Neutral), (Token)target, owner));
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

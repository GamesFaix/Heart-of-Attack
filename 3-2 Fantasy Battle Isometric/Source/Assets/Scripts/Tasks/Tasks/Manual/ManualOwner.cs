using UnityEngine; 

namespace HOA.Actions { 
	
	public class ManualOwner : Task, IManualFree, IMultiTarget{
		
		public override string Desc {get {return "Set owner of up to 10 units.";} }
		
		Player owner;

		public ManualOwner (Player owner) {
			Parent = null;
			Name = "Manual Set Owner: "+owner.ToString();
			Weight = 0;
			Price = Price.Free;
			this.owner = owner;

			NewAim(Aim.Free(Special.Token, EPurp.ATTACK));
			for (int i=2; i<=10; i++) {
				Aims.Add(Aim.Free(Special.Token, EPurp.ATTACK));
			}
		}
		protected override void ExecuteStart () {}
		protected override void ExecuteMain (TargetGroup targets) {
			foreach (Target target in targets) {
				EffectQueue.Add(new Effects.SetOwner(new Source(Roster.Neutral), (Token)target, owner));
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

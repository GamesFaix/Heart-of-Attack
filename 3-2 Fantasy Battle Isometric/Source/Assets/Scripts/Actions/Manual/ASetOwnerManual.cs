using UnityEngine; 

namespace HOA { 
	
	public class ASetOwnerManual : Task, IManualFree, IMultiTarget{
		
		public override string Desc {get {return "Set owner of up to 10 units.";} }
		
		Player owner;

		public ASetOwnerManual (Player owner) {
			Parent = null;
			Name = "Manual Set Owner: "+owner.ToString();
			Weight = 0;
			Price = Price.Free;
			this.owner = owner;

			NewAim(new Aim(ETraj.FREE, EType.UNIT, EPurp.ATTACK));
			for (int i=2; i<=10; i++) {
				Aim.Add(new Aim(ETraj.FREE, EType.TOKEN, EPurp.ATTACK));
			}
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			foreach (Target target in targets) {
				EffectQueue.Add(new ESetOwner(new Source(Roster.Neutral), (Token)target, owner));
			}
		}
		
		public override bool Legal {get {return true;} }
		public override void Charge () {}
	}
}

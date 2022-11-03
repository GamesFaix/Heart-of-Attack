using UnityEngine; 

namespace HOA { 
	
	public class AStatSetManual : Task, IManualFree, IMultiTarget{
		
		public override string Desc {get {return "Set stat of up to 10 units.";} }
		
		EStat stat;
		int newValue;
		
		public AStatSetManual (EStat stat, int newValue) {
			Parent = null;
			Name = "Manual "+stat+" Set to "+newValue;
			Weight = 0;
			Price = Price.Free;
			
			this.stat = stat;
			this.newValue = newValue;
			
			NewAim(new Aim(ETraj.FREE, EType.UNIT, EPurp.ATTACK));
			for (int i=2; i<=10; i++) {
				Aim.Add(new Aim(ETraj.FREE, EType.UNIT, EPurp.ATTACK));
			}
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			foreach (Target target in targets) {
				EffectQueue.Add(new ESetStat(new Source(Roster.Neutral), (Unit)target, stat, newValue));
			}
		}
		
		public override bool Legal {get {return true;} }
		public override void Charge () {}
	}
}

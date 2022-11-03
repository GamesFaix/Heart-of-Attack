using UnityEngine; 

namespace HOA { 

	public class AStatAddManual : Task, IManualFree, IMultiTarget{
		
		public override string Desc {get {return "Increase/Descrease stat of up to 10 units.";} }

		EStat stat;
		int change;

		public AStatAddManual (EStat stat, int change) {
			Parent = null;
			Name = "Manual "+stat+" Add "+change;
			Weight = 0;
			Price = Price.Free;

			this.stat = stat;
			this.change = change;

			NewAim(new Aim(ETraj.FREE, EType.UNIT, EPurp.ATTACK));
			for (int i=2; i<=10; i++) {
				Aim.Add(new Aim(ETraj.FREE, EType.UNIT, EPurp.ATTACK));
			}
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			foreach (Target target in targets) {
				EffectQueue.Add(new EAddStat(new Source(Roster.Neutral), (Unit)target, stat, change));
			}
		}
		
		public override bool Legal {get {return true;} }
		public override void Charge () {}
	}
}

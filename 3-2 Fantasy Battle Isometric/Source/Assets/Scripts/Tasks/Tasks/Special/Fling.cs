using UnityEngine; 

namespace HOA.Actions { 

	public class Fling : Task {
		
		int damage = 16;
		
		public override string desc {get {return "Do "+damage+" damage to target unit.";} }
		
		public Fling (Unit parent) : base(parent) {
			name = "Fling";
			weight = 3;
			price = new Price(1,1);
			aims += Aim.AttackArc(Filters.Units, 0, 3);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Damage (source, (Unit)targets[0], damage));
		}
	}
}

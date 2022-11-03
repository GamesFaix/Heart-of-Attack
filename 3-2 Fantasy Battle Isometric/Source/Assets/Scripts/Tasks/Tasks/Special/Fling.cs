using UnityEngine; 

namespace HOA.Actions { 

	public class Fling : Task {
		
		int damage = 16;
		
		public override string Desc {get {return "Do "+damage+" damage to target unit.";} }
		
		public Fling (Unit u) {
			Name = "Fling";
			Weight = 3;
			Parent = u;
			Price = new Price(1,1);
			NewAim(Aim.AttackArc(Special.Unit, 0, 3));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Damage (new Source(Parent), (Unit)targets[0], damage));
		}
	}
}

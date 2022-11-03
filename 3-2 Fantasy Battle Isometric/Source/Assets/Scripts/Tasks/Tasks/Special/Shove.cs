using UnityEngine; 

namespace HOA.Actions { 

	public class Shove : Task {
		
		int damage = 12;
		int kb = 5;
		int kbdmg = 2;
		
		public override string desc {get {return "Do "+damage+" damage to target unit." +
				"\nKnockback "+kb+" (Move target in a line away from "+parent+", up to "+kb+" cells.)" +
					"\nTarget takes "+kbdmg+" damage per cell knocked back.";} }
		
		public Shove (Unit parent) : base(parent) {
			name = "Shove";
			weight = 4;
			price = new Price(1,1);
			aims += Aim.AttackNeighbor(Filters.UnitDest);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectGroup e = new EffectGroup();
			e.Add(new Effects.Damage (source, (Unit)targets[0], damage));
			e.Add(new Effects.Knockback (source, (Unit)targets[0], kb, kbdmg));
			EffectQueue.Add(e);
		}
	}
}

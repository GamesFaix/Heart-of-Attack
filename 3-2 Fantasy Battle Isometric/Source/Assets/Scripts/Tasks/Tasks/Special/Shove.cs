using UnityEngine; 

namespace HOA.Actions { 

	public class Shove : Task {
		
		int damage = 12;
		int kb = 5;
		int kbdmg = 2;
		
		public override string Desc {get {return "Do "+damage+" damage to target unit." +
				"\nKnockback "+kb+" (Move target in a line away from "+Parent+", up to "+kb+" cells.)" +
					"\nTarget takes "+kbdmg+" damage per cell knocked back.";} }
		
		public Shove (Unit u) {
			Name = "Shove";
			Weight = 4;
			
			Parent = u;
			Price = new Price(1,1);
			NewAim(Aim.AttackNeighbor(Special.UnitDest));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectGroup e = new EffectGroup();
			e.Add(new Effects.Damage (new Source(Parent), (Unit)targets[0], damage));
			e.Add(new Effects.Knockback (new Source(Parent), (Unit)targets[0], kb, kbdmg));
			EffectQueue.Add(e);
		}
	}
}

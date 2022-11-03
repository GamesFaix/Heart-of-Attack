using UnityEngine; 

namespace HOA.Actions { 

	public class LaserShot : Task {
		
		public override string desc {get {return "Do "+damage+" damage to all units in target cell." +
				"\nIf there are no obstacles in target cell, do reduce damage 50% (rounded up) " +
					"and damage all units in the next occupied cell in the same direction.  " +
						"Repeat until damage is 1 or an obstacle is hit.";} }
		
		int damage = 16;
		
		public LaserShot (Unit parent) : base(parent) {
			name = "Laser Shot";
			weight = 3;
			aims += Aim.AttackLine(Filters.Units, 3);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.LaserLine(source, (Unit)targets[0], damage));
		}
	}

	public class Refract : Task {
		
		int damage = 12;
		
		public override string desc {get {return "50% chance of missing target." +
				"\nDo "+damage+" damage to all units in target cell." +
					"\nIf there are no obstacles in target cell, do reduce damage 50% (rounded up) " +
						"and damage all units in the next occupied cell in the same direction.  " +
						"Repeat until damage is 1 or an obstacle is hit.";
			} }
		
		public Refract (Unit parent) : base(parent) {
			name = "Refract";
			weight = 4;
			price = new Price(1,1);
			aims += Aim.AttackLine(Filters.Units, 3);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			int flip = DiceCoin.Throw(source, EDice.COIN);
			
			if (flip == 1) {
				EffectQueue.Add(new Effects.LaserLine(source, (Unit)targets[0], damage));
			}
			else {
				EffectQueue.Add(new Effects.Miss(source, parent));
				GameLog.Out(parent+" attempts to Refract and misses.");
			}
		}
	}

	public class GammaBurst : Task {
		
		public override string desc {get {return "Do "+damage+" damage to all units in target direction";} }
		
		int damage = 16;
		
		public GammaBurst (Unit parent) : base(parent) {
			name = "Gamma Burst";
			weight = 4;
			price = new Price(2,1);
			aims += Aim.AttackLine(Filters.Units, 20);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.LaserLine(source, (Unit)targets[0], damage, 1));
		}
	}
}

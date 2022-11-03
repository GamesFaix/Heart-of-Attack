using UnityEngine; 

namespace HOA.Actions { 

	public class LaserShot : Task {
		
		public override string Desc {get {return "Do "+damage+" damage to all units in target cell." +
				"\nIf there are no obstacles in target cell, do reduce damage 50% (rounded up) " +
					"and damage all units in the next occupied cell in the same direction.  " +
						"Repeat until damage is 1 or an obstacle is hit.";} }
		
		int damage = 16;
		
		public LaserShot (Unit parent) {
			Name = "Laser Shot";
			Weight = 3;
			Parent = parent;
			Price = Price.Cheap;
			NewAim(Aim.AttackLine(Special.Unit, 3));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.LaserLine(new Source(Parent), (Unit)targets[0], damage));
		}
	}

	public class Refract : Task {
		
		int damage = 12;
		
		public override string Desc {get {return "50% chance of missing target." +
				"\nDo "+damage+" damage to all units in target cell." +
					"\nIf there are no obstacles in target cell, do reduce damage 50% (rounded up) " +
						"and damage all units in the next occupied cell in the same direction.  " +
						"Repeat until damage is 1 or an obstacle is hit.";
			} }
		
		public Refract (Unit u) {
			Name = "Refract";
			Weight = 4;
			Parent = u;
			Price = new Price(1,1);
			NewAim(Aim.AttackLine(Special.Unit, 3));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			int flip = DiceCoin.Throw(new Source(Parent), EDice.COIN);
			
			if (flip == 1) {
				EffectQueue.Add(new Effects.LaserLine(new Source(Parent), (Unit)targets[0], damage));
			}
			else {
				EffectQueue.Add(new Effects.Miss(new Source(Parent), Parent));
				GameLog.Out(Parent+" attempts to Refract and misses.");
			}
		}
	}

	public class GammaBurst : Task {
		
		public override string Desc {get {return "Do "+damage+" damage to all units in target direction";} }
		
		int damage = 16;
		
		public GammaBurst (Unit parent) {
			Name = "Gamma Burst";
			Weight = 4;
			Parent = parent;
			Price = new Price(2,1);
			NewAim(Aim.AttackLine(Special.Unit, 20));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.LaserLine(new Source(Parent), (Unit)targets[0], damage, 1));
		}
	}
}

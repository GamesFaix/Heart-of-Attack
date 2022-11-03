using UnityEngine; 

namespace HOA { 

	public class AMawtLaser : Task {
		
		public override string Desc {get {return "Do "+damage+" damage to all units in target cell." +
				"\nIf there are no obstacles in target cell, do reduce damage 50% (rounded up) " +
					"and damage all units in the next occupied cell in the same direction.  " +
						"Repeat until damage is 1 or an obstacle is hit.";} }
		
		int damage = 16;
		
		public AMawtLaser (Unit parent) {
			Name = "Laser Shot";
			Weight = 3;
			Parent = parent;
			Price = Price.Cheap;
			NewAim(HOA.Aim.Shoot(3));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new ELaser(new Source(Parent), (Unit)targets[0], damage));
		}
	}

	public class APrisRefract : Task {
		
		int damage = 12;
		
		public override string Desc {get {return "50% chance of missing target." +
				"\nDo "+damage+" damage to all units in target cell." +
					"\nIf there are no obstacles in target cell, do reduce damage 50% (rounded up) " +
						"and damage all units in the next occupied cell in the same direction.  " +
						"Repeat until damage is 1 or an obstacle is hit.";
			} }
		
		public APrisRefract (Unit u) {
			Name = "Refract";
			Weight = 4;
			Parent = u;
			Price = new Price(1,1);
			NewAim(HOA.Aim.Shoot(3));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			int flip = DiceCoin.Throw(new Source(Parent), EDice.COIN);
			
			if (flip == 1) {
				Parent.Display.Effect(EEffect.HEADS);
				Unit u = (Unit)targets[0];
				
				int dmg = damage;
				Cell cell = u.Body.Cell;
				Int2 direction = Direction.FromCells(cell, Parent.Body.Cell);
				bool stop = false;
				
				TokenGroup affected;
				Mixer.Play(SoundLoader.Effect(EEffect.LASER));
				while (dmg > 0 && !stop) {
					affected = cell.Occupants;
					if (affected.OnlyType(EType.OB).Count > 0) {stop = true;/* Debug.Log("obstacle hit");*/}
					foreach(Token t in affected.OnlyType(EType.UNIT)) {
						((Unit)t).Damage(new Source(Parent), dmg);
						t.Display.Effect(EEffect.LASER);
					}
					if (targets.Count > 0) {dmg = (int)Mathf.Floor(dmg*0.5f);}
					Int2 nextIndex = cell.Index - direction;
					if (!Game.Board.HasCell(nextIndex, out cell)) {stop = true;}
				}
			}
			else {
				EffectQueue.Add(new ETails(new Source(Parent), Parent));
				GameLog.Out(Parent+" attempts to Refract and misses.");
			}
		}
	}
	public class AKabuLaser : Task {
		
		public override string Desc {get {return "Do "+damage+" damage to all units in target direction";} }
		
		int damage = 16;
		
		public AKabuLaser (Unit parent) {
			Name = "Gamma Burst";
			Weight = 4;
			Parent = parent;
			Price = new Price(2,1);
			NewAim(HOA.Aim.Shoot(20));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			int dmg = damage;
			Unit u = (Unit)targets[0];
			Cell cell = u.Body.Cell;
			Int2 direction = Direction.FromCells(cell, Parent.Body.Cell);
			bool stop = false;
			
			TokenGroup affected;
			
			while (dmg > 0 && !stop) {
				affected = cell.Occupants;
				
				TokenGroup blockers = new TokenGroup (affected);
				blockers = blockers.OnlyType(EType.OB);
				blockers = blockers.RemovePlane(EPlane.SUNK);
				
				if (blockers.Count > 0) {
					stop = true; 
					Debug.Log("obstacle hit");
				}
				
				foreach (Token t in affected.OnlyType(EType.UNIT)) {
					((Unit)t).Damage(new Source(Parent), dmg);
					t.Display.Effect(EEffect.LASER);
				}
				//if (targets.Count > 0) {dmg = (int)Mathf.Floor(dmg*0.5f);}
				Int2 nextIndex = cell.Index - direction;
				if (!Game.Board.HasCell(nextIndex, out cell)) {stop = true;}
			}
		}
	}






}

using UnityEngine;
using System.Collections.Generic;

namespace HOA{
	public class PrismGuard : Unit {
		public PrismGuard(Source s, bool template=false){
			id = new ID(this, EToken.PRIS, s, false, template);
			plane = Plane.Gnd;
			ScaleSmall();
			NewHealth(15);
			NewWatch(3);
			BuildArsenal();
		}

		protected override void BuildArsenal () {
			base.BuildArsenal();
			arsenal.Add(new Task[]{
				new AMovePath(this, 3),
				new AStrike(this, 8),
				new APrisRefract(this)
			});
			arsenal.Sort();
		}

		public override string Notes () {return "Actions targetting "+id.Name+" have a 50% of missing.";}
		
		public override void Select (Source s) {
			int flip = DiceCoin.Throw(s, EDice.COIN);
			if (flip == 1) {
				Display.Effect(EEffect.HEADS);
				GUISelectors.Instance = this;
			}
			else {
				GameLog.Out(s.ToString()+" tried to target "+ToString()+" and missed.");
				EffectQueue.Add(new ETails(new Source(this), this));
			}
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
			AddAim(HOA.Aim.Shoot(3));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			int flip = DiceCoin.Throw(new Source(Parent), EDice.COIN);

			if (flip == 1) {
				Parent.Display.Effect(EEffect.HEADS);
				Unit u = (Unit)targets[0];

				int dmg = damage;
				Cell cell = u.Body.Cell;
				int[] direction = Direction.FromCells(cell, Parent.Body.Cell);
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
					
					int nextX = cell.X-direction[0];
					int nextY = cell.Y-direction[1];
					
					if (!Board.HasCell(nextX, nextY, out cell)) {stop = true;}
				}
			}
			else {
				EffectQueue.Add(new ETails(new Source(Parent), Parent));
				GameLog.Out(Parent+" attempts to Refract and misses.");
			}
		}
	}
}
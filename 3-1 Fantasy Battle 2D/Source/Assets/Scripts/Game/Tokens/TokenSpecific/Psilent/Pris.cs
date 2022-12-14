using UnityEngine;
using System.Collections.Generic;

namespace HOA{
	public class PrismGuard : Unit {
		public PrismGuard(Source s, bool template=false){
			NewLabel(EToken.PRIS, s, false, template);
			BuildGround();
			
			NewHealth(15);
			NewWatch(3);
			
			arsenal.Add(new AMovePath(this, 3));
			arsenal.Add(new AAttack("Melee", Price.Cheap, this, Aim.Melee(), 8));
			arsenal.Add(new APrisRefract(new Price(1,1), this, Aim.Shoot(3), 12));
			arsenal.Sort();
		}		
		public override string Notes () {return "Actions targetting "+Name+" have a 50% of missing.";}
		
		public override void Select (Source s) {
			int flip = DiceCoin.Throw(s, EDice.COIN);
			if (flip == 1) {
				SpriteEffect(EEffect.HEADS);
				GUISelectors.Instance = this;
			}
			else {
				GameLog.Out(s.ToString()+" tried to target "+FullName+" and missed.");
				EffectQueue.Add(new ETails(new Source(this), this));
			}
		}
		
		
	}

	public class APrisRefract : Action {
		
		int damage;
		
		public APrisRefract (Price p, Unit u, Aim a, int d) {
			weight = 4;
			actor = u;
			price = p;
			AddAim(a);
			damage = d;
			
			name = "Refract";
			desc = "50% chance of missing target.\nDo "+d+" damage to all units in target cell.\nIf there are no obstacles in target cell, do reduce damage 50% (rounded up) and damage all units in the next occupied cell in the same direction.  Repeat until damage is 1 or an obstacle is hit.";
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();
			int flip = DiceCoin.Throw(new Source(actor), EDice.COIN);

			if (flip == 1) {
				actor.SpriteEffect(EEffect.HEADS);
				Unit u = (Unit)targets[0];

				int dmg = damage;
				Cell cell = u.Cell;
				int[] direction = Direction.FromCells(cell, actor.Cell);
				bool stop = false;
				
				TokenGroup affected;
				Mixer.Play(SoundLoader.Effect(EEffect.LASER));
				while (dmg > 0 && !stop) {
					affected = cell.Occupants;
					if (affected.OnlyClass(EClass.OB).Count > 0) {stop = true;/* Debug.Log("obstacle hit");*/}
					foreach(Token t in affected.OnlyClass(EClass.UNIT)) {
						((Unit)t).Damage(new Source(actor), dmg);
						t.SpriteEffect(EEffect.LASER);
					}
					if (targets.Count > 0) {dmg = (int)Mathf.Floor(dmg*0.5f);}
					
					int nextX = cell.X-direction[0];
					int nextY = cell.Y-direction[1];
					
					if (!Board.HasCell(nextX, nextY, out cell)) {stop = true;}
				}
			}
			else {
				EffectQueue.Add(new ETails(new Source(actor), actor));
				GameLog.Out(actor+" attempts to Refract and misses.");
			}
			Targeter.Reset();
		}
	}
}
using UnityEngine;
using System.Collections.Generic;

namespace HOA {
	public class Kabutomachine : Unit {
	
		public Kabutomachine(Source s, bool template=false){
			id = new ID(this, EToken.KABU, s, true, template);
			plane = Plane.Air;
			type.Add(EType.KING);
			onDeath = EToken.HSIL;

			ScaleJumbo();
			NewHealth(75);
			NewWatch(4);
			NewWallet(3);
			arsenal.Add(new AMoveLine(this, 5));
			//arsenal.Add(new AMove(this, Aim.MoveLine(5)));
			arsenal.Add(new AAttack("Melee", Price.Cheap, this, Aim.Melee(), 16));
			arsenal.Add(new ACreate(Price.Cheap, this, EToken.KATA));
			arsenal.Add(new ACreate(new Price(2,1), this, EToken.CARA));
			arsenal.Add(new ACreate(new Price(2,2), this, EToken.MAWT));
			arsenal.Add(new AKabuTeleport(this));
			arsenal.Add(new AKabuLaser(this));
			arsenal.Sort();
		}
		public override string Notes () {return "";}
	}

	public class AKabuLaser : Action {
		
		int damage;
		
		public AKabuLaser (Unit u) {
			weight = 4;
			actor = u;
			price = new Price(2,1);
			AddAim(HOA.Aim.Shoot(20));
			damage = 16;
			
			name = "Gamma Burst";
			desc = "Do "+damage+" damage to all units in target direction";
		}
		
		public override void Execute (List<ITarget> targets) {
			Charge();

			int dmg = damage;
			Unit u = (Unit)targets[0];
			Cell cell = u.Body.Cell;
			int[] direction = Direction.FromCells(cell, actor.Body.Cell);
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
					((Unit)t).Damage(new Source(actor), dmg);
					t.Display.Effect(EEffect.LASER);
				}
				//if (targets.Count > 0) {dmg = (int)Mathf.Floor(dmg*0.5f);}
				
				int nextX = cell.X-direction[0];
				int nextY = cell.Y-direction[1];
				
				if (!Board.HasCell(nextX, nextY, out cell)) {stop = true;}
			}
			Targeter.Reset();
		}
	}

	public class AKabuTeleport : Action, ITeleport {
		
		public AKabuTeleport (Unit u) {
			weight = 4;
			actor = u;
			price = new Price(1,1);
			AddAim(new Aim(ETraj.ARC, EType.UNIT, 5));
			aim[0].TeamOnly = true;
			AddAim(new Aim(ETraj.ARC, EType.CELL, EPurp.MOVE, 5));
			
			name = "Warp Drive";
			desc = "Move target teammate (including self) to target cell.\n"+aim[1].ToString();
		}
		
		public override void Execute (List<ITarget> targets) {
			Charge();
			EffectQueue.Add(new ETeleport(new Source(actor), (Unit)targets[0], (Cell)targets[1]));
			Targeter.Reset();
		}
	}
}
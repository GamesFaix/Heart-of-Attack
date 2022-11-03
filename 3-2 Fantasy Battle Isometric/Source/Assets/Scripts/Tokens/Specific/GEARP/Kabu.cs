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
			BuildArsenal();
		}

		protected override void BuildArsenal() {
			base.BuildArsenal();
			arsenal.Add(new Task[]{
				new AMoveLine(this, 5),
				new AStrike(this, 16),
				new AKabuTeleport(this),
				new AKabuLaser(this),
				new ACreate(this, Price.Cheap, EToken.KATA),
				new ACreate(this, new Price(2,1), EToken.CARA),
				new ACreate(this, new Price(2,2), EToken.MAWT)
			});
			arsenal.Sort();
		}


		public override string Notes () {return "";}
	}

	public class AKabuLaser : Task {

		public override string Desc {get {return "Do "+damage+" damage to all units in target direction";} }

		int damage = 16;
		
		public AKabuLaser (Unit parent) {
			Name = "Gamma Burst";
			Weight = 4;
			Parent = parent;
			Price = new Price(2,1);
			AddAim(HOA.Aim.Shoot(20));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			int dmg = damage;
			Unit u = (Unit)targets[0];
			Cell cell = u.Body.Cell;
			int[] direction = Direction.FromCells(cell, Parent.Body.Cell);
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
				
				int nextX = cell.X-direction[0];
				int nextY = cell.Y-direction[1];
				
				if (!Board.HasCell(nextX, nextY, out cell)) {stop = true;}
			}
		}
	}

	public class AKabuTeleport : Task, ITeleport {

		public override string Desc {get {return "Move target teammate (including self) to target cell." +
				"\n"+aim[1].ToString();} }

		public AKabuTeleport (Unit parent) {
			AddAim(new Aim(ETraj.ARC, EType.UNIT, 5));
			aim[0].TeamOnly = true;
			AddAim(new Aim(ETraj.ARC, EType.CELL, EPurp.MOVE, 5));

			Name = "Warp Drive";
			Weight = 4;
			Parent = parent;
			Price = new Price(1,1);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new ETeleport(new Source(Parent), (Unit)targets[0], (Cell)targets[1]));
		}
	}
}
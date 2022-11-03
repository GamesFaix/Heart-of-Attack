using UnityEngine; 

namespace HOA.Effects { 

	public class LaserLine : Effect {
		public override string ToString () {return "Effect - LaserLine";}
		Unit target; int damage; float decay;
		
		public LaserLine (Source s, Unit u, int n, float f=0.5f) {
			source = s; target = u; damage = n; decay = f;
		}
		public override void Process() {
			Token Parent = source.Token;
			Cell cell = target.Body.Cell;
			Int2 direction = Direction.FromCells(cell, Parent.Body.Cell);

			CellGroup cells = new CellGroup(cell);
			bool stop = false;

			while (!stop) {
				Int2 nextIndex = cell.Index - direction;
				if (Game.Board.HasCell(nextIndex, out cell)) {
					cells.Add(cell);
				}
				else {stop = true;}
			}
			EffectQueue.Add(new Laser (source, cells, damage, decay));
		}
	}

	public class Laser : Effect {
		public override string ToString () {return "Effect - Laser";}
		CellGroup cells;
		float decay;
		int damage;

		public Laser (Source s, CellGroup c, int n, float f = 0.5f) {
			source = s; decay = f; damage = n; cells = c;
		}

		public override void Process() {
			int currentDmg = damage;
			foreach (Cell c in cells) {
				Group<Unit> units = c.Occupants.Units;
				foreach (Unit u in units) {
					if (u.Damage(source, currentDmg)) {
						u.Display.Effect(EEffect.LASER);
						Mixer.Play(SoundLoader.Effect(EEffect.LASER));
					}
					else {
						u.Display.Effect(EEffect.MISS);
						Mixer.Play(SoundLoader.Effect(EEffect.MISS));
					}
				}
				if (Block(c.Occupants)) {return;}
				if (units.Count > 0) {currentDmg = (int)Mathf.Floor(currentDmg*decay);}
			}
		}

		bool Block (TokenGroup tokens) {
			tokens = tokens.OnlyType(ESpecial.OB);
			tokens = tokens.RemovePlane(EPlane.SUNK);
			if (tokens.Count > 0) {return true;}
			return false;
		}
	}

	public class Laser2 : Effect {
		public override string ToString () {return "Effect - Laser2";}
		Unit target; int dmg;
		
		public Laser2 (Source s, Unit u, int n) {
			source = s; target = u; dmg = n;
		}
		public override void Process() {
			if (target.Damage(source, dmg)) {
				target.Display.Effect(EEffect.LASER);
				Mixer.Play(SoundLoader.Effect(EEffect.LASER));
			}
			else {
				target.Display.Effect(EEffect.MISS);
				Mixer.Play(SoundLoader.Effect(EEffect.MISS));
			}
		}
	}
}

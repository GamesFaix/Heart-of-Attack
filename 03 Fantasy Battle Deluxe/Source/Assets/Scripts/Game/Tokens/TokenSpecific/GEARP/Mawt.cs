using UnityEngine;
using System.Collections.Generic;

namespace HOA {
	public class Mawth : Unit {
		public Mawth(Source s, bool template=false){
			NewLabel(EToken.MAWT, s, false, template);
			BuildAir();
			
			NewHealth(55);
			NewWatch(3);

			arsenal.Add(new AMoveLine(this, 4));
			arsenal.Add(new ALaser("Laser shot", Price.Cheap, this, Aim.Shoot(3), 16));
			arsenal.Add(new AMawtBomb(this));
			arsenal.Sort();
		}
		public override string Notes () {return "";}
	}

	public class AMawtBomb : Action, IMultiMove {
		
		int damage = 10;
		int range = 4;
		public int Optional () {return 1;}
		
		public AMawtBomb (Unit u) {
			weight = 4;
			actor = u;
			price = new Price(2,0);

			name = "Bombard";
			desc = "Once per Focus, move upto "+range+" cells in a line and deal "+damage+" explosive damage at that cell (up to 3 times).\n("+actor+" receives no damage.)";
		}
		
		public override void Adjust () {
			int shots = Mathf.Min(actor.FP, 3);
			for (int i=0; i<shots; i++) {
				AddAim(new Aim(EAim.LINE, EClass.CELL, EPurpose.MOVE, range));
			}
		}
		
		public override void UnAdjust () {
			aim = new List<HOA.Aim>();
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();
			actor.SetStat(new Source(actor), EStat.FP, 0);

			Cell start = actor.Cell;

			for (int i=0; i<targets.Count; i++) {
				Cell endCell = (Cell)targets[i];
				Debug.Log("creating line from "+start+" to "+endCell);

				//Debug.Log("end: "+endCell);
				
				CellGroup line = new CellGroup();
				
				int[] dir = Direction.FromCells(start, endCell);
				Debug.Log("direction: "+dir[0]+","+dir[1]);
				
				int length = Length(start, endCell);
				Debug.Log("length: "+length);
				
				Cell c = start;
				
				for (int j=0; j<length; j++) {
					int x = c.X + dir[0];
					int y = c.Y + dir[1];
					c = Board.Cell(x,y);
					line.Add(c);
					//Debug.Log("adding "+c+" to line");
				}
				
				foreach (Cell point in line) {
					EffectQueue.Add(new EMove(new Source(actor), actor, point));
					Debug.Log("move actor to "+point);
				}

				EffectQueue.Add(new EMawtExplosion(new Source(actor), endCell, 10));
				start = endCell;
			}
			Targeter.Reset();
		}

		int Length (Cell c1, Cell c2) {
			int x = Mathf.Abs(c1.X-c2.X);
			int y = Mathf.Abs(c1.Y-c2.Y);
			return Mathf.Max(x,y);
		}

		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, Name, p.s);
			
			DrawPrice(new Panel(p.LineBox, p.LineH, p.s));

			Aim a = new Aim(EAim.LINE, EClass.CELL, EPurpose.MOVE);
			a.Draw(new Panel(p.LineBox, p.LineH, p.s));
			
			float descH = (p.H-(p.LineH*2))/p.H;
			//Rect descBox = new Rect(p.x2, p.y2, p.W, descH);
			
			GUI.Label(p.TallBox(descH), Desc());	
		}

	}

	public class EMawtExplosion : Effect {
		public override string ToString () {return "Effect - Mawth Explosion";}
		Cell target; int dmg;
		
		public EMawtExplosion (Source s, Cell c, int n) {
			source = s; target = c; dmg = n;
		}
		public override void Process() {
			CellGroup affected = new CellGroup();
			CellGroup thisRad = new CellGroup(target);
			CellGroup nextRad = new CellGroup();
			
			int currentDmg = dmg;

			EffectSeq sequence = new EffectSeq();

			while (currentDmg > 0) {
				EffectGroup nextEffects = new EffectGroup();
				for (int j=0; j<thisRad.Count; j++) {
					Cell next = thisRad[j];
					
					if (!affected.Contains(next)) {
						if (next.Occupants.Count > 0) {
							nextEffects.Add(new EMawtExplosion2 (source, next, currentDmg));
						}
						foreach (Cell c in next.Neighbors()) {nextRad.Add(c);}
						affected.Add(next);
					}
				}
				sequence.Add(nextEffects);
				//EffectQueue.Interrupt(nextEffects);
				thisRad = nextRad;
				nextRad = new CellGroup();
				currentDmg = (int)Mathf.Floor(currentDmg * 0.5f);
			}
			EffectQueue.Interrupt(sequence);
		}
	}
	
	public class EMawtExplosion2 : Effect {
		public override string ToString () {return "Effect - Mawth Explosion2";}
		Cell cell; int dmg;
		
		public EMawtExplosion2 (Source s, Cell c, int n) {
			source = s; cell = c; dmg = n;
		}
		public override void Process() {
			EffectGroup nextEffects = new EffectGroup();
			TokenGroup targets = cell.Occupants.OnlyClass(new List<EClass> {EClass.UNIT, EClass.DEST});
			
			foreach (Token t in targets) {
				t.SpriteEffect(EEffect.EXP);
				Mixer.Play(SoundLoader.Effect(EEffect.EXP));
				if (t.IsClass(EClass.DEST)) {
					nextEffects.Add(new EDestruct(source, t));
				}
				
				else if (t is Unit && t!=source.Token) {
					Unit u = (Unit)t;
					u.Damage(source, dmg);
				}
			}		
			EffectQueue.Interrupt(nextEffects);
		}
	}
}
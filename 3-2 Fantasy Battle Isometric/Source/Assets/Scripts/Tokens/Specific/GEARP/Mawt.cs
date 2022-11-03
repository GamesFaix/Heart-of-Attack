using UnityEngine;
using System.Collections.Generic;

namespace HOA {
	public class Mawth : Unit {
		public Mawth(Source s, bool template=false){
			id = new ID(this, EToken.MAWT, s, false, template);
			plane = Plane.Air;
			ScaleLarge();
			NewHealth(55);
			NewWatch(3);
			BuildArsenal();
		}

		protected override void BuildArsenal () {
			base.BuildArsenal();
			arsenal.Add(new Task[]{
				new AMoveLine(this, 4),
				new AMawtLaser(this),
				new AMawtBomb(this)
			});
			arsenal.Sort();
		}

		public override string Notes () {return "";}
	}

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
			AddAim(HOA.Aim.Shoot(3));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new ELaser(new Source(Parent), (Unit)targets[0], damage));
		}
	}

	public class AMawtBomb : Task, IMultiMove {

		public override string Desc {get {return "Once per Focus, move upto "+range+" cells in a line and " +
				"deal "+damage+" explosive damage at that cell (up to 3 times)." +
					"\n("+Parent+" receives no damage.)";} }

		int damage = 10;
		int range = 4;
		public int Optional () {return 1;}
		
		public AMawtBomb (Unit parent) {
			Name = "Bombard";
			Weight = 4;
			Parent = parent;
			Price = new Price(2,0);
		}
		
		public override void Adjust () {
			int shots = Mathf.Min(Parent.FP, 3);
			for (int i=0; i<shots; i++) {
				AddAim(new Aim(ETraj.LINE, EType.CELL, EPurp.MOVE, range));
			}
		}
		
		public override void UnAdjust () {
			aim = new List<HOA.Aim>();
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Parent.SetStat(new Source(Parent), EStat.FP, 0);

			Cell start = Parent.Body.Cell;

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
					EffectQueue.Add(new EMove(new Source(Parent), Parent, point));
					Debug.Log("move Parent to "+point);
				}

				EffectQueue.Add(new EMawtExplosion(new Source(Parent), endCell, 10));
				start = endCell;
			}
		}

		int Length (Cell c1, Cell c2) {
			int x = Mathf.Abs(c1.X-c2.X);
			int y = Mathf.Abs(c1.Y-c2.Y);
			return Mathf.Max(x,y);
		}

		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, Name, p.s);
			
			DrawPrice(new Panel(p.LineBox, p.LineH, p.s));

			Aim a = new Aim(ETraj.LINE, EType.CELL, EPurp.MOVE);
			a.Draw(new Panel(p.LineBox, p.LineH, p.s));
			
			float descH = (p.H-(p.LineH*2))/p.H;
			//Rect descBox = new Rect(p.x2, p.y2, p.W, descH);
			
			GUI.Label(p.TallBox(descH), Desc);	
		}

	}

	public class EMawtExplosion : EffectSeq {
		public override string ToString () {return "Effect - Mawth Explosion";}
		Cell target; int dmg;
		
		public EMawtExplosion (Source s, Cell c, int n) {
			source = s; target = c; dmg = n;
		
			list = new List<EffectGroup>();

			CellGroup affected = new CellGroup();
			CellGroup thisRad = new CellGroup(target);
			CellGroup nextRad = new CellGroup();
			
			int currentDmg = dmg;

		//	EffectSeq sequence = new EffectSeq();

			while (currentDmg > 0) {
				EffectGroup group = new EffectGroup();
				for (int j=0; j<thisRad.Count; j++) {
					Cell next = thisRad[j];
					
					if (!affected.Contains(next)) {
						if (next.Occupants.Count > 0) {
							group.Add(new EMawtExplosion2 (new Source(source.Token, this), next, currentDmg));
						}
						else {
							group.Add(new EExplosionDummy (new Source (source.Token, this), next));
						}
						foreach (Cell cell in next.Neighbors()) {nextRad.Add(cell);}
						affected.Add(next);
					}
				}
				//sequence.Add(nextEffects);
				//EffectQueue.Interrupt(nextEffects);
				thisRad = nextRad;
				nextRad = new CellGroup();
				currentDmg = (int)Mathf.Floor(currentDmg * 0.5f);
				list.Add(group);
			}
		//	EffectQueue.Add(sequence);
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
			TokenGroup targets = cell.Occupants.OnlyType(Special.UnitDest);
			
			foreach (Token t in targets) {
				t.Display.Effect(EEffect.EXP);
				Mixer.Play(SoundLoader.Effect(EEffect.EXP));
				if (t.Special.Is(EType.DEST)) {
					source.Sequence.AddToNext(new EDestruct(source, t));
				}
				
				else if (t is Unit && t!=source.Token) {
					Unit u = (Unit)t;
					u.Damage(source, dmg);
				}
			}		
			EffectQueue.Add(nextEffects);
		}
	}
}
using UnityEngine; 
using System.Collections.Generic;

namespace HOA.Actions {

	public class ThrowTerrain : Task {
		
		public override string desc {get {return 
				"Destroy target non-Remains destructible." +
				"\nDo "+damage+" damage to target unit.";} } 
		
		int damage = 16;

		public ThrowTerrain (Unit parent) : base(parent) {
			aims += Aim.AttackNeighbor(Filters.Destructible);
			aims += Aim.AttackArc(Filters.Units, 0, 3);
			name = "Throw Terrain";
			weight = 4;
			price = new Price(1,1);
		} 
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Kill (source, (Token)targets[0]));
			EffectQueue.Add(new Effects.Damage(source, (Unit)targets[1], damage));
		}

		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();

			aims[0].Draw(new Panel(p.LineBox, p.LineH, p.s));
			aims[1].Draw(new Panel(p.LineBox, p.LineH, p.s));
			float descH = (p.H-(p.LineH*2))/p.H;
			GUI.Label(p.TallWideBox(descH), desc);	
		}
	}

	public class Quickdraw : Task, IMultiTarget {
		
		public override string desc {get {return 
				"Once per Focus (Max: 5), select and deal "+damage+" damage to target unit." +
				"\n(You may choose the same target multiple times.)" +
				"\nLose all Focus.";} }
		
		int damage = 6;

		public Quickdraw (Unit parent) : base(parent) {
			name = "Quickdraw";
			weight = 4;
			price = new Price(0,1);
			aims += Aim.AttackLine(Filters.Units,3);
		}
		
		public override void Adjust () {
			int shots = Mathf.Min(parent.FP, 5);
			for (int i=2; i<=shots; i++) {aims += Aim.AttackLine(Filters.Units,3);}
		}
		
		public override void UnAdjust () {aims = new AimSeq(Aim.AttackLine(Filters.Units, 3));}

		protected override void ExecuteMain (TargetGroup targets) {
			for (int i=0; i<targets.Count; i++) {
				EffectQueue.Add(new Effects.Damage (source, (Unit)targets[i], damage));
			}
			parent.SetStat(source, EStat.FP, 0);
		}

		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, name, p.s);
			int FP = parent.FP;
			Price price = new Price(0, (byte)FP);
			price.Draw(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();

			int shots = Mathf.Min(parent.FP, 5);
			for (byte i=0; i<shots; i++) {
				aims[0].Draw(new Panel(p.LineBox, p.LineH, p.s));
			}
			float descH = (p.H-(p.LineH*2))/p.H;
			GUI.Label(p.TallWideBox(descH), desc);	
		}
	} 

	public class Bombard : Task, IMultiTarget, IRecursiveMove {
		
		public override string desc {get {return 
				"Once per Focus (Max: 3), move upto "+range+" cells in a line and " +
				"deal "+damage+" explosive damage at that cell." +
				"\n("+parent+" receives no damage.)" +
				"\nLose all Focus.";} }
		
		int damage = 10;
		int range = 4;

		public Bombard (Unit parent) : base(parent) {
			name = "Bombard";
			weight = 4;
			price = new Price(2,0);
			aims += Aim.MoveLine(range);
		}
		
		public override void Adjust () {
			int shots = Mathf.Min(parent.FP, 3);
			for (int i=2; i<=shots; i++) {aims += Aim.MoveLine(range);}
		}
		
		public override void UnAdjust () {aims = new AimSeq(Aim.MoveLine(range));}	

		protected override void ExecuteMain (TargetGroup targets) {
			parent.SetStat(source, EStat.FP, 0);
			
			Cell start = parent.Body.Cell;
			
			for (int i=0; i<targets.Count; i++) {
				Cell endCell = (Cell)targets[i];
				Debug.Log("creating line from "+start+" to "+endCell);
				
				CellGroup line = new CellGroup();
				
				int2 dir = Direction.FromCells(start, endCell);
				int length = Length(start, endCell);
				Cell c = start;
				
				for (int j=0; j<length; j++) {
					index2 index = c.Index + dir;
					c = Game.Board.Cell(index);
					line.Add(c);
				}
				
				foreach (Cell point in line) {EffectQueue.Add(new Effects.Move(source, parent, point));}
				EffectQueue.Add(new Effects.Explosion(source, endCell, 10, true));
				start = endCell;
			}
		}
		
		int Length (Cell c1, Cell c2) {
			int x = Mathf.Abs(c1.X-c2.X);
			int y = Mathf.Abs(c1.Y-c2.Y);
			return Mathf.Max(x,y);
		}
		
		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, name, p.s);
			Price actual = new Price((byte)price.E, (byte)parent.FP);
			actual.Draw(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();
			int shots = Mathf.Min(3, parent.FP);
			for (byte i=0; i<shots; i++) {
				aims[0].Draw(p.LinePanel);
			}
			float descH = (p.H-(p.LineH*2))/p.H;
			GUI.Label(p.TallWideBox(descH), desc);	
		}
	}
}

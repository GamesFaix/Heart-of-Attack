using UnityEngine; 
using System.Collections.Generic;

namespace HOA { 
	public class AUltrThrow : Task {
		
		public override string Desc {get {return 
				"Destroy target non-Remains destructible." +
				"\nDo "+damage+" damage to target unit.";} } 
		
		int damage = 16;

		public AUltrThrow (Unit parent) {
			NewAim(new Aim(ETraj.NEIGHBOR, EType.DEST));
			Aim.Add(HOA.Aim.Arc(3));
			Name = "Throw Terrain";
			Weight = 4;
			Parent = parent;
			Price = new Price(1,1);
		} 
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new EKill (new Source(Parent), (Token)targets[0]));
			EffectQueue.Add(new EDamage(new Source(Parent), (Unit)targets[1], damage));
		}

		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, Name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();

			Aim[0].Draw(new Panel(p.LineBox, p.LineH, p.s));
			Aim[1].Draw(new Panel(p.LineBox, p.LineH, p.s));
			float descH = (p.H-(p.LineH*2))/p.H;
			GUI.Label(p.TallBox(descH), Desc);	
		}
	}

	public class ARevoQuick : Task, IMultiTarget {
		
		public override string Desc {get {return 
				"Once per Focus (Max: 5), select and deal "+damage+" damage to target unit." +
				"\n(You may choose the same target multiple times.)" +
				"\nLose all Focus.";} }
		
		int damage = 6;

		public ARevoQuick (Unit parent) {
			Name = "Quickdraw";
			Weight = 4;
			Parent = parent;
			Price = new Price(0,1);
			NewAim(HOA.Aim.Shoot(3));
		}
		
		public override void Adjust () {
			int shots = Mathf.Min(Parent.FP, 5);
			for (int i=2; i<=shots; i++) {Aim.Add(HOA.Aim.Shoot(3));}
		}
		
		public override void UnAdjust () {NewAim(HOA.Aim.Shoot(3));}

		protected override void ExecuteMain (TargetGroup targets) {
			for (int i=0; i<targets.Count; i++) {
				EffectQueue.Add(new EDamage (new Source(Parent), (Unit)targets[i], damage));
			}
			Parent.SetStat(new Source(Parent), EStat.FP, 0);
		}

		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, Name, p.s);
			int FP = Parent.FP;
			Price price = new Price(0, FP);
			price.Draw(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();

			int shots = Mathf.Min(Parent.FP, 5);
			Aim[0].Draw(new Panel(p.LineBox, p.LineH, p.s));
			for (int i=2; i<=shots; i++) {
				Aim[0].Draw(new Panel(p.LineBox, p.LineH, p.s));
			}
			float descH = (p.H-(p.LineH*2))/p.H;
			GUI.Label(p.TallBox(descH), Desc);	
		}
	} 

	public class AMawtBomb : Task, IMultiTarget {
		
		public override string Desc {get {return 
				"Once per Focus (Max: 3), move upto "+range+" cells in a line and " +
				"deal "+damage+" explosive damage at that cell." +
				"\n("+Parent+" receives no damage.)" +
				"\nLose all Focus.";} }
		
		int damage = 10;
		int range = 4;

		public AMawtBomb (Unit parent) {
			Name = "Bombard";
			Weight = 4;
			Parent = parent;
			Price = new Price(2,0);
			NewAim(HOA.Aim.MoveLine(range));
		}
		
		public override void Adjust () {
			int shots = Mathf.Min(Parent.FP, 3);
			for (int i=2; i<=shots; i++) {Aim.Add(HOA.Aim.MoveLine(range));}
		}
		
		public override void UnAdjust () {NewAim(HOA.Aim.MoveLine(range));}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Parent.SetStat(new Source(Parent), EStat.FP, 0);
			
			Cell start = Parent.Body.Cell;
			
			for (int i=0; i<targets.Count; i++) {
				Cell endCell = (Cell)targets[i];
				Debug.Log("creating line from "+start+" to "+endCell);
				
				CellGroup line = new CellGroup();
				
				Int2 dir = Direction.FromCells(start, endCell);
				int length = Length(start, endCell);
				Cell c = start;
				
				for (int j=0; j<length; j++) {
					Int2 index = c.Index + dir;
					c = Game.Board.Cell(index);
					line.Add(c);
				}
				
				foreach (Cell point in line) {EffectQueue.Add(new EMove(new Source(Parent), Parent, point));}
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
			Price price = new Price(Price.AP, Parent.FP);
			price.Draw(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();
			Aim[0].Draw(p.LinePanel);
			int shots = Mathf.Min(3, Parent.FP);
			for (int i=2; i<=shots; i++) {
				Aim[0].Draw(p.LinePanel);
			}
			float descH = (p.H-(p.LineH*2))/p.H;
			GUI.Label(p.TallBox(descH), Desc);	
		}
	}
}

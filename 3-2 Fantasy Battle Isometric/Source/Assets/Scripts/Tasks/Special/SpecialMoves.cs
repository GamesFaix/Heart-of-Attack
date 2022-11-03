using UnityEngine; 
using System.Collections.Generic;

namespace HOA { 

	public class ADeciMove : Task, IMultiMove {
		public override string Desc {get {return "Move "+Parent+" to target cell.";} }
		
		public ADeciMove (Unit parent) {
			Name = "Move";
			Weight = 1;
			Parent = parent;
			NewAim(HOA.Aim.MovePath(3));
			Price = Price.Cheap;
		}
		
		public override void Adjust () {
			Debug.Log("Adjusting");
			Aim[0].Range = Mathf.Max(0, Aim[0].Range-Parent.FP);}
		public override void UnAdjust () {
			Debug.Log("Unadjusting");

			NewAim(HOA.Aim.MovePath(3));
		}

		protected override void ExecuteMain (TargetGroup targets) {
			foreach (Target target in targets) {
				EffectQueue.Add(new EMove(new Source(Parent), Parent, (Cell)target));
			}
		}
		
		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, Name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();

			Aim actual = new Aim(ETraj.PATH, EType.CELL, EPurp.MOVE, Mathf.Max(0, Aim[0].Range-Parent.FP));
			actual.Draw(new Panel(p.LineBox, p.LineH, p.s));
			float descH = (p.H-(p.LineH*2))/p.H;
			GUI.Label(p.TallBox(descH), Desc);	
		}
	}

	public class ARookMove : Task, IMultiMove {
		public override string Desc {get {return "Move "+Parent+" to target cell.";} }
		
		public ARookMove (Unit u) {
			Name = "Rebuild";
			Weight = 1;
			Parent = u;
			Price = new Price(0,2);
			NewAim(HOA.Aim.MovePath(2));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			foreach (Target target in targets) {
				EffectQueue.Add(new EMove(new Source(Parent), Parent, (Cell)target));
			}
		}
	}

	public class AKataSprint : Task, IMultiMove {
		public override string Desc {get {return "Move "+Parent+" to target cell.  " +
			"\nRange +1 per focus (up to +6). " +
			"\n"+Parent+" loses all focus.";} }
		
		public AKataSprint (Unit parent) {
			Name = "Sprint";
			Weight = 4;
			Parent = parent;
			Price = Price.Free;
			NewAim(HOA.Aim.MovePath(0));
		}
		
		public override void Adjust () {Aim[0].Range = Mathf.Min(Parent.FP, 6);}
		public override void UnAdjust () {NewAim(HOA.Aim.MovePath(0));}

		
		protected override void ExecuteMain (TargetGroup targets) {
			foreach (Target target in targets) {
				EffectQueue.Add(new EMove(new Source(Parent), Parent, (Cell)target));
			}
			Parent.SetStat(new Source(Parent), EStat.FP, 0);
		}
		
		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, Name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();

			Aim actual = new Aim(ETraj.PATH, EType.CELL, EPurp.MOVE, Mathf.Min(6, Parent.FP));
			actual.Draw(new Panel(p.LineBox, p.LineH, p.s));
			float descH = (p.H-(p.LineH*2))/p.H;
			GUI.Label(p.TallBox(descH), Desc);	
		}
	}

	public class AMartMove : Task, IMultiMove {
		public override string Desc {get {return "Range +1 per focus.";} }
		
		public AMartMove (Unit u) {
			Name = "Move";
			Weight = 1;
			Parent = u;
			Price = Price.Cheap;
			NewAim(HOA.Aim.MovePath(1));
		}
		
		public override void Adjust () {Aim[0].Range = Aim[0].Range + Parent.FP;}
		public override void UnAdjust () {NewAim(HOA.Aim.MovePath(1));}

		protected override void ExecuteMain (TargetGroup targets) {
			foreach (Target target in targets) {
				EffectQueue.Add(new EMove(new Source(Parent), Parent, (Cell)target));
			}
		}
		
		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, Name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();

			Aim actual = new Aim(ETraj.PATH, EType.CELL, EPurp.MOVE, Aim[0].Range+Parent.FP);
			actual.Draw(new Panel(p.LineBox, p.LineH, p.s));
			float descH = (p.H-(p.LineH*2))/p.H;
			GUI.Label(p.TallBox(descH), Desc);	
		}
	}

	public class AGateBurrow : Task {
		
		public override string Desc {get {return "Move "+Parent+" to target cell.";} }
		
		public AGateBurrow (Unit u) {
			Name = "Burrow";
			Weight = 1;
			NewAim(new Aim(ETraj.ARC, EType.CELL, EPurp.MOVE, 3, 0));
			Parent = u;
			Price = Price.Cheap;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new EBurrow(new Source(Parent), Parent, (Cell)targets[0]));
		}
	}
}

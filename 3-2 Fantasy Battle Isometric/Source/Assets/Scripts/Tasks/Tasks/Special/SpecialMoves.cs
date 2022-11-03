using UnityEngine; 
using System.Collections.Generic;

namespace HOA.Actions { 

	public class Tread : Task {
		public override string desc {get {return "Move "+parent+" to target cell.";} }
		
		public Tread (Unit parent) : base(parent) {
			name = "Tread";
			weight = 1;
			aims += Aim.MovePath(3);
		}
		
		public override void Adjust () {aims[0].range = Mathf.Max(0, aims[0].range-parent.FP);}
		public override void UnAdjust () {aims[0] = Aim.MovePath(3);}

		protected override void ExecuteMain (TargetGroup targets) {
			foreach (Target target in targets) {
				EffectQueue.Add(new Effects.Move(source, parent, (Cell)target));
			}
		}
		
		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();

			Aim actual = Aim.MovePath(Mathf.Max(0, aims[0].range-parent.FP));
			actual.Draw(new Panel(p.LineBox, p.LineH, p.s));
			float descH = (p.H-(p.LineH*2))/p.H;
			GUI.Label(p.TallWideBox(descH), desc);	
		}
	}

	public class Rebuild : Task {
		public override string desc {get {return "Move "+parent+" to target cell.";} }
		
		public Rebuild (Unit parent) : base(parent) {
			name = "Rebuild";
			weight = 1;
			price = new Price(0,2);
			aims += Aim.MovePath(2);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			foreach (Target target in targets) {
				EffectQueue.Add(new Effects.Move(source, parent, (Cell)target));
			}
		}
	}

	public class Sprint : Task {
		public override string desc {get {return "Move "+parent+" to target cell.  " +
			"\nRange +1 per focus (up to +6). " +
			"\n"+parent+" loses all focus.";} }
		
		public Sprint (Unit parent) : base(parent) {
			name = "Sprint";
			weight = 4;
			price = Price.Free;
			aims += Aim.MovePath(0);
		}
		
		public override void Adjust () {aims[0].range = Mathf.Min(parent.FP, 6);}
		public override void UnAdjust () {aims[0] = Aim.MovePath(0);}

		
		protected override void ExecuteMain (TargetGroup targets) {
			foreach (Target target in targets) {
				EffectQueue.Add(new Effects.Move(source, parent, (Cell)target));
			}
			parent.SetStat(source, EStat.FP, 0);
		}
		
		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();

			Aim actual = Aim.MovePath(Mathf.Min(6, parent.FP));
			actual.Draw(new Panel(p.LineBox, p.LineH, p.s));
			float descH = (p.H-(p.LineH*2))/p.H;
			GUI.Label(p.TallWideBox(descH), desc);	
		}
	}

	public class Creep : Task {
		public override string desc {get {return "Range +1 per focus.";} }
		
		public Creep (Unit parent) : base(parent) {
			name = "Creep";
			weight = 1;
			aims += Aim.MovePath(1);
		}
		
		public override void Adjust () {aims[0].range = aims[0].range + parent.FP;}
		public override void UnAdjust () {aims[0] = Aim.MovePath(1);}

		protected override void ExecuteMain (TargetGroup targets) {
			foreach (Target target in targets) {
				EffectQueue.Add(new Effects.Move(source, parent, (Cell)target));
			}
		}
		
		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();

			Aim actual = Aim.MovePath(aims[0].range+parent.FP);
			actual.Draw(new Panel(p.LineBox, p.LineH, p.s));
			float descH = (p.H-(p.LineH*2))/p.H;
			GUI.Label(p.TallWideBox(descH), desc);	
		}
	}

	public class Burrow : Task {
		
		public override string desc {get {return "Move "+parent+" to target cell.";} }
		
		public Burrow (Unit parent) : base(parent) {
			name = "Burrow";
			weight = 1;
			aims += Aim.MoveArc(0, 3);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Burrow(source, parent, (Cell)targets[0]));
		}
	}
}

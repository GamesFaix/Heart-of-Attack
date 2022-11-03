using UnityEngine;

namespace HOA.Actions { 

	public class Defile : Task, ITeleport {
		public override string desc {get {return "Move target remains to target cell.";} } 
		
		public Defile (Unit parent) : base(parent) {
			aims += Aim.AttackArc(Filters.Corpses, 0, 5);
			aims += Aim.MoveArc(0, 5);
			name = "Defile";
			weight = 4;
			price = new Price(0,1);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Teleport(source, (Token)targets[0], (Cell)targets[1]));
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
	public class Warp : Task, ITeleport {
		
		public override string desc {get {return "Move target teammate (including self) to target cell.";} }
		
		public Warp (Unit parent) : base(parent) {
			aims += Aim.AttackArc(Filters.TeamUnits, 0, 5);
			aims += Aim.MoveArc(0, 5);
			name = "Warp";
			weight = 4;
			price = new Price(1,1);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Teleport(source, (Unit)targets[0], (Cell)targets[1]));
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

	public class Dislocate : Task, ITeleport {
		public override string desc {get {return "Move target enemy (exluding Attack Kings) to target cell.";} }
		
		public Dislocate (Unit parent) : base(parent) {
			aims += Aim.AttackArc(Filters.EnemyUnitsNoKings, 0, 5);
			aims += Aim.MoveArc(0, 5);
			name = "Dislocate";
			weight = 4;
			price = new Price(1,1);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Teleport(source, (Unit)targets[0], (Cell)targets[1]));
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



}

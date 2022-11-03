using UnityEngine; 

namespace HOA.Actions { 

	public class Defile : Task, ITeleport {
		public override string Desc {get {return "Move target remains to target cell.";} } 
		
		public Defile (Unit u) {
			int range = 5;
			NewAim(new Aim(ETraj.ARC, ESpecial.REM, range));
			Aim.Add(HOA.Aim.MoveArc(range));
			Name = "Defile";
			Weight = 4;
			Parent = u;
			Price = new Price(0,1);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Teleport(new Source(Parent), (Token)targets[0], (Cell)targets[1]));
		}

		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, Name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();

			Aim[0].Draw(new Panel(p.LineBox, p.LineH, p.s));
			Aim[1].Draw(new Panel(p.LineBox, p.LineH, p.s));
			float descH = (p.H-(p.LineH*2))/p.H;
			GUI.Label(p.TallWideBox(descH), Desc);	
		}
	}
	public class Warp : Task, ITeleport {
		
		public override string Desc {get {return "Move target teammate (including self) to target cell.";} }
		
		public Warp (Unit parent) {
			NewAim(new Aim(ETraj.ARC, ESpecial.UNIT, 5));
			Aim[0].TeamOnly = true;
			Aim.Add(HOA.Aim.MoveArc(5));
			Name = "Warp";
			Weight = 4;
			Parent = parent;
			Price = new Price(1,1);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Teleport(new Source(Parent), (Unit)targets[0], (Cell)targets[1]));
		}

		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, Name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();

			Aim[0].Draw(new Panel(p.LineBox, p.LineH, p.s));
			Aim[1].Draw(new Panel(p.LineBox, p.LineH, p.s));
			float descH = (p.H-(p.LineH*2))/p.H;
			GUI.Label(p.TallWideBox(descH), Desc);	
		}
	}

	public class Dislocate : Task, ITeleport {
		public override string Desc {get {return "Move target enemy (exluding Attack Kings) to target cell.";} }
		
		public Dislocate (Unit u) {
			NewAim(new Aim(ETraj.ARC, ESpecial.UNIT, 5));
			Aim[0].EnemyOnly = true;
			Aim[0].NoKings = true;
			Aim.Add(HOA.Aim.MoveArc(5));
			Name = "Dislocate";
			Weight = 4;
			Parent = u;
			Price = new Price(1,1);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Teleport(new Source(Parent), (Unit)targets[0], (Cell)targets[1]));
		}

		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, Name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();

			Aim[0].Draw(new Panel(p.LineBox, p.LineH, p.s));
			Aim[1].Draw(new Panel(p.LineBox, p.LineH, p.s));
			float descH = (p.H-(p.LineH*2))/p.H;
			GUI.Label(p.TallWideBox(descH), Desc);	
		}
	}



}

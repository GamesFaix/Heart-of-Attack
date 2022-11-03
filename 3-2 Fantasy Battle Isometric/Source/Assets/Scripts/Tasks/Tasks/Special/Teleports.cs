using UnityEngine;

namespace HOA.Actions { 

	public class Defile : Task, ITeleport {
		public override string Desc {get {return "Move target remains to target cell.";} } 
		
		public Defile (Unit u) {
			NewAim(Aim.AttackArc(Special.Rem, 0, 5));
			Aims.Add(Aim.MoveArc(0, 5));
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

			Aims[0].Draw(new Panel(p.LineBox, p.LineH, p.s));
			Aims[1].Draw(new Panel(p.LineBox, p.LineH, p.s));
			float descH = (p.H-(p.LineH*2))/p.H;
			GUI.Label(p.TallWideBox(descH), Desc);	
		}
	}
	public class Warp : Task, ITeleport {
		
		public override string Desc {get {return "Move target teammate (including self) to target cell.";} }
		
		public Warp (Unit parent) {
			NewAim(Aim.AttackArc(Special.Unit, 0, 5));
			Aims[0].TeamOnly = true;
			Aims[0].IncludeSelf = true;
			Aims.Add(Aim.MoveArc(0, 5));
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

			Aims[0].Draw(new Panel(p.LineBox, p.LineH, p.s));
			Aims[1].Draw(new Panel(p.LineBox, p.LineH, p.s));
			float descH = (p.H-(p.LineH*2))/p.H;
			GUI.Label(p.TallWideBox(descH), Desc);	
		}
	}

	public class Dislocate : Task, ITeleport {
		public override string Desc {get {return "Move target enemy (exluding Attack Kings) to target cell.";} }
		
		public Dislocate (Unit u) {
			NewAim(Aim.AttackArc(Special.Unit, 0, 5));
			Aims[0].EnemyOnly = true;
			Aims[0].NoKings = true;
			Aims.Add(Aim.MoveArc(0, 5));
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

			Aims[0].Draw(new Panel(p.LineBox, p.LineH, p.s));
			Aims[1].Draw(new Panel(p.LineBox, p.LineH, p.s));
			float descH = (p.H-(p.LineH*2))/p.H;
			GUI.Label(p.TallWideBox(descH), Desc);	
		}
	}



}

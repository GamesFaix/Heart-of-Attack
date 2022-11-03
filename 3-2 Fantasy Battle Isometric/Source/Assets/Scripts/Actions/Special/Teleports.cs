using UnityEngine; 

namespace HOA { 

	public class ANecrTeleport : Task, ITeleport {
		public override string Desc {get {return "Move target remains to target cell.";} } 
		
		public ANecrTeleport (Unit u) {
			int range = 5;
			NewAim(new Aim(ETraj.ARC, EType.REM, range));
			Aim.Add(HOA.Aim.MoveArc(range));
			Name = "Defile";
			Weight = 4;
			Parent = u;
			Price = new Price(0,1);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new ETeleport(new Source(Parent), (Token)targets[0], (Cell)targets[1]));
		}

		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, Name, p.s);
			DrawPrice(new Panel(p.LineBox, p.LineH, p.s));
			Aim[0].Draw(new Panel(p.LineBox, p.LineH, p.s));
			Aim[1].Draw(new Panel(p.LineBox, p.LineH, p.s));
			float descH = (p.H-(p.LineH*2))/p.H;
			GUI.Label(p.TallBox(descH), Desc);	
		}
	}
	public class AKabuTeleport : Task, ITeleport {
		
		public override string Desc {get {return "Move target teammate (including self) to target cell.";} }
		
		public AKabuTeleport (Unit parent) {
			NewAim(new Aim(ETraj.ARC, EType.UNIT, 5));
			Aim[0].TeamOnly = true;
			Aim.Add(HOA.Aim.MoveArc(5));
			Name = "Warp";
			Weight = 4;
			Parent = parent;
			Price = new Price(1,1);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new ETeleport(new Source(Parent), (Unit)targets[0], (Cell)targets[1]));
		}

		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, Name, p.s);
			DrawPrice(new Panel(p.LineBox, p.LineH, p.s));
			Aim[0].Draw(new Panel(p.LineBox, p.LineH, p.s));
			Aim[1].Draw(new Panel(p.LineBox, p.LineH, p.s));
			float descH = (p.H-(p.LineH*2))/p.H;
			GUI.Label(p.TallBox(descH), Desc);	
		}
	}

	public class ADreaTeleport : Task, ITeleport {
		public override string Desc {get {return "Move target enemy (exluding Attack Kings) to target cell.";} }
		
		public ADreaTeleport (Unit u) {
			NewAim(new Aim(ETraj.ARC, EType.UNIT, 5));
			Aim[0].EnemyOnly = true;
			Aim[0].NoKings = true;
			Aim.Add(HOA.Aim.MoveArc(5));
			Name = "Dislocate";
			Weight = 4;
			Parent = u;
			Price = new Price(1,1);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new ETeleport(new Source(Parent), (Unit)targets[0], (Cell)targets[1]));
		}

		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, Name, p.s);
			DrawPrice(new Panel(p.LineBox, p.LineH, p.s));
			Aim[0].Draw(new Panel(p.LineBox, p.LineH, p.s));
			Aim[1].Draw(new Panel(p.LineBox, p.LineH, p.s));
			float descH = (p.H-(p.LineH*2))/p.H;
			GUI.Label(p.TallBox(descH), Desc);	
		}
	}



}

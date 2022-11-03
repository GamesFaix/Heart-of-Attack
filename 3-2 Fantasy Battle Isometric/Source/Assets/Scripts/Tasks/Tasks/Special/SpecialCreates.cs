using UnityEngine; 

namespace HOA.Actions { 

	public class Exhume : Task {

		public override string Desc {get {return "Create Corpse in target cell.";} }

		public override Token Template {get {return TokenFactory.Template(EToken.CORP);} }

		public Exhume (Unit par) {
			Name = "Exhume";
			Weight = 5;
			Parent = par;
			Price = Price.Free;
			NewAim(new Aim(ETraj.FREE, ESpecial.CELL, EPurp.CREATE));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Create(new Source(Parent), EToken.CORP, (Cell)targets[0]));
		}

		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, Name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();

			DrawAim(0, p.LinePanel);
			Template.DisplayThumbNameTemplate(p.LinePanel);
			float descH = (p.H-(p.LineH*2))/p.H;
			GUI.Label(p.TallWideBox(descH), Desc);	
		}
	}

	public class CreateROOK : Task {
		
		public override string Desc {get {return "Create Rook in "+Parent+"'s cell.";} } 
		public override Token Template {get {return TokenFactory.Template(EToken.ROOK);} }

		public CreateROOK (Unit par) {
			Name = "Build Rook";
			Weight = 5;
			Parent = par;
			Price = new Price(1,1);
			NewAim(HOA.Aim.Self());
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			if (!Parent.Body.Cell.Occupied(EPlane.GND)) {
				Charge();
				TokenFactory.Create(new Source(Parent), EToken.ROOK, Parent.Body.Cell);
			}
		}

		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, Name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();

			DrawAim(0, p.LinePanel);
			Template.DisplayThumbNameTemplate(p.LinePanel);
			float descH = (p.H-(p.LineH*2))/p.H;
			GUI.Label(p.TallWideBox(descH), Desc);	
		}
	}

	public class WebShot : Task {
		int damage = 12;
		
		public override string Desc {get {return "Create Web in target cell." +
				"\nAll Units in target cell take "+damage+" damage.";} }
		public override Token Template {get {return TokenFactory.Template(EToken.WEBB);} }

		public WebShot (Unit par) {
			Name = "Web Shot";
			Weight = 4;
			Parent = par;
			Price = new Price(1,1);
			NewAim(HOA.Aim.CreateArc(3));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Cell c = (Cell)targets[0];
			
			EffectQueue.Add(new Effects.Create(new Source(Parent), EToken.WEBB, c));
			
			TokenGroup occupants = c.Occupants.OnlyType(ESpecial.UNIT);
			foreach (Unit u in occupants) {
				EffectQueue.Add(new Effects.Damage(new Source(Parent), u, damage));
			}
		}

		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, Name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();

			DrawAim(0, p.LinePanel);
			Template.DisplayThumbNameTemplate(p.LinePanel);
			float descH = (p.H-(p.LineH*2))/p.H;
			GUI.Label(p.TallWideBox(descH), Desc);	
		}
	}
	
	public class CreateLICH : Task, IMultiTarget{
		
		public override string Desc {get {return "Create Lichenthropes in up to two target cells.";} }
		public override Token Template {get {return TokenFactory.Template(EToken.LICH);} }

		public CreateLICH (Unit par) {
			Name = "Create Lichenthropes";
			Weight = 5;
			Parent = par;
			Price = Price.Cheap;
			NewAim(HOA.Aim.Create());
			Aim.Add(HOA.Aim.Create());
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Create(new Source(Parent), EToken.LICH, (Cell)targets[0]));
			if (targets[1] != null) {
				EffectQueue.Add(new Effects.Create(new Source(Parent), EToken.LICH, (Cell)targets[1]));
			}
			Targeter.Reset();
		}
		protected override void ExecuteFinish() {}

		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, Name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();

			DrawAim(0, p.LinePanel);
			DrawAim(1, p.LinePanel);
			Template.DisplayThumbNameTemplate(p.LinePanel);
			float descH = (p.H-(p.LineH*2))/p.H;
			GUI.Label(p.TallWideBox(descH), Desc);	
		}
	}
	/*
	public class APiecAper : Task {
		public override string Desc {get {return "Create Aperture in target cell.";} }
		public override Token Template {get {return TemplateFactory.Template(EToken.APER);} }
		
		public APieceAper (Unit par) {
			Name = "Open Aperture";
			Weight = 4;
			Parent = par;
			Price = new Price(1,1);
			NewAim(HOA.Aim.CreateArc(3));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Cell c = (Cell)targets[0];
			
			EffectQueue.Add(new ECreate(new Source(Parent), EToken.APER, c));
			
		}
		
		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, Name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();
			
			DrawAim(0, p.LinePanel);
			Template.DisplayThumbNameTemplate(p.LinePanel);
			float descH = (p.H-(p.LineH*2))/p.H;
			GUI.Label(p.TallWideBox(descH), Desc);	
		}
	}
	*/
}

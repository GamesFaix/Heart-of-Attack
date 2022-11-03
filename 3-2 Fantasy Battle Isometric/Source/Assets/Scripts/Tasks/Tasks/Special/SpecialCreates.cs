using UnityEngine;

namespace HOA.Actions { 

	public class Exhume : Task {

		public override string desc {get {return "Create Corpse in target cell.";} }

		public override Token template {get {return TokenFactory.Template(EToken.CORP);} }

		public Exhume (Unit parent) : base(parent) {
			name = "Exhume";
			weight = 5;
			price = Price.Free;
			aims += Aim.Free(Filters.Create);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Create(source, EToken.CORP, (Cell)targets[0]));
		}

		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();

			DrawAim(0, p.LinePanel);
			template.DisplayThumbNameTemplate(p.LinePanel);
			float descH = (p.H-(p.LineH*2))/p.H;
			GUI.Label(p.TallWideBox(descH), desc);	
		}
	}

	public class CreateROOK : Task {
		
		public override string desc {get {return "Create Rook in "+parent+"'s cell.";} } 
		public override Token template {get {return TokenFactory.Template(EToken.ROOK);} }

		public CreateROOK (Unit parent) : base(parent) {
			name = "Build Rook";
			weight = 5;
			price = new Price(1,1);
			aims += Aim.Self();
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			if ((parent.Body.Cell.Occupants/Plane.Ground).Count > 0) {
				Charge();
				TokenFactory.Create(source, EToken.ROOK, parent.Body.Cell);
			}
		}

		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();

			DrawAim(0, p.LinePanel);
			template.DisplayThumbNameTemplate(p.LinePanel);
			float descH = (p.H-(p.LineH*2))/p.H;
			GUI.Label(p.TallWideBox(descH), desc);	
		}
	}

	public class WebShot : Task {
		int damage = 12;
		
		public override string desc {get {return "Create Web in target cell." +
				"\nAll Units in target cell take "+damage+" damage.";} }
		public override Token template {get {return TokenFactory.Template(EToken.WEBB);} }

		public WebShot (Unit parent) : base(parent) {
			name = "Web Shot";
			weight = 4;
			price = new Price(1,1);
			aims += Aim.CreateArc(0,3);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Cell c = (Cell)targets[0];
			
			EffectQueue.Add(new Effects.Create(source, EToken.WEBB, c));
			
			TokenGroup occupants = c.Occupants.units;
			foreach (Unit u in occupants) {
				EffectQueue.Add(new Effects.Damage(source, u, damage));
			}
		}

		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();

			DrawAim(0, p.LinePanel);
			template.DisplayThumbNameTemplate(p.LinePanel);
			float descH = (p.H-(p.LineH*2))/p.H;
			GUI.Label(p.TallWideBox(descH), desc);	
		}
	}
	
	public class CreateLICH : Task, IMultiTarget{
		
		public override string desc {get {return "Create Lichenthropes in up to two target cells.";} }
		public override Token template {get {return TokenFactory.Template(EToken.LICH);} }

		public CreateLICH (Unit parent) : base(parent) {
			name = "Create Lichenthropes";
			weight = 5;
			for (byte i=0; i<2; i++) {aims += Aim.CreateNeighbor();}
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Create(source, EToken.LICH, (Cell)targets[0]));
			if (targets.Count > 1 && targets[1] != null) {
				EffectQueue.Add(new Effects.Create(source, EToken.LICH, (Cell)targets[1]));
			}
			Targeter.Reset();
		}
		protected override void ExecuteFinish() {}

		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();

			DrawAim(0, p.LinePanel);
			DrawAim(1, p.LinePanel);
			template.DisplayThumbNameTemplate(p.LinePanel);
			float descH = (p.H-(p.LineH*2))/p.H;
			GUI.Label(p.TallWideBox(descH), desc);	
		}
	}
	/*
	public class APiecAper : Task {
		public override string Desc {get {return "Create Aperture in target cell.";} }
		public override Token Template {get {return TemplateFactory.Template(EToken.APER);} }
		
		public APieceAper (Unit par) {
			Name = "Open Aperture";
			Weight = 4;
			parent = par;
			Price = new Price(1,1);
			NewAim(HOA.Aim.CreateArc(3));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Cell c = (Cell)targets[0];
			
			EffectQueue.Add(new ECreate(source, EToken.APER, c));
			
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

	public class CreateAREN : Task {
		
		public override string desc {get {return "Create Arena in target cell.";} } 
		public override Token template {get {return TokenFactory.Template(EToken.AREN);} }
		
		public CreateAREN (Unit parent) : base(parent) {
			name = "Create "+template;
			weight = 5;
			price = new Price(1,1);
			aims += Aim.CreateAren();
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Create(source, EToken.AREN, (Cell)targets[0]));
		}
		
		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();
			
			DrawAim(0, p.LinePanel);
			template.DisplayThumbNameTemplate(p.LinePanel);
			float descH = (p.H-(p.LineH*2))/p.H;
			GUI.Label(p.TallWideBox(descH), desc);	
		}
	}
}

using UnityEngine; 

namespace HOA { 

	public class ANecrCorpse : Task {

		public override string Desc {get {return "Create Corpse in target cell.";} }

		public override Token Template {get {return TemplateFactory.Template(EToken.CORP);} }

		public ANecrCorpse (Unit par) {
			Name = "Plant corpse";
			Weight = 5;
			Parent = par;
			Price = Price.Free;
			NewAim(new Aim(ETraj.FREE, EType.CELL, EPurp.CREATE));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new ECreate(new Source(Parent), EToken.CORP, (Cell)targets[0]));
		}

		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, Name, p.s);
			DrawPrice(p.LinePanel);
			DrawAim(0, p.LinePanel);
			Template.DisplayTemplate(p.LinePanel, 30);
			float descH = (p.H-(p.LineH*2))/p.H;
			GUI.Label(p.TallBox(descH), Desc);	
		}
	}

	public class AGargRook : Task {
		
		public override string Desc {get {return "Create Rook in "+Parent+"'s cell.";} } 
		public override Token Template {get {return TemplateFactory.Template(EToken.ROOK);} }

		public AGargRook (Unit par) {
			Name = "Build Rook";
			Weight = 5;
			Parent = par;
			Price = new Price(1,1);
			NewAim(HOA.Aim.Self());
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			if (!Parent.Body.Cell.Occupied(EPlane.GND)) {
				Charge();
				TokenFactory.Add(EToken.ROOK, new Source(Parent), Parent.Body.Cell);
			}
		}

		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, Name, p.s);
			DrawPrice(p.LinePanel);
			DrawAim(0, p.LinePanel);
			Template.DisplayTemplate(p.LinePanel, 30);
			float descH = (p.H-(p.LineH*2))/p.H;
			GUI.Label(p.TallBox(descH), Desc);	
		}
	}

	public class ABlacWeb : Task {
		int damage = 12;
		
		public override string Desc {get {return "Create Web in target cell." +
				"\nAll Units in target cell take "+damage+" damage.";} }
		public override Token Template {get {return TemplateFactory.Template(EToken.WEBB);} }

		public ABlacWeb (Unit par) {
			Name = "Web Shot";
			Weight = 4;
			Parent = par;
			Price = new Price(1,1);
			NewAim(HOA.Aim.CreateArc(3));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Cell c = (Cell)targets[0];
			
			EffectQueue.Add(new ECreate(new Source(Parent), EToken.WEBB, c));
			
			TokenGroup occupants = c.Occupants.OnlyType(EType.UNIT);
			foreach (Unit u in occupants) {
				EffectQueue.Add(new EDamage(new Source(Parent), u, damage));
			}
		}

		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, Name, p.s);
			DrawPrice(p.LinePanel);
			DrawAim(0, p.LinePanel);
			Template.DisplayTemplate(p.LinePanel, 30);
			float descH = (p.H-(p.LineH*2))/p.H;
			GUI.Label(p.TallBox(descH), Desc);	
		}
	}
	
	public class ABlacLich : Task, IMultiTarget{
		
		public override string Desc {get {return "Create Lichenthropes in up to two target cells.";} }
		public override Token Template {get {return TemplateFactory.Template(EToken.LICH);} }

		public ABlacLich (Unit par) {
			Name = "Create Lichenthropes";
			Weight = 5;
			Parent = par;
			Price = Price.Cheap;
			NewAim(HOA.Aim.Create());
			Aim.Add(HOA.Aim.Create());
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new ECreate(new Source(Parent), EToken.LICH, (Cell)targets[0]));
			if (targets[1] != null) {
				EffectQueue.Add(new ECreate(new Source(Parent), EToken.LICH, (Cell)targets[1]));
			}
			Targeter.Reset();
		}
		protected override void ExecuteFinish() {}

		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, Name, p.s);
			DrawPrice(p.LinePanel);
			DrawAim(0, p.LinePanel);
			DrawAim(1, p.LinePanel);
			Template.DisplayTemplate(p.LinePanel, 30);
			float descH = (p.H-(p.LineH*2))/p.H;
			GUI.Label(p.TallBox(descH), Desc);	
		}
	}
}

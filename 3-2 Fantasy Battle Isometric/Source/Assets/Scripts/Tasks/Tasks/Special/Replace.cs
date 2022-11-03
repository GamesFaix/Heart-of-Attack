using UnityEngine;

namespace HOA.Actions { 

	public class Evolve : Task {
		
		EToken child;
		
		string ChildName {get {return template.ID.Name;} }
		
		public override string desc {get {return "Transform "+parent+" into a "+ChildName+".  " +
				"\n(New "+ChildName+" is added to the end of the Queue and does not retain any of "+parent+"'s attributes.)";} }
		
		public Evolve (Unit parent, Price price, EToken chi) : base(parent) {
			child = chi;
			template = TokenFactory.Template(child);
			name = "Evolve to "+ChildName;
			weight = 4;
			this.price = price;
			aims += Aim.Self();
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Replace(source, parent, child));
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

	public class Arise : Task {
		
		public override string desc {get {return "Transform "+parent+" into a Conflagragon." +
				"\n(New Conflagragon starts with "+parent+"'s health.)";} }
		public override Token template {get {return TokenFactory.Template(EToken.CONF);} }

		public Arise (Unit parent) : base(parent) {
			name = "Arise";
			weight = 4;
			price = new Price(2,0);
			aims += Aim.Self();
		}
		
		public override bool Restrict () {
			Cell c = parent.Body.Cell;
			if ((c.Occupants/Plane.Air).Count>0) {return true;}
			return false;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			int hp = ((Unit)parent).HP;
			parent.Die(source, false, false);
			Token newToken;
			TokenFactory.Create(source, EToken.CONF, parent.Body.Cell, out newToken, false);
			((Unit)newToken).SetStat(source, EStat.HP, hp);
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

	public class Recycle : Task {
		
		public override string desc {get {return "Replace target remains with Recyclops.";} }
		public override Token template {get {return TokenFactory.Template(EToken.RECY);} }

		public Recycle (Unit parent, Price price) : base(parent) {
			name = "Recycle Recyclops";
			weight = 5;
			this.price = price;
			aims += Aim.AttackNeighbor(Filters.Corpses);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Replace(source, (Token)targets[0], EToken.RECY));
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
	
	public class Animate : Task {
		
		public override string desc {get {return "Replace target non-remains destructible with Metaterrainean.";} }
		public override Token template {get {return TokenFactory.Template(EToken.META);} }

		public Animate (Unit parent) : base(parent) {
			name = "Animate Metaterrainean";
			weight = 5;
			price = new Price(1,2);
			aims += Aim.AttackNeighbor(Filters.DestNoCorpse);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Replace(source, (Token)targets[0], EToken.META));
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

	
	public class Seed : Task {
		public override string desc {get {return "Replace target non-Remains destructible with Lichenthrope.";} }
		public override Token template {get {return TokenFactory.Template(EToken.LICH);} }

		public Seed (Unit parent) : base(parent) {
			name = "Seed";
			weight = 5;
			price = new Price(1,1);
			aims += Aim.AttackArc(Filters.DestNoCorpse, 0, 2);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Replace(source, (Token)targets[0], EToken.LICH));
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

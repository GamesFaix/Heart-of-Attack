using UnityEngine;

namespace HOA.Actions { 

	public class Evolve : Task {
		
		EToken child;
		
		string ChildName {get {return Template.ID.Name;} }
		
		public override string Desc {get {return "Transform "+Parent+" into a "+ChildName+".  " +
				"\n(New "+ChildName+" is added to the end of the Queue and does not retain any of "+Parent+"'s attributes.)";} }
		
		public Evolve (Unit u, Price p, EToken chi) {
			Parent = u;
			child = chi;
			Template = TokenFactory.Template(child);
			Name = "Evolve to "+ChildName;
			Weight = 4;
			Price = p;
			NewAim(Aim.Self());
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Replace(new Source(Parent), Parent, child));
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

	public class Arise : Task {
		
		public override string Desc {get {return "Transform "+Parent+" into a Conflagragon." +
				"\n(New Conflagragon starts with "+Parent+"'s health.)";} }
		public override Token Template {get {return TokenFactory.Template(EToken.CONF);} }

		public Arise (Unit par) {
			Name = "Arise";
			Weight = 4;
			Price = new Price(2,0);
			NewAim(Aim.Self());
			Parent = par;
		}
		
		public override bool Restrict () {
			Cell c = Parent.Body.Cell;
			if (c.Contains(EPlane.AIR)) {return true;}
			return false;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			int hp = ((Unit)Parent).HP;
			Parent.Die(new Source(Parent), false, false);
			Token newToken;
			TokenFactory.Create(new Source(Parent), EToken.CONF, Parent.Body.Cell, out newToken, false);
			((Unit)newToken).SetStat(new Source(Parent), EStat.HP, hp);
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

	public class Recycle : Task {
		
		public override string Desc {get {return "Replace target remains with Recyclops.";} }
		public override Token Template {get {return TokenFactory.Template(EToken.RECY);} }

		public Recycle (Unit par, Price p) {
			Name = "Recycle Recyclops";
			Weight = 5;
			Price = p;
			Parent = par;
			NewAim(Aim.AttackNeighbor(Special.Rem));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Replace(new Source(Parent), (Token)targets[0], EToken.RECY));
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
	
	public class Animate : Task {
		
		public override string Desc {get {return "Replace target non-remains destructible with Metaterrainean.";} }
		public override Token Template {get {return TokenFactory.Template(EToken.META);} }

		public Animate (Unit parent) {
			Name = "Animate Metaterrainean";
			Weight = 5;
			Price = new Price(1,2);
			Parent = parent;
			NewAim(Aim.AttackNeighbor(Special.Dest));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Replace(new Source(Parent), (Token)targets[0], EToken.META));
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

	
	public class Seed : Task {
		public override string Desc {get {return "Replace target non-Remains destructible with Lichenthrope.";} }
		public override Token Template {get {return TokenFactory.Template(EToken.LICH);} }

		public Seed (Unit par) {
			Name = "Seed";
			Weight = 5;
			Price = new Price(1,1);
			Parent = par;
			NewAim(Aim.AttackArc(Special.Dest, 0, 2));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Replace(new Source(Parent), (Token)targets[0], EToken.LICH));
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

}

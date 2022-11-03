using System.Collections.Generic;
using UnityEngine;

namespace HOA.Actions {
	
	public class CreateArc : Task {
		EToken child;

		public override string Desc {get {return "Create "+Template.ID.Name+" in target cell.";} }

		public CreateArc (Unit parent, Price p, EToken chi, int range, int minRange=0) {
			Parent = parent;
			child = chi;
			Template = TokenFactory.Template(child);
			Name = "Create "+Template.ID.Name;

			Weight = 5;
			Price = p;
			Aim = new List<Aim>{HOA.Aim.CreateArc(range, minRange)};
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Create(new Source(Parent), child, (Cell)targets[0]));
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

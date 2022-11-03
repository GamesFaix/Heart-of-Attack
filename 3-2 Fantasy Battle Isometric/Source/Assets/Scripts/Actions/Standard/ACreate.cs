using System.Collections.Generic;
using UnityEngine;

namespace HOA {

	public class ACreate : Task {

		public override string Desc {get {return "Create "+Template.ID.Name+" in target cell.";} }
		EToken child;

		public ACreate (Unit parent, Price p, EToken child) {
			Parent = parent;
			this.child = child;
			Template = TemplateFactory.Template(child);
			Name = "Create "+Template.ID.Name;

			Weight = 5;
			Price = p;
			NewAim(HOA.Aim.Create());
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new ECreate(new Source(Parent), child, (Cell)targets[0]));
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
}



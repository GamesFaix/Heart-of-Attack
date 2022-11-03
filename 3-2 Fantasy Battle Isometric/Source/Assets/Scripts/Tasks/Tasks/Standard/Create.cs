using System.Collections.Generic;
using UnityEngine;

namespace HOA.Actions {

	public class Create : Task {

		public override string desc {get {return "Create "+template.ID.Name+" in target cell.";} }
		EToken child;

		public Create (Unit parent, Price price, EToken child) : base(parent) {
			this.child = child;
			template = TokenFactory.Template(child);
			name = "Create "+template.ID.Name;
			weight = 5;
			this.price = price;
			aims += Aim.CreateNeighbor();
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Create(source, child, (Cell)targets[0]));
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



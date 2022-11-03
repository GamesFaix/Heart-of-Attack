using System.Collections.Generic;

namespace HOA {
	
	public class ACreateArc : Task {
		EToken child;

		public override string Desc {get {return "Create "+Template.ID.Name+" in target cell.";} }

		public ACreateArc (Unit parent, Price p, EToken chi, int range, int minRange=0) {
			Parent = parent;
			child = chi;
			Template = TemplateFactory.Template(child);
			Name = "Create "+Template.ID.Name;

			Weight = 5;
			Price = p;
			AddAim(HOA.Aim.CreateArc(range, minRange));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new ECreate(new Source(Parent), child, (Cell)targets[0]));
		}
	}
}

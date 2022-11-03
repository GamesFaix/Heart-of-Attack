using System.Collections.Generic;

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
			AddAim(HOA.Aim.Create());
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new ECreate(new Source(Parent), child, (Cell)targets[0]));
		}
	}
}



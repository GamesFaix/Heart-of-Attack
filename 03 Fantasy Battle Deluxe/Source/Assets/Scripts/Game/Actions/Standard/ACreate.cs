using System.Collections.Generic;

namespace HOA {

	public class ACreate : Action {


		Cell cell;
		EToken child;

		public ACreate (Price p, Unit par, EToken chi, Aim a=default(Aim)) {
			weight = 5;
			actor = par;
			child = chi;
			childTemplate = TemplateFactory.Template(child);
			price = p;

			if (a == default(Aim)) {a = HOA.Aim.Create();}
			AddAim(a);

			name = "Create "+childTemplate.Name;
			desc = "Create "+childTemplate.Name+" in target cell.";
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();
			AEffects.Create(new Source(actor), child, (Cell)targets[0]);
			Targeter.Reset();
		}
	}
}

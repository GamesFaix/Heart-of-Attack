using UnityEngine;
using System.Collections.Generic;

namespace HOA{
	public class MeinSchutz : Unit {
		public MeinSchutz(Source s, bool template=false){
			id = new ID(this, EToken.MEIN, s, false, template);
			plane = Plane.Gnd;

			ScaleMedium();
			NewHealth(40);
			NewWatch(4);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			arsenal.Add(new Task[]{
				new AMovePath(this, 5),
				new AShoot(this, 2, 12),
				new ACreate(this, new Price(0,1), EToken.MINE),
				new AMeinDetonate(this)
			});
			arsenal.Sort();
		}

		public override string Notes () {return "";}
	}

	public class AMeinDetonate : Task {

		public override string Desc {get {return "Destroy all mines on team.";} }

		public AMeinDetonate (Unit u) {
			Name = "Detonate";
			Weight = 4;

			Parent = u;
			Price = new Price(1,1);
			AddAim(new Aim(ETraj.GLOBAL, EType.DEST));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			TokenGroup mines = Parent.Owner.OwnedUnits;
			for (int i=mines.Count-1; i>=0; i--) {
				Token t = mines[i];
				if (t.ID.Code != EToken.MINE) {mines.Remove(t);}
			}
			
			foreach (Token t in mines) {t.Die(new Source(Parent));}
		}
	}
}


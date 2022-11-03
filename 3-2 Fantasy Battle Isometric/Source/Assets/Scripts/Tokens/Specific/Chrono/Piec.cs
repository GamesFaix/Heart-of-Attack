using System.Collections.Generic;

namespace HOA{
	public class Piecemaker : Unit {
		public Piecemaker(Source s, bool template=false){
			id = new ID(this, EToken.PIEC, s, false, template);
			plane = Plane.Gnd;
			ScaleMedium();

			NewHealth(35,3);
			NewWatch(1); 
			BuildArsenal();

		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			arsenal.Add(new Task[] {
				new AMovePath(this, 4),
				new AStrike(this, 10),
				new ACreateArc(this, new Price(1,1), EToken.APER, 2),
				new APiecHeal(this)
			});
			arsenal.Sort();
		}

		public override string Notes () {return "";}
	}

	public class APiecHeal : Task {

		public override string Desc {get {return "Target unit gains "+magnitude+" health." +
				"\n(Can target self.)";} }

		int magnitude = 10;
		
		public APiecHeal (Unit u) {
			Name = "Heal";
			Weight = 4;

			Parent = u;
			Price = new Price(0,2);
			AddAim(new Aim(ETraj.ARC, EType.UNIT, 2));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new EAddStat(new Source(Parent), (Unit)targets[0], EStat.HP, magnitude));
		}
	}
}

using System.Collections.Generic;

namespace HOA{
	public class BatteringRambuchet : Unit {
		public BatteringRambuchet(Source s, bool template=false){
			ID = new ID(this, EToken.BATT, s, false, template);
			Plane = Plane.Gnd;
			Special.Add(EType.TRAM);
			ScaleLarge();
			NewHealth(65);
			NewWatch(1);
			BuildArsenal();
		}

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Task[]{
				new AMovePath(this, 2),
				new AStrike(this, 16),
				new ABattFling(this),
				new ABattCocktail(this)
			});
			Arsenal.Sort();
		}

		public override string Notes () {return "";}
	}	

	public class ABattFling : Task {
		
		int damage = 16;

		public override string Desc {get {return "Do "+damage+" damage to target unit.";} }

		public ABattFling (Unit u) {
			Name = "Fling";
			Weight = 3;
			Parent = u;
			Price = new Price(1,1);
			NewAim(HOA.Aim.Arc(3));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new EDamage (new Source(Parent), (Unit)targets[0], damage));
		}
	}


}
using System.Collections.Generic;

namespace HOA{
	public class BatteringRambuchet : Unit {
		public BatteringRambuchet(Source s, bool template=false){
			id = new ID(this, EToken.BATT, s, false, template);
			plane = Plane.Gnd;
			type.Add(EType.TRAM);
			ScaleLarge();
			NewHealth(65);
			NewWatch(1);
			BuildArsenal();
		}

		protected override void BuildArsenal () {
			base.BuildArsenal();
			arsenal.Add(new Task[]{
				new AMovePath(this, 2),
				new AStrike(this, 16),
				new ABattFling(this),
				new ABattCocktail(this)
			});
			arsenal.Sort();
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
			AddAim(HOA.Aim.Arc(3));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new EDamage (new Source(Parent), (Unit)targets[0], damage));
		}
	}

	public class ABattCocktail : Task {
		int damage = 20;

		public override string Desc {get {return "Do "+damage+" damage to target unit. " +
				"\nTarget's neighbors and cellmates take 50% damage (rounded down).  " +
					"\nDestroy all destructible tokens that would take damage.";} }

		public ABattCocktail (Unit u) {
			Name = "Cocktail";
			Weight = 3;
			Price = new Price(1,2);
			AddAim(new HOA.Aim(ETraj.ARC, Special.UnitDest, 3));
			Parent = u;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new EFire(new Source(Parent), (Token)targets[0], damage));
		}
	}
}
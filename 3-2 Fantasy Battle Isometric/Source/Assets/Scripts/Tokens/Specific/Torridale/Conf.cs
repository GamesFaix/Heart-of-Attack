using System.Collections.Generic;

namespace HOA{
	public class Conflagragon : Unit {
		public Conflagragon(Source s, bool template=false){
			id = new ID(this, EToken.CONF, s, false, template);
			plane = Plane.Air;
			onDeath = EToken.ASHE;
			ScaleMedium();
			NewHealth(30);
			NewWatch(4);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			arsenal.Add(new Task[]{
				new AMovePath(this, 6),
				new AConfStrike(this),
				new AConfFire(this)
			});

			arsenal.Sort();
		}

		public override string Notes () {return "";}
	}

	public class AConfStrike : Task {
		
		int damage = 12;

		public override string Desc {get {return "Do "+damage+" damage to target unit.";} }

		public AConfStrike (Unit u) {
			Name = "Strike";
			Weight = 3;
			Parent = u;
			Price = new Price(0,1);
			AddAim(HOA.Aim.Melee());
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new EDamage (new Source(Parent), (Unit)targets[0], damage));
		}
	}

	public class AConfFire : Task {
		
		int damage = 10;

		public override string Desc {get {return "Do "+damage+" damage to target unit. " +
				"\nTarget's neighbors and cellmates take 50% damage (rounded down).  " +
					"\nDestroy all destructible tokens that would take damage.";} }
		
		public AConfFire (Unit u) {
			Name = "Firebreathing";
			Weight = 3;
			Price = new Price(2,0);
			AddAim(new HOA.Aim(ETraj.LINE, Special.UnitDest, 3));
			Parent = u;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new EFire(new Source(Parent), (Token)targets[0], damage));
		}
	}


}
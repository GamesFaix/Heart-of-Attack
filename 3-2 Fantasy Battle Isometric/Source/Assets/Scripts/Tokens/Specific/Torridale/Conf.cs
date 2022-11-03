namespace HOA{
	public class Conflagragon : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new Conflagragon (source, template);
		}

		Conflagragon(Source s, bool template=false){
			ID = new ID(this, EToken.CONF, s, false, template);
			Plane = Plane.Air;
			OnDeath = EToken.ASHE;
			ScaleMedium();
			NewHealth(30);
			NewWatch(4);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Task[]{
				new AMovePath(this, 6),
				new AConfStrike(this),
				new AConfFire(this)
			});

			Arsenal.Sort();
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
			NewAim(HOA.Aim.Melee());
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new EDamage (new Source(Parent), (Unit)targets[0], damage));
		}
	}




}
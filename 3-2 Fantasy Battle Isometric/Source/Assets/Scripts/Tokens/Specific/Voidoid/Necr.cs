using System.Collections.Generic;

namespace HOA{
	public class Necrochancellor : Unit {
		public Necrochancellor(Source s, bool template=false){
			ID = new ID(this, EToken.NECR, s, false, template);
			Plane = Plane.Eth;
			OnDeath = EToken.NONE;
			ScaleMedium();
			NewHealth(30,5);
			NewWatch(3);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Task[]{
				new AMovePath(this, 3),
				new ANecrTeleport(this),
				new ANecrTouch(this)
			});
			Arsenal.Sort();
		}

		public override string Notes () {return "";}
	}

	public class ANecrTouch : Task {
		int damage = 16;

		public override string Desc {get {return "Do "+damage+" damage to target unit." +
			"\nIf target has less than 10 health after damage is dealt, destroy target." +
			"\nIf target is destroyed and is not an Attack King, it leaves no remains and you may place a Corpse in any cell.";
		} } 

		public ANecrTouch (Unit par) {
			Name = "Touch of Death";
			Weight = 3;
			Parent = par;
			Price = Price.Cheap;
			NewAim(HOA.Aim.Melee());
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Unit u = (Unit)targets[0];
			int oldHP = u.HP;
			int def = u.DEF;
			
			int dmg = damage - def;
			if (oldHP - dmg < 10) {dmg = oldHP;}
			if (dmg >= oldHP) {
				EffectQueue.Add(new EKill(new Source(Parent), u));
				Targeter.Start(new ANecrCorpse(Parent));

			}
			else {
				EffectQueue.Add(new EDamage(new Source(Parent), u, damage));
				Targeter.Reset();
			}
		}
		protected override void ExecuteFinish() {}
	}
}
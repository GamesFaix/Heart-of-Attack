using System.Collections.Generic;

namespace HOA{
	public class DreamReaver : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new DreamReaver (source, template);
		}

		DreamReaver(Source s, bool template=false){
			ID = new ID(this, EToken.DREA, s, true, template);
			Plane = Plane.Air;
			Special.Add(EType.KING);
			OnDeath = EToken.HGLA;
			ScaleJumbo();
			NewHealth(75,2);
			NewWatch(3);
			NewWallet(3);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Task[]{
				new AMovePath(this, 4),
				new ADreaBeam(this),
				new ADreaTeleport(this),

				new ACreate(this, Price.Cheap, EToken.PRIS),
				new ACreate(this, new Price(1,1), EToken.AREN),
				new ACreate(this, new Price(1,2), EToken.PRIE)
			});
			Arsenal.Sort();
		}

		public override string Notes () {return "";}
	}

	public class ADreaBeam : Task {
		
		int damage = 12;

		public override string Desc {get {return "Do "+damage+" damage to target unit." +
				"\nTarget loses all Focus.";} } 

		public ADreaBeam (Unit u) {
			Name = "Psi Beam";
			Weight = 4;

			Parent = u;
			Price = new Price(1,1);
			NewAim(HOA.Aim.Shoot(3));

		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Unit u = (Unit)targets[0];
			EffectQueue.Add(new EDamage (new Source(Parent), u, damage));
			EffectQueue.Add(new EAddStat (new Source(Parent), u, EStat.FP, 0-u.FP));
		}
	}
}

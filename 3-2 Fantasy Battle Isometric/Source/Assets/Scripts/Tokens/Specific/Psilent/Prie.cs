using System.Collections.Generic;

namespace HOA{
	public class PriestOfNaja : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new PriestOfNaja (source, template);
		}

		PriestOfNaja(Source s, bool template=false){
			ID = new ID(this, EToken.PRIE, s, false, template);
			Plane = Plane.Gnd;
			ScaleLarge();
			NewHealth(50,2);
			NewWatch(4);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Task[]{
				new AMovePath(this, 4),
				new AStrike(this, 15),
				new APrieShove(this)
			});
			Arsenal.Sort();
		}

		public override string Notes () {return "";}
	}
		
	public class APrieShove : Task {
		
		int damage = 12;
		int kb = 5;
		int kbdmg = 2;

		public override string Desc {get {return "Do "+damage+" damage to target unit." +
				"\nKnockback "+kb+" (Move target in a line away from "+Parent+", up to "+kb+" cells.)" +
					"\nTarget takes "+kbdmg+" damage per cell knocked back.";} }

		public APrieShove (Unit u) {
			Name = "Shove";
			Weight = 4;

			Parent = u;
			Price = new Price(1,1);
			NewAim(HOA.Aim.Melee());
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectGroup e = new EffectGroup();
			e.Add(new EDamage (new Source(Parent), (Unit)targets[0], damage));
			e.Add(new EKnockback (new Source(Parent), (Unit)targets[0], kb, kbdmg));
			EffectQueue.Add(e);
		}
	}










}
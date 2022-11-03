using System.Collections.Generic;

namespace HOA{
	public class PriestOfNaja : Unit {
		public PriestOfNaja(Source s, bool template=false){
			id = new ID(this, EToken.PRIE, s, false, template);
			plane = Plane.Gnd;
			ScaleLarge();
			NewHealth(50,2);
			NewWatch(4);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			arsenal.Add(new Task[]{
				new AMovePath(this, 4),
				new AStrike(this, 15),
				new APrieShove(this)
			});
			arsenal.Sort();
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
			AddAim(HOA.Aim.Melee());
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectGroup e = new EffectGroup();
			e.Add(new EDamage (new Source(Parent), (Unit)targets[0], damage));
			e.Add(new EKnockback (new Source(Parent), (Unit)targets[0], kb, kbdmg));
			EffectQueue.Add(e);
		}
	}










}
using System.Collections.Generic;

namespace HOA{
	public class PriestOfNaja : Unit {
		public PriestOfNaja(Source s, bool template=false){
			id = new ID(this, EToken.PRIE, s, false, template);
			plane = Plane.Gnd;
			ScaleLarge();
			NewHealth(50,2);
			NewWatch(4);
			
			arsenal.Add(new AMovePath(this, 4));
			arsenal.Add(new AAttack("Melee", Price.Cheap, this, Aim.Melee(), 15));
			arsenal.Add(new APrieShove(this));

			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}
		
	public class APrieShove : Action {
		
		int damage = 12;
		int kb = 5;
		int kbdmg = 2;

		public APrieShove (Unit u) {
			weight = 4;
			actor = u;
			price = new Price(1,1);
			AddAim(HOA.Aim.Melee());

			name = "Shove";
			desc = "Do "+damage+" damage to target unit.\nKnockback "+kb+" (Move target in a line away from "+actor+", up to "+kb+" cells.)\nTarget takes "+kbdmg+" damage per cell knocked back.";
		}
		
		public override void Execute (List<ITarget> targets) {
			Charge();
			EffectGroup e = new EffectGroup();
			e.Add(new EDamage (new Source(actor), (Unit)targets[0], damage));
			e.Add(new EKnockback (new Source(actor), (Unit)targets[0], kb, kbdmg));
			EffectQueue.Add(e);
			Targeter.Reset();
		}
	}










}
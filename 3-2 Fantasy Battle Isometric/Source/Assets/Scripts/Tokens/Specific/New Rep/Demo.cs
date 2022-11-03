using UnityEngine;
using System.Collections.Generic;

namespace HOA{
	public class Demolitia : Unit {
		public Demolitia(Source s, bool template=false){
			id = new ID(this, EToken.DEMO, s, false, template);
			plane = Plane.Gnd;

			ScaleSmall();
			health = new HealthDemo(this, 30);
			NewWatch(3);

			arsenal.Add(new AMovePath(this, 3));
			arsenal.Add(new AGrenade("Throw", new Price(1,1), this, 3, 10));
			arsenal.Add(new ADemoSticky(this));
			arsenal.Sort();
		}
		public override string Notes () {return "Defense +1 per Focus (up to +4).";}
	}

	public class HealthDemo : Health{
		public HealthDemo (Unit u, int n=0, int d=0){
			parent = u; max = n; Fill(); def = d;
		}
		public override int DEF {
			get {return def + Mathf.Min(4, parent.FP);}
		}
	}

	public class ADemoSticky : Action {

		public ADemoSticky (Unit u) {
			weight = 4;
			actor = u;
			price = new Price(1,0);
			AddAim(HOA.Aim.Melee());
			int damage = 10;

			name = "Plant";
			desc = "At the end of target Unit's next turn, do "+damage+" damage to all units in its cell. " +
				"\nAll units in neighboring cells take 50% damage (rounded down). " +
				"\nDamage continues to spread outward with 50% reduction until 1. " +
				"\nDestroy all destructible tokens that would take damage.";
		}
		
		public override void Execute (List<ITarget> targets) {
			Charge();
			Unit u = (Unit)targets[0];
			u.timers.Add(new TStickyGrenade(u,actor));
			Targeter.Reset();
		}
	}

	public class TStickyGrenade : Timer {

		Token source;
		
		public TStickyGrenade (Unit par, Token s) {
			parent = par;
			source = s;
			turns = 1;

			name = "Active Grenade";
			desc = "At the end of "+parent.ToString()+" next turn, do 10 damage to all units in its cell. " +
				"\nAll units in neighboring cells take 50% damage (rounded down). " +
				"\nDamage continues to spread outward with 50% reduction until 1. " +
				"\nDestroy all destructible tokens that would take damage.";

		}
		
		public override void Activate () {
			EffectQueue.Add(new EExplosion(new Source(source), parent.Body.Cell, 10));
		}
	}


}
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
			BuildArsenal();
		}

		protected override void BuildArsenal () {
			base.BuildArsenal();
			arsenal.Add(new Task[] {
				new AMovePath(this, 3),
				new ADemoThrow(this),
				new ADemoSticky(this)
			});
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

	public class ADemoSticky : Task {

		public override string Desc {get {return "At the end of target Unit's next turn, do "+damage+" damage to all units in its cell. " +
				"\nAll units in neighboring cells take 50% damage (rounded down). " +
					"\nDamage continues to spread outward with 50% reduction until 1. " +
						"\nDestroy all destructible tokens that would take damage.";} }

		int damage = 10;

		public ADemoSticky (Unit parent) {
			Name = "Plant";
			Weight = 4;
			Parent = parent;
			Price = new Price(1,0);
			AddAim(HOA.Aim.Melee());
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Unit target = (Unit)targets[0];
			target.timers.Add(new TStickyGrenade(target, Parent));
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

	public class ADemoThrow : Task {

		public override string Desc {get {return "Do "+damage+" damage to all units in target cell. " +
				"\nAll units in neighboring cells take 50% damage (rounded down). " +
					"\nDamage continues to spread outward with 50% reduction until 1. " +
						"\nDestroy all destructible tokens that would take damage.";} }

		int range = 3;
		int damage = 10;
		
		public ADemoThrow (Unit parent) {
			Name = "Throw";
			Weight = 3;
			Price = new Price(1,1);
			Parent = parent;
			AddAim(new Aim (ETraj.ARC, EType.CELL, EPurp.ATTACK, range));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new EExplosion(new Source(Parent), (Cell)targets[0], damage));
		}
	}

}
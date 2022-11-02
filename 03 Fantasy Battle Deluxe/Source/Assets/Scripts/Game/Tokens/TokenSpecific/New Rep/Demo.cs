﻿using UnityEngine;
using System.Collections.Generic;

namespace HOA{
	public class Demolitia : Unit {
		public Demolitia(Source s, bool template=false){
			NewLabel(EToken.DEMO, s, false, template);
			BuildGround();
			
			health = new HealthDemo(this, 30);
			NewWatch(3);
			
			arsenal.Add(new AMove(this, Aim.MovePath(3)));
			arsenal.Add(new AGrenade("Grenade", Price.Cheap, this, 3, 10));
			arsenal.Add(new ADemoSticky(this));
			arsenal.Sort();
		}
		public override string Notes () {return "Defense +1 per Focus (up to 4).";}
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
			price = new Price(1,1);
			AddAim(HOA.Aim.Melee());
			int damage = 10;

			name = "Sticky Grenade";
			desc = "At the end of target Unit's next turn, do "+damage+" damage to all units in its cell. \nAll units in neighboring cells take 50% damage (rounded down). \nDamage continues to spread outward with 50% reduction until 1. \nDestroy all destructible tokens that would take damage.";
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();
			Unit u = (Unit)targets[0];
			u.timers.Add(new TStickyGrenade(u,actor));
		}
	}

	public class TStickyGrenade : Timer {

		Token source;
		
		public TStickyGrenade (Unit par, Token s) {
			parent = par;
			source = s;
			turns = 1;

			name = "Sticky Grenade";
			desc = "At the end of "+parent.ToString()+" next turn, do 10 damage to all units in its cell. \nAll units in neighboring cells take 50% damage (rounded down). \nDamage continues to spread outward with 50% reduction until 1. \nDestroy all destructible tokens that would take damage.";

		}
		
		public override void Activate () {
			InputBuffer.Submit(new RExplosion(new Source(source), parent.Cell, 10));
		}
	}


}
﻿using UnityEngine;
using System.Collections.Generic;

namespace HOA{
	public class Panopticannon : Unit {
		public Panopticannon(Source s, bool template=false){
			id = new ID(this, EToken.PANO, s, false, template);
			plane = Plane.Gnd;
			type.Add(EType.TRAM);
			ScaleLarge();
			health = new HealthPano(this, 65);
			NewWatch(1);

			arsenal.Add(new AMovePath(this, 1));
			arsenal.Add(new APanoCannon(Price.Cheap, this, 12));
			arsenal.Add(new APanoPierce(new Price(1,2), this, 20));
			arsenal.Sort();
		}		
		public override string Notes () {return "Defense +1 per Focus (up to +2).";}
	}	

	public class HealthPano : Health{
		public HealthPano (Unit u, int n=0, int d=0){
			parent = u; max = n; Fill(); def = d;
		}
		public override int DEF {
			get {return def + Mathf.Min(4, parent.FP);}
		}
	}

	public class APanoCannon : Action {
		int damage;
		
		public APanoCannon (Price p, Unit u, int d) {
			weight = 3;
			price = p;
			actor = u;
			
			AddAim(new Aim(ETraj.ARC, EType.UNIT, 3, 2));
			damage = d;
			
			name = "Cannon";
			desc = "Do "+d+" damage to target unit.  " +
				"\nMax range +1 per focus (up to +3).";
			
		}

		public override void Adjust () {
			int bonus = Mathf.Min(actor.FP, 3);
			aim[0] = new Aim (aim[0].Trajectory, aim[0].Type, aim[0].Range+bonus, aim[0].MinRange);
		}

		public override void UnAdjust () {
			aim[0] = new Aim(ETraj.ARC, EType.UNIT, 3, 2);
		}

		public override void Execute (List<ITarget> targets) {
			Charge();
			EffectQueue.Add(new EDamage(new Source(actor), (Unit)targets[0], damage));
			Targeter.Reset();
		}
	}
	public class APanoPierce : Action {
		int damage;
		
		public APanoPierce (Price p, Unit u, int d) {
			weight = 4;
			price = p;
			actor = u;
			
			AddAim(new Aim(ETraj.ARC, EType.UNIT, 3, 2));
			damage = d;
			
			name = "Armor Pierce";
			desc = "Do "+d+" damage to target unit (ignore defense).  " +
				"\nMax range +1 per focus (up to +3).";
		}

		public override void Adjust () {
			int bonus = Mathf.Min(actor.FP, 3);
			aim[0] = new Aim (aim[0].Trajectory, aim[0].Type, aim[0].Range+bonus, aim[0].MinRange);
		}

		public override void UnAdjust () {
			aim[0] = new Aim(ETraj.ARC, EType.UNIT, 4, 3);
		}

		public override void Execute (List<ITarget> targets) {
			Charge();
			EffectQueue.Add(new EPierce (new Source(actor), (Unit)targets[0], damage));
			Targeter.Reset();
		}
	}
}
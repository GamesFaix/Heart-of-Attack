using UnityEngine;
using System.Collections.Generic;

namespace HOA{
	public class Demolitia : Unit {
		public Demolitia(Source s, bool template=false){
			ID = new ID(this, EToken.DEMO, s, false, template);
			Plane = Plane.Gnd;

			ScaleSmall();
			NewHealth(30);
			NewWatch(3);
			Wallet = new DEFWallet(this, 2, 4);
			BuildArsenal();
		}

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Task[] {
				new AMovePath(this, 3),
				new ADemoThrow(this),
				new ADemoSticky(this)
			});
			Arsenal.Sort();
		}
		public override string Notes () {return "";}
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
			NewAim(new Aim (ETraj.ARC, EType.CELL, EPurp.ATTACK, range));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new EExplosion(new Source(Parent), (Cell)targets[0], damage));
		}
	}

}
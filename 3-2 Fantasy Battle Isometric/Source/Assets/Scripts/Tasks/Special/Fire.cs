using UnityEngine; 

namespace HOA { 
	public class ABattCocktail : Task {
		int damage = 20;
		
		public override string Desc {get {return "Do "+damage+" damage to target unit. " +
				"\nTarget's neighbors and cellmates take 50% damage (rounded down).  " +
					"\nDestroy all destructible tokens that would take damage.";} }
		
		public ABattCocktail (Unit u) {
			Name = "Cocktail";
			Weight = 3;
			Price = new Price(1,2);
			NewAim(new HOA.Aim(ETraj.ARC, Special.UnitDest, 3));
			Parent = u;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new EFire(new Source(Parent), (Token)targets[0], damage));
		}
	}

	public class AConfFire : Task {
		
		int damage = 10;
		
		public override string Desc {get {return "Do "+damage+" damage to target unit. " +
				"\nTarget's neighbors and cellmates take 50% damage (rounded down).  " +
					"\nDestroy all destructible tokens that would take damage.";} }
		
		public AConfFire (Unit u) {
			Name = "Firebreathing";
			Weight = 3;
			Price = new Price(2,0);
			NewAim(new HOA.Aim(ETraj.LINE, Special.UnitDest, 3));
			Parent = u;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new EFire(new Source(Parent), (Token)targets[0], damage));
		}
	}
	/*
	public class AMonoFlame : Task {
		int damage;
		
		public AMonoFlame (Unit u) {
			weight = 4;
			Price = new Price(1,2);
			Parent = u;
			
			NewAim(new Aim (ETraj.LINE, new List<EType> {EType.UNIT, EType.DEST}, 2));
			damage = 20;
			
			name = "Eternal Flame";
			desc = "Do "+damage+" damage to target unit. \nTarget's neighbors and cellmates take 50% damage (rounded down). \nDamage continues spreading until less than 1. \nDestroy all destructible tokens that would take damage.";
		}
		
		public override void Execute (TargetGroup targets) {
			Charge();
			Token tar = (Token)targets[0];

			TokenGroup affected = new TokenGroup(Parent);
			TokenGroup thisRad = new TokenGroup(tar);
			TokenGroup nextRad = new TokenGroup();
			
			int dmg = damage;
			
			while (dmg > 0) {
				for (int j=0; j<thisRad.Count; j++) {
					Token next = thisRad[j];
					
					if (!affected.Contains(next)) {		
						next.Display.Effect(EEffect.FIRE);
						AEffects.DamageDest(new Source(Parent), next, dmg);
						
						foreach (Token t in next.Neighbors(true)) {
							nextRad.Add(t);
						}
						affected.Add(next);
					}
				}
				thisRad = nextRad;
				nextRad = new TokenGroup();
				dmg = (int)Mathf.Floor(dmg * 0.5f);
				Targeter.Reset();
			}

		}
	}
	*/


}

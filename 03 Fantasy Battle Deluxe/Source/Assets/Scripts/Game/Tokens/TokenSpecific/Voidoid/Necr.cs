using System.Collections.Generic;

namespace HOA{
	public class Necrochancellor : Unit {
		public Necrochancellor(Source s, bool template=false){
			NewLabel(EToken.NECR, s, false, template);
			BuildEth();
			
			NewHealth(30,5);
			NewWatch(3);
			
			arsenal.Add(new AMove(this, Aim.MovePath(3)));
			arsenal.Add(new ANecrTeleport(this));
			arsenal.Add(new ANecrTouch(this));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}

	public class ANecrTouch : Action {
		
		int damage;
		
		public ANecrTouch (Unit par) {
			weight = 3;
			actor = par;
			price = Price.Cheap;
			AddAim(HOA.Aim.Melee());
			damage = 12;
			
			name = "Touch of Death";
			desc = "Do "+damage+" damage to target unit.\nIf target has less than 10 health after damage is dealt, destroy target.\nIf target is destroyed and is not an Attack King, it leaves no remains and you may place a Corpse in any cell.";
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();
			Unit u = (Unit)targets[0];
			int oldHP = u.HP;
			int def = u.DEF;
			
			int dmg = damage - def;
			if (oldHP - dmg < 10) {dmg = oldHP;}
			if (dmg >= oldHP) {
				u.Die(new Source(actor), false, true);
				Reset();
				Targeter.Find(new ANecrCorpseSpawn(actor));
			}
			else {
				u.Damage(new Source(actor), damage);
				u.SpriteEffect(EEffect.DMG);
			}
		}
	}

	public class ANecrCorpseSpawn : Action {
		public ANecrCorpseSpawn (Unit u) {
			actor = u;
			AddAim (new Aim(EAim.FREE, EClass.CELL, EPurpose.CREATE));
			price = new Price(0,0);
			name = "Exhume";
			desc = "Create Corpse in any cell.";

			childTemplate = TemplateFactory.Template(EToken.CORP);
		}

		public override void Execute (List<ITargetable> targets) {
			Charge();
			InputBuffer.Submit(new RCreate (new Source(actor), EToken.CORP, (Cell)targets[0]));

		}


	}

	public class ANecrTeleport : Action {
		Aim aim2;
		
		public ANecrTeleport (Unit u) {
			int range = 5;
			weight = 4;
			actor = u;
			price = new Price(0,1);
			AddAim(new Aim(EAim.ARC, EClass.REM, range));
			AddAim(HOA.Aim.MoveArc(range));
			
			name = "Defile";
			desc = "Move target remains to target cell.\n"+aim[1].ToString();
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();
			InputBuffer.Submit(new RMove(new Source(actor), (Token)targets[0], (Cell)targets[1]));
			
		}
	}
}
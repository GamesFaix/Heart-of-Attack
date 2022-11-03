using System.Collections.Generic;

namespace HOA{
	public class Necrochancellor : Unit {
		public Necrochancellor(Source s, bool template=false){
			id = new ID(this, EToken.NECR, s, false, template);
			plane = Plane.Eth;
			onDeath = EToken.NONE;
			ScaleMedium();
			NewHealth(30,5);
			NewWatch(3);
			
			arsenal.Add(new AMovePath(this, 3));
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
			damage = 16;
			
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
				Targeter.Find(new ANecrCorpse(actor));

			}
			else {
				u.Damage(new Source(actor), damage);
				u.Display.Effect(EEffect.DMG);
				Targeter.Reset();
			}
		}
	}

	public class ANecrCorpse : Action {

		public ANecrCorpse (Unit par) {
			weight = 5;
			actor = par;
			childTemplate = TemplateFactory.Template(EToken.CORP);
			price = Price.Free;
			
			AddAim(new Aim(EAim.FREE, EType.CELL, EPurpose.CREATE));
			
			name = "Plant corpse";
			desc = "Create "+childTemplate.ID.Name+" in target cell.";
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();
			EffectQueue.Add(new ECreate(new Source(actor), EToken.CORP, (Cell)targets[0]));
			Targeter.Reset();
		}
	}

	public class ANecrTeleport : Action, ITeleport {
		Aim aim2;
		
		public ANecrTeleport (Unit u) {
			int range = 5;
			weight = 4;
			actor = u;
			price = new Price(0,1);
			AddAim(new Aim(EAim.ARC, EType.REM, range));
			AddAim(HOA.Aim.MoveArc(range));
			
			name = "Defile";
			desc = "Move target remains to target cell.\n"+aim[1].ToString();
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();
			EffectQueue.Add(new ETeleport(new Source(actor), (Token)targets[0], (Cell)targets[1]));
			Targeter.Reset();
		}
	}

}
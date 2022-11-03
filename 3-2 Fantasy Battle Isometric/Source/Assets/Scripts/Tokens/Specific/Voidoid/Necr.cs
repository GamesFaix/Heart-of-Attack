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
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			arsenal.Add(new Task[]{
				new AMovePath(this, 3),
				new ANecrTeleport(this),
				new ANecrTouch(this)
			});
			arsenal.Sort();
		}

		public override string Notes () {return "";}
	}

	public class ANecrTouch : Task {
		int damage = 16;

		public override string Desc {get {return "Do "+damage+" damage to target unit." +
			"\nIf target has less than 10 health after damage is dealt, destroy target." +
			"\nIf target is destroyed and is not an Attack King, it leaves no remains and you may place a Corpse in any cell.";
		} } 

		public ANecrTouch (Unit par) {
			Name = "Touch of Death";
			Weight = 3;
			Parent = par;
			Price = Price.Cheap;
			AddAim(HOA.Aim.Melee());
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Unit u = (Unit)targets[0];
			int oldHP = u.HP;
			int def = u.DEF;
			
			int dmg = damage - def;
			if (oldHP - dmg < 10) {dmg = oldHP;}
			if (dmg >= oldHP) {
				EffectQueue.Add(new EKill(new Source(Parent), u));
				Targeter.Find(new ANecrCorpse(Parent));

			}
			else {
				EffectQueue.Add(new EDamage(new Source(Parent), u, damage));
				Targeter.Reset();
			}
		}
		protected override void ExecuteFinish() {}
	}

	public class ANecrCorpse : Task {

		public override string Desc {get {return "Create Corpse in target cell.";} }

		public ANecrCorpse (Unit par) {
			Name = "Plant corpse";
			Weight = 5;
			Parent = par;
			Template = TemplateFactory.Template(EToken.CORP);
			Price = Price.Free;
			AddAim(new Aim(ETraj.FREE, EType.CELL, EPurp.CREATE));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new ECreate(new Source(Parent), EToken.CORP, (Cell)targets[0]));
		}
	}

	public class ANecrTeleport : Task, ITeleport {
		public override string Desc {get {return "Move target remains to target cell." +
				"\n"+aim[1].ToString();} } 

		public ANecrTeleport (Unit u) {
			int range = 5;
			AddAim(new Aim(ETraj.ARC, EType.REM, range));
			AddAim(HOA.Aim.MoveArc(range));
			Name = "Defile";
			Weight = 4;
			Parent = u;
			Price = new Price(0,1);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new ETeleport(new Source(Parent), (Token)targets[0], (Cell)targets[1]));
		}
	}
}
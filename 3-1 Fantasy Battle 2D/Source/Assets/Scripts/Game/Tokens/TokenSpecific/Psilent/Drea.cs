using System.Collections.Generic;

namespace HOA{
	public class DreamReaver : Unit {
		public DreamReaver(Source s, bool template=false){
			NewLabel(EToken.DREA, s, true, template);
			BuildAir();
			AddKing();
			OnDeath = EToken.HGLA;
			
			NewHealth(75,2);
			NewWatch(3);
			
			arsenal.Add(new AMovePath(this, 4));
			arsenal.Add(new ADreaBeam(this));

			arsenal.Add(new ACreate(Price.Cheap, this, EToken.PRIS));
			arsenal.Add(new ACreate(new Price(1,1), this, EToken.AREN));
			arsenal.Add(new ACreate(new Price(1,2), this, EToken.PRIE));
			arsenal.Add(new ADreaTeleport(this));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}

	public class ADreaTeleport : Action, ITeleport {
		Aim aim2;
		
		public ADreaTeleport (Unit u) {
			weight = 4;
			actor = u;
			price = new Price(1,1);
			AddAim(new Aim(EAim.ARC, EClass.UNIT, 5));
			aim[0].EnemyOnly = true;
			aim[0].NoKings = true;
			AddAim(new Aim(EAim.ARC, EClass.CELL, EPurpose.MOVE, 5));
			
			name = "Teleport Enemy";
			desc = "Move target enemy (exluding Attack Kings) to target cell.\n"+aim[1].ToString();
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();
			EffectQueue.Add(new ETeleport(new Source(actor), (Unit)targets[0], (Cell)targets[1]));
			Targeter.Reset();
		}
	}

	public class ADreaBeam : Action {
		
		int damage = 12;
		
		public ADreaBeam (Unit u) {
			weight = 4;
			actor = u;
			price = new Price(1,1);
			AddAim(HOA.Aim.Shoot(3));

			name = "Psi Beam";
			desc = "Do "+damage+" damage to target unit.\nTarget loses all Focus.";
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();
			Unit u = (Unit)targets[0];
			EffectQueue.Add(new EDamage (new Source(actor), u, damage));
			EffectQueue.Add(new EAddStat (new Source(actor), u, EStat.FP, 0-u.FP));

			//AEffects.Damage (new Source(actor), (Unit)targets[0], damage);
			Targeter.Reset();
		}
	}
}

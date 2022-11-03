using System.Collections.Generic;

namespace HOA{
	public class DreamReaver : Unit {
		public DreamReaver(Source s, bool template=false){
			id = new ID(this, EToken.DREA, s, true, template);
			plane = Plane.Air;
			type.Add(EType.KING);
			onDeath = EToken.HGLA;
			ScaleJumbo();
			NewHealth(75,2);
			NewWatch(3);
			NewWallet(3);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			arsenal.Add(new Task[]{
				new AMovePath(this, 4),
				new ADreaBeam(this),
				new ADreaTeleport(this),

				new ACreate(this, Price.Cheap, EToken.PRIS),
				new ACreate(this, new Price(1,1), EToken.AREN),
				new ACreate(this, new Price(1,2), EToken.PRIE)
			});
			arsenal.Sort();
		}

		public override string Notes () {return "";}
	}

	public class ADreaTeleport : Task, ITeleport {
		public override string Desc {get {return "Move target enemy (exluding Attack Kings) to target cell." +
				"\n"+aim[1].ToString();} }

		public ADreaTeleport (Unit u) {
			AddAim(new Aim(ETraj.ARC, EType.UNIT, 5));
			aim[0].EnemyOnly = true;
			aim[0].NoKings = true;
			AddAim(new Aim(ETraj.ARC, EType.CELL, EPurp.MOVE, 5));

			Name = "Teleport Enemy";
			Weight = 4;
			Parent = u;
			Price = new Price(1,1);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new ETeleport(new Source(Parent), (Unit)targets[0], (Cell)targets[1]));
		}
	}

	public class ADreaBeam : Task {
		
		int damage = 12;

		public override string Desc {get {return "Do "+damage+" damage to target unit." +
				"\nTarget loses all Focus.";} } 

		public ADreaBeam (Unit u) {
			Name = "Psi Beam";
			Weight = 4;

			Parent = u;
			Price = new Price(1,1);
			AddAim(HOA.Aim.Shoot(3));

		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Unit u = (Unit)targets[0];
			EffectQueue.Add(new EDamage (new Source(Parent), u, damage));
			EffectQueue.Add(new EAddStat (new Source(Parent), u, EStat.FP, 0-u.FP));
		}
	}
}

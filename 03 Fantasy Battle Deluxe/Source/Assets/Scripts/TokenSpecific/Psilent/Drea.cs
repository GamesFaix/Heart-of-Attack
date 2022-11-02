
namespace HOA{
	public class DreamReaver : Unit {
		public DreamReaver(Source s, bool template=false){
			NewLabel(TTYPE.DREA, s, true, template);
			BuildAir();
			AddKing();
			OnDeath = TTYPE.HGLA;
			
			NewHealth(75,2);
			NewWatch(3);
			
			arsenal.Add(new AMove(this, Aim.MovePath(4)));
			
			arsenal.Add(new ACreate(Price.Cheap, this, TTYPE.PRIS));
			arsenal.Add(new ACreate(new Price(1,1), this, TTYPE.AREN));
			arsenal.Add(new ACreate(new Price(1,2), this, TTYPE.PRIE));
			arsenal.Add(new ADreaTeleport(new Price(1,1), this, 5));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}

	public class ADreaTeleport : Action {
		Aim aim2;
		
		public ADreaTeleport (Price p, Unit u, int range) {
			weight = 4;
			actor = u;
			price = p;
			aim = new Aim(AIMTYPE.ARC, TARGET.TOKEN, TTAR.UNIT, range);
			//aim.EnemyOnly = true;
			aim.NoKings = true;
			aim2 = new Aim(AIMTYPE.ARC, TARGET.CELL, CTAR.MOVE, range);
			
			name = "Teleport Enemy";
			desc = "Move target enemy (exluding Attack Kings) to target cell.\n"+aim2.ToString();
		}
		
		public override void Perform () {
			if (Charge()) {
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new RTeleport(new Source(actor), default(Token), aim2));
			}
		}
	}
}

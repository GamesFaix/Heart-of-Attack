using HOA.Tokens.Components;
using HOA.Players;
using HOA.Actions;
using HOA.Map;

namespace HOA.Tokens{

	public class Lichenthrope : Unit {
		public Lichenthrope(Source s, bool template=false){
			NewLabel(TTYPE.LICH, s, false, template);
			BuildGround();
			AddCorpseless();
			
			NewHealth(15);
			NewWatch(3);
			
			arsenal.Add(new AMove(this, Aim.MovePath(0)));
			arsenal.Add(new ALeech(Price.Cheap, this, Aim.Melee(), 5));
			arsenal.Add(new AEvolve(Price.Cheap, this, TTYPE.BEES));
			arsenal.Add(new AEvolve(new Price(1,2), this, TTYPE.MYCO));
			arsenal.Add(new AEvolve(new Price(2,3), this, TTYPE.MART));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}

	public class Beesassin : Unit {
		public Beesassin(Source s, bool template=false){
			NewLabel(TTYPE.BEES, s, false, template);
			BuildAir();
			
			NewHealth(25);
			NewWatch(5);
			AddStat(new Source(this), STAT.COR, 12, false);
			
			arsenal.Add(new AMove(this, Aim.MoveLine(5)));
			arsenal.Add(new ACorrode(Price.Cheap, this, Aim.Melee(), 8));
			arsenal.Add(new ABeesDeathSting(new Price(1,1), this, Aim.Melee(), 15));
			arsenal.Sort();
			
		}		
		public override string Notes () {return "";}
	}

	public class Mycolonist : Unit {
		public Mycolonist(Source s, bool template=false){
			NewLabel(TTYPE.MYCO, s, false, template);
			BuildGround();
			
			NewHealth(40);
			NewWatch(2);
			
			arsenal.Add(new AMove(this, Aim.MovePath(2)));
			Aim corrAim = new Aim (AIMTYPE.ARC, TARGET.TOKEN, TTAR.UNIT, 2);
			arsenal.Add(new ACorrode(new Price(1,0), this, corrAim, 12));
			arsenal.Add(new ADonate(new Price(1,1), this, Aim.Melee(), 6));
			arsenal.Add(new AMycoSeed(new Price(1,1), this));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}

	public class MartianManTrap : Unit {
		public MartianManTrap(Source s, bool template=false){
			NewLabel(TTYPE.MART, s, false, template);
			BuildTrample();
			
			NewHealth(70);
			NewWatch(4);
			
			arsenal.Add(new AMove(this, Aim.MovePath(2)));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}

	public class BlackWinnow : Unit {
		public BlackWinnow(Source s, bool template=false){
			NewLabel(TTYPE.BLAC, s, true, template);
			BuildGround();
			AddKing();
			OnDeath = TTYPE.HSLK;
			
			NewHealth(75);
			NewWatch(3); 
			
			arsenal.Add(new AMove(this, Aim.MovePath(3)));
			arsenal.Add(new ACorrode(Price.Cheap, this, Aim.Melee(), 15));
			arsenal.Add(new ACreate(new Price(0,0), this, TTYPE.LICH));
			Aim webAim = new Aim (AIMTYPE.ARC, TARGET.CELL, CTAR.CREATE, 3);
			arsenal.Add(new ACreate(new Price(1,1), this, TTYPE.WEBB, webAim));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}
	
	public class Web : Obstacle {
		public Web(Source s, bool template=false){
			NewLabel(TTYPE.WEBB, s, false, template);
			BuildSunken();
			AddDest();
		}
		public override string Notes () {return "";}

		public override bool Enter (Cell cell) {
			bool enter = body.Enter(cell);
			if (enter) {DamageCellmates();}
			return enter;
		}

		void DamageCellmates () {
			foreach (Token t in CellMates){
				if (t is Unit) {
					InputBuffer.Submit(new RDamage(new Source(this), (Unit)t, 12));
				}
			}
		}
	}

}
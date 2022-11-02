using HOA.Tokens.Components;
using HOA.Players;
using HOA.Actions;
using HOA.Map;

namespace HOA.Tokens{
	
	public class RevolvingTom : Unit {
		public RevolvingTom(Source s, bool template=false){
			NewLabel(TTYPE.REVO, s, false, template);
			BuildGround();
			
			NewHealth(30);
			NewWatch(4);
			
			arsenal.Add(new AMove(this, Aim.MovePath(3)));
		}		
		public override string Notes () {return "";}
	}

	public class Piecemaker : Unit {
		public Piecemaker(Source s, bool template=false){
			NewLabel(TTYPE.PIEC, s, false, template);
			BuildGround();
			
			NewHealth(35,3);
			NewWatch(1); 
			
			arsenal.Add(new AMove(this, Aim.MovePath(4)));
			arsenal.Add(new AAttack(Price.Cheap, this, Aim.Melee(), 10));
			Aim aperAim = new Aim (AIMTYPE.ARC, TARGET.CELL, CTAR.CREATE, 2);
			arsenal.Add(new ACreate(new Price(1,1), this, TTYPE.APER, aperAim));
			
		}		
		public override string Notes () {return "";}
	}

	public class Reprospector : Unit {
		public Reprospector(Source s, bool template=false){
			NewLabel(TTYPE.REPR, s, false, template);
			BuildGround();
			
			NewHealth(55);
			NewWatch(2);
			
			arsenal.Add(new AMove(this, Aim.MovePath(4)));
		}		
		public override string Notes () {return "";}
	}

	public class OldThreeHands : Unit {
		public OldThreeHands(Source s, bool template=false){
			NewLabel(TTYPE.OLDT, s, true, template);
			BuildGround();
			AddKing();
			OnDeath = TTYPE.HBRA;
			
			NewHealth(85,2);
			watch = new WatchOldt(this, 2);
			
			arsenal.Add(new AMove(this, Aim.MovePath(2)));
			Aim attackAim = new Aim (AIMTYPE.ARC, TARGET.TOKEN, TTAR.UNIT, 3);
			arsenal.Add(new AAttack(Price.Cheap, this, attackAim, 15));
			arsenal.Add(new ACreate(Price.Cheap, this, TTYPE.REVO));
			arsenal.Add(new ACreate(new Price(1,1), this, TTYPE.PIEC));
			arsenal.Add(new ACreate(new Price(1,2), this, TTYPE.REPR));
		}		
		public override string Notes () {return "+1 IN per FP, up to 5";}
	}
	
	public class Aperture : Obstacle {
		public Aperture(Source s, bool template=false){
			NewLabel(TTYPE.APER, s, false, template);
			BuildSunken();
		}
		public override string Notes () {return "";}
	}
}
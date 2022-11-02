using HOA.Tokens.Components;
using HOA.Players;
using HOA.Actions;
using HOA.Map;

namespace HOA.Tokens{
	
	public class Demolitia : Unit {
		public Demolitia(Source s, bool template=false){
			NewLabel(TTYPE.DEMO, s, false, template);
			BuildGround();
			
			health = new HealthDemo(this, 30);
			NewWatch(3);
			
			arsenal.Add(new AMove(this, Aim.MovePath(3)));
			arsenal.Add(new AGrenade(Price.Cheap(), this, 3, 10));
		}
		public override string Notes () {return "+1 DEF per FP, up to 4";}
	}
	
	public class MeinSchutz : Unit {
		public MeinSchutz(Source s, bool template=false){
			NewLabel(TTYPE.MEIN, s, false, template);
			BuildGround();
			
			NewHealth(40);
			NewWatch(4);
			
			arsenal.Add(new AMove(this, Aim.MovePath(5)));
			arsenal.Add(new AAttack(Price.Cheap(), this, Aim.Shoot(2), 12));
			arsenal.Add(new ACreate(new Price(1,1), this, TTYPE.MINE));
		}		
		public override string Notes () {return "";}
	}
	
	public class Panopticannon : Unit {
		public Panopticannon(Source s, bool template=false){
			NewLabel(TTYPE.PANO, s, false, template);
			BuildTrample();
			
			health = new HealthPano(this, 65);
			NewWatch(1);
			
			arsenal.Add(new AMove(this, Aim.MovePath(1)));
		}		
		public override string Notes () {return "+1 DEF per FP, up to 2";}
	}		
	
	public class Decimatrix : Unit {
		public Decimatrix(Source s, bool template=false){
			NewLabel(TTYPE.DECI, s, true);
			BuildTrample();
			AddKing();
			SetOnDeath(TTYPE.HSTE,false);
			
			NewHealth(85,3);
			NewWatch(2);
			
			arsenal.Add(new AMove(this, Aim.MovePath(2)));
			
			arsenal.Add(new AAttack(Price.Cheap(), this, Aim.Shoot(3), 18));
			Aim fireAim = new Aim (AIMTYPE.LINE, TARGET.TOKEN, TTAR.UNITDEST, 2);
			arsenal.Add(new AAttackFir(new Price(1,1), this, fireAim, 12));
			arsenal.Add(new ACreate(Price.Cheap(), this, TTYPE.DEMO));
			arsenal.Add(new ACreate(new Price(1,1), this, TTYPE.MEIN));
			arsenal.Add(new ACreate(new Price(2,2), this, TTYPE.PANO));
		}		
		public override string Notes () {return "";}
	}
	
	public class Mine : Obstacle {
		public Mine(Source s, bool template=false){
			NewLabel(TTYPE.MINE, s, false, template);
			BuildSunken();
			AddDest();
		}
		public override string Notes () {return "";}
	}
}
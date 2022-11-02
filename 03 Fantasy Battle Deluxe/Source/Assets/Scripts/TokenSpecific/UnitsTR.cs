using HOA.Tokens.Components;
using HOA.Players;
using HOA.Actions;
using HOA.Map;
	
namespace HOA.Tokens{
	
	public class Rook : Unit {
		public Rook(Source s, bool template=false){
			NewLabel(TTYPE.ROOK, s, false, template);
			BuildGround();
			AddCorpseless();
			
			NewHealth(30,5);
			NewWatch(3);
			
//			arsenal.Add(new AMove(this, AIM.PATH, 0));
		}		
		public override string Notes () {return "";}
	}
	
	public class Smashbuckler : Unit {
		public Smashbuckler(Source s, bool template=false){
			NewLabel(TTYPE.SMAS, s, false, template);
			BuildGround();
			
			NewHealth(30);
			NewWatch(3);
			
			arsenal.Add(new AMove(this, Aim.MovePath(3)));
			arsenal.Add(new ASmasFlail(Price.Cheap, this, Aim.Melee(), 8));
			arsenal.Add(new ASmasSlam(new Price(1,1), this, Aim.Melee(), 8));
		}		
		
		public override string Notes () {return "";}
	}	
	
	public class Conflagragon : Unit {
		public Conflagragon(Source s, bool template=false){
			NewLabel(TTYPE.CONF, s, false, template);
			BuildAir();
			OnDeath = TTYPE.ASHE;
			
			NewHealth(40);
			NewWatch(4);
			
			arsenal.Add(new AMove(this, Aim.MovePath(6)));
			arsenal.Add(new AAttack(new Price(0,1), this, Aim.Melee(), 12));
			Aim fireAim = new Aim (AIMTYPE.LINE, TARGET.TOKEN, TTAR.UNITDEST, 3);
			arsenal.Add(new AAttackFir(new Price(2,0), this, fireAim, 10));
		}		
		public override string Notes () {return "";}
	}	
	
	public class Ashes : Unit {
		public Ashes(Source s, bool template=false){
			NewLabel(TTYPE.ASHE, s, false, template);
			BuildGround();
			AddRem();
			AddCorpseless();
			
			NewHealth(15);
			NewWatch(2);
			
	//		arsenal.Add(new AMove(this, AIM.PATH, 0));
			
			arsenal.Add(new AsheArise(new Price(0,2), this, TTYPE.CONF));
		}		
		public override string Notes () {return "";}
	}	
	
	public class BatteringRambuchet : Unit {
		public BatteringRambuchet(Source s, bool template=false){
			NewLabel(TTYPE.BATT, s, false, template);
			BuildTrample();
			
			NewHealth(65);
			NewWatch(1);
			
			arsenal.Add(new AMove(this, Aim.MovePath(2)));
			arsenal.Add(new AAttack(Price.Cheap, this, Aim.Melee(), 16));
			
			Aim attackAim = new Aim (AIMTYPE.ARC, TARGET.TOKEN, TTAR.UNIT, 3);
			arsenal.Add(new AAttack(new Price(1,1), this, attackAim, 12));
			
			Aim fireAim = new Aim (AIMTYPE.ARC, TARGET.TOKEN, TTAR.UNITDEST, 3);
			arsenal.Add(new AAttackFir(new Price(1,2), this, fireAim, 16));	
		}		
		public override string Notes () {return "";}
	}	
	
	public class Gargoliath : Unit {
		public Gargoliath(Source s, bool template=false){
			NewLabel(TTYPE.GARG, s, true, template);
			BuildAir();
			AddKing();
			OnDeath = TTYPE.HSTO;

			NewHealth(75);
			NewWatch(3);
			
			arsenal.Add(new AMove(this, Aim.MovePath(4)));
			arsenal.Add(new AAttack(Price.Cheap, this, Aim.Melee(), 18));
			arsenal.Add(new ACreate(Price.Cheap, this, TTYPE.SMAS));
			arsenal.Add(new ACreate(new Price(1,1), this, TTYPE.CONF));
			arsenal.Add(new ACreate(new Price(2,2), this, TTYPE.BATT));
		}		
		public override string Notes () {return "";}
	}	
}
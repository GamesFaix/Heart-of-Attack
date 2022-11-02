using HOA.Tokens.Components;
using HOA.Players;
using HOA.Actions;
using HOA.Map;

namespace HOA.Tokens{
	
	public class GrizzlyElder : Unit {
		public GrizzlyElder(Source s, bool template=false){
			NewLabel(TTYPE.GRIZ, s, false, template);
			BuildGround();
			
			NewHealth(25);
			NewWatch(3);
			
			arsenal.Add(new AMove(this, Aim.MovePath(3)));
			arsenal.Add(new ALeech(Price.Cheap, this, Aim.Melee(), 7));
			arsenal.Add(new ACreate(new Price(1,1), this, TTYPE.TREE));
			
		}		
		public override string Notes () {return "";}
	}
	public class TalonedScout : Unit {
		public TalonedScout(Source s, bool template=false){
			NewLabel(TTYPE.TALO, s, false, template);
			BuildAir();
			
			NewHealth(45);
			NewWatch(4);
			
			arsenal.Add(new AMove(this, Aim.MovePath(6)));
			arsenal.Add(new AAttack(Price.Cheap, this, Aim.Melee(), 10));
		}		
		public override string Notes () {return "";}
	}
	public class Metaterrainean : Unit {
		public Metaterrainean(Source s, bool template=false){
			NewLabel(TTYPE.META, s, false, template);
			BuildTrample();
			OnDeath = TTYPE.ROCK;
			
			NewHealth(50);
			NewWatch(1);
			
			arsenal.Add(new AMove(this, Aim.MovePath(2)));
			arsenal.Add(new AAttack(Price.Cheap, this, Aim.Melee(), 20));
		}		
		public override string Notes () {return "";}
	}
	public class Ultratherium : Unit {
		public Ultratherium(Source s, bool template=false){
			NewLabel(TTYPE.ULTR, s, true, template);
			BuildTrample();
			AddKing();
			OnDeath = TTYPE.HFIR;
			
			NewHealth(80);
			NewWatch(2);
			
			arsenal.Add(new AMove(this, Aim.MovePath(3)));
			arsenal.Add(new AAttack(Price.Cheap, this, Aim.Melee(), 16));
			arsenal.Add(new ACreate(Price.Cheap, this, TTYPE.GRIZ));
			arsenal.Add(new ACreate(new Price(1,1), this, TTYPE.TALO));
			arsenal.Add(new AUltrCreateMeta(new Price(1,2), this, TTYPE.META));
		}		
		public override string Notes () {return "";}
	}	
}

namespace HOA{
	public class BatteringRambuchet : Unit {
		public BatteringRambuchet(Source s, bool template=false){
			NewLabel(TTYPE.BATT, s, false, template);
			BuildTrample();
			
			NewHealth(65);
			NewWatch(1);
			
			arsenal.Add(new AMove(this, Aim.MovePath(2)));
			arsenal.Add(new AAttack("Ram", Price.Cheap, this, Aim.Melee(), 16));
			
			Aim attackAim = new Aim (AIMTYPE.ARC, TARGET.TOKEN, TTAR.UNIT, 3);
			arsenal.Add(new AAttack("Fling", new Price(1,1), this, attackAim, 12));
			
			Aim fireAim = new Aim (AIMTYPE.ARC, TARGET.TOKEN, TTAR.UNITDEST, 3);
			arsenal.Add(new AAttackFir("Cocktail", new Price(1,2), this, fireAim, 16));	
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}	
}
using System.Collections.Generic;

namespace HOA{
	public class BatteringRambuchet : Unit {
		public BatteringRambuchet(Source s, bool template=false){
			NewLabel(EToken.BATT, s, false, template);
			BuildTrample();
			
			NewHealth(65);
			NewWatch(1);
			
			arsenal.Add(new AMovePath(this, 2));
			arsenal.Add(new AAttack("Ram", Price.Cheap, this, Aim.Melee(), 16));

			arsenal.Add(new AAttack("Fling", new Price(1,1), this, Aim.Arc(3), 12));
			
			Aim fireAim = new Aim (EAim.ARC, new List<EClass> {EClass.UNIT, EClass.DEST}, 3);
			arsenal.Add(new AAttackFir("Cocktail", new Price(1,2), this, fireAim, 16));	
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}	
}
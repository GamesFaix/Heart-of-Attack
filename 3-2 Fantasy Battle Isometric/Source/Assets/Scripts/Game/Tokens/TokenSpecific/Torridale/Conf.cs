using System.Collections.Generic;

namespace HOA{
	public class Conflagragon : Unit {
		public Conflagragon(Source s, bool template=false){
			id = new ID(this, EToken.CONF, s, false, template);
			plane = Plane.Air;
			onDeath = EToken.ASHE;
			ScaleMedium();
			NewHealth(30);
			NewWatch(4);
			
			arsenal.Add(new AMovePath(this, 6));
			arsenal.Add(new AAttack("Melee", new Price(0,1), this, Aim.Melee(), 12));
			Aim fireAim = new Aim (ETraj.LINE, new List<EClass> {EClass.UNIT, EClass.DEST}, 3);
			arsenal.Add(new AAttackFir("Firebreathing", new Price(2,0), this, fireAim, 10));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}
}
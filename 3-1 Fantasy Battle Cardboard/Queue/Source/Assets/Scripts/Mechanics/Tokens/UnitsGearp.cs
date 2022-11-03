using UnityEngine;
using System.Collections;

namespace Tokens {

	public class Katandroid : Unit {
		public Katandroid(){
			NewLabel(TTYPE.KATA);
			NewResources();
			NewBody(PLANE.GND);
			NewHealth(25);
			SetOwner(1,false);
			NewClock(5);	
		}
	}
	public class Carapace : Unit {
		public Carapace(){
			NewLabel(TTYPE.CARA);
			NewResources();
			NewBody(PLANE.GND);
			NewHealth(35,3);
			SetOwner(1,false);
			NewClock(4);
		}
	}
	public class Mawth : Unit {
		public Mawth(){
			NewLabel(TTYPE.MAWT);
			NewResources();
			NewBody(PLANE.AIR);
			NewHealth(55);
			SetOwner(1,false);
			NewClock(3);
		}
	}
	public class Kabutomachine : Unit {
		public Kabutomachine(){
			NewLabel(TTYPE.KABU);
			NewResources(3);
			NewBody(PLANE.GND, SPECIAL.KING);
			NewHealth(75);
			SetOwner(1,false);
			NewClock(4);
			SetOnDeath(TTYPE.HSIL, false);
			Roster.Activate(1);
		}
	}
}

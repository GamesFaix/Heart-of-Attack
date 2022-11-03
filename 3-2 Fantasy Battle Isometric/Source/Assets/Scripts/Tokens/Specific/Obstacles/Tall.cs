using UnityEngine; 

namespace HOA.Tokens { 

	public class Mountain : Obstacle {
		public static Token Instantiate (Source source, bool template) {
			return new Mountain (source, template);
		}
		Mountain(Source s, bool template=false){
			ID = new ID(this, EToken.MNTN, s, false, template);
			Plane = Plane.Tall;	
			ScaleTall();
			Neutralize();
		}
		public override string Notes () {return "";}
	}
	
	public class Pyramid : Obstacle {
		public static Token Instantiate (Source source, bool template) {
			return new Pyramid (source, template);
		}
		Pyramid(Source s, bool template=false){
			ID = new ID(this, EToken.PYRA, s, false, template);
			Plane = Plane.Tall;	
			ScaleTall();
			Neutralize();
		}
		public override string Notes () {return "";}
	}
	
	public class Pylon : Obstacle {
		public static Token Instantiate (Source source, bool template) {
			return new Pylon (source, template);
		}
		Pylon(Source s, bool template=false){
			ID = new ID(this, EToken.PYLO, s, false, template);
			Plane = Plane.Tall;	
			ScaleTall();
			Neutralize();
		}
		public override string Notes () {return "";}
	}
}

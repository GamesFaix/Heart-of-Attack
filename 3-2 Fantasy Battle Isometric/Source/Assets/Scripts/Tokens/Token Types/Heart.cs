using UnityEngine; 

namespace HOA { 

	public class Heart : Obstacle {
	
		protected Heart () {
			Plane = Plane.Tall;
		//	Neutralize();
		}

		public override string Notes () {return "";}

	}
}

using UnityEngine; 

namespace HOA { 

	public class DEFBonus: DEF {
		
		public DEFBonus (Unit parent, int normal) : base(parent, normal) {}

		public new DEFBonus DeepCopy (Unit parent) {return new DEFBonus (parent, Normal);}

		public override int Modified () {return 1;}
	}
}

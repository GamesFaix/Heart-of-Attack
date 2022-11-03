using UnityEngine; 

namespace HOA { 

	public class AP : Stat, IDeepCopyUnit<AP> {
		public AP (Unit parent, int max) : base(parent, 0) {
			label = "Energy";
			eStat = EStat.AP;
			eTip = ETip.AP;
			Min = 0;
			Max = max;
		}

		public AP DeepCopy (Unit parent) {return new AP(parent, Max);}
	}
}

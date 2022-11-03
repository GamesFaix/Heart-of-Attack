using UnityEngine; 

namespace HOA { 

	public class DEF : Stat, IDeepCopyUnit<DEF> {
		public DEF (Unit parent, int normal, int max) : base(parent, normal) {
			label = "Defense";
			eStat = EStat.DEF;
			eTip = ETip.DEF;
			Min = 0;
			Max = max;
		}
		public DEF (Unit parent, int normal) : this (parent, normal, 255) {}

		public DEF DeepCopy (Unit parent) {return new DEF (parent, Normal, Max);}
	}
}

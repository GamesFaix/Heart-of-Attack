using UnityEngine; 

namespace HOA { 

	public class IN : Stat, IDeepCopyUnit<IN> {
		public IN (Unit parent, int normal) : base(parent, normal) {
			label = "Initiative";
			eStat = EStat.IN;
			eTip = ETip.IN;
			Min = 1;
			Max = 255;
		}

		public IN DeepCopy (Unit parent) {return new IN(parent, Normal);}
	}
}

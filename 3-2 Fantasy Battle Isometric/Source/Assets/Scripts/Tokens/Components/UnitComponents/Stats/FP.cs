using UnityEngine; 

namespace HOA { 

	public class FP : Stat, IDeepCopyUnit<FP> {
		public FP (Unit parent) : base(parent, 0) {
			label = "Focus";
			eStat = EStat.FP;
			eTip = ETip.FP;
			Min = 0;
			Max = 255;
		}

		public FP DeepCopy (Unit parent) {return new FP (parent);}
	}
}

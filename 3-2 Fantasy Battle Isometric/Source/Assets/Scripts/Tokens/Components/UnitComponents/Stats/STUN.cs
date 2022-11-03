using UnityEngine; 

namespace HOA { 

	public class STUN : Stat, IDeepCopyUnit<STUN> {
		public STUN (Unit parent) : base(parent, 0) {
			label = "Stun";
			eStat = EStat.STUN;
			eTip = ETip.NONE;
			Min = 0;
			Max = 255;
			debuff = true;
		}

		public STUN DeepCopy (Unit parent) {return new STUN (parent);}
	}
}

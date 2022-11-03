using UnityEngine;

namespace HOA {
	
	public class Wallet : IDeepCopyUnit<Wallet> {

		public Stat AP {get; protected set;}
		public Stat FP {get; protected set;}

		protected Unit parent;
		
		public Wallet () {}
		
		public Wallet (Unit p, int n=2) {
			parent = p;
			AP = new AP(parent, n);
			FP = new FP(parent);
		}

		public Wallet DeepCopy (Unit parent) {return new Wallet (parent, AP.Max);}

		public void FillAP (bool log=true) {
			if (AP.Max > AP) {
				AP.Add(new Source(parent), (AP.Max-AP), log);
				if (log) {GameLog.Out(parent+" AP filled.");}
			}
		}

		public bool CanAfford (Price Price) {
			if (AP >= Price.AP
			&& FP >= Price.FP
			&& !Price.Other) {
				return true;
			}
			return false;
		}
		
		public void Charge (Price Price) {
			AP.Add (new Source(parent), 0-Price.AP, false);
			FP.Add (new Source(parent), 0-Price.FP, false);
		}
		
		public void Refund (Price Price) {
			AP.Add (new Source(parent), Price.AP, false);
			FP.Add (new Source(parent), Price.FP, false);
		}

		public virtual void Display (Panel p, float iconSize) {
			AP.Display (new Panel(p.Box(iconSize*2 +5), p.LineH, p.s), iconSize);
			FP.Display (new Panel(p.Box(iconSize*2 +5), p.LineH, p.s), iconSize);
		}
	}
}

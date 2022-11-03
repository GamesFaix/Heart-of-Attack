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
				if (log) {GameLog.Out(parent+" Energy filled.");}
			}
		}

		public bool CanAfford (Price Price) {return ( (AP>=Price.E && FP>=Price.F) ? true : false );}

		public void Charge (Price Price) {
			AP.Add (new Source(parent), 0-Price.E, false);
			FP.Add (new Source(parent), 0-Price.F, false);
		}
		
		public void Refund (Price Price) {
			AP.Add (new Source(parent), Price.E, false);
			FP.Add (new Source(parent), Price.F, false);
		}

		public virtual void Display (Panel p, float iconSize) {
			AP.Display (new Panel(p.Box(iconSize*2 +5), p.LineH, p.s), iconSize);
			FP.Display (new Panel(p.Box(iconSize*2 +5), p.LineH, p.s), iconSize);
		}
	}
}

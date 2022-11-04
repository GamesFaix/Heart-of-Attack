using UnityEngine;

namespace HOA {
	
	public class Wallet : TokenComponent, IDeepCopyUnit<Wallet>, IInspectable {

		public Stat AP {get; protected set;}
		public Stat FP {get; protected set;}

		public Wallet (Unit parent, int n=2) : base (parent) {
			AP = Stat.Energy(parent, n);
			FP = Stat.Focus(parent);
		}

		public Wallet DeepCopy (Unit parent) {return new Wallet (parent, AP.Max);}

		public void FillAP (bool log=true) {
			if (AP.Max > AP) {
				AP.Add(new Source(Parent), (AP.Max-AP), log);
				if (log) {GameLog.Out(Parent+" Energy filled.");}
			}
		}

		public bool CanAfford (Price Price) {return ( (AP>=Price.Energy && FP>=Price.Focus) ? true : false );}

		public void Charge (Price Price) {
			AP.Add (new Source(Parent), 0-Price.Energy, false);
			FP.Add (new Source(Parent), 0-Price.Focus, false);
		}
		
		public void Refund (Price Price) {
			AP.Add (new Source(Parent), Price.Energy, false);
			FP.Add (new Source(Parent), Price.Focus, false);
		}

        public override void Draw(Panel panel) { InspectorInfo.Wallet(this, panel); }
        public override string ToString()
        {
            return Parent + "'s Wallet";
        }
	}
}

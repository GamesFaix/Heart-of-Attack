using UnityEngine;

namespace HOA { 

	public class WalletIN : Wallet, IInspectable {
		
		public WalletIN (Unit parent, int ap=2) : base (parent, ap) 
        {
            FP = Stat.FocusAddsInitiative(parent);
        }

		public new WalletIN DeepCopy (Unit parent) {return new WalletIN (parent, AP.Max);}

        public override void Draw(Panel p) { InspectorInfo.WalletIN(this, p); }
	}
}

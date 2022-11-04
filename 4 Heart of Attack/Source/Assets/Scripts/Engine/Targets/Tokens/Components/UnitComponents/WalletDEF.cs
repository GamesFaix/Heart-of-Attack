using UnityEngine; 

namespace HOA {

    public class WalletDEF : Wallet, IInspectable
    {

        public int cap;

        public WalletDEF(Unit parent, int ap = 2, int cap = 4) : base (parent, ap)
        {
            this.cap = cap;
            FP = Stat.FocusAddsDefense(parent, cap);
        }

        public new WalletDEF DeepCopy(Unit parent) { return new WalletDEF(parent, AP.Max, cap); }

        public override void Draw(Panel p) { InspectorInfo.WalletDEF(this, p); }
    }
}

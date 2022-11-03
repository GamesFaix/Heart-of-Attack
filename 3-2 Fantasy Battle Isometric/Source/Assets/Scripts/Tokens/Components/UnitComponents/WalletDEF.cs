using UnityEngine; 

namespace HOA { 

	public class WalletDEF : Wallet {
		
		int cap;
		
		public WalletDEF (Unit parent, int ap=2, int cap=4) {
			this.parent = parent;
			this.cap = cap;
			AP = new AP(parent, ap);
			FP = new FPAddsDEF(parent, cap);
		}

		public new WalletDEF DeepCopy (Unit parent) {return new WalletDEF(parent, AP.Max, cap);}

		public override void Display (Panel p, float iconSize) {
			AP.Display (new Panel(p.Box(iconSize*2 +5), p.LineH, p.s), iconSize);
			FP.Display (new Panel(p.Box(iconSize*2 +5), p.LineH, p.s), iconSize);
			
			p.NudgeX(); p.NudgeX();
			iconSize = 20;
			GUI.Box(p.Box(iconSize), Icons.Stats.defense, p.s);
			GUI.Label(p.Box(40), "+1 per ");
			GUI.Box(p.Box(iconSize), Icons.Stats.focus, p.s);
			p.NudgeX();
			GUI.Label(p.Box(60), "(Max +"+cap+")");
		}
	}
}

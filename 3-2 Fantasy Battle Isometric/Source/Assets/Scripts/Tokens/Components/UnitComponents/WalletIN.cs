using UnityEngine;

namespace HOA { 

	public class WalletIN : Wallet {
		
		public WalletIN (Unit parent, int ap=2) {
			this.parent = parent;
			AP = new AP(parent, ap);
			FP = new FPAddsIN(parent);
		}

		public new WalletIN DeepCopy (Unit parent) {return new WalletIN (parent, AP.Max);}
		
		public override void Display (Panel p, float iconSize) {
			AP.Display (new Panel(p.Box(iconSize*2 +5), p.LineH, p.s), iconSize);
			FP.Display (new Panel(p.Box(iconSize*2 +5), p.LineH, p.s), iconSize);
			
			p.NudgeX(); p.NudgeX();
			iconSize = 20;
			GUI.Box(p.Box(iconSize), Icons.Stat(EStat.IN), p.s);
			GUI.Label(p.Box(40), "+1 per ");
			GUI.Box(p.Box(iconSize), Icons.Stat(EStat.FP), p.s);
		}
	}
}

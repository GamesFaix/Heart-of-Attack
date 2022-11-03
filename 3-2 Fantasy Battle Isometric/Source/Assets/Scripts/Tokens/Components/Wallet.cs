using UnityEngine;

namespace HOA {
	
	public class Wallet {

		public Stat AP {get; protected set;}
		public Stat FP {get; protected set;}

		protected Unit parent;
		
		public Wallet () {}
		
		public Wallet (Unit p, byte n=2) {
			parent = p;
			AP = Stat.AP(parent, n);
			FP = Stat.FP(parent);
		}

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

	public class INWallet : Wallet{
		
		public INWallet (Unit parent, byte ap=2) {
			this.parent = parent;
			AP = Stat.AP(parent, ap);
			FP = Stat.FPaddsIN(parent);
		}

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

	public class DEFWallet : Wallet{

		byte cap;

		public DEFWallet (Unit parent, byte ap=2, byte cap=4) {
			this.parent = parent;
			this.cap = cap;
			AP = Stat.AP(parent, ap);
			FP = Stat.FPaddsDEF(parent, cap);
		}
		public override void Display (Panel p, float iconSize) {
			AP.Display (new Panel(p.Box(iconSize*2 +5), p.LineH, p.s), iconSize);
			FP.Display (new Panel(p.Box(iconSize*2 +5), p.LineH, p.s), iconSize);
			
			p.NudgeX(); p.NudgeX();
			iconSize = 20;
			GUI.Box(p.Box(iconSize), Icons.Stat(EStat.DEF), p.s);
			GUI.Label(p.Box(40), "+1 per ");
			GUI.Box(p.Box(iconSize), Icons.Stat(EStat.FP), p.s);
			p.NudgeX();
			GUI.Label(p.Box(60), "(Max +"+cap+")");
		}
	}
}

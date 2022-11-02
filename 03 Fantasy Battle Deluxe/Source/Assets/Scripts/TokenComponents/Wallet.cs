using HOA.Players;
using HOA.Actions;

namespace HOA.Tokens.Components {
	
	public class Wallet {
		protected int maxAp;
		protected int ap;
		protected int fp;
		protected Unit parent;
		
		public Wallet () {}
		
		public Wallet (Unit p, int n=2) {
			parent = p;
			SetMaxAP(new Source(),n,false);
			SetAP(new Source(),0,false);
			SetFP(new Source(),0,false);
		}

		public int AP () {return ap;}
		public int MaxAP () {return maxAp;}
		public int FP () {return fp;}

		public string APString () {return "("+ap+"/"+maxAp+")";}
		public string FPString () {return "("+fp+")";}


		public int SetAP (Source s, int n, bool log=true) {
			ap = Clamp(n); 
			if (log) {GameLog.Out(s+" set "+parent+"'s AP to "+ap+".");}
			return ap;
		}
		public int SetMaxAP (Source s, int n, bool log=true) {
			maxAp = Clamp(n);
			if (log) {GameLog.Out(s+" set "+parent+"'s max AP to "+maxAp+".");}
			return maxAp;
		}
		public int AddAP (Source s, int n, bool log=true) {
			ap = Clamp(ap+n);
			string sign ="+"+n;
			if (n<0) {sign = ""+n;}
			if (log) {GameLog.Out(s+": "+parent+" "+sign+"AP. AP="+ap);}
			return ap;
		}
		public void FillAP (bool log=true) {
			ap=maxAp;
			if (log) {GameLog.Out(parent+" AP filled.");}
		}

		public virtual int SetFP (Source s, int n, bool log=true) {
			fp = Clamp(n);
			if (log) {GameLog.Out(s+" set "+parent+"'s FP to "+fp+".");}
			return fp;
		}
		public virtual int AddFP (Source s, int n, bool log=true) {
			fp = Clamp(fp+n);
			string sign ="+"+n;
			if (n<0) {sign = ""+n;}
			if (log) {GameLog.Out(s+": "+parent+" "+sign+"FP. FP="+fp);}
			return fp;
		}

		protected int Clamp (int x) {
			if (x<0){x=0;}
			return x;
		}
		
		public bool CanAfford (Price price) {
			if (ap >= price.AP()
			&& fp >= price.FP() 
			&& !price.Other()) {
				return true;
			}
			return false;
		}
		
		public void Charge (Price price) {
			AddAP (new Source(parent), 0-price.AP(), false);
			AddFP (new Source(parent), 0-price.FP(), false);
		}
		
		public void Refund (Price price) {
			AddAP (new Source(parent), price.AP(), false);
			AddFP (new Source(parent), price.FP(), false);
		}
	}
}

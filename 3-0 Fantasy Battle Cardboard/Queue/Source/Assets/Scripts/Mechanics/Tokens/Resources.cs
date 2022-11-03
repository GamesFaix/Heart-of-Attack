using UnityEngine;
using System.Collections;

namespace Tokens {
	public class Resources {
		int maxAp;
		int ap;
		int fp;
		Unit parent;

		public Resources(Unit p, int n=2){
			parent = p;
			SetMaxAP(n,false);
			SetAP(0,false);
			SetFP(0,false);
		}

		public int AP(){return ap;}
		public int MaxAP(){return maxAp;}
		public int FP(){return fp;}

		public string APString(){return "("+ap+"/"+maxAp+")";}
		public string FPString(){return "("+fp+")";}


		public int SetAP (int n, bool log=true){
			ap = Clamp(n); 
			if (log) {GameLog.Out(parent+"'s AP set to "+ap+".");}
			return ap;
		}
		public int SetMaxAP(int n, bool log=true){
			maxAp=Clamp(n);
			if (log) {GameLog.Out(parent+"'s max AP set to "+maxAp+".");}
			return maxAp;
		}
		public int AddAP (int n, bool log=true){
			ap = Clamp(ap+n);
			string sign ="+"+n;
			if (n<0) {sign = ""+n;}
			if (log) {GameLog.Out(parent+" "+sign+"AP. AP="+ap);}
			return ap;
		}
		public void FillAP(bool log=true){
			ap=maxAp;
			if (log) {GameLog.Out(parent+" AP filled.");}
		}

		public int SetFP (int n, bool log=true){
			fp = Clamp(n);
			if (log) {GameLog.Out(parent+"'s FP set to "+fp+".");}
			return fp;
		}
		public int AddFP (int n, bool log=true){
			fp = Clamp(fp+n);
			string sign ="+"+n;
			if (n<0) {sign = ""+n;}
			if (log) {GameLog.Out(parent+" "+sign+"FP. FP="+fp);}
			return fp;
		}

		int Clamp(int x){
			if (x<0){x=0;}
			return x;
		}



	}
}

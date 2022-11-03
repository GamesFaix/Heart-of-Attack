using UnityEngine; 

namespace HOA { 

	public class FPAddsDEF: FP, IDeepCopyUnit<FPAddsDEF> {
		//+1 DEF per Focus (with cap)
		
		int cap;
		
		public FPAddsDEF (Unit parent, int cap) : base(parent) {
			this.cap = cap;
		}

		public new FPAddsDEF DeepCopy (Unit parent) {return new FPAddsDEF (parent, cap);}
		
		public override int Add (Source source, int n, bool log=true) {
			sbyte defChange = 0;
			if (n > 0) {
				for (sbyte i=1; i<=n; i++) {
					if (Current+i <= cap) {defChange++;}
				}
			}
			if (n < 0) {
				for (int i=1; i<=(-n); i++) {
					if (Current-i < cap) {defChange--;}
				}
			}
			parent.AddStat(source, EStat.DEF, defChange, log);
			
			Current += n;
			Clamp();
			string sign = Sign(n);
			if (log) {GameLog.Out(source+": "+parent+" "+sign+n+label+". "+label+" = "+Current);}
			return Current;
		}
	}
}

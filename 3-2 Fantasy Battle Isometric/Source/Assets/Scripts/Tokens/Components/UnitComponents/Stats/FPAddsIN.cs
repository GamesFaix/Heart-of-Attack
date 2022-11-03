using UnityEngine; 

namespace HOA { 

	public class FPAddsIN: FP, IDeepCopyUnit<FPAddsIN> {
		//+1 IN per Focus (no cap)
		public FPAddsIN (Unit parent) : base(parent) {}

		public new FPAddsIN DeepCopy (Unit parent) {return new FPAddsIN(parent);}

		public override int Add (Source source, int n, bool log=true) {
			Current += n;
			parent.AddStat(source, EStat.IN, n, log);
			Clamp();
			string sign = Sign(n);
			if (log) {GameLog.Out(source+": "+parent+" "+sign+n+label+". "+label+" = "+Current);}
			return Current;
		}
	}
}

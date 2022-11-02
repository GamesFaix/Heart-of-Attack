
namespace HOA.Actions {

	public class Price {
		int ap;
		int fp;
		bool other;
		
		public Price (int a, int f, bool o=false) {
			ap = a;
			fp = f;
			other = o;
		}
		
		public int AP () {return ap;}
		public int FP () {return fp;}
		public bool Other () {return other;}
		
		public override string ToString () {
			string s = "("+ap+"AP / "+fp+"FP)";
			if (other) {s += "*";}
			return s;
		}
		
		public static Price Cheap () {return new Price(1,0);}
	
	
	}
}


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
		
		public int AP {get {return ap;} }
		public int FP {get {return fp;} }
		public bool Other {get {return other;} }
		
		public override string ToString () {
			string s = "("+ap+"AP / "+fp+"FP)";
			if (other) {s += "*";}
			return s;
		}
		
		public static Price Cheap {get {return new Price(1,0);} }
	
	
	}
}

using UnityEngine;

namespace HOA{

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
		public int Total {get {return ap+fp;} }

		public bool Other {get {return other;} }
		
		public override string ToString () {
			string s = "("+ap+"AP / "+fp+"FP)";
			if (other) {s += "*";}
			return s;
		}
		
		public void Draw (Panel p) {
			float iconSize = p.LineH;
		
			Rect box = p.Box(iconSize);
			GUI.Box(box, Icons.Stat(EStat.AP), p.s);
			box = p.Box(iconSize);
			GUI.Label(box, ap+"", p.s);
			p.NudgeX();

			box = p.Box(iconSize);
			GUI.Box(box, Icons.Stat(EStat.FP), p.s);
			box = p.Box(iconSize);
			GUI.Label(box, fp+"", p.s);
		}

		public static Price Free {get {return new Price(0,0);} }
		public static Price Cheap {get {return new Price(1,0);} }
	
	
	}
}

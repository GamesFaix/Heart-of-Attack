namespace HOA.Actions {
	public enum AIMTYPE {CELLMATE, NEIGHBOR, PATH, LINE, ARC, FREE, SELF, GLOBAL, OTHER}
	public enum TARGET {TOKEN, CELL, SELF, NONE}
	public enum CTAR {MOVE, CREATE, ATTACK, NA}
	public enum TTAR {UNIT, UNITDEST, DEST, REM, DESTREM, NA}
	
	public class Aim {
		int range;
		int minRange;
		AIMTYPE aimType;
		TARGET target;
		CTAR ctar;
		TTAR ttar;
		
		public Aim (AIMTYPE a, TARGET t, CTAR ct, int r=0, int rMin=0) {
			aimType = a;
			target = t;
			ttar = TTAR.NA;
			ctar = ct;
			range = r;
			minRange = rMin;	
			
		}
		
		
		
		public Aim (AIMTYPE a, TARGET t, TTAR tt, int r=0, int rMin=0) {
			aimType = a;
			target = t;
			ttar = tt;
			ctar = CTAR.NA;
			range = r;
			minRange = rMin;
		}
		
		public AIMTYPE AimType () {return aimType;}
		public TARGET Target () {return target;}
		public TTAR TTar () {return ttar;}
		public CTAR CTar () {return ctar;}
		public int Range () {return range;}
		public int MinRange () {return minRange;}
		
		public override string ToString () {
			string s = "[";
			if (AimTypeString() != "") {s += AimTypeString();}
			if (RangeString() != "") {s += " "+RangeString();}
			if (TargetString() != "") {s += " "+TargetString();}
			s += "]";
			return s;
		}
		string AimTypeString () {
			switch (aimType) {
				case AIMTYPE.CELLMATE: return "Cellmate";
				case AIMTYPE.NEIGHBOR: return "Neighbor";
				case AIMTYPE.PATH: return "Path";
				case AIMTYPE.LINE: return "Line";
				case AIMTYPE.ARC: return "Arc";
				case AIMTYPE.FREE: return "Free";
				case AIMTYPE.SELF: return "Self";
				case AIMTYPE.GLOBAL: return "Global";
				default: return "Other";
			}
		}
		string RangeString () {
			if (aimType == AIMTYPE.PATH || aimType == AIMTYPE.LINE) {return range+"";}	
			else if (aimType == AIMTYPE.ARC) {
				if (minRange > 0) {return minRange+"-"+range;}
				return range+"";
			}
			return "";
		}
		string TargetString () {	
			if (target == TARGET.CELL) {return "Cell";}
			if (ttar == TTAR.UNIT) {return "Unit";}
			if (ttar == TTAR.UNITDEST) {return "Unit or Destructible";}
			if (ttar == TTAR.DEST) {return "Destructible (non-Remains)";}
			if (ttar == TTAR.DESTREM) {return "Destructible";}
			if (ttar == TTAR.REM) {return "Remains";}
			return "";
		}
		
		public static Aim MovePath (int r) {
			return new Aim (AIMTYPE.PATH, TARGET.CELL, CTAR.MOVE, r);	
		}
		public static Aim MoveLine (int r) {
			return new Aim (AIMTYPE.LINE, TARGET.CELL, CTAR.MOVE, r);	
		}
		
		public static Aim Create () {
			return new Aim (AIMTYPE.NEIGHBOR, TARGET.CELL, CTAR.CREATE);
		}
		
		public static Aim Melee () {
			return new Aim (AIMTYPE.NEIGHBOR, TARGET.TOKEN, TTAR.UNIT);
		}
		
		public static Aim Shoot (int n) {
			return new Aim (AIMTYPE.LINE, TARGET.TOKEN, TTAR.UNIT, n);
		}
		
		public static Aim Powerup () {
			return new Aim (AIMTYPE.SELF, TARGET.SELF, TTAR.NA);
		}
		
	}
}

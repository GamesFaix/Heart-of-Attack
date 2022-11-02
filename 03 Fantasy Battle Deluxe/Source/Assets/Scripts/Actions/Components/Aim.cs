using UnityEngine;

namespace HOA.Actions {
	public enum AIMTYPE {CELLMATE, NEIGHBOR, PATH, LINE, ARC, FREE, SELF, GLOBAL, OTHER}
	public enum TARGET {TOKEN, CELL, SELF, NONE}
	public enum CTAR {MOVE, CREATE, ATTACK, NA}
	public enum TTAR {UNIT, UNITDEST, DEST, REM, DESTREM, NA}
	
	public class Aim {
		AIMTYPE aimType;
		public AIMTYPE AimType {get {return aimType;} }
		TARGET target;
		public TARGET Target {get {return target;} }
		CTAR ctar;
		public CTAR CTar {get {return ctar;} }
		TTAR ttar;
		public TTAR TTar {get {return ttar;} }

		int range;
		public int Range {get {return range;} }
		int minRange;
		public int MinRange {get {return minRange;} }

		bool teamOnly = false;
		public bool TeamOnly { 
			get {return teamOnly;} 
			set {teamOnly = value;}
		}
		bool enemyOnly = false;
		public bool EnemyOnly { 
			get {return enemyOnly;} 
			set {enemyOnly = value;}
		}
		bool includeSelf = true;
		public bool IncludeSelf { 
			get {return includeSelf;} 
			set {includeSelf = value;}
		}
		bool noKings = false;
		public bool NoKings {
			get {return noKings;}
			set {noKings = value;}
		}


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

		public override string ToString () {
			string s = "[";
			if (AimTypeString != "") {s += AimTypeString;}
			if (RangeString != "") {s += " "+RangeString;}
			if (TargetString != "") {s += " "+TargetString;}
			s += "]";
			return s;
		}
		string AimTypeString {
			get {
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
		}

		string RangeString {
			get {
				if (aimType == AIMTYPE.PATH || aimType == AIMTYPE.LINE) {return range+"";}	
				else if (aimType == AIMTYPE.ARC) {
					if (minRange > 0) {return minRange+"-"+range;}
					return range+"";
				}
				return "";
			}
		}
		string TargetString {
			get {
				if (target == TARGET.CELL) {return "Cell";}
				if (ttar == TTAR.UNIT) {return "Unit";}
				if (ttar == TTAR.UNITDEST) {return "Unit or Destructible";}
				if (ttar == TTAR.DEST) {return "Destructible (non-Remains)";}
				if (ttar == TTAR.DESTREM) {return "Destructible";}
				if (ttar == TTAR.REM) {return "Remains";}
				return "";
			}
		}
		Texture2D[] TargetIcon {
			get {
				if (target == TARGET.CELL) {
					return new Texture2D[] {Icons.CELL()};
				}
				if (ttar != TTAR.NA) {return Icons.TTar(ttar);}
				return default(Texture2D[]);
			}
		}


		public void Draw (Panel p) {
			float iconSize = p.LineH;

			Rect iconBox = p.Box(iconSize);
			if (Icons.AimType(aimType) != default(Texture2D)) {
				GUI.Box(iconBox, Icons.AimType(AimType), p.s);
				if (GUIInspector.ShiftMouseOver(iconBox)) {
					GUIInspector.Tip = GUIToolTips.AimType(aimType);
				}
			}
			if (RangeString != "") {
				GUI.Label(p.Box(iconSize), RangeString, p.s);
			}
			p.NudgeX();
			if (TargetIcon != default(Texture2D[])) {
				foreach (Texture2D tex in TargetIcon) {
					GUI.Box(p.Box(iconSize), tex, p.s);
				}
			}
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

		public static Aim Arc (int n) {
			return new Aim (AIMTYPE.ARC, TARGET.TOKEN, TTAR.UNIT, n);
		}

		public static Aim Powerup () {
			return new Aim (AIMTYPE.SELF, TARGET.SELF, TTAR.NA);
		}
		
	}
}

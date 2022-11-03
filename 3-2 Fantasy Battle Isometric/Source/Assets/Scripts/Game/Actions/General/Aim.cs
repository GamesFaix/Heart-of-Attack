using UnityEngine;
using System.Collections.Generic;
namespace HOA {

	public class Aim {

		ETraj aimType;
		public ETraj AimType {get {return aimType;} }

		List<EClass> targetClass = new List<EClass>();
		public List<EClass> TargetClass {get {return targetClass;} }

		EPurp purpose;
		public EPurp Purpose {get {return purpose;} }

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

		public Aim (ETraj a, EClass tc, int r=0, int rMin=0) {
			aimType = a;
			targetClass = new List<EClass> {tc};
			purpose = EPurp.ATTACK;
			range = r;
			minRange = rMin;
		}

		public Aim (ETraj a, List<EClass> tc, int r=0, int rMin=0) {
			aimType = a;
			targetClass = tc;
			purpose = EPurp.ATTACK;
			range = r;
			minRange = rMin;
		}

		public Aim (ETraj a, EClass tc, EPurp p, int r=0, int rMin=0) {
			aimType = a;
			targetClass = new List<EClass> {tc};
			purpose = p;
			range = r;
			minRange = rMin;
		}
		
		public Aim (ETraj a, List<EClass> tc, EPurp p, int r=0, int rMin=0) {
			aimType = a;
			targetClass = tc;
			purpose = p;
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
					case ETraj.CELLMATE: return "Cellmate";
					case ETraj.NEIGHBOR: return "Neighbor";
					case ETraj.PATH: return "Path";
					case ETraj.LINE: return "Line";
					case ETraj.ARC: return "Arc";
					case ETraj.FREE: return "Free";
					case ETraj.SELF: return "Self";
					case ETraj.GLOBAL: return "Global";
					default: return "Other";
				}
			}
		}

		string RangeString {
			get {
				if (aimType == ETraj.PATH || aimType == ETraj.LINE) {return range+"";}	
				else if (aimType == ETraj.ARC) {
					if (minRange > 0) {return minRange+"-"+range;}
					return range+"";
				}
				return "";
			}
		}
		string TargetString {
			get {
				if (targetClass.Contains(EClass.CELL)) {return "Cell";}

				else if (targetClass.Contains(EClass.UNIT) 
					&& targetClass.Contains(EClass.DEST)) {
					return "Unit or Destructible";
				}
				else if (targetClass.Contains(EClass.UNIT)) {return "Unit";}
				else if (targetClass.Contains(EClass.DEST) 
				    && !targetClass.Contains(EClass.REM)) {
					return "Destructible (non-Remains)";
				}
				else if (targetClass.Contains(EClass.DEST)
				    && targetClass.Contains(EClass.REM)) {
					return "Destructible";
				}
				else if (targetClass.Contains(EClass.REM)) {return "Remains";}
				return "";
			}
		}
		Texture2D[] TargetIcon {
			get {
				Texture2D[] texs = new Texture2D[targetClass.Count];
				for (int i=0; i<texs.Length; i++) {
					texs[i] = Icons.Class(targetClass[i]);
				}
				return texs;
			}
		}


		public void Draw (Panel p) {
			float iconSize = p.LineH;

			Rect iconBox = p.Box(iconSize);
			if (Icons.Aim(aimType) != default(Texture2D)) {
				GUI.Box(iconBox, Icons.Aim(aimType), p.s);
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

		public static Aim Self {
			get {return new Aim (ETraj.SELF, new List<EClass>());}
		}

		///
		public static Aim MovePath (int r) {
			return new Aim (ETraj.PATH, EClass.CELL, EPurp.MOVE, r);	
		}
		public static Aim MoveLine (int r) {
			return new Aim (ETraj.LINE, EClass.CELL, EPurp.MOVE, r);	
		}
		public static Aim MoveArc (int r, int mr=0) {
			return new Aim (ETraj.ARC, EClass.CELL, EPurp.MOVE, r, mr);
		}

		public static Aim Create () {
			return new Aim (ETraj.NEIGHBOR, EClass.CELL, EPurp.CREATE);
		}
		public static Aim CreateArc (int r, int mr=0) {
			return new Aim (ETraj.ARC, EClass.CELL, EPurp.CREATE, r, mr);
		}
		
		public static Aim Melee () {
			return new Aim (ETraj.NEIGHBOR, EClass.UNIT);
		}
		public static Aim Shoot (int n) {
			return new Aim (ETraj.LINE, EClass.UNIT, n);
		}
		public static Aim Arc (int r, int mr=0) {
			return new Aim (ETraj.ARC, EClass.UNIT, r, mr);
		}

	}
}

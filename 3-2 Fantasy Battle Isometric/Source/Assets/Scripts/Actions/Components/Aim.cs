using UnityEngine;
using System.Collections.Generic;
namespace HOA {
	public enum ETraj/*ectory*/ {CELLMATE, NEIGHBOR, PATH, LINE, ARC, FREE, SELF, GLOBAL, OTHER}
	public enum EPurp/*ose*/ {MOVE, CREATE, ATTACK, OTHER}

	public class Aim {

		ETraj trajectory;
		public ETraj Trajectory {get {return trajectory;} }

		Type type;
		public Type Type {get {return type;} }

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

		public Aim (ETraj a) {
			trajectory = a;
			type = default(Type);
			purpose = EPurp.ATTACK;
			range = 0;
			minRange = 0;
		}

		public Aim (ETraj a, EType tc, int r=0, int rMin=0) {
			trajectory = a;
			type = new Type(tc);
			purpose = EPurp.ATTACK;
			range = r;
			minRange = rMin;
		}

		public Aim (ETraj a, Type t, int r=0, int rMin=0) {
			trajectory = a;
			type = t;
			purpose = EPurp.ATTACK;
			range = r;
			minRange = rMin;
		}

		public Aim (ETraj a, EType tc, EPurp p, int r=0, int rMin=0) {
			trajectory = a;
			type = new Type(tc);
			purpose = p;
			range = r;
			minRange = rMin;
		}

		public Aim (ETraj a, Type t, EPurp p, int r=0, int rMin=0) {
			trajectory = a;
			type = t;
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
				switch (trajectory) {
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
				if (trajectory == ETraj.PATH || trajectory == ETraj.LINE) {return range+"";}	
				else if (trajectory == ETraj.ARC) {
					if (minRange > 0) {return minRange+"-"+range;}
					return range+"";
				}
				return "";
			}
		}
		string TargetString {
			get {
				if (type.Is(EType.CELL)) {return "Cell";}

				else if (type.Is(EType.UNIT) 
					&& type.Is(EType.DEST)) {
					return "Unit or Destructible";
				}
				else if (type.Is(EType.UNIT)) {return "Unit";}
				else if (type.Is(EType.DEST) 
				    && !type.Is(EType.REM)) {
					return "Destructible (non-Remains)";
				}
				else if (type.Is(EType.DEST)
				    && type.Is(EType.REM)) {
					return "Destructible";
				}
				else if (type.Is(EType.REM)) {return "Remains";}
				return "";
			}
		}
		Texture2D[] TargetIcon {
			get {
				if (type != default(Type)) {
					Texture2D[] texs = new Texture2D[type.Count];
					for (int i=0; i<texs.Length; i++) {
						texs[i] = Icons.Type(type[i]);
					}
					return texs;
				}
				else return new Texture2D[0];
			}
		}


		public void Draw (Panel p) {
			float iconSize = p.LineH;

			Rect iconBox = p.Box(iconSize);
			if (Icons.Traj(trajectory) != default(Texture2D)) {
				GUI.Box(iconBox, Icons.Traj(trajectory), p.s);
				if (GUIInspector.ShiftMouseOver(iconBox)) {
					GUIInspector.Tip = GUIToolTips.Trajectory(trajectory);
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

		public static Aim Self () {return new Aim (ETraj.SELF);}

		///
		public static Aim MovePath (int r) {
			return new Aim (ETraj.PATH, EType.CELL, EPurp.MOVE, r);	
		}
		public static Aim MoveLine (int r) {
			return new Aim (ETraj.LINE, EType.CELL, EPurp.MOVE, r);	
		}
		public static Aim MoveArc (int r, int mr=0) {
			return new Aim (ETraj.ARC, EType.CELL, EPurp.MOVE, r, mr);
		}

		public static Aim Create () {
			return new Aim (ETraj.NEIGHBOR, EType.CELL, EPurp.CREATE);
		}
		public static Aim CreateArc (int r, int mr=0) {
			return new Aim (ETraj.ARC, EType.CELL, EPurp.CREATE, r, mr);
		}
		
		public static Aim Melee () {
			return new Aim (ETraj.NEIGHBOR, EType.UNIT);
		}
		public static Aim Shoot (int n) {
			return new Aim (ETraj.LINE, EType.UNIT, n);
		}
		public static Aim Arc (int r, int mr=0) {
			return new Aim (ETraj.ARC, EType.UNIT, r, mr);
		}

	}
}

using UnityEngine;
using System.Collections.Generic;
namespace HOA {
	public enum ETraj {CELLMATE, NEIGHBOR, PATH, LINE, ARC, FREE, SELF, GLOBAL, RADIAL, OTHER}
	public enum EPurp {MOVE, CREATE, ATTACK, OTHER}

	public class Aim {

		public ETraj Trajectory {get; private set;}
		public Special Special {get; private set;}
		public EPurp Purpose {get; private set;}

		public int Range {get; set;}
		public int MinRange {get; set;}

		public bool TeamOnly {get; set;}
		public bool EnemyOnly {get; set;}
		public bool IncludeSelf {get; set;} 
		public bool NoKings {get; set;}

		public Aim (ETraj a, EType tc, EPurp p, int r=0, int rMin=0) {
			Trajectory = a;
			Special = new Special(tc);
			Purpose = p;
			Range = r;
			MinRange = rMin;
		}
		public Aim (ETraj a, Special t, EPurp p, int r=0, int rMin=0) {
			Trajectory = a;
			Special = t;
			Purpose = p;
			Range = r;
			MinRange = rMin;
		}
		public Aim (ETraj a, EType tc, int r=0, int rMin=0) : this (a, tc, EPurp.ATTACK, r, rMin) {}
		public Aim (ETraj a, Special t, int r=0, int rMin=0) : this (a, t, EPurp.ATTACK, r, rMin) {}
		public Aim (ETraj a) : this (a, null, EPurp.ATTACK, 0, 0) {}

		string RangeString {
			get {
				if (Trajectory == ETraj.PATH || Trajectory == ETraj.LINE) {return Range+"";}	
				else if (Trajectory == ETraj.ARC) {
					if (MinRange > 0) {return MinRange+"-"+Range;}
					return Range+"";
				}
				return "";
			}
		}

		Texture2D[] TargetIcon {
			get {
				if (Special != default(Special)) {
					Texture2D[] texs = new Texture2D[Special.Count];
					for (int i=0; i<texs.Length; i++) {
						texs[i] = Icons.Special(Special[i]);
					}
					return texs;
				}
				else return new Texture2D[0];
			}
		}

		public void Draw (Panel p) {
			float iconSize = p.LineH;

			Rect iconBox = p.Box(iconSize);
			if (Icons.Traj(Trajectory) != default(Texture2D)) {
				if (GUI.Button(iconBox, "")) {
					//if (GUIInspector.RightClick) {
						TipInspector.Inspect(Tip.Trajectory(Trajectory));
					//}
				}
				GUI.Box(iconBox, Icons.Traj(Trajectory));
			}
			if (RangeString != "") {
				GUI.Label(p.Box(iconSize), RangeString, p.s);
			}
			p.NudgeX();
			if (Special != null) {Special.Display(new Panel(new Rect(p.x2, p.y2, 200, p.LineH), p.LineH, p.s));}
			/*if (TargetIcon != default(Texture2D[])) {
				foreach (Texture2D tex in TargetIcon) {
					GUI.Box(p.Box(iconSize), tex, p.s);
				}
			}
			*/
		}

		public static Aim Self () {return new Aim (ETraj.SELF);}
		public static Aim MoveNeighbor () {return new Aim (ETraj.NEIGHBOR, EType.CELL, EPurp.MOVE);}
		public static Aim MovePath (int r) {return new Aim (ETraj.PATH, EType.CELL, EPurp.MOVE, r);}
		public static Aim MoveLine (int r) {return new Aim (ETraj.LINE, EType.CELL, EPurp.MOVE, r);}
		public static Aim MoveArc (int r, int mr=0) {return new Aim (ETraj.ARC, EType.CELL, EPurp.MOVE, r, mr);}
		public static Aim Create () {return new Aim (ETraj.NEIGHBOR, EType.CELL, EPurp.CREATE);}
		public static Aim CreateArc (int r, int mr=0) {return new Aim (ETraj.ARC, EType.CELL, EPurp.CREATE, r, mr);}
		public static Aim Melee () {return new Aim (ETraj.NEIGHBOR, EType.UNIT);}
		public static Aim Shoot (int n) {
			Aim a = new Aim (ETraj.LINE, EType.UNIT, n);
			a.IncludeSelf = false;
			return a;
		}
		public static Aim Arc (int r, int mr=0) {return new Aim (ETraj.ARC, EType.UNIT, r, mr);}
	}
}

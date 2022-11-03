using UnityEngine;
using System.Collections.Generic;
namespace HOA {
	public enum ETraj {CELLMATE, NEIGHBOR, PATH, LINE, ARC, FREE, SELF, GLOBAL, RADIAL, OTHER}
	public enum EPurp {MOVE, CREATE, ATTACK, OTHER}

	public partial class Aim : IDeepCopy<Aim>{

		public ETraj Trajectory {get; protected set;}
		public Special Special {get; protected set;}
		public EPurp Purpose {get; protected set;}
		public int Range {get; set;}
		public int MinRange {get; set;}
		public bool TeamOnly {get; set;}
		public bool EnemyOnly {get; set;}
		public bool IncludeSelf {get; set;} 
		public bool NoKings {get; set;}

		public delegate TargetGroup TargetFinder (Token actor, Cell center, Token other);
		public TargetFinder Targets {get; private set;}

		private Aim () {
			Special = Special.None;
		}

		public Aim DeepCopy () {
			Aim a = new Aim();
			a.Trajectory = this.Trajectory;
			a.Special = this.Special.DeepCopy();
			a.Purpose = this.Purpose;
			a.Range = this.Range;
			a.MinRange = this.MinRange;
			a.TeamOnly = this.TeamOnly;
			a.EnemyOnly = this.EnemyOnly;
			a.IncludeSelf = this.IncludeSelf;
			a.NoKings = this.NoKings;
			a.Targets = this.Targets;
			return a;
		}

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
	}
}

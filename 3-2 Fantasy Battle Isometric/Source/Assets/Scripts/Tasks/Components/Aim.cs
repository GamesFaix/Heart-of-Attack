using UnityEngine;
using System.Collections.Generic;
namespace HOA {
	public enum Trajectory {Neighbor, Path, Line, Arc, Free, Self, Radial}

	public delegate TargetGroup Finder (Token actor, Cell center, Token other);
	public delegate void AimExtension (Aim aim, AimSeq aims);


	public partial class Aim {

		public Trajectory trajectory {get; protected set;}
		public Finder Find {get; private set;}
		public Filter Filter {get; protected set;}
		public int range {get; set;}
		public int minRange {get; set;}
		public AimExtension Extend {get; set;}
		public bool recursiveTarget;
		public bool optional;

		public Aim Copy {
			get {
				Aim a = new Aim();
				a.trajectory = trajectory;
				a.Find = Find;
				a.Filter = Filter;
				a.range = range;
				a.minRange = minRange;
				a.recursiveTarget = recursiveTarget;
				a.optional = optional;
				a.Extend = Extend;
				return a;
			}
		}

		string RangeString {
			get {
				if (trajectory == Trajectory.Path || trajectory == Trajectory.Line) {return range+"";}	
				else if (trajectory == Trajectory.Arc) {
					if (minRange > 0) {return minRange+" to "+range;}
					return range+"";
				}
				return "";
			}
		}

		public void Draw (Panel p) {
			float iconSize = p.LineH;

			Rect iconBox = p.Box(iconSize);
			if (GUI.Button(iconBox, "")) {TipInspector.Inspect(Tip.Trajectory(trajectory));}
			GUI.Box(iconBox, Icons.Aims.aims[(int)trajectory]);

			if (RangeString != "") {GUI.Label(p.Box(iconSize), RangeString, p.s);}
			p.NudgeX();
			/*Filters.Display(Filters, new Panel(new Rect(p.x2, p.y2, 200, p.LineH), p.LineH, p.s));}*/
			/*if (TargetIcon != default(Texture2D[])) {
				foreach (Texture2D tex in TargetIcon) {
					GUI.Box(p.Box(iconSize), tex, p.s);
				}
			}
			*/
		}

	}
}

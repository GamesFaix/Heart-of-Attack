using UnityEngine; 
using System;

namespace HOA { 

	public static class GUIInspectorCell {

		static float iconSize = 30;

		public static void Display (Cell c, Panel p, Panel super) {
			GUI.Box(p.Box(iconSize), Icons.Special(EType.CELL), p.s);
			p.NudgeX(); p.NudgeY();
			GUI.Label(p.Box(0.5f), c.ToString(), p.s);
			p.NextLine();
			
			Token t;

			int planes = Enum.GetValues(typeof(EPlane)).Length;
			for (int i=0; i<planes; i++) {
				GUI.Box(p.Box(iconSize), Icons.Plane((EPlane)i), p.s);
				if (c.Contains((EPlane)i, out t)) {
					p.NudgeX();
					p.NudgeY();
					t.DisplayThumbName(new Panel(p.Box(0.5f), p.LineH, p.s));
					p.NudgeY(false);
				}
				p.NextLine();
			}

			if (c.Sensors().Count > 0) {
				p.NextLine();
				GUI.Label(p.Box(0.5f), "Local effects:", p.s);
				p.NextLine();
				
				foreach (Sensor s in c.Sensors()) {
					p.NudgeX();
					FancyText.Highlight(p.Box(0.5f), s.ToString(), p.s, s.Parent.Owner.Colors);
					p.NextLine();
				}
			}
			
		}

	}
}

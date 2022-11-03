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
				Rect box = p.IconBox;
				if (GUI.Button(box, "")) {TipInspector.Inspect(ETip.PLANE);}
				GUI.Box(box, Icons.Plane((EPlane)i), p.s);
				if (c.Contains((EPlane)i, out t)) {
					p.NudgeX();
					t.DisplayThumbName(new Panel(p.Box(0.5f), p.LineH, p.s));
				}
				p.NextLine();
			}

			if (c.Sensors().Count > 0) {
				p.NextLine();
				foreach (Sensor s in c.Sensors()) {
					p.NudgeX();
					s.Display(p.LinePanel);
					p.NextLine();
					p.NextLine();
				}
			}
			
		}

	}
}

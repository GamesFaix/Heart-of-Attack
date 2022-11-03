using UnityEngine; 
using System;

namespace HOA { 

	public static class GUIInspectorCell {

		static float iconSize = 30;

		public static void Display (Cell c, Panel p, Panel super) {
			GUI.Box(p.Box(iconSize), Icons.Types.cell, p.s);
			p.NudgeX(); p.NudgeY();
			GUI.Label(p.Box(0.5f), c.ToString(), p.s);
			p.NextLine();
			
			Token t;

			for (int i=0; i<Plane.count; i++) {
				Rect box = p.IconBox;
				if (GUI.Button(box, "")) {TipInspector.Inspect(ETip.Plane);}
				GUI.Box(box, Icons.Planes.planes[i], p.s);
				if (c.Occupied(i, out t)) {
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

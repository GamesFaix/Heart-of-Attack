using UnityEngine; 
using System;

namespace HOA { 

	public static class GUIInspectorCell {

		static float iconSize = 30;

		public static void Display (Cell c, Panel p, Panel super) {
			GUI.Box(p.Box(iconSize), Icons.TargetClass(TargetClasses.Cell), p.s);
			p.NudgeX(); p.NudgeY();
			GUI.Label(p.Box(0.5f), c.ToString(), p.s);
			p.NextLine();
			
			Token t;

			int planes = Enum.GetValues(typeof(Planes)).Length;
			for (int i=0; i<planes; i++) {
				Rect box = p.IconBox;
				if (GUI.Button(box, "")) {TipInspector.Inspect(ETip.PLANE);}
				GUI.Box(box, Icons.Plane((Planes)i), p.s);
				if (c.Contains((Planes)i, out t)) {
					p.NudgeX();
					t.DisplayThumbName(new Panel(p.Box(0.5f), p.LineH, p.s));
				}
				p.NextLine();
			}

			if (c.Sensors().Count > 0) {
				p.NextLine();
				foreach (Sensor s in c.Sensors()) {
					p.NudgeX();
					s.Draw(p.LinePanel);
					p.NextLine();
					p.NextLine();
				}
			}
			
		}

	}
}

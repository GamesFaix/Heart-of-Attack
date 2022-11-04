using UnityEngine; 
using System;

namespace HOA { 

	public static class GUIInspectorCell {

		static float iconSize = 30;

		public static void Display (Cell c, Panel p, Panel super) {
			GUI.Box(p.Box(iconSize), Icons.Cell, p.s);
			p.NudgeX(); p.NudgeY();
			GUI.Label(p.Box(0.5f), c.ToString(), p.s);
			p.NextLine();
			

            Plane[] planes = new Plane[4] {Plane.Sunken, Plane.Ground, Plane.Air, Plane.Ethereal};

			foreach (Plane plane in planes) {
				Rect box = p.IconBox;
				if (GUI.Button(box, "")) {TipInspector.Inspect(ETip.PLANE);}
				GUI.Box(box, Icons.Planes[plane], p.s);
                TargetGroup group = c.Occupants - TargetFilter.Plane(plane, true);
                if (group.Count > 0) {
                    foreach (Token t in group)
                    {
                        p.NudgeX();
                        InspectorInfo.InspectTokenButton(t, new Panel(p.Box(0.5f), p.LineH, p.s));
                    }
                }
				p.NextLine();
			}

			if (c.Sensors.Count > 0) {
				p.NextLine();
				foreach (Sensor s in c.Sensors) {
					p.NudgeX();
					s.Draw(p.LinePanel);
					p.NextLine();
					p.NextLine();
				}
			}
			
		}

	}
}

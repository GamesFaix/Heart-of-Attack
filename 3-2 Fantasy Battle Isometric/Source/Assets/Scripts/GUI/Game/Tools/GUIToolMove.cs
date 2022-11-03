using UnityEngine;

namespace HOA {

	public static class GUIToolMove {
		public static void Display (Panel p) {
			p.y2 += 5;
			p.x2 += p.W*0.4f;
			GUI.Label(p.Box(0.3f), "Move:", GUIMaster.S);
			p.NextLine();
		
			Panel subPanel = new Panel(p.TallBox(6), p.LineH, p.s);
			GUISelectors.InstanceGrid(subPanel);

			Token instance = GUISelectors.Instance;

			if (instance != default(Token)) {
				Targeter.Start(new AMoveManual(TurnQueue.Top, instance));
			}
		}
	}
}
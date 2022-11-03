using UnityEngine;

namespace HOA {

	public static class GUIToolCreate {

		public static void Display (Panel p) {
			EToken token = GUISelectors.Token;

			p.y2 += 5;
			p.x2 += p.W*0.4f;
			GUI.Label(p.Box(0.3f), "Create:", GUIMaster.S);
			p.NextLine();
		
			Panel subPanel = new Panel(p.TallBox(12), p.LineH, p.s);
			GUISelectors.TokenGrid(subPanel); 
		
			if (token != EToken.NONE) {
				Targeter.Find(new ACreateManual(TurnQueue.Top, token));
			}

			if (Targeter.Pending() != default(Action)) {
				if (GUI.Button(p.LineBox, Targeter.Pending().ToString())
				    || Input.GetKeyUp ("space")) {
					Targeter.Execute();
				}
			}
		}
	}
}
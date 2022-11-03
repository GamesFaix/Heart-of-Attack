using UnityEngine;

namespace HOA {

	public static class GUIToolKill {
		public static void Display (Panel p) {
			p.y2 += 5;
			p.x2 += p.W*0.4f;
			GUI.Label(p.Box(0.3f), "Kill:");
			p.NextLine();
		
			Panel subPanel = new Panel(p.TallBox(20), p.LineH, p.s);
			GUISelectors.InstanceGrid(subPanel);

			Token instance = GUISelectors.Instance;
			string btnLabel = "Kill ";
			if (instance != default(Token)) {btnLabel += instance.FullName;}

			p.y2 += 5;
			if (GUI.Button(p.LineBox, btnLabel) || Input.GetKeyUp("space")){ 
				if (instance != default(Token)) {
					EffectQueue.Add(new EKill(Source.ActivePlayer, instance));
					GUISelectors.Reset();
					btnLabel = "Kill ";
				}
			}
		}
	}
}
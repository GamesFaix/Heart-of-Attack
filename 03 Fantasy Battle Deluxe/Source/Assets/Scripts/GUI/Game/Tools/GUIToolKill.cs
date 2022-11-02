using UnityEngine;
using HOA.Tokens;
using HOA.Players;

public static class GUIToolKill {
	public static void Display (Panel p) {
		Token instance = GUISelectors.Instance();
		
		p.y2 += 5;
		p.x2 += p.W()*0.4f;
		GUI.Label(p.Box(0.3f), "Kill:");
		p.NextLine();
	
		Panel subPanel = new Panel(p.TallBox(6), p.LineH(), p.s);
		GUISelectors.InstanceGrid(subPanel);
		
		p.y2 += 5;
		if (instance != default(Token)) {
			if (GUI.Button(p.LineBox(), "Kill "+instance.FullName())
			|| Input.GetKeyUp("space")){ 
				if (instance != default(Token)) {
					InputBuffer.Submit(new RKill(Source.ActivePlayer(), instance));
					GUISelectors.Reset();
				}
			}
		}
	}
}

using UnityEngine;
using HOA.Tokens;
using HOA.Players;

public static class GUIToolReplace {
	
	public static void Display (Panel p) {
		Token instance = GUISelectors.Instance();
		TTYPE token = GUISelectors.Token();
		
		p.y2 += 5;
		p.x2 += p.W*0.4f;
		GUI.Label(p.Box(0.3f), "Replace:");
		p.NextLine();
	
		Panel subPanel = new Panel(p.TallBox(6), p.LineH, p.s);
		GUISelectors.InstanceGrid(subPanel);  
		
		p.y2 += 5;
		p.x2 += p.W*0.45f;
		GUI.Label(p.Box(0.3f), "with:");
		p.NextLine();
	
		subPanel = new Panel(p.TallBox(6), p.LineH, p.s);	
		GUISelectors.TokenGrid(subPanel); 
		
		p.y2 += 5;		
		if (instance != default(Token)) {
			if (GUI.Button(p.LineBox, "Replace "+instance.FullName+" with "+TokenRef.CodeToString(token))
			|| Input.GetKeyUp("space")){
				if (instance != default(Token) && token != TTYPE.NONE) {	 
					InputBuffer.Submit(new RReplace(Source.ActivePlayer, instance, token));
					GUISelectors.Reset();
				}
			}
		}
	}
}
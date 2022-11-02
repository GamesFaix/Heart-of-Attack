using UnityEngine;
using HOA.Players;

public static class GUIToolRandom {
	static string[] randLabels = new string[7] {"Coin", "d4", "d6", "d8", "d10", "d12", "d20"};

	static int randBtn = 0;

	public static void Display (Panel p) {
		GUI.Box(p.FullBox,"");
		
		randBtn = GUI.Toolbar(p.LineBox, randBtn, randLabels);
		DICE d = (DICE)randBtn;
		
		p.y2 += 5;
		string actionLabel = "Roll";
		if (randBtn == 0) {actionLabel = "Flip";}
		
		p.x2 += 5;
		if (GUI.Button(p.Box(0.5f), actionLabel)) {
			InputBuffer.Submit(new RRandom(new Source(Referee.ActivePlayer), d));
		}
		
		
		p.y2 += 5;
		p.x2 += 20;
		GUI.Label(p.Box(0.5f), DiceCoin.PrintResult);	
	}
}

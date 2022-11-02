using UnityEngine;
using HOA.Tokens;
using HOA.Map;
using HOA.Players;

public static class GUIToolCreate {
	static string printCommand = "";
	
	public static void Display (Panel p) {
		TTYPE token = GUISelectors.Token();
		Cell cell = GUISelectors.Cell();
		
		p.y2 += 5;
		p.x2 += p.W*0.4f;
		GUI.Label(p.Box(0.3f), "Create:");
		p.NextLine();
	
		Panel subPanel = new Panel(p.TallBox(6), p.LineH, p.s);
		GUISelectors.TokenGrid(subPanel); 
	
		p.x2 += p.W*0.25f;
		p.y2 += 5;
		GUI.Label(p.Box(0.5f), "(Shift+Click cell to select)");
		
		p.NextLine();		
		
		p.y2 += 5;
		printCommand = "Create ";
		
		if (token != TTYPE.NONE) {
			printCommand += TokenRef.CodeToString(token)+" at ";
		}
		
		if (cell != default(Cell)) {
			printCommand += cell.ToString()+".";
		}
		
		if (GUI.Button(p.LineBox, printCommand)
			|| Input.GetKeyUp("space")){
			InputBuffer.Submit(new RCreate(Source.ActiveUnit, token, cell));
			Reset();
		}
	}
	
	public static void Reset () {
		printCommand = "";
	}
}

using UnityEngine;
using HOA.Tokens;
using HOA.Players;
using HOA.Map;

public static class GUIToolMove {
	static string printCommand = "";
	
	public static void Display (Panel p) {
		Token instance = GUISelectors.Instance();
		Cell cell = GUISelectors.Cell();
		
		p.y2 += 5;
		p.x2 += p.W*0.4f;
		GUI.Label(p.Box(0.3f), "Move:");
		p.NextLine();
	
		Panel subPanel = new Panel(p.TallBox(6), p.LineH, p.s);
		GUISelectors.InstanceGrid(subPanel);
		
		p.x2 += p.W*0.25f;
		GUI.Label(p.Box(0.1f), "To:");
		p.x2 += 5;
		GUI.Label(p.Box(0.5f), "(Shift+Click cell to select)");
		
		p.NextLine();		
		p.y2 += 5;
		
		printCommand = "Move ";
		if (instance != default(Token)) {
			printCommand += instance.FullName+" to ";
		}
		if (cell != default(Cell)) {
			printCommand += cell.ToString();
		}
			
		if (GUI.Button(p.LineBox, printCommand)
			|| Input.GetKeyUp("space")){ 
			InputBuffer.Submit(new RMove(Source.ActivePlayer, instance, cell));
			GUISelectors.Reset();
		}
	}
}

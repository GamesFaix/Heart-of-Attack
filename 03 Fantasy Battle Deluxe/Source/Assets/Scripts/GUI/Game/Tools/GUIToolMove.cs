using UnityEngine;

namespace HOA {

	public static class GUIToolMove {
		public static void Display (Panel p) {
			p.y2 += 5;
			p.x2 += p.W*0.4f;
			GUI.Label(p.Box(0.3f), "Move:");
			p.NextLine();
		
			Panel subPanel = new Panel(p.TallBox(6), p.LineH, p.s);
			GUISelectors.InstanceGrid(subPanel);
			GUISelectors.WaitForInstance = true;

			p.x2 += p.W*0.25f;
			GUI.Label(p.Box(0.1f), "To:");
			p.x2 += 5;
			GUI.Label(p.Box(0.5f), "(Shift+Click cell to select)");
			
			p.NextLine();		
			p.y2 += 5;



			Token instance = GUISelectors.Instance;
			Cell cell = GUISelectors.Cell;

			string btnLabel = "Move ";
			if (instance != default(Token)) {
				btnLabel += instance.FullName+" to ";
				foreach (Cell c in Board.cells) {
					if (instance.CanEnter(c)) {c.Legal = true;}
				}
				GUISelectors.WaitForCell = true;
			
			}
			if (cell != default(Cell)) {btnLabel += cell.ToString();}
				
			if (GUI.Button(p.LineBox, btnLabel) || Input.GetKeyUp("space")){ 
				if (instance != default(Token) && cell != default(Cell)) {
					InputBuffer.Submit(new RMove(Source.ActivePlayer, instance, cell));
					GUISelectors.Reset();
					btnLabel = "Move ";
				}
			}
		}
	}
}
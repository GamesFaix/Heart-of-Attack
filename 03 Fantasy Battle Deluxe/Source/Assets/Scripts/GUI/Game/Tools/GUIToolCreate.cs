using UnityEngine;

namespace HOA {

	public static class GUIToolCreate {

		public static void Display (Panel p) {
			TTYPE token = GUISelectors.Token;
			Cell cell = GUISelectors.Cell;
			
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
			string btnLabel = "Create ";
			
			if (token != TTYPE.NONE) {
				btnLabel += TokenRef.CodeToString(token)+" at ";
				Token t = TemplateFactory.Template(token);
				foreach (Cell c in Board.cells) {
					if (t.CanEnter(c)) {c.Legal = true;}
				}
				GUISelectors.WaitForCell = true;
			}
			
			if (cell != default(Cell)) {
				btnLabel += cell.ToString()+".";
			}
			
			if (GUI.Button(p.LineBox, btnLabel) || Input.GetKeyUp("space")){
				InputBuffer.Submit(new RCreate(Source.ActiveUnit, token, cell));
				GUISelectors.Reset();
			}
		}
	}
}
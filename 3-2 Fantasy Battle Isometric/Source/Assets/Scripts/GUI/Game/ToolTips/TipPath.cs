using UnityEngine;

namespace HOA {

	public static class TipPath {
		static float iconSize = 20;
		
		public static void Draw (Vector2 mousePos) {
			float x = mousePos.x;
			float y = mousePos.y;
			float w = 250;
			float h = 200;
			
			Rect box = new Rect(x,y,w,h);
			
			
			Panel p = new Panel(box, iconSize, GUIMaster.S);
			
			GUI.Box(p.FullBox,"");
			GUI.Box(p.FullBox,"");
			GUI.Box(p.FullBox,"");
			GUI.Box(p.FullBox,"");
			GUI.Box(p.FullBox,"");
			
			Rect iconBox = p.Box(30);
			iconBox.height = 30;
			GUI.Box(iconBox, Icons.Aim(ETraj.PATH));
			p.NudgeX(); p.NudgeY();
			GUI.Box(p.Box(100), "Path", p.s);
			p.NudgeY();
			p.NextLine();
			
			int oldSize = p.s.fontSize;
			p.s.fontSize = 12;
			
			GUI.Label(p.TallBox(2.3f), "Path-targetting Actions require the selection \nof a target Cell or Token within a specified \nRange. A Path can change direction any \nnumber of times, as long as the number of \nCells entered does not exceed its Range.  \nMovement Paths may enter any Cells the \nmoving Token could occupy. \nAttack Paths may only pass through Cells \nnot occupied by Ground or Air Tokens, and \nmust end with a valid target. ", p.s);
			
			
			p.s.fontSize = oldSize;
			
		}
		
	}
	
}

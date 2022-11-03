using UnityEngine;

namespace HOA {

	public static class TipFree {
		static float iconSize = 20;
		
		public static void Draw (Vector2 mousePos) {
			float x = mousePos.x;
			float y = mousePos.y;
			float w = 250;
			float h = 100;
			
			Rect box = new Rect(x,y,w,h);
			
			
			Panel p = new Panel(box, iconSize, GUIMaster.S);
			
			GUI.Box(p.FullBox,"");
			GUI.Box(p.FullBox,"");
			GUI.Box(p.FullBox,"");
			GUI.Box(p.FullBox,"");
			GUI.Box(p.FullBox,"");
			
			Rect iconBox = p.Box(30);
			iconBox.height = 30;
			GUI.Box(iconBox, Icons.Traj(ETraj.FREE));
			p.NudgeX(); p.NudgeY();
			GUI.Box(p.Box(100), "Free", p.s);
			p.NudgeY();
			p.NextLine();
			
			int oldSize = p.s.fontSize;
			p.s.fontSize = 12;
			
			GUI.Label(p.TallBox(2.3f), "Free-targetting Actions require the selection \nof a target Cell or Token. A Free target may be \nselected from anywhere on the board, as long \nas it meets the specified criteria.", p.s);
			
			
			p.s.fontSize = oldSize;
			
		}
		
	}
	
}

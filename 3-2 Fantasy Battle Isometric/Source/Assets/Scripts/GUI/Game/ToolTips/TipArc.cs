using UnityEngine;

namespace HOA { 

	public static class TipArc {
		static float iconSize = 20;
		
		public static void Draw (Vector2 mousePos) {
			float x = mousePos.x;
			float y = mousePos.y;
			float w = 250;
			float h = 120;
			
			Rect box = new Rect(x,y,w,h);
			
			
			Panel p = new Panel(box, iconSize, GUIMaster.S);
			
			GUI.Box(p.FullBox,"");
			GUI.Box(p.FullBox,"");
			GUI.Box(p.FullBox,"");
			GUI.Box(p.FullBox,"");
			GUI.Box(p.FullBox,"");
			
			Rect iconBox = p.Box(30);
			iconBox.height = 30;
			GUI.Box(iconBox, Icons.Aim(EAim.ARC));
			p.NudgeX(); p.NudgeY();
			GUI.Box(p.Box(100), "Arc", p.s);
			p.NudgeY();
			p.NextLine();
			
			int oldSize = p.s.fontSize;
			p.s.fontSize = 12;
			
			GUI.Label(p.TallBox(2.3f), "Arc-targetting Actions require the selection \nof a target Cell or Token within a specified \nRange (and sometimes above a Minimum \nRange). An Arc can go in any direction, \nthrough any Cell, as long as the number \nof Cells entered is not outside its Range.", p.s);
			
			
			p.s.fontSize = oldSize;
			
		}
		
	}
	
}

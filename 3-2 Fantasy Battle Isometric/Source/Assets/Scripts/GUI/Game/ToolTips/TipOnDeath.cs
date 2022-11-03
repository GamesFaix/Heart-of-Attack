using UnityEngine;

namespace HOA {

	public static class TipOnDeath {
		static float iconSize = 20;
		
		public static void Draw (Vector2 mousePos) {
			float x = mousePos.x;
			float y = mousePos.y;
			float w = 150;
			float h = 150;
			
			Rect box = new Rect(x,y,w,h);
			
			
			Panel p = new Panel(box, iconSize, GUIMaster.S);
			
			GUI.Box(p.FullBox,"");
			GUI.Box(p.FullBox,"");
			GUI.Box(p.FullBox,"");
			GUI.Box(p.FullBox,"");
			GUI.Box(p.FullBox,"");
			
			Rect iconBox = p.Box(30);
			iconBox.height = 30;
			GUI.Box(iconBox, Icons.ONDEATH());
			p.NudgeX(); p.NudgeY();
			GUI.Box(p.Box(100), "Token-on-Death", p.s);
			p.NudgeY();
			p.NextLine();
			
			int oldSize = p.s.fontSize;
			p.s.fontSize = 12;
			
			GUI.Label(p.TallBox(2.3f), "When a Token is destroyed, \nits Token-on-Death is \ncreated in its Cell (if no \nother Token is in the way).", p.s);
			
			
			p.s.fontSize = oldSize;
			
		}
		
	}
	
}

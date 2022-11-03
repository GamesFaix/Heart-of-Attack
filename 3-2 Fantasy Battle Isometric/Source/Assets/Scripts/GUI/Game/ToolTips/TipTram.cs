using UnityEngine;

namespace HOA {

	public static class TipTram {
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
			GUI.Box(iconBox, Icons.Special(EType.TRAM));
			p.NudgeX(); p.NudgeY();
			GUI.Box(p.Box(100), "Trample", p.s);
			p.NudgeY();
			p.NextLine();
			
			int oldSize = p.s.fontSize;
			p.s.fontSize = 12;
			
			GUI.Label(p.TallBox(2.3f), "Units with Trample may \nmove into a Cell with a \nDestructible already \noccupying their Plane.  \nMoving into a Destructible's \nCell destroys it and ends \nthe Trampling Unit's \nmovement.", p.s);
			
			
			p.s.fontSize = oldSize;
			
		}
		
	}
	
}

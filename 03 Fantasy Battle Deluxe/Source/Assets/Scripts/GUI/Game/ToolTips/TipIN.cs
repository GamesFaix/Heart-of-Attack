using UnityEngine;

namespace HOA {

	public static class TipIN {
		static float iconSize = 20;
		
		public static void Draw (Vector2 mousePos) {
			float x = mousePos.x;
			float y = mousePos.y;
			float w = 150;
			float h = 270;
			
			Rect box = new Rect(x,y,w,h);
			
			
			Panel p = new Panel(box, iconSize, GUIMaster.S);
			
			GUI.Box(p.FullBox,"");
			GUI.Box(p.FullBox,"");
			GUI.Box(p.FullBox,"");
			GUI.Box(p.FullBox,"");
			GUI.Box(p.FullBox,"");
			
			Rect iconBox = p.Box(30);
			iconBox.height = 30;
			GUI.Box(iconBox, Icons.Stat(STAT.IN));
			p.NudgeX(); p.NudgeY();
			GUI.Box(p.Box(100), "Initative", p.s);
			p.NudgeY();
			p.NextLine();
			
			int oldSize = p.s.fontSize;
			p.s.fontSize = 12;
			
			GUI.Label(p.TallBox(2.3f), "A Unit's Initiative affects \nhow quickly it can take \nanother turn after ending \none. \nWhen a Unit ends its turn, \nit moves to the bottom of \nthe Queue.  If the Unit \nabove it has a lower \nInitiative, it may skip over \na number of Units equal \nto the difference in \nInitiative.\nOnce a Unit has been \nskipped, it cannot be \nskipped again until it \ntakes a turn.", p.s);
			
			
			p.s.fontSize = oldSize;
			
			
		}
	}
	
}

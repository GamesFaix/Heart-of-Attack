using UnityEngine;

namespace HOA {

	public static class TipAP {
		static float iconSize = 20;
		
		public static void Draw (Vector2 mousePos) {
			float x = mousePos.x;
			float y = mousePos.y;
			float w = 150;
			float h = 170;
			
			Rect box = new Rect(x,y,w,h);
			
			
			Panel p = new Panel(box, iconSize, GUIMaster.S);
			
			GUI.Box(p.FullBox,"");
			GUI.Box(p.FullBox,"");
			GUI.Box(p.FullBox,"");
			GUI.Box(p.FullBox,"");
			GUI.Box(p.FullBox,"");
			
			Rect iconBox = p.Box(30);
			iconBox.height = 30;
			GUI.Box(iconBox, Icons.Stat(EStat.AP));
			p.NudgeX(); p.NudgeY();
			GUI.Box(p.Box(100), "Energy", p.s);
			p.NudgeY();
			p.NextLine();
			
			int oldSize = p.s.fontSize;
			p.s.fontSize = 12;
			
			GUI.Label(p.TallBox(2.3f), "Energy is one of two \nresources Units may use \nto perform Actions. \nMost Units receive 2 \nEnergy at the start of their \nturn. \n(Attack Kings receive 3.)\nAny unspent Energy is lost \nat the end of a Unit's turn.", p.s);
			
			
			p.s.fontSize = oldSize;
			
			
			
		}
	}
}

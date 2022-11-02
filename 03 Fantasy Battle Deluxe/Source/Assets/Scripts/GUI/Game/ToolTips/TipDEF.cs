using UnityEngine;

namespace HOA {

	public static class TipDEF {
		static float iconSize = 20;
		
		public static void Draw (Vector2 mousePos) {
			float x = mousePos.x;
			float y = mousePos.y;
			float w = 150;
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
			GUI.Box(iconBox, Icons.Stat(EStat.DEF));
			p.NudgeX(); p.NudgeY();
			GUI.Box(p.Box(100), "Defense", p.s);
			p.NudgeY();
			p.NextLine();
			
			int oldSize = p.s.fontSize;
			p.s.fontSize = 12;
			
			GUI.Label(p.TallBox(2.3f), "If a Unit has Defense, each \nincoming Damage source \nis reduced by the amount \nof Defense.", p.s);
			
			
			p.s.fontSize = oldSize;
			
			
			
		}
		
	}
}

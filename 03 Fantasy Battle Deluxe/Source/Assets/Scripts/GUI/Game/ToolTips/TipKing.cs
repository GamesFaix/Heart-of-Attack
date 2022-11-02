using UnityEngine;

namespace HOA {

	public static class TipKing {
		static float iconSize = 20;
		
		public static void Draw (Vector2 mousePos) {
			float x = mousePos.x;
			float y = mousePos.y;
			float w = 150;
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
			GUI.Box(iconBox, Icons.Special(SPECIAL.KING));
			p.NudgeX(); p.NudgeY();
			GUI.Box(p.Box(100), "Attack King", p.s);
			p.NudgeY();
			p.NextLine();
			
			int oldSize = p.s.fontSize;
			p.s.fontSize = 12;
			
			GUI.Label(p.TallBox(2.3f), "Attack Kings are a player's \nmost valuable asset. \nIf your Attack King is \nkilled, you lose. \nIf you kill an Attack King, \nyou take control of all \nremaining Units on its team. \nAttack Kings receive 3 \nEnergy at the start of their \nturn, instead of the \nstandard 2.", p.s);
			
			
			p.s.fontSize = oldSize;
			
			
			
		}
	}
}

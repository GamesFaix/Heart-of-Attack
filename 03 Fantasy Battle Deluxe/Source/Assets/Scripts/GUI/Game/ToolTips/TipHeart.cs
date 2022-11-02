using UnityEngine;

namespace HOA {

	public static class TipHeart {
		static float iconSize = 20;
		
		public static void Draw (Vector2 mousePos) {
			float x = mousePos.x;
			float y = mousePos.y;
			float w = 150;
			float h = 250;
			
			Rect box = new Rect(x,y,w,h);
			
			
			Panel p = new Panel(box, iconSize, GUIMaster.S);
			
			GUI.Box(p.FullBox,"");
			GUI.Box(p.FullBox,"");
			GUI.Box(p.FullBox,"");
			GUI.Box(p.FullBox,"");
			GUI.Box(p.FullBox,"");
			
			Rect iconBox = p.Box(30);
			iconBox.height = 30;
			GUI.Box(iconBox, Icons.Special(SPECIAL.HOA));
			p.NudgeX(); p.NudgeY();
			GUI.Box(p.Box(100), "Heart of Attack", p.s);
			p.NudgeY();
			p.NextLine();
			
			int oldSize = p.s.fontSize;
			p.s.fontSize = 12;
			
			GUI.Label(p.TallBox(2.3f), "Heart of Attacks are a \nspecial type of Token left \nwhen an Attack King is \nkilled. \nAttack Kings may move \ninto a Cell with a Heart of \nAttack already occupying \ntheir Plane. Moving into a \nHeart of Attack's Cell \ndestroys it, ends the Attack \nKing's movement, and gives \nthe Heart of Attack's powers \nto the Attack King.", p.s);
			
			
			p.s.fontSize = oldSize;
		}
		
		
	}
	
}

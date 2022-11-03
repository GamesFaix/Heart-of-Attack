using UnityEngine;

namespace HOA {

	public static class TipDest {
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
			GUI.Box(iconBox, Icons.Class(EClass.DEST));
			p.NudgeX(); p.NudgeY();
			GUI.Box(p.Box(100), "Destructible", p.s);
			p.NudgeY();
			p.NextLine();
			
			int oldSize = p.s.fontSize;
			p.s.fontSize = 12;
			
			GUI.Label(p.TallBox(2.3f), "Destructible Tokens can \nbe destroyed in the \nfollowing ways:", p.s);
			
			p.NudgeX();
			GUI.Box (p.LineBox, "\tNearby explosions", p.s);
			p.NudgeX();
			GUI.Box (p.LineBox, "\tNearby fire", p.s);
			p.NudgeX();
			GUI.Box (p.TallBox(1.5f), "\tTrample Units entering \n\ttheir cell", p.s);
			p.NudgeX();
			GUI.Box (p.TallBox(1.5f), "\tActions specifically \n\ttargeting Destructibles", p.s);

			GUI.Label (p.TallBox(2), "Some actions only target \nDestructibles that are not \nRemains.", p.s);

			/*
			foreach (Texture2D tex in Icons.Class(EClass.DEST)) {
				iconBox = p.Box(30);
				iconBox.height = 30;
				GUI.Box (iconBox, tex);
			}
*/
			p.s.fontSize = oldSize;
			
		}	
			
	}
	
}

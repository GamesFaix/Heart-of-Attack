using UnityEngine;

namespace HOA {

	public static class TipCell {
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
			GUI.Box(iconBox, Icons.Type(EType.CELL));
			p.NudgeX(); p.NudgeY();
			GUI.Box(p.Box(100), "Cell", p.s);
			p.NudgeY();
			p.NextLine();
			
			int oldSize = p.s.fontSize;
			p.s.fontSize = 12;
			
			GUI.Label(p.TallBox(2), "A Cell is a location on the \nBoard. Each Cell contains\n four Planes:", p.s);
			
			p.NudgeX();
			GUI.Box (p.Box(iconSize), Icons.Plane(EPlane.SUNK), p.s);
			p.NudgeY(); p.NudgeX();
			GUI.Box (p.Box(iconSize*3), "Sunken", p.s);
			p.NudgeY(false);
			p.NextLine();
			p.NudgeX();
			GUI.Box (p.Box(iconSize), Icons.Plane(EPlane.GND), p.s);
			p.NudgeY(); p.NudgeX();
			GUI.Box (p.Box(iconSize*3), "Ground", p.s);
			p.NudgeY(false);
			p.NextLine();
			p.NudgeX();
			GUI.Box (p.Box(iconSize), Icons.Plane(EPlane.AIR), p.s);
			p.NudgeY(); p.NudgeX();
			GUI.Box (p.Box(iconSize*3), "Air", p.s);
			p.NudgeY(false);
			p.NextLine();
			p.NudgeX();
			GUI.Box (p.Box(iconSize), Icons.Plane(EPlane.ETH), p.s);
			p.NudgeY(); p.NudgeX();
			GUI.Box (p.Box(iconSize*3), "Ethereal", p.s);
			p.NudgeY(false);
			p.NextLine();
			
			GUI.Label (p.TallBox(2), "Cells may be occupied by \n(at most) four Tokens, one \nin each Plane. (Most \nTokens only occupy one \nPlane, but some occupy \nmore.)", p.s);
			
			p.s.fontSize = oldSize;
			
			
			
		}
	}
}

using UnityEngine;

namespace HOA {

	public static class TipNeighbor {
		static float iconSize = 20;
		
		public static void Draw (Vector2 mousePos) {
			float x = mousePos.x;
			float y = mousePos.y;
			float w = 250;
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
			GUI.Box(iconBox, Icons.Traj(ETraj.NEIGHBOR));
			p.NudgeX(); p.NudgeY();
			GUI.Box(p.Box(100), "Neighbor", p.s);
			p.NudgeY();
			p.NextLine();
			
			int oldSize = p.s.fontSize;
			p.s.fontSize = 12;
			
			GUI.Label(p.TallBox(2.3f), "Neighbor-targetting Actions require the \nselection of a target Cell or Token adjacent \nto the performing Unit. \n(Unless otherwise specified, Tokens sharing \nthe performing Unit's Cell may be targetted.)", p.s);
			
			
			p.s.fontSize = oldSize;
			
			
			
		}
	}
	
}

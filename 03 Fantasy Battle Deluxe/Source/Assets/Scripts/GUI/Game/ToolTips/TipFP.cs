using UnityEngine;
using HOA.Tokens;
using HOA.Actions;

public static class TipFP {
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
		GUI.Box(iconBox, Icons.Stat(STAT.FP));
		p.NudgeX(); p.NudgeY();
		GUI.Box(p.Box(100), "Focus", p.s);
		p.NudgeY();
		p.NextLine();
		
		int oldSize = p.s.fontSize;
		p.s.fontSize = 12;
		
		GUI.Label(p.TallBox(2.3f), "Focus is one of two \nresources Units may use \nto perform Actions. \nMost Units may perform \nthe Focus action to gain 1 \nFocus.\nFocus may be accumulated \nwith no limit.", p.s);
		
		
		p.s.fontSize = oldSize;
		
		
		
	}
	
}

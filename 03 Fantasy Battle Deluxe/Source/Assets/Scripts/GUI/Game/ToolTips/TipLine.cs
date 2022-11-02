using UnityEngine;
using HOA.Tokens;
using HOA.Actions;

public static class TipLine {
	static float iconSize = 20;
	
	public static void Draw (Vector2 mousePos) {
		float x = mousePos.x;
		float y = mousePos.y;
		float w = 250;
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
		GUI.Box(iconBox, Icons.AimType(AIMTYPE.LINE));
		p.NudgeX(); p.NudgeY();
		GUI.Box(p.Box(100), "Line", p.s);
		p.NudgeY();
		p.NextLine();
		
		int oldSize = p.s.fontSize;
		p.s.fontSize = 12;
		
		GUI.Label(p.TallBox(2.3f), "Line-targetting Actions require the selection \nof a target Cell or Token within a specified \nRange. A Line cannot change direction, and \nthe number of Cells entered cannot exceed its \nRange.  \nMovement Lines may enter any Cells the \nmoving Token could occupy.  \nAttack Lines may only pass through Cells \nnot occupied by Ground or Air Tokens, and \nmust end with a valid target. ", p.s);
		
		
		p.s.fontSize = oldSize;
		
		
		
	}
	
}

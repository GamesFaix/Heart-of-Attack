using UnityEngine;
using System.Collections.Generic;
using HOA;

public static class GUIQueue {
	
	public static void Display (Panel p){
		GUI.DrawTexture(p.FullBox, ImageLoader.wood[1], ScaleMode.StretchToFill);
		p.NudgeY();
		if (TurnQueue.Count > 0) {
			float listHeight = (p.H-p.LineH) / p.H;
			Panel listPanel = new Panel(p.TallWideBox(listHeight), p.LineH, p.s);
			DrawList(listPanel);
		}
	}

	//List
	static Vector2 scrollPos = new Vector2 (0,0);
	static float internalW = 100;

	
	static void DrawList (Panel p) {
		int oldSize = p.s.fontSize;
		p.s.fontSize = 16;

		float iconSize = p.LineH;
		float internalH = TurnQueue.Count * p.LineH;
		float nameW = 150;

		scrollPos = GUI.BeginScrollView(p.FullBox, scrollPos, new Rect(p.X, p.Y, internalW, internalH));
		
			for (int i=0; i<TurnQueue.Count; i++) {
				Unit u = TurnQueue.Index(i);	
				
				p.NudgeX();
				if (GUI.Button(p.Box(nameW), "", p.s)) {
					GUIInspector.Inspected = u;
                    AVEffect.Highlight.Play(u);
					CameraPanner.MoveTo(u);
				}
				p.ResetX(); p.NudgeX();
				FancyText.Highlight(p.Box(nameW), u.ToString(), p.s, u.Owner.Colors);

			////watch
				GUI.Box(p.Box(iconSize), Icons.Stat(Stats.Initiative), p.s);
				p.x2 += 5;
				GUI.Label(p.Box(iconSize), u.IN+"", p.s);
				
				if (u.IsStunned()) {
					GUI.Box(p.Box(iconSize), Icons.Stat(Stats.Stun), p.s);
					p.x2 += 5;
					GUI.Label(p.Box(iconSize), u.STUN+"", p.s);
				}
				else if (u.IsSkipped()) {
					GUI.Box(p.Box(iconSize), Icons.SKIP(), p.s);
				}
				else {p.x2 += iconSize;}

			////wallet
				p.NudgeX();		
				if (u.FP > 0) {
					GUI.Box(p.Box(iconSize), Icons.Stat(Stats.Focus), p.s);
					p.NudgeX();
					GUI.Label(p.Box(iconSize), u.FP+"", p.s);
				}
				else {p.x2 += iconSize*2 +5;}
	

			////health
				//p.NudgeX();
				GUI.Box(p.Box(iconSize), Icons.Stat(Stats.Health), p.s);
				p.NudgeX();
				GUI.Label (p.Box(iconSize*3), u.Health.HP.ToString(), p.s);
				
				if (u.DEF > 0) {
					GUI.Box(p.Box(iconSize), Icons.Stat(Stats.Defense), p.s);
					p.x2 += 5;
					GUI.Label(p.Box(iconSize), u.DEF+"", p.s);
				}
				
				if (p.x2-p.X > internalW) {internalW = p.x2-p.X;}
				p.NextLine();
			}
		GUI.EndScrollView();

		p.s.fontSize = oldSize;
	}

}

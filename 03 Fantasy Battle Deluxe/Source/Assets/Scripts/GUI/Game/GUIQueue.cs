using UnityEngine;
using System.Collections.Generic;
using HOA.Tokens;

public class GUIQueue : MonoBehaviour {
	
	public void Display (Panel p){
		GUI.Box(p.FullBox, "");
		
		p.x2 += 5;
		GUI.Label(p.Box(0.2f), "QUEUE");
		p.x2 -= 5;
		p.x2 += p.W*0.6f;
		
		
		if (GUI.Button(p.Box(0.2f), "Advance")) {
			InputBuffer.Submit(new RQueueAdvance(Source.ActivePlayer));
		}
		
		p.NextLine();

		if (TurnQueue.Count > 0) {
			float listHeight = (p.H-p.LineH) / p.H;
			Panel listPanel = new Panel(p.TallBox(listHeight), p.LineH, p.s);
			DrawList(listPanel);
		}
	}

	//List
	Vector2 scrollPos = new Vector2 (0,0);
	float internalW = 100;

	
	void DrawList (Panel p) {
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
					if (Input.GetMouseButtonUp(1)) {GUIInspector.Inspected = u;}
					else if (Input.GetMouseButtonUp(0)) {u.SpriteEffect(EFFECT.SHOW);}
				}
				p.ResetX(); p.NudgeX();
				FancyText.Highlight(p.Box(nameW), u.ToString(), p.s, u.Owner.Colors);

			////watch
				GUI.Box(p.Box(iconSize), Icons.Stat(STAT.IN), p.s);
				p.x2 += 5;
				GUI.Label(p.Box(iconSize), u.IN+"", p.s);
				
				if (u.IsStunned()) {
					GUI.Box(p.Box(iconSize), Icons.Stat(STAT.STUN), p.s);
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
					GUI.Box(p.Box(iconSize), Icons.Stat(STAT.FP), p.s);
					p.NudgeX();
					GUI.Label(p.Box(iconSize), u.FP+"", p.s);
				}
				else {p.x2 += iconSize*2 +5;}
	

			////health
				//p.NudgeX();
				GUI.Box(p.Box(iconSize), Icons.Stat(STAT.HP), p.s);
				p.NudgeX();
				GUI.Label (p.Box(iconSize*3), u.HPString, p.s);
				
				if (u.DEF > 0) {
					GUI.Box(p.Box(iconSize), Icons.Stat(STAT.DEF), p.s);
					p.x2 += 5;
					GUI.Label(p.Box(iconSize), u.DEF+"", p.s);
				}
				
				if (u.COR > 0) {
					GUI.Box(p.Box(iconSize), Icons.Stat(STAT.COR), p.s);
					p.x2 += 5;
					GUI.Label(p.Box(iconSize), u.COR+"", p.s);
				}
				
				
				if (p.x2-p.X > internalW) {internalW = p.x2-p.X;}
				p.NextLine();
			}
		GUI.EndScrollView();

		p.s.fontSize = oldSize;
	}

}

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
		
		
		if (GUI.Button(p.Box(0.2f), "Advance") || Input.GetKeyUp("=")) {
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
	float iconSize = 30;
	
	
	void DrawList (Panel p) {
		float internalH = TurnQueue.Count * p.LineH;
		
		scrollPos = GUI.BeginScrollView(p.FullBox, scrollPos, new Rect(p.X, p.Y, internalW, internalH));
		
			for (int i=0; i<TurnQueue.Count; i++) {
				Unit u = TurnQueue.Index(i);	
				
				p.x2 += 5;
				if (GUI.Button(p.Box(250), "", p.s)) {GUIInspector.Inspect(u);}
				p.ResetX();
				StyledText.Highlight(p.Box(250), u.ToString(), p.s, u.Owner.Colors);
			
				GUI.Box(p.Box(iconSize), Icons.IN(), p.s);
				p.x2 += 5;
				GUI.Label(p.Box(30), u.IN+"", p.s);
				
				if (u.FP > 0) {
					GUI.Box(p.Box(iconSize), Icons.FP(), p.s);
					p.x2 += 5;
					GUI.Label(p.Box(30), u.FP+"", p.s);
				}
				else {p.x2 += 65;}
	
				GUI.Box(p.Box(iconSize), Icons.HP(), p.s);
				p.x2 += 5;
				GUI.Label (p.Box(90), u.HPString, p.s);
				
				if (u.DEF > 0) {
					GUI.Box(p.Box(iconSize), Icons.DEF(), p.s);
					p.x2 += 5;
					GUI.Label(p.Box(30), u.DEF+"", p.s);
				}
				
				if (u.COR > 0) {
					GUI.Box(p.Box(iconSize), Icons.COR(), p.s);
					p.x2 += 5;
					GUI.Label(p.Box(30), u.COR+"", p.s);}
				
				if (u.IsStunned()) {
					GUI.Box(p.Box(iconSize), Icons.STUN(), p.s);
					p.x2 += 5;
					GUI.Label(p.Box(30), u.STUN+"", p.s);
				}
				else if (u.IsSkipped()) {GUI.Label(p.Box(100), "Skipped!", p.s);}
				
				if (p.x2 > internalW) {internalW = p.x2;}
				p.NextLine();
			}
		GUI.EndScrollView();
	}
}

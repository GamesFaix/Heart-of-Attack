using UnityEngine;
using System;
using System.Collections.Generic;
using HOA;

public class GUITools : MonoBehaviour {
	
	int toolView = 0;
	string[] toolLabels = new string[6] {"Create", "Move", "Kill", "Stats", "Queue", "Dice"};

	public void Display (Panel p){
		GUI.DrawTexture(p.FullBox, ImageLoader.wood[1], ScaleMode.StretchToFill);
		float btnW = p.W / toolLabels.Length;
		
		for (int i=0; i<toolLabels.Length; i++) {
			if (GUI.Button(p.Box(btnW), toolLabels[i])) {
				GUISelectors.Reset();
				toolView = i;
			}
		}		
		
		p.NextLine();
		p.y2 += 5;

		float panelH = (p.H-p.LineH)/p.H;
		Panel subPanel = new Panel (p.TallBox(panelH), p.LineH, p.s);
		
		
		switch(toolView){
			case 0: GUIToolCreate.Display(subPanel); break;
			case 1: GUIToolMove.Display(subPanel); break;
			case 2: GUIToolKill.Display(subPanel); break;
			case 3: GUIToolStats.Display(subPanel); break;
			case 4: GUIToolQueue.Display(subPanel); break;
			case 5: GUIToolRandom.Display(subPanel); break;
			default: break;
		}
	}
}

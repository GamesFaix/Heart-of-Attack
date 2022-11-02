﻿using UnityEngine;
using System;
using System.Collections.Generic;
using HOA.Tokens;
using HOA.Players;

public static class GUIToolQueue {
	
	static string magString = "";
	static bool viewShift = false;
	
	public static void Display (Panel p) {
		GUI.Box(p.FullBox,"");
		
		float btnW = 0.25f;

		if (GUI.Button(p.Box(btnW), "Advance")
			|| (Input.GetKeyUp("a") && Input.GetKey("left ctrl"))) {
			InputBuffer.Submit(new RQueueAdvance(Source.ActivePlayer));
		}
		if (GUI.Button(p.Box(btnW), "Shuffle")){
			InputBuffer.Submit(new RQueueShuffle(Source.ActivePlayer));
		}
		if (GUI.Button(p.Box(btnW), "Shift")){viewShift = !viewShift;}

		if (viewShift){
			p.NextLine();
			p.y2 += 5;
			p.x2 += p.W*0.2f;
			GUI.Label(p.Box(0.2f), "Distance: ");
				
			p.x2 += 5;
			magString = GUI.TextField (p.Box(0.1f), magString, 2);
			
			int magnitude = 0;
			if (magString != ""){Int32.TryParse(magString, out magnitude);}
			p.x2 += p.W*0.1f ;
			
			GUI.Label(p.Box(0.3f), "(+ Up / - Down)");
			
			
			
			p.NextLine();
			p.x2 += p.W*0.4f;
			p.y2 += 10;
			GUI.Label(p.Box(0.4f), "Select unit:");
			p.NextLine();
			
			float instanceH = (p.H-2*p.LineH) / p.H;
			Panel subPanel = new Panel(p.TallBox(instanceH), p.LineH, p.s);
			GUISelectors.InstanceGrid(subPanel);
			Token instance = GUISelectors.Instance();
			
			string printInst = "";
			if (instance != default(Token)) {printInst = instance.FullName;}
			
			if (GUI.Button(p.LineBox, "Shift "+printInst+" "+magnitude)
			|| Input.GetKeyUp("space")){
				InputBuffer.Submit(new RQueueShift(Source.ActivePlayer, instance, magnitude));
				GUISelectors.Reset();
			}
		}
	}
}

using System;
using System.Collections.Generic;
using UnityEngine;
using HOA.Tokens;
using HOA.Players;

public static class GUIToolStats {

	static int statBtn = 0;
	static string[] statLabels = new string[8] {"HP", "MHP", "DEF", "IN", "AP", "FP", "STUN", "COR"};
	static int signBtn = 0;
	static string[] signLabels = new string[3] {"=","+","-"};
	static string magString="";
	
	public static void Display (Panel p) {
		
		if (Input.GetKey("left ctrl") && Input.GetKey("f")) {
			Unit u = TurnQueue.Top;
			GUISelectors.SelectInstance(u);
			u.Focus();		
		}
		
		GUI.Box(p.FullBox(),"");
		
		p.y2 += 5;
		p.x2 += p.W()*0.35f;
		GUI.Label(p.Box(0.4f), "Select property:");
		p.NextLine();
		
		statBtn = GUI.SelectionGrid(p.LineBox(), statBtn, statLabels, 8);
		STAT stat = (STAT)statBtn;
		
		p.y2 += 5;
		
		GUI.Label(p.Box(0.3f), "Select change:");
		signBtn = GUI.Toolbar(p.Box(0.3f), signBtn, signLabels);
		
		p.x2 += 5;
		magString = GUI.TextField (p.Box(0.1f), magString, 3);
		int magnitude = 0;
		if (magString != ""){Int32.TryParse(magString, out magnitude);}
		
		p.NextLine();
		
		p.y2 += 5;
		p.x2 += p.W()*0.4f;
		GUI.Label(p.Box(0.4f), "Select unit:");
		p.NextLine();
		
		Panel subPanel = new Panel(p.TallBox(0.5f), p.LineH(), p.s);
		GUISelectors.InstanceGrid(subPanel);
		Token instance = GUISelectors.Instance();
		
		string label = "";
		if (instance != default(Token)) {label += instance.FullName();}
		label += " "+statLabels[statBtn]+" "+signLabels[signBtn]+" "+magnitude;
	
		if (GUI.Button(p.LineBox(), label) || Input.GetKeyUp("space")){
			if (signBtn == 0) {InputBuffer.Submit(new RSetStat(Source.ActivePlayer(), instance, stat, magnitude));}
			if (signBtn == 1) {InputBuffer.Submit(new RAddStat(Source.ActivePlayer(), instance, stat, magnitude));}
			if (signBtn == 2) {InputBuffer.Submit(new RAddStat(Source.ActivePlayer(), instance, stat, 0-magnitude));}
			GUISelectors.Reset();
		}
		
		
	}
}

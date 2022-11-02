using System;
using System.Collections.Generic;
using UnityEngine;

namespace HOA {

	public static class GUIToolStats {

		static int statBtn = 0;
		static string[] statLabels = new string[8] {"HP", "MHP", "DEF", "IN", "AP", "FP", "STUN", "COR"};
		static int signBtn = 0;
		static string[] signLabels = new string[3] {"=","+","-"};
		static string magString="";
		
		public static void Display (Panel p) {

			GUI.Box(p.FullBox,"");
			
			p.y2 += 5;
			p.x2 += p.W*0.35f;
			GUI.Label(p.Box(0.4f), "Select property:");
			p.NextLine();
			
			statBtn = GUI.SelectionGrid(p.LineBox, statBtn, statLabels, 8);
			EStat stat = (EStat)statBtn;
			
			p.y2 += 5;
			
			GUI.Label(p.Box(0.3f), "Select change:");
			signBtn = GUI.Toolbar(p.Box(0.3f), signBtn, signLabels);
			
			p.x2 += 5;
			magString = GUI.TextField (p.Box(0.1f), magString, 3);
			int magnitude = 0;
			if (magString != ""){Int32.TryParse(magString, out magnitude);}
			
			p.NextLine();
			
			p.y2 += 5;
			p.x2 += p.W*0.4f;
			GUI.Label(p.Box(0.4f), "Select unit:");
			p.NextLine();
			
			Panel subPanel = new Panel(p.TallBox(0.5f), p.LineH, p.s);
			GUISelectors.InstanceGrid(subPanel);

			Token instance = GUISelectors.Instance;
			
			string label = "";
			if (instance != default(Token)) {label += instance.FullName;}
			label += " "+statLabels[statBtn]+" "+signLabels[signBtn]+" "+magnitude;
		
			if (GUI.Button(p.LineBox, label) || Input.GetKeyUp("space")){
				if (signBtn == 0) {EffectQueue.Add(new ESetStat(Source.ActivePlayer, (Unit)instance, stat, magnitude));}
           		if (signBtn == 1) {EffectQueue.Add(new EAddStat(Source.ActivePlayer, (Unit)instance, stat, magnitude));}
                if (signBtn == 2) {EffectQueue.Add(new EAddStat(Source.ActivePlayer, (Unit)instance, stat, 0-magnitude));}
				GUISelectors.Reset();
			}
			
		}
	}
}

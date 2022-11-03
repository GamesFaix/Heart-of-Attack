using UnityEngine; 
using System;

namespace HOA { 

	public static class GUICheats {
	
		static float btnW = 150;
		static float btnH = 30;
		static bool showCreate = false;
		static bool showStats = false;
		static bool showQueue = false;
		static bool showOwner = false;
		
		public static void Display (Panel p) {
			GUI.DrawTexture(p.FullBox, ImageLoader.wood[1], ScaleMode.StretchToFill);

			p.NextLine();

			Panel subPanel = new Panel(new Rect(p.x2+btnW, p.y2, p.W-btnW, p.H-p.LineH), p.LineH, p.s);

			if (GUI.Button(p.Box(btnW), "Kill")) {
				GUIMaster.PlaySound(EGUISound.CLICK);
				Reset();
				Targeter.Start(new Actions.ManualKill());
			}

			p.NextLine();
			if (GUI.Button(p.Box(btnW), "Create")) {
				GUIMaster.PlaySound(EGUISound.CLICK);
				Reset();
				showCreate = true;
			}
			if (showCreate) {TokenList(subPanel);}

			p.NextLine();
			if (GUI.Button(p.Box(btnW), "Move")) {
				GUIMaster.PlaySound(EGUISound.CLICK);
				Reset();
				Targeter.Start(new Actions.ManualMove());
			}

			p.NextLine();
			if (GUI.Button(p.Box(btnW), "Stats")) {
				GUIMaster.PlaySound(EGUISound.CLICK);
				Reset();
				showStats = true;
			}
			if (showStats) {StatMenu(subPanel);}

			p.NextLine();
			if (GUI.Button(p.Box(btnW), "End Turn")) {
				GUIMaster.PlaySound(EGUISound.CLICK);
				Reset();
				Targeter.Start(new Actions.ManualEnd());
			}

			p.NextLine();
			if (GUI.Button(p.Box(btnW), "Queue Shift")) {
				GUIMaster.PlaySound(EGUISound.CLICK);
				Reset();
				showQueue = true;
			}
			if (showQueue) {QueueMenu(subPanel);}

			p.NextLine();
			if (GUI.Button(p.Box(btnW), "Set Owner")) {
				GUIMaster.PlaySound(EGUISound.CLICK);
				Reset();
				showOwner = true;
			}
			if (showOwner) {OwnerMenu(subPanel);}



			p.NextLine();
			p.NextLine();
			if (Targeter.Pending != default(Task)) {
				p.NudgeX(); p.NudgeY();
				GUI.Label(p.TallWideBox(7), "Pending: \n"+Targeter.PendingString());
				
				//p.NudgeX();
				if (GUI.Button(p.Box(btnW), "Execute") || Input.GetKeyUp("space")) {
					Targeter.Execute();
					GUIMaster.PlaySound(EGUISound.CLICK);
				}
				p.NextLine();
				if (GUI.Button(p.Box(btnW), "Cancel") || Input.GetKeyUp("escape")) {
					Targeter.Reset();
					GUIMaster.PlaySound(EGUISound.CLICK);
				}
			}
		}

		static Vector2 tokenScroll = new Vector2 (0,0);
		
		static void TokenList (Panel p){
			GUI.Box (p.FullBox, "");
			
			float internalW = btnW;
			float internalH = btnH * (FactionRef.Count + Enum.GetValues(typeof(EToken)).Length);

			tokenScroll = GUI.BeginScrollView (p.FullBox, tokenScroll, new Rect(p.X, p.Y, internalW, internalH));
			
			for (int i=0; i<FactionRef.Count; i++){
				Faction faction = FactionRef.Index(i);

				GUI.Label (p.LineBox, faction.Name, p.s);
				for (int j=0; j<faction.Count; j++){
					if (GUI.Button (new Rect(p.x2, p.y2, p.W, btnH), "")) {
						GUIMaster.PlaySound(EGUISound.CLICK);
						Targeter.Start(new Actions.ManualCreate (TurnQueue.Top, faction[j]));
					}
					GUI.Box (p.Box(btnH), Thumbs.CodeToThumb(faction[j]));
					GUI.Label (p.Box(p.W-btnH), TokenFactory.Template(faction[j]).ID.Name);
					p.NextLine();
				}
			}
			GUI.EndScrollView();
		}

		static string[] statLabels = new string[7] {"HP", "MHP", "DEF", "IN", "Energy", "Focus", "Stun"};
		static string[] signLabels = new string[3] {"=","+","-"};
		static int statBtn = 0;
		static int signBtn = 1;
		static string statString = "0";
		static int statValue = 0;

		static void StatMenu (Panel p) {
			float btnW = 50;

			for (int i=0; i<statLabels.Length; i++) {
				if (GUI.Button(p.Box(btnW), statLabels[i])) {
					GUIMaster.PlaySound(EGUISound.CLICK);
					statBtn = i;
				}
				p.NextLine();
			}
			EStat stat = (EStat)statBtn;

			p.ResetY();
			float x3 = p.x2 + btnW;
			for (int i=0; i<signLabels.Length; i++) {
				p.x2 = x3;
				if (GUI.Button(p.Box(btnW), signLabels[i])) {
					GUIMaster.PlaySound(EGUISound.CLICK);
					signBtn = i;
				}
				p.NextLine();
			}

			p.ResetY();
			x3 += btnW;
			p.x2 = x3;
			statString = GUI.TextField (p.Box(btnW), statString, 3);
			if (statString != ""){Int32.TryParse(statString, out statValue);}

			p.ResetY();
			x3 += btnW;
			p.x2 = x3;
			if (GUI.Button(p.Box(btnW*2), "Select Tokens")) {
				GUIMaster.PlaySound(EGUISound.CLICK);
				if (signBtn == 0) {Targeter.Start(new Actions.ManualSet(stat, statValue));}
				else if (signBtn == 1) {Targeter.Start(new Actions.ManualAdd(stat, statValue));}
				else {Targeter.Start(new Actions.ManualAdd(stat, 0-statValue));}
			}
		}

		static string queueString = "0";
		static int queueValue = 0;

		static void QueueMenu (Panel p) {
			float btnW = 50;

			if (GUI.Button(p.Box(btnW), "+")) {
				GUIMaster.PlaySound(EGUISound.CLICK);
				signBtn = 1;
			}
			p.NextLine();
			if (GUI.Button(p.Box(btnW), "-")) {
				GUIMaster.PlaySound(EGUISound.CLICK);
				signBtn = 2;
			}

			p.ResetY();
			float x3 = p.x2;
			queueString = GUI.TextField (p.Box(btnW), queueString, 3);
			if (queueString != ""){Int32.TryParse(queueString, out queueValue);}

			p.ResetY();
			x3 += btnW;
			p.x2 = x3;
			if (GUI.Button(p.Box(btnW*2), "Select Tokens")) {
				GUIMaster.PlaySound(EGUISound.CLICK);
				if (signBtn == 1) {Targeter.Start(new Actions.ManualShift(queueValue));}
				else if (signBtn == 2) {Targeter.Start(new Actions.ManualShift(0-queueValue));}
			}
		}

		static Player newOwner;

		static void OwnerMenu (Panel p) {
			float btnW = 100;

			foreach (Player player in Roster.Players(true)) {
				if (GUI.Button(p.Box(btnW), player.ToString())) {newOwner = player;}
				p.NextLine();
			}
			p.ResetY();
			p.x2 += btnW;
			if (GUI.Button(p.Box(btnW), "Select Tokens")) {
				GUIMaster.PlaySound(EGUISound.CLICK);
				Targeter.Start(new Actions.ManualOwner(newOwner));
			}
		}


		static void Reset () {
			showCreate = false;
			showStats = false;
			statBtn = 0;
			signBtn = 1;
			statString = "0";
			statValue = 0;
			showQueue = false;
			queueString = "0";
			queueValue = 0;
			showOwner = false;
			newOwner = null;
			Targeter.Reset();
		}
	}
}

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
			GUI.DrawTexture(p.FullBox, Textures.Backgrounds.WoodLarge, ScaleMode.StretchToFill);

			p.NextLine();

			Panel subPanel = new Panel(new Rect(p.x2+btnW, p.y2, p.W-btnW, p.H-p.LineH), p.LineH, p.s);

			if (GUI.Button(p.Box(btnW), "Kill")) {
				GUIMaster.PlaySound(GUISounds.Click);
				Reset();
				Targeter.Start(Ability.ManualDestroy());
			}

			p.NextLine();
			if (GUI.Button(p.Box(btnW), "Create")) {
				GUIMaster.PlaySound(GUISounds.Click);
				Reset();
				showCreate = true;
			}
			if (showCreate) {TokenList(subPanel);}

			p.NextLine();
			if (GUI.Button(p.Box(btnW), "Move")) {
				GUIMaster.PlaySound(GUISounds.Click);
				Reset();
				Targeter.Start(Ability.ManualMove());
			}

			p.NextLine();
			if (GUI.Button(p.Box(btnW), "Stats")) {
				GUIMaster.PlaySound(GUISounds.Click);
				Reset();
				showStats = true;
			}
			if (showStats) {StatMenu(subPanel);}

			p.NextLine();
			if (GUI.Button(p.Box(btnW), "End Turn")) {
				GUIMaster.PlaySound(GUISounds.Click);
				Reset();
				Targeter.Start(Ability.ManualEnd());
			}

			p.NextLine();
			if (GUI.Button(p.Box(btnW), "Queue Shift")) {
				GUIMaster.PlaySound(GUISounds.Click);
				Reset();
				showQueue = true;
			}
			if (showQueue) {QueueMenu(subPanel);}

			p.NextLine();
			if (GUI.Button(p.Box(btnW), "Set Owner")) {
				GUIMaster.PlaySound(GUISounds.Click);
				Reset();
				showOwner = true;
			}
			if (showOwner) {OwnerMenu(subPanel);}



			p.NextLine();
			p.NextLine();
			if (Targeter.Pending != default(Ability)) {
				p.NudgeX(); p.NudgeY();
				GUI.Label(p.TallWideBox(7), "Pending: \n"+Targeter.PendingString());
				
				//p.NudgeX();
				if (GUI.Button(p.Box(btnW), "Execute") || Input.GetKeyUp("space")) {
					Targeter.Execute();
					GUIMaster.PlaySound(GUISounds.Click);
				}
				p.NextLine();
				if (GUI.Button(p.Box(btnW), "Cancel") || Input.GetKeyUp("escape")) {
					Targeter.Reset();
					GUIMaster.PlaySound(GUISounds.Click);
				}
			}
		}

		static Vector2 tokenScroll = new Vector2 (0,0);
		
		static void TokenList (Panel p){
			GUI.Box (p.FullBox, "");
			
			float internalW = btnW;
			float internalH = btnH * (FactionRegistry.Factions.Count + Enum.GetValues(typeof(Species)).Length);

			tokenScroll = GUI.BeginScrollView (p.FullBox, tokenScroll, new Rect(p.X, p.Y, internalW, internalH));

            foreach (Faction faction in FactionRegistry.Factions.Values)
            {
             	GUI.Label (p.LineBox, faction.Name, p.s);
				for (int j=0; j<faction.Species.Count; j++){
					if (GUI.Button (new Rect(p.x2, p.y2, p.W, btnH), "")) {
						GUIMaster.PlaySound(GUISounds.Click);
						Targeter.Start(Ability.ManualCreate (TurnQueue.Top, faction.Species[j]));
					}
					GUI.Box (p.Box(btnH), Textures.TokenThumbnails.BySpecies(faction.Species[j]));
					GUI.Label (p.Box(p.W-btnH), TokenRegistry.Templates[faction.Species[j]].ID.Name);
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
					GUIMaster.PlaySound(GUISounds.Click);
					statBtn = i;
				}
				p.NextLine();
			}
			Stats stat = (Stats)statBtn;

			p.ResetY();
			float x3 = p.x2 + btnW;
			for (int i=0; i<signLabels.Length; i++) {
				p.x2 = x3;
				if (GUI.Button(p.Box(btnW), signLabels[i])) {
					GUIMaster.PlaySound(GUISounds.Click);
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
				GUIMaster.PlaySound(GUISounds.Click);
				if (signBtn == 0) {Targeter.Start(Ability.ManualSet(stat, statValue));}
				else if (signBtn == 1) {Targeter.Start(Ability.ManualAdd(stat, statValue));}
				else {Targeter.Start(Ability.ManualAdd(stat, 0-statValue));}
			}
		}

		static string queueString = "0";
		static int queueValue = 0;

		static void QueueMenu (Panel p) {
			float btnW = 50;

			if (GUI.Button(p.Box(btnW), "+")) {
				GUIMaster.PlaySound(GUISounds.Click);
				signBtn = 1;
			}
			p.NextLine();
			if (GUI.Button(p.Box(btnW), "-")) {
				GUIMaster.PlaySound(GUISounds.Click);
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
				GUIMaster.PlaySound(GUISounds.Click);
				if (signBtn == 1) {Targeter.Start(Ability.ManualShift(queueValue));}
				else if (signBtn == 2) {Targeter.Start(Ability.ManualShift(0-queueValue));}
			}
		}

		static Player newOwner;

		static void OwnerMenu (Panel p) {
			float btnW = 100;

			foreach (Player player in Roster.PlayersWithNeutral) {
				if (GUI.Button(p.Box(btnW), player.ToString())) {newOwner = player;}
				p.NextLine();
			}
			p.ResetY();
			p.x2 += btnW;
			if (GUI.Button(p.Box(btnW), "Select Tokens")) {
				GUIMaster.PlaySound(GUISounds.Click);
				Targeter.Start(Ability.ManualOwner(newOwner));
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

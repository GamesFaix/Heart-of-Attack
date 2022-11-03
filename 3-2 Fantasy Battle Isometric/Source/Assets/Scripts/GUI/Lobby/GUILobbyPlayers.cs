using UnityEngine;
using HOA;
using System.Collections.Generic;


public static class GUILobbyPlayers {

	public static void Display (Panel p) {
		/*fix next line!!!!!!!!!!*/
		if (Roster.Count() < /*Board.MaxPlayers()*/8) {
			if (GUI.Button (p.LineBox, "Add player")) {
				Roster.Add(new Player(Roster.Count()));
			}
		}
		else {GUI.Label(p.LineBox, "Roster full.");}
		p.y2+=5;
		
		for (int i=0; i<Roster.Count(); i++) {
			Player player = Roster.Players()[i];
			string name = GUI.TextField(p.Box(0.4f), player.ToString());	
			player.Rename(name);
			if (GUI.Button(p.Box(0.2f), "Delete")) {
				Roster.Remove(player);
			}
			string printFaction;
			if (player.Faction == default(Faction)) {printFaction = "Select faction";}
			else {printFaction = player.Faction.ToString();}
			if (GUI.Button(p.Box(0.4f), printFaction)) {selectee = player;}			
			p.NextLine();
		}
		
		p.y2 = p.Y + 9.5f*p.LineH;
		
		if (selectee != default(Player)){
			FactionSelector(new Panel (p.TallBox(3), p.LineH, p.s), selectee);
		}
		
		bool ready = true;
		for (int i=1; i<Roster.Count(); i++) {
			Player player = Roster.Index(i);
			if (player.Faction == default(Faction)) {ready = false;}
		}
		
		p.y2 += 5;
		if (Roster.Count() > 1) {
		
			if (ready) {
				if (GUI.Button(p.LineBox, "Start game")
				|| Input.GetKeyUp("space")) {
					Game.Start();
				}
			}
			else {
				p.x2 += 5;
				GUI.Label(p.Box(0.6f), "Not all players have selected factions.");
				p.x2 -= 5;
				if (GUI.Button(p.Box(0.4f), "Force random")
				|| Input.GetKeyUp("space")) {
					Roster.ForceRandomFactions();
				}
			}
		}
		else {
			p.x2 += 5;
			GUI.Label(p.Box(0.8f), "Other players required.");
		}
	}
	
	static Player selectee;
	static int selected;
	
	static void FactionSelector (Panel p, Player player) {
		p.x2 += p.W*0.2f;
		GUI.Label(p.Box(0.6f), player.ToString()+", select faction:");
		p.NextLine();
		
		if (selectee != default(Player)) {
			for (int i=0; i<4; i++){
				if (i < FactionRef.Free.Count){
					if (GUI.Button(p.Box(0.25f), FactionRef.FreeNames[i])) {
						Roster.AssignFaction(selectee, FactionRef.Free[i]);
						selectee = default(Player);
					}
				}
			}
			p.NextLine();
			for (int i=4; i<8; i++){
				if (i < FactionRef.Free.Count){
					if (GUI.Button(p.Box(0.25f), FactionRef.FreeNames[i])) {
						Roster.AssignFaction(selectee, FactionRef.Free[i]);
						selectee = default(Player);
					}
				}
			}
		}
	}
}

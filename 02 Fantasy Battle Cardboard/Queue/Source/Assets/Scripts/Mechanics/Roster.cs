using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Roster {
	static bool[] active = new bool[9];
	static string[] pNames = new string[9] {"Neutral","P1","P2","P3","P4","P5","P6","P7","P8"};

	public static void Activate(int p){
		if (p>0 && p<=8) {active[p]=true;}
		else {GameLog.Debug("Roster: Attempt to activate invalid player number.");}
	}
	public static void Deactivate(int p){
		if (p>0 && p<=8) {active[p]=false;}
		else {GameLog.Debug("Roster: Attempt to deactivate invalid player number.");}
	}

	public static void Reset(){
		for (int i=0; i<=8; i++) {active[i] = false;}
	}

	public static int Count(){
		int count = 0;
		for (int i=1; i<=8; i++){
			if (active[i]) {count++;}
		}
		return count;
	}

	public static int[] Active(){
		int[] players = new int[Count()];
		int last = 1;

		//iterate through each slot in players
		for (int i=0; i<players.Length; i++){
			//iterate through potential factions
			for (int j=last; j<=8; j++){

				if (active[j]) {
					players[i]=j;
					last = j+1;
					break;
				}
			}
		}
		return players;
	}

	public static List<Unit> OwnedUnits(int player){
		List<Unit> owned = new List<Unit>();
		foreach (Unit u in TurnQueue.units){
			if (u.Owner() == player){
				owned.Add(u);
			}
		}
		return owned;
	}

	public static string Name (int p){
		if (p>0 && p<=8) {return pNames[p];}
		else {
			GameLog.Debug("Roster: Attempt to get name from invalid player number.");
			return "";
		}
	}

	public static void Capture(int captive, int captor){
		if (captor>0 && captor<=8 && captive>0 && captive<=8) {
			foreach (Unit u in OwnedUnits(captive)){
				u.SetOwner(captor);
			}
		}
		else {GameLog.Debug("Roster: Attempt to capture invalid team.");}


	}
}

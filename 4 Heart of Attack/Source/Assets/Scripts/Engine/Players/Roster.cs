using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HOA {

	public static class Roster {
		static Player neutral = Player.Neutral;
		static List<Player> players = new List<Player>();

		//static int max = 8;
		
		public static void Reset () {
			FactionRef.ReleaseAll();
			players = new List<Player>();
		}
		
		public static void Add (Player player) {
			if (players.Count < 8) {
				if (!Names.Contains(player.ToString())) {
					players.Add(player);
					GameLog.Debug("Roster: Added "+player.ToString());
				}
				else {GameLog.Debug("Roster: Duplicate player names illegal.");}
			}
			else {GameLog.Debug("Roster: Full, cannot add player.");}
		}
		
		public static void Remove (Player p) {
			if (players.Contains(p)) {
				FactionRef.Release(p.Faction);
			//	players[0].Capture(p);
				players.Remove(p);
			}
			else {GameLog.Debug("Roster: Does not contain player, cannot remove.");}
		}
		
	/*	public static bool IsFull () {
			if (players.Count >= MapFactory.MaxPlayers()) {return true;}
			return false;
		}
	*/
        public static bool Contains(Player p) { return players.Contains(p); }
	
		public static int Count (bool addNeutral=false) {
			if (!addNeutral) {return players.Count;} 
			return players.Count+1;
		}
		
		public static int LargestTeamSize { 
			get {
				int largest = 0;
				foreach (Player p in players) 
					if (p.Tokens.Count > largest) largest = p.Tokens.Count;
				return largest;
			}
		}

		public static List<string> Names {
			get {
				List<string> names = new List<string>();
				foreach (Player p in players) {names.Add(p.ToString());}
				return names;
			}
		}
		
		public static List<Player> Players (bool addNeutral=false) {
			if (!addNeutral) {return players;}
			else {
				List<Player> withNeutral = new List<Player>();
				foreach (Player p in players) {withNeutral.Add(p);}
				withNeutral.Add(neutral);
				return withNeutral;
			}
		}
		
		public static Player Index (int n) {return players[n];}

		public static Player Neutral {get {return neutral;} }

		public static void AssignFaction (Player p, Faction f) {
			//Debug.Log("assigning faction "+f.ToString()+" to "+p.ToString());
			p.Faction = f;
			FactionRef.Take(f);
			//Debug.Log(p.ToString()+"'s faction is now "+p.Faction.ToString());
			//Debug.Log(p.ToString()+"'s king is now "+p.King.ToString());
		}
		
		public static void ForceRandomFactions () {
			foreach (Player p in Players()) {
				if (p.Faction == default(Faction)) {
					AssignFaction(p, FactionRef.RandomFree);
				}
			}
		}
		
	}
}

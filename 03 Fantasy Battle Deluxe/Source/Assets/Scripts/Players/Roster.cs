using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HOA.Tokens;

namespace HOA.Players {

	public static class Roster {
		static Player neutral = Player.Neutral;
		static List<Player> players = new List<Player>();

		static int max = 8;
		
		public static void Reset () {
			New(8);
			FactionRef.ReleaseAll();
		}
		
		public static void New (int n=8) {
			if (n > 1 && n <= 8) {
				players = new List<Player>();
				max = n;
				FactionRef.ReleaseAll();
				GameLog.Debug("Roster: New "+(max)+"-player roster created.");
			}
			else {GameLog.Debug("Roster: Must have 1 to 8 players.");}
		}
		
		public static void Add (Player player) {
			if (players.Count <= max) {
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
				players[0].Capture(p);
				players.Remove(p);
			}
			else {GameLog.Debug("Roster: Does not contain player, cannot remove.");}
		}
		
		public static bool IsFull () {
			if (players.Count >= max) {return true;}
			return false;
		}
		
		public static bool Contains (Player p) {
			if (players.Contains(p)) {return true;}
			return false;
		}
	
		public static int Count {get {return players.Count;} }
		
		public static int LargestTeamSize { 
			get {
				int largest = 0;
				foreach (Player p in players) {
					if (p.OwnedUnits.Count > largest) {largest = p.OwnedUnits.Count;}
				}
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
				List<Player> withNeutral = players;
				withNeutral.Add(neutral);
				return withNeutral;
			}
		}
		
		public static Player Index (int n) {return players[n];}

		public static Player Neutral {get {return neutral;} }

		public static void AssignFaction (Player p, Faction f) {
			p.Faction = f;
			FactionRef.Take(f);
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

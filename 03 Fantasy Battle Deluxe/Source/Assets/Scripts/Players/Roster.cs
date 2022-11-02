using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HOA.Tokens;

namespace HOA.Players {

	public static class Roster {
		static Player neutral = new Player("Neutral");
		static List<Player> players = new List<Player>();

		static int max = 8;
		
		public static void Reset () {
			New(8);
			neutral = new Player("Neutral");
			neutral.SetFaction(FactionRef.Faction(FactionRef.Count()));
			takenFactions = new List<Faction>();
		}
		
		public static void New (int n=8) {
			if (n > 1 && n <= 8) {
				players = new List<Player>();
				neutral = new Player("Neutral");
				neutral.SetFaction(FactionRef.Faction(FactionRef.Count()));
				max = n;
				takenFactions = new List<Faction>();
				GameLog.Debug("Roster: New "+(max)+"-player roster created.");
			}
			else {GameLog.Debug("Roster: Must have 1 to 8 players.");}
		}
		
		public static void Add (Player player) {
			if (players.Count <= max) {
				if (!Names().Contains(player.ToString())) {
					players.Add(player);
					GameLog.Debug("Roster: Added "+player.ToString());
				}
				else {GameLog.Debug("Roster: Duplicate player names illegal.");}
			}
			else {GameLog.Debug("Roster: Full, cannot add player.");}
		}
		
		public static void Remove (Player p) {
			if (players.Contains(p)) {
				ReleaseFaction(p.Faction());
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
	
		public static int Count () {return players.Count;}
		
		public static int LargestTeamSize () {
			int largest = 0;
			foreach (Player p in players) {
				if (p.OwnedUnits().Count > largest) {largest = p.OwnedUnits().Count;}
			}
			return largest;
		}
	
		
		public static List<string> Names () {
			List<string> names = new List<string>();
			foreach (Player p in players) {names.Add(p.ToString());}
			return names;
		}
		
		public static List<Player> Players (bool addNeutral=false) {
			if (!addNeutral) {return players;}
			else {
				List<Player> withNeutral = new List<Player>();
				foreach (Player player in players) {withNeutral.Add(player);}
				withNeutral.Add(neutral);
				return withNeutral;
			}
		}
		
		public static Player Player (int n) {
			if (n>=0 && n<Count()) {return players[n];}
			GameLog.Debug("Roster: Attempt to acess invalid player.");
			return default(Player);	
		}
		
		public static Player Neutral () {return neutral;}
		
		public static Player StringToPlayer (string name) {
			for (int i=0; i<players.Count; i++) {
				if (Names()[i] == name) {return players[i];}
			}
			GameLog.Debug("Roster: StringToPlayer invalid string.");
			return default(Player);		
		}
		
		static List<Faction> takenFactions = new List<Faction>();
		public static List<Faction> TakenFactions () {return takenFactions;}
		public static void TakeFaction(Faction f) {takenFactions.Add(f);}
		public static void ReleaseFaction(Faction f) {takenFactions.Remove(f);}
			
		
		public static List<Faction> FreeFactions () {
			List<Faction> factions = FactionRef.Playable();
			List<Faction> free = new List<Faction>();
			foreach (Faction f in factions) {
				if (!takenFactions.Contains(f)) {free.Add(f);}
			}
			return free;
		}
		
		public static string[] FreeFactionNames () {
			string[] names = new string[FreeFactions().Count];
			for (int i=0; i<names.Length; i++) {
				names[i] = FreeFactions()[i].ToString();
			}
			return names;	
		}
		
		public static void AssignFaction (Player p, Faction f) {
			ReleaseFaction(p.Faction());
			p.SetFaction(f);
			TakeFaction(f);
		}
		
		public static Faction RandomFreeFaction () {
			int random = Mathf.RoundToInt(Random.Range(0, FreeFactions().Count));
			return FreeFactions()[random];
			
		}
		
		public static void ForceRandomFactions () {
			for (int i=1; i<Players().Count; i++) {
				Player p = Player(i);
				if (p.Faction() == default(Faction)) {
					AssignFaction(p, RandomFreeFaction());
				}
				
			}
		}
		
	}
}

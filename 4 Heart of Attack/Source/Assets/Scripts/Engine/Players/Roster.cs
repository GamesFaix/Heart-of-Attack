using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HOA 
{

	public static class Roster 
    {
        public static Player Neutral { get; private set; }
        public static List<Player> Players { get; private set; }
        public static List<Player> PlayersWithNeutral
        {
            get
            {
                List<Player> list = new List<Player>(Players);
                list.Add(Neutral);
                return list;
            }
        }

         public static void OnGameStart()
        {
            Neutral = new Player ("Neutral");
			Neutral.Faction = FactionRegistry.Factions[Factions.Neutral];
            Reset();
		}
		
		public static void Reset () {
			FactionRegistry.ReleaseAll();
			Players = new List<Player>();
		}
		
		public static void Add (Player player) {
			if (Players.Count < 8) {
				if (!Names.Contains(player.ToString())) {
					Players.Add(player);
					GameLog.Debug("Roster: Added "+player.ToString());
				}
				else {GameLog.Debug("Roster: Duplicate player names illegal.");}
			}
			else {GameLog.Debug("Roster: Full, cannot add player.");}
		}
		
		public static void Remove (Player p) {
			if (Players.Contains(p)) {
				FactionRegistry.Release(p.Faction.Factions);
				Players.Remove(p);
			}
			else {GameLog.Debug("Roster: Does not contain player, cannot remove.");}
		}
	
        public static bool Contains(Player p) { return Players.Contains(p); }
	
		public static int LargestTeamSize { 
			get {
				int largest = 0;
				foreach (Player p in Players) 
					if (p.Tokens.Count > largest) largest = p.Tokens.Count;
				return largest;
			}
		}

		public static List<string> Names {
			get {
				List<string> names = new List<string>();
				foreach (Player p in Players) {names.Add(p.ToString());}
				return names;
			}
		}
		
		public static Player Index (int n) {return Players[n];}

		public static void AssignFaction (Player p, Factions f) {
           p.Faction = FactionRegistry.Take(f);
		}
		
		public static void ForceRandomFactions () {
			foreach (Player p in Players) {
				if (p.Faction == default(Faction)) {
					AssignFaction(p, FactionRegistry.RandomFree);
				}
			}
		}
		
	}
}

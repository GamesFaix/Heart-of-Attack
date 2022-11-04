using UnityEngine;
using System.Collections.Generic;

namespace HOA {
	
	public class Player {
	
		string name;
		string ip;
		bool alive;
        public Faction Faction { get; set; }
		
		public bool Alive {
			get {return alive;}
		}
		
		public Player (int n, bool a=true) {
			name = defaultNames[n];
			alive = a;
		}
		
		public Player (string newName, bool a=true) {
			name = newName;
			alive = a;		
		}
		
		public void Kill () {
			if (alive) {alive = false;}
			else {GameLog.Debug(name+" is already dead.");}
		}
		
		public void Rename (string str) {name = str;}
		
		public override string ToString() {return name;}
		
		public TokenSet Tokens {
			get {
                TokenSet owned = new TokenSet();
				foreach (Token t in TokenRegistry.Tokens) 
                    if (t.Owner == this)
                        owned.Add(t);
				return owned;
			}
		}
		
		public void Capture (Player captive) {
			foreach (Token t in captive.Tokens) t.Owner = this;
		}
		
		public Species King {get {return Faction.King;} }
		public Color[] Colors {get {return Faction.Colors;} }
		
		static List<string> defaultNames = new List<string> {
			"DINGUS", 
            "CROMDOR", 
            "ELVIS", 
            "LOSER", 
			"BUTTERS", 
            "ATHEISMO", 
            "B4PH0M3T", 
            "SKIPPERT"
		};	

		public static Player Neutral {
			get {
				Player neutral = new Player ("Neutral");
				neutral.Faction = FactionRef.Index(FactionRef.Count-1);
				return neutral;
			}
		}

	}
}
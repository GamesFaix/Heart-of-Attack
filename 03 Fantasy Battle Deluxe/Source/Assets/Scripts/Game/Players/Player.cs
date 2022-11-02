using UnityEngine;
using System.Collections.Generic;

namespace HOA {
	
	public class Player {
	
		string name;
		string ip;
		bool alive;
		Faction faction;

		public bool Alive {
			get {return alive;}
		}
		
		public Player (int n, bool a=true) {
			name = defaultNames[n];
			//Debug.Log(name+" created");
			alive = a;
		}
		
		public Player (string newName, bool a=true) {
			name = newName;
			
		//	Debug.Log(name+" created");
			alive = a;		
		}
		
		public void Kill () {
			if (alive) {alive = false;}
			else {GameLog.Debug(name+" is already dead.");}
		}
		
		public void Rename (string str) {name = str;}
		
		public override string ToString() {return name;}
		
		public TokenGroup OwnedUnits {
			get {
				TokenGroup owned = new TokenGroup();
				foreach (Token t in TokenFactory.Tokens) {if (t.Owner == this){owned.Add(t);} }
				return owned;
			}
		}
		
		public void Capture (Player captive) {
			foreach (Token t in captive.OwnedUnits) {t.Owner = this;}
		}
		
		public Faction Faction {
			get {
	//			Debug.Log("faction: "+faction);
				return faction;
			}
			set {faction = value;}
		}

		public EToken King {get {
				//Debug.Log(name +" king get");
				return Faction.King;
			} }
		public Color[] Colors {get {return faction.Colors;} }
		
		static List<string> defaultNames = new List<string> {
			"DINGUS", "CROMDOR", "ELVIS", "LOSER", 
			"BUTTERS", "ATHEISMO", "B4PH0M3T", "SKIPPERT"
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
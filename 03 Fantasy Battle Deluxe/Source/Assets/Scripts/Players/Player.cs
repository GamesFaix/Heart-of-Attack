using HOA.Tokens;
using UnityEngine;
using System.Collections.Generic;

namespace HOA.Players {
	
	public class Player {
	
		string name;
		string ip;
		bool alive;
		Faction faction;
		
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
		
		public TokenGroup OwnedUnits (){
			TokenGroup owned = new TokenGroup();
			foreach (Token t in TokenFactory.Tokens()){
				if (t.Owner() == this){
					owned.Add(t);
				}
			}
			return owned;
		}
		
		public void Capture(Player captive){
			foreach (Token t in captive.OwnedUnits()){
				t.SetOwner(this);
			}
		}
		
		public void SetFaction(Faction f) {faction = f;}
		public Faction Faction() {return faction;}
		public TTYPE King () {return faction.King();}
		public Color[] Colors () {return faction.Colors();}	
		
		static List<string> defaultNames = new List<string> {
			"DINGUS", "CROMDOR", "ELVIS", "LOSER", 
			"BUTTERS", "ATHEISMO", "B4PH0M3T", "SKIPPERT"
		};
		
	}

}
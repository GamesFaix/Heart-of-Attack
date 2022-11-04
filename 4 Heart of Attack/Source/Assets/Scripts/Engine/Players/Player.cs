using UnityEngine;
using System.Collections.Generic;

namespace HOA 
{
	
	public class Player 
    {

        public string Name { get; set; }
        public string IP { get; private set; }
        public bool Alive { get; private set; }
        public Faction Faction { get; set; }
		
		public Player (int defaultNameNumber, bool alive=true) {
			Name = defaultNames[defaultNameNumber];
			Alive = alive;
		}
		
		public Player (string name, bool alive=true) {
			Name = name;
			Alive = alive;		
		}
		
		public void Kill () {
			if (Alive) 
                Alive = false;
			else 
                GameLog.Debug(Name+" is already dead.");
		}
		
		public override string ToString() {return Name;}
		
		public TokenSet Tokens 
        {
			get 
            { 
                return TokenRegistry.Tokens - TargetFilter.Owner(this, true); 
            }
		}
		
		public void Capture (Player captive) {
			foreach (Token t in captive.Tokens) 
                t.Owner = this;
		}
		
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
	}
}
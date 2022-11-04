using System.Collections.Generic;
using UnityEngine;

namespace HOA {

    public enum Factions : byte 
    { 
        Gearp, 
        Republic, 
        Torridale, 
        Grove, 
        Chrono, 
        Psycho, 
        Psilent, 
        Voidoid, 
        Neutral 
    }

	public partial class Faction
    {
        #region//Properties

        public string Name { get; private set; }
        public ListSet<Species> Species { get; private set; }
        public Species King { get; private set; }
        public Color[] Colors { get; private set; }
        public bool Playable { get; private set; }
        public Factions Factions { get; private set; }

        #endregion 

        private Faction(Factions factions, string name, Color color1, Color color2, bool playable = true) 
        {
            Factions = factions;
            Name = name;
            Colors = new Color[2] { color1, color2 };
            Playable = playable;
            Species = new ListSet<Species>();
        }

        public override string ToString () {return Name;}

        
	}
}
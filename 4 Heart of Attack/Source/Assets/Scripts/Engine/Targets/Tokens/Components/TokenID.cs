using System;
using System.Collections.Generic;
using UnityEngine;

namespace HOA {

	public class TokenID : TokenComponent 
    {
	    public string Name {get; private set;}
        public Species Species {get; private set;}
        public ushort Instance { get; private set; }
        public bool Unique { get; private set; }
        public Player Owner { get; set; }
        public bool Template { get; private set; }

        public string FullName {get {return Name + " " + Instance;} }
        public string SpeciesInst {get {return Species+" "+Instance;} }
        
		public TokenID (Source source, Token parent, Species species, 
            string name, bool unique=false, bool template=false)
            : base (parent)
        {
            Species = species;
            Name = name;
            Unique = unique;
            Template = template;

            if (Template)
            {
                Owner = Roster.Neutral;
                Instance = ushort.MaxValue;
            }
            else
            {
                Owner = source.Player;
                Instance = TokenRegistry.Add(Parent, Species);
            }
        }



        public override void Draw(Panel p) { InspectorInfo.TokenID(this, p); }

        public override string ToString() { return Parent + "'s ID"; }
	}
}

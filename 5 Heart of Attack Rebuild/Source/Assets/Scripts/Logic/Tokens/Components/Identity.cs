using System;
using System.Collections.Generic;
using UnityEngine; 

namespace HOA.Tokens 
{ 
    public class Identity : TokenComponent
	{
        public string Name { get; private set; }
        public Species Species { get; private set; }
        public int Instance { get; private set; }
        public Player Owner { get; set; }

        public Identity(IPlayer owner, Token thisToken, string name, Species species)
            : base (thisToken)
        {
            Name = name;
            Species = species;
            Instance = Session.Active.NextAvailableInstance(species);
            Owner = owner as Player;
        }
        
        public override string ToString() { return Name + " " + Instance; }
	}
}

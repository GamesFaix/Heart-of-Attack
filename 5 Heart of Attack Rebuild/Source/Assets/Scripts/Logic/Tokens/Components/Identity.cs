using System;
using System.Collections.Generic;
using HOA.Sessions;

namespace HOA.Tokens
{ 
    public class Identity : TokenComponent
	{
        public string Name { get; private set; }
        public Species Species { get; private set; }
        public int Instance { get; private set; }
        public Player Owner { get; set; }

        public Identity(Source source, Token self, Species species)
            : base (self)
        {
            Name = Content.Tokens.names[species];
            Species = species;
            if (Session.Active != null) 
                Instance = Session.Active.NextAvailableInstance(species);
            else //(only if template)
                Instance = -1;
            Owner = source.Last<Player>();
        }
        
        public override string ToString() { return Name + " " + Instance; }
	}
}

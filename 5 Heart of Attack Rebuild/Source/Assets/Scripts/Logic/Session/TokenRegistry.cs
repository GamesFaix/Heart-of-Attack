using System;
using System.Collections.Generic;

namespace HOA.Tokens
{ 
    public class TokenRegistry : SessionComponent
    {
        public TokenSet Tokens { get; private set; }
        private Dictionary<Species, ushort> instanceCounts;

        public TokenRegistry(Session session) : base(session)
        {
            Tokens = new TokenSet();
            instanceCounts = new Dictionary<Species, ushort>();
        }

        public ushort NextAvailableInstance(Species species)
        {
            if (!instanceCounts.ContainsKey(species))
                instanceCounts.Add(species, 0);
            return instanceCounts[species];
        }


        public void Add(Token t, Species species)
        {
            Tokens.Add(t);
            instanceCounts[species]++;            
            if (t is Unit)
                session.Queue.Add((Unit)t);
        }

        public void Remove(Token t)
        {
            if (t is Unit)
                session.Queue.Remove((Unit)t);
            Tokens.Remove(t);
        }

        public void ClearLegal() { Tokens.Legalize(false); }
	}

}

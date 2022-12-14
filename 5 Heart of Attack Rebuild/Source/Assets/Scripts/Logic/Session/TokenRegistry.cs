using System;
using System.Collections.Generic;
using HOA.Tokens;
using Cell = HOA.Board.Cell;
using HOA.Collections;

namespace HOA.Sessions
{ 
    public class TokenRegistry : SessionComponent
    {
        public Set<IEntity> Tokens { get; private set; }
        private Dictionary<Species, ushort> instanceCounts;

        public TokenRegistry(Session session) : base(session)
        {
            Tokens = new Set<IEntity>();
            instanceCounts = new Dictionary<Species, ushort>();
        }

        public ushort NextAvailableInstance(Species species)
        {
            if (!instanceCounts.ContainsKey(species))
                instanceCounts.Add(species, 0);
            return instanceCounts[species];
        }

        public Token Create(object source, Species species, Cell cell)
        {
            if (Content.Tokens.templates[species].CanEnter(cell))
            {
                Token newToken = Content.TokenBuilders.builders[species](source);
                newToken.Enter(cell);
                return newToken;
            }
            else
                throw new Exception(species + " cannot be created in " + cell + ".");
        }


        void Add(Token t, Species species)
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

        public void ClearLegal() { Tokens.ForEach((t) => { t.Legal = false; }); }
	}

}

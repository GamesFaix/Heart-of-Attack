using System;
using System.Collections.Generic;

namespace HOA { 

    public static class TokenRegistry 
    {
        public static Dictionary<Species, Token> Templates { get; private set; }
        public static TokenSet Tokens { get; private set; }
        private static Dictionary<Species, ushort> instanceCounts;

        public static void OnGameStart()
        {
            CreateTemplates();
            Reset();
        }

        public static void CreateTemplates()
        {
            Templates = new Dictionary<Species, Token>();
            
            int speciesCount = Enum.GetValues(typeof(Species)).Length;
            
            for (short i=0; i < speciesCount; i++)
            {
                Token template = Token.Constructors[(Species)i](Source.Neutral, true);
                Templates.Add((Species)i, template);
            }
        }

        public static void Reset()
        {
            if (Tokens != null)
                for (int i = Tokens.Count - 1; i >= 0; i--)
                    ((Token)(Tokens[i])).Destroy(new Source(), false, false);

            Tokens = new TokenSet();
            TurnQueue.Reset();
            instanceCounts = new Dictionary<Species, ushort>();
        }

        public static ushort Add(Token t, Species species)
        {
            Tokens.Add(t);
            if (t is Unit) 
                TurnQueue.Add((Unit)t);

            if (!instanceCounts.ContainsKey(species)) 
                instanceCounts.Add(species, 0);
            instanceCounts[species]++;
            return instanceCounts[species];
        }

        public static void Remove(Token t) {
            if (t is Unit)
                TurnQueue.Remove((Unit)t);
            Tokens.Remove(t); 
        }

        public static void ClearLegal() { Tokens.Legalize(false); }
    }
}

using System;
using System.Collections.Generic;

namespace HOA { 

    public static class TokenRegistry 
    {
        public static Dictionary<Species, Token> Templates { get; private set; }
        public static TargetGroup Tokens { get; private set; }

        public static void BuildTemplates()
        {
            Templates = new Dictionary<Species, Token>();
            
            int speciesCount = Enum.GetValues(typeof(Species)).Length;
            
            for (short i=0; i < speciesCount; i++)
            {
                Token template = Token.Constructors[(Species)i](Source.Neutral, true);
                template.Owner = Roster.Neutral;
                Templates.Add((Species)i, template);
            }


        }

        public static void Add(Token t)
        {
            Tokens.Add(t);
            if (t is Unit) 
                TurnQueue.Add((Unit)t);
        }
        public static void Remove(Token t) { 
            Tokens.Remove(t); 
        }

        public static void Reset()
        {
            if (Tokens != null)
            {
                for (int i = Tokens.Count - 1; i >= 0; i--)
                    ((Token)(Tokens[i])).Destroy(new Source(), false, false);
            }
            Tokens = new TargetGroup();
        }

        public static void ClearLegal() { Tokens.Legalize(false); }

    }
}

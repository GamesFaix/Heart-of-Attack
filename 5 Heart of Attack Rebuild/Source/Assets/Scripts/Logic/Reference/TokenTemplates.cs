using System;
using System.Collections.Generic;

namespace HOA.Reference
{
	
    public static class TokenTemplates
    {
        public static Dictionary<Tokens.Species, Token> tokens { get; private set; }

        public static void Load() { CreateTemplates(); }

        public static void CreateTemplates()
        {
            tokens = new Dictionary<Tokens.Species, Token>();

            int speciesCount = Enum.GetValues(typeof(Tokens.Species)).Length;

            for (short i = 0; i < speciesCount; i++)
            {
                Debug.Log("Not implemented.");
                //  Token template = Token.Constructors[(Species)i](Source.Neutral, true);
                //  Templates.Add((Species)i, template);
            }
        }


	}
}
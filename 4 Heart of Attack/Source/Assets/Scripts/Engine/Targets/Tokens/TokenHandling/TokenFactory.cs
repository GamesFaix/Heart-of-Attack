using UnityEngine;
using System;
using System.Collections.Generic;

namespace HOA {
	
	public static class TokenFactory {

		//Creation
		public static Token Create (Source source, Species species, Cell cell, bool log=true) {
            if ( TokenRegistry.Templates[species].Body.CanEnter(cell) ) 
            {
                Token newToken = Token.Constructors[species](source, false);
				if (log) 
                    Log(source, newToken, cell);
				TokenDisplay.Attach(newToken);
				newToken.Body.Enter(cell);
				if (newToken.Plane.ContainsAny(Plane.Sunken)) 
                    cell.EnterSunken(newToken);
                return newToken;
			}
			else 
            {
				throw new Exception("TokenFactory: Token cannot be created in that cell.");
			}	
		}
		
		static void Log (Source source, Token newToken, Cell cell) {
			if (source.Player != Roster.Neutral) {
				GameLog.Out(source+" created "+newToken+" in cell "+cell+".");
			}
		}
	}
}
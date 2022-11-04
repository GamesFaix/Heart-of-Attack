using UnityEngine;
using System;
using System.Collections.Generic;

namespace HOA {
	
	public static class TokenFactory {

		//Creation
		public static bool Create (Source source, Species code, Cell cell, out Token newToken, bool log=true) {
			newToken = Instantiate(source, code);

			if (newToken.Body.CanEnter(cell)) {
				TokenRegistry.Add(newToken);
				InheritOwnership (newToken, source);
				if (log) {Log(source, newToken, cell);}
				TokenDisplay.Attach(newToken);
				newToken.Body.Enter(cell);
				if (newToken.Plane.ContainsAny(Plane.Sunken)) {cell.EnterSunken(newToken);}
				return true;
			}
			else {
				GameLog.Debug("TokenFactory: Token cannot be created in that cell.");
				return false;
			}	
		}
		
		public static bool Create (Source source, Species code, Cell cell, bool log=true) {
			Token newToken;
			return Create(source, code, cell, out newToken, log);
		}

		static Token Instantiate (Source source, Species species) 
        {
            return Token.Constructors[species](source, false);
        }

		static void InheritOwnership (Token t, Source s) {
			if (!FactionRef.Neutral().Contains(t.ID.Species)
			    && !(t is Heart)) {
				t.Owner = s.Player;
			}
		}
		static void Log (Source source, Token newToken, Cell cell) {
			if (source.Player != Roster.Neutral) {
				GameLog.Out(source+" created "+newToken+" in cell "+cell+".");
			}
		}

		//Setup
		public static void Setup () {
			Token.Load();
            TokenRegistry.BuildTemplates();
		}
	}
}
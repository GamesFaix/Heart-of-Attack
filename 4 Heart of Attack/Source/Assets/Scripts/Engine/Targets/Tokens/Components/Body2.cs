using UnityEngine; 

namespace HOA { 

	public partial class Body {
		public static bool CanTrample (Token token, Cell newCell) {
			if (TrampleVsDestructible(token, newCell)) {return true;}
			else if (KingVsHeart(token, newCell)) {return true;}
			return false;
		}
		
		static bool TrampleVsDestructible (Token token, Cell newCell) {
			if (token.Body.Trample) {
				foreach (Token occupant in newCell.Occupants) {
					if (occupant.Body.Destructible
					    && CanTakePlaceOf(token, occupant)) {
						return true;
					}
				}
			}
			return false;
		}
		
		static bool KingVsHeart (Token token, Cell newCell) {
			if (token is King) {
				foreach (Token occupant in newCell.Occupants) {
					if (occupant is Heart
					    && CanTakePlaceOf(token, occupant)) {
						return true;
					}
				}
			}
			return false;
		}
		static bool CanTakePlaceOf (Token taker, Token taken) {
            return (taker.Plane == taken.Plane);
		}
		
		public static bool CanSwap (Token a, Token b) {
			if (CanTakePlaceOf(a, b) && CanTakePlaceOf(b, a)) {return true;}
			return false;
		}

		protected static bool TrampleCell (Token trampler, Cell newCell) {
			if (CanTrample (trampler, newCell)) {
				TargetGroup occupants = newCell.Occupants;
				EffectGroup effects = new EffectGroup();
			
				if (TrampleVsDestructible(trampler, newCell)) {
					TargetGroup destructibles = occupants - TargetFilter.Dest;
					for (int i=destructibles.Count-1; i>=0; i--) 
						effects.Add(Effect.DestroyObstacle(new Source(trampler), (Token)(destructibles[i])));
				}
				if (KingVsHeart(trampler, newCell)) {
					TargetGroup hearts = occupants - TargetFilter.Heart;
					if (hearts.Count>0)
						effects.Add(Effect.GetHeart(new Source(trampler.Owner), (Token)(hearts[0])));
				}
				EffectQueue.Add(effects);
				return true;
			}
			return false;
		}
	}
}

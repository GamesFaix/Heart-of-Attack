using UnityEngine; 

namespace HOA { 

	public partial class Body {
		public static bool CanTrample (Token token, Cell newCell) {
			if (TrampleVsDestructible(token, newCell)) {return true;}
			else if (KingVsHeart(token, newCell)) {return true;}
			return false;
		}
		
		static bool TrampleVsDestructible (Token token, Cell newCell) {
			if (token.Special.Is(ESpecial.TRAM)) {
				foreach (Token occupant in newCell.Occupants) {
					if (occupant.Special.Is(ESpecial.HEART) 
					    && CanTakePlaceOf(token, occupant)) {
						return true;
					}
				}
			}
			return false;
		}
		
		static bool KingVsHeart (Token token, Cell newCell) {
			if (token.Special.Is(ESpecial.KING)) {
				foreach (Token occupant in newCell.Occupants) {
					if (occupant.Special.Is(ESpecial.HEART)
					    && CanTakePlaceOf(token, occupant)) {
						return true;
					}
				}
			}
			return false;
		}
		static bool CanTakePlaceOf (Token taker, Token taken) {
			Cell otherCell = taken.Body.Cell;
			Token blocker;
			
			foreach (EPlane plane in taker.Plane.Value) {
				if (otherCell.Contains(plane, out blocker)) {
					if (blocker != taken) {return false;}
				}
			}
			return true;
		}
		
		public static bool CanSwap (Token a, Token b) {
			if (CanTakePlaceOf(a, b) && CanTakePlaceOf(b, a)) {return true;}
			return false;
		}

		protected static bool Trample (Token trampler, Cell newCell) {
			if (CanTrample (trampler, newCell)) {
				TokenGroup occupants = newCell.Occupants;
				EffectGroup effects = new EffectGroup();
			
				if (TrampleVsDestructible(trampler, newCell)) {
					TokenGroup destructibles = occupants.OnlyType(ESpecial.DEST);
					for (int i=destructibles.Count-1; i>=0; i--) {
						effects.Add(new Effects.Destruct(new Source(trampler), destructibles[i]));
					}
				}
				if (KingVsHeart(trampler, newCell)) {
					TokenGroup hearts = occupants.OnlyType(ESpecial.HEART);
					if (hearts.Count>0) {
						effects.Add(new Effects.GetHeart(new Source(trampler.Owner), hearts[0]));
					}
				}
				EffectQueue.Add(effects);
				return true;
			}
			return false;
		}
	}
}

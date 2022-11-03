using System.Collections.Generic;

namespace HOA {
	
	public delegate TargetGroup Filter (TargetGroup targets, Token actor);
	
	public static class Filters {

		public static TargetGroup None (TargetGroup targets, Token actor=null) {return targets;}

		public static TargetGroup Cells (TargetGroup targets, Token actor=null) {
			return (TargetGroup)(targets.cells);
		}

		public static TargetGroup Move (TargetGroup targets, Token actor) {
			return (TargetGroup)((targets.cells/actor) - actor.Body.Cell);
		}

		public static TargetGroup Create (TargetGroup targets, Token actor) {
			return (TargetGroup)(targets.cells/actor);
		}

		public static TargetGroup Tokens (TargetGroup targets, Token actor=null) {
			return (TargetGroup)(targets.tokens);
		}
		public static TargetGroup Units (TargetGroup targets, Token actor=null) {
			return (TargetGroup)(targets.tokens.units);
		}

		public static TargetGroup UnitsNoSelf (TargetGroup targets, Token actor) {
			return (TargetGroup)(targets.tokens.units - actor);
		}

		public static TargetGroup Destructible (TargetGroup targets, Token actor=null) {
			return (TargetGroup)(targets.tokens.destructible);
		}
		public static TargetGroup UnitDest (TargetGroup targets, Token actor=null) {
			return (TargetGroup)(targets.tokens.destructible + targets.tokens.units);
		}
		public static TargetGroup DestNoCorpse (TargetGroup targets, Token actor=null) {
			TokenGroup tokens = targets.tokens.destructible;
			tokens -= EToken.CORP;
			tokens -= EToken.RECY;
			return (TargetGroup)tokens;
		}
		public static TargetGroup Corpses (TargetGroup targets, Token actor=null) {
			return (targets.tokens) / (new List<EToken> {EToken.CORP, EToken.RECY});
		}

		public static TargetGroup TeamUnits (TargetGroup targets, Token actor) {
			return (TargetGroup)(targets.tokens.units/actor.Owner);
		}

		public static TargetGroup EnemyUnits (TargetGroup targets, Token actor) {
			return (TargetGroup)(targets.tokens.units-actor.Owner);
		}

		public static TargetGroup EnemyUnitsNoKings (TargetGroup targets, Token actor) {
			return (TargetGroup)(targets.tokens.units - targets.tokens.kings - actor.Owner);
		}

	}
}
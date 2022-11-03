using UnityEngine;

namespace HOA {
	
	public static class Legalizer {
		
		public static void Find (Token actor, Aim a, Cell start=default(Cell), Token other=default(Token)) {
			//GUISelectors.Reset();
			if (start == default(Cell)) {start = actor.Body.Cell;}

			switch (a.AimType) {
				case EAim.CELLMATE: FindCellmate(start, actor, a, other); break;
				case EAim.NEIGHBOR: LegalizerNeighbor.Find(start, actor, a, other); break;
				case EAim.PATH: LegalizerPath.Find(start, actor, a, other); break;
				case EAim.LINE: LegalizerLine.Find(start, actor, a, other); break;
				case EAim.ARC: LegalizerArc.Find(start, actor, a, other); break;
				case EAim.GLOBAL: FindGlobal(actor, a, other); break;
				case EAim.FREE: LegalizerFree.Find(actor, a, other); break;
				default: break;
			}
		}

		public static void FindArenMove (Token actor, Aim a) {
			CellGroup block = ((ArenaNonSensus)actor).Cells;

			CellGroup neighbors = new CellGroup();
			foreach (Cell c in block) {neighbors.Add(c.Neighbors());}

			foreach (Cell c in neighbors) {
				if (actor.Body.CanEnter(c)) {
					c.Legalize();
				}
			}
		}
		
		static void FindCellmate (Cell start, Token actor, Aim a, Token other) {}
		static void FindGlobal (Token t, Aim a, Token other) {}

		public static TokenGroup Restrict (TokenGroup tokens, Token actor, Aim a) {
			TokenGroup legal = new TokenGroup(tokens);
			legal = legal.OnlyClass(a.TargetClass);
			if (a.TeamOnly) {legal = legal.OnlyOwner(actor.Owner);}
			if (a.EnemyOnly) {legal = legal.RemoveOwner(actor.Owner);}
			if (a.NoKings) {legal = legal.RemoveClass(EType.KING);}
			if (!a.IncludeSelf) {legal.Remove(actor);}
			return legal;
		}

		public static bool Stop (Token mover, Cell c) {
			return c.StopToken(mover);
		}
	}
}
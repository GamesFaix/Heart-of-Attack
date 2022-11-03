using System.Collections.Generic;
using UnityEngine;

namespace HOA {
	public static class LegalizerLine {
		//line
		public static void Find (Cell start, Token actor, Aim a, Token other) {
			List<CellGroup> star = CellStar(start, a.Range);
			
			TargetGroup legal = new TargetGroup();

			foreach (CellGroup line in star) {

				if (a.TargetClass.Contains(EType.CELL)) {
					if (a.Purpose == EPurpose.MOVE) {legal.Add(LineUntilStop(line, actor));}
					if (a.Purpose == EPurpose.CREATE) {legal.Add(LineUntilStop(line, other));}
				}

				else {
					foreach (Cell c in LineUntilToken(line)) {
						legal.Add(Legalizer.Restrict(c.Occupants, actor, a));
					}
				}
			}
			legal.Legalize();
		}

		static List<CellGroup> CellStar (Cell start, int range) {
			List<CellGroup> star = new List<CellGroup>();

			for (int i=0; i<8; i++) {
				CellGroup line = new CellGroup();
				int[] dir = Direction.FromInt(i);
				Cell last = start;
				for (int j=1; j<=range; j++) {
					Cell next;
					int checkX = last.X + dir[0];
					int checkY = last.Y + dir[1];

					if (Board.HasCell(checkX, checkY, out next)) {
						line.Add(next);
						last = next;
					}
				}
				star.Add(line);
			}
			return star;
		}

		static CellGroup LineUntilStop (CellGroup line, Token actor) {
			CellGroup legal = new CellGroup();
			foreach (Cell c in line) {
				if (actor.Body.CanEnter(c)) {legal.Add(c);}
				else {break;}
				if (Legalizer.Stop(actor,c)) {break;}
			}
			return legal;
		}

		static CellGroup LineUntilToken (CellGroup line) {
			CellGroup legal = new CellGroup();
			foreach (Cell c in line) {
				legal.Add(c);
				if (!(c.IsEmpty() || c.ContainsOnly(EPlane.SUNK))) {
					break;
				}
			}
			return legal;
		}
	}
}
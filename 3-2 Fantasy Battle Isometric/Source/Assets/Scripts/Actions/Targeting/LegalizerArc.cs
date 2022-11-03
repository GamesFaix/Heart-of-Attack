using UnityEngine;

namespace HOA {
	public static class LegalizerArc {

		//arc
		public static void Find (Cell start, Token actor, Aim a, Token other) {
			TargetGroup legal = new TargetGroup();
			CellGroup square = CellSquare (start, a.Range, a.MinRange);

			if (a.TargetClass.Contains(EType.CELL)) {
				if (a.Purpose == EPurpose.ATTACK) {legal.Add(square);}
				if (a.Purpose == EPurpose.CREATE) {legal.Add(square.Occupiable(other));}
				if (a.Purpose == EPurpose.MOVE) {
					if (other == default(Token)) {legal.Add(square.Occupiable(actor));}
					else {legal.Add(square.Occupiable(other));}
				}
			}
			else {legal.Add(Legalizer.Restrict(square.Occupants, actor, a));}
			legal.Legalize();
		}

		static CellGroup CellSquare (Cell start, int range, int min) {
			CellGroup square = new CellGroup();
			Cell c;
			for (int x=(start.X-range); x<=(start.X+range); x++) {
				for (int y=(start.Y-range); y<=(start.Y+range); y++) {
					if (Board.HasCell(x, y, out c)) {square.Add(c);}
				}
			}
			if (min > 0) {square = RemoveMin(square, start, min);}
			return square;
		}

		static CellGroup RemoveMin (CellGroup square, Cell start, int min) {
			CellGroup ring = new CellGroup();
			foreach (Cell c in square) {
				if ((Mathf.Abs(c.X - start.X) >= min) 
				    || (Mathf.Abs(c.Y - start.Y) >= min)) {
					ring.Add(c);
				}
			}
			return ring;
		}
	}
}
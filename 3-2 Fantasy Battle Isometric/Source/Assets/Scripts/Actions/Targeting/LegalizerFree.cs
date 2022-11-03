
namespace HOA {
	public static class LegalizerFree {
		public static void Find (Token actor, Aim a, Token other) {
			TargetGroup legal = new TargetGroup();

			if (a.TargetClass.Contains(EType.CELL)) {
				if (a.Purpose == EPurpose.CREATE) {legal.Add(Board.Cells.Occupiable(other));}
				if (a.Purpose == EPurpose.MOVE) {legal.Add(Board.Cells.Occupiable(actor));}
			}
			else {legal.Add(Legalizer.Restrict(TokenFactory.Tokens, actor, a));}

			legal.Legalize();
		}
	}
}
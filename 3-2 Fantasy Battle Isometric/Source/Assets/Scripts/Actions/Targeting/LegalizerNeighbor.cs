namespace HOA {
	public static class LegalizerNeighbor {

		public static void Find (Cell start, Token actor, Aim a, Token other) {
			CellGroup neighborCells = start.Neighbors(true);
			if (a.Purpose == EPurpose.MOVE) {neighborCells.Remove(start);}

			TargetGroup legal = new TargetGroup();

			if (a.TargetClass.Contains(EType.CELL)) {
				if (a.Purpose == EPurpose.CREATE) {legal.Add(neighborCells.Occupiable(other));}
				if (a.Purpose == EPurpose.MOVE) {legal.Add(neighborCells.Occupiable(actor));}
			}
			else {legal.Add(Legalizer.Restrict(neighborCells.Occupants, actor, a));}

			legal.Legalize();
		}


	}
}
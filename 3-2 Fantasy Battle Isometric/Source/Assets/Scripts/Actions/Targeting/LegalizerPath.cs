
namespace HOA {
	public static class LegalizerPath {

		//path
		public static void Find (Cell start, Token actor, Aim a, Token other) {
			if (!a.TargetClass.Contains(EType.CELL)) {PathTokens (start, actor, a);}
			else {
				if (a.Purpose == EPurpose.MOVE) {PathCellMove (start, actor, a);}
				
			}
		}
		
		static void PathTokens (Cell start, Token actor, Aim a) {
			int range = a.Range;
			
			CellGroup toCheck = new CellGroup();
			CellGroup nextRad = new CellGroup();
			
			nextRad.Add(start);
			
			for (int rad=1; rad<=range; rad++) {
				toCheck = nextRad;
				nextRad = new CellGroup();
				
				for (int j=0; j<toCheck.Count; j++) {
					for (int i=0; i<8; i++) {
						int[] dir = Direction.FromInt(i);
						
						Cell next;
						int checkX = toCheck[j].X + dir[0];
						int checkY =  toCheck[j].Y + dir[1];					
						
						if (Board.HasCell(checkX, checkY, out next)) {
							if (next.IsEmpty() || next.ContainsOnly(EPlane.SUNK)) {
								nextRad.Add(next);
							}
							TokenGroup targets = next.Occupants;
							targets = targets.OnlyClass(a.TargetClass);
							targets = Legalizer.Restrict(targets, actor, a);
							foreach (Token t in targets) {t.Legalize();}
						}
					}
				}
			}
		}
		
		static void PathCellMove (Cell start, Token actor, Aim a) {
			int range = a.Range;
			
			CellGroup legal = new CellGroup();
			CellGroup toCheck = new CellGroup();
			CellGroup nextRad = new CellGroup();
			
			nextRad.Add(start);
			
			for (int rad=1; rad<=range; rad++) {
				toCheck = nextRad;
				nextRad = new CellGroup();
				
				for (int j=0; j<toCheck.Count; j++) {
					for (int i=0; i<8; i++) {
						int[] dir = Direction.FromInt(i);
						
						Cell next;
						int checkX = toCheck[j].X + dir[0];
						int checkY =  toCheck[j].Y + dir[1];					
						
						if (Board.HasCell(checkX, checkY, out next)) {
							if (actor.Body.CanEnter(next)) {
								if (!legal.Contains(next)) {
									if ( ! (actor.Type.Is(EType.TRAM) && next.Contains(EType.DEST))) {
										
										if (!Legalizer.Stop(actor, next)) {
											
											nextRad.Add(next);
											
										}
									}
									
									legal.Add(next);
									next.Legalize();
								}
							}
						}
					}
				}
			}
			
		}

	}


}

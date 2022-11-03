using UnityEngine;
using System.Collections.Generic;

namespace HOA {
	
	public static class Legalizer {
		
		public static void Find (Token actor, Aim a, Cell start=default(Cell), Token other=default(Token)) {
			//GUISelectors.Reset();
			if (start == default(Cell)) {start = actor.Body.Cell;}

			switch (a.Trajectory) {
				case ETraj.NEIGHBOR: Neighbor(start, actor, a, other); break;
				case ETraj.LINE: Line(start, actor, a, other); break;
				case ETraj.ARC: Arc(start, actor, a, other); break;
				case ETraj.FREE: Free(actor, a, other); break;
				case ETraj.CELLMATE: Debug.Log("Cellmate targeting no longer active."); break;
				case ETraj.PATH: Debug.Log("Path aim no longer active."); break;
				case ETraj.GLOBAL: Debug.Log("Global aim no longer active."); break;
				default: break;
			}
		}

		static void Neighbor (Cell start, Token actor, Aim a, Token other) {
			CellGroup neighborCells = start.Neighbors(true);
			if (a.Purpose == EPurp.MOVE) {neighborCells.Remove(start);}
			
			TargetGroup legal = new TargetGroup();
			
			if (a.Type.Is(EType.CELL)) {
				if (a.Purpose == EPurp.CREATE) {legal.Add(neighborCells.Occupiable(other));}
				if (a.Purpose == EPurp.MOVE) {legal.Add(neighborCells.Occupiable(actor));}
			}
			else {legal.Add(neighborCells.Occupants.Restrict(actor, a));}
			
			legal.Legalize();
		}

		static void Free (Token actor, Aim a, Token other) {
			TargetGroup legal = new TargetGroup();
			
			if (a.Type.Is(EType.CELL)) {
				if (a.Purpose == EPurp.CREATE) {legal.Add(Board.Cells.Occupiable(other));}
				if (a.Purpose == EPurp.MOVE) {legal.Add(Board.Cells.Occupiable(actor));}
			}
			else {legal.Add(TokenFactory.Tokens.Restrict(actor, a));}
			
			legal.Legalize();
		}

		static void Line (Cell start, Token actor, Aim a, Token other) {
			List<CellGroup> star = CellStar(start, a.Range);
			
			TargetGroup legal = new TargetGroup();
			
			foreach (CellGroup line in star) {
				
				if (a.Type.Is(EType.CELL)) {
					if (a.Purpose == EPurp.MOVE) {legal.Add(LineUntilStop(line, actor));}
					if (a.Purpose == EPurp.CREATE) {legal.Add(LineUntilStop(line, other));}
				}
				
				else {
					foreach (Cell c in LineUntilToken(line)) {
						legal.Add(c.Occupants.Restrict(actor, a));
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
				if (c.StopToken(actor)) {break;}
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

		static void Arc (Cell start, Token actor, Aim a, Token other) {
			TargetGroup legal = new TargetGroup();
			CellGroup square = CellSquare (start, a.Range, a.MinRange);
			
			if (a.Type.Is(EType.CELL)) {
				if (a.Purpose == EPurp.ATTACK) {legal.Add(square);}
				if (a.Purpose == EPurp.CREATE) {legal.Add(square.Occupiable(other));}
				if (a.Purpose == EPurp.MOVE) {
					if (other == default(Token)) {legal.Add(square.Occupiable(actor));}
					else {legal.Add(square.Occupiable(other));}
				}
			}
			else {legal.Add(square.Occupants.Restrict(actor, a));}
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
	}
}
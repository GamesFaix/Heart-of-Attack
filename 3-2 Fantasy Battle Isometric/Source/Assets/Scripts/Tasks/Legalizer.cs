using UnityEngine;
using System;
using System.Collections.Generic;

namespace HOA {
	
	public static class Legalizer {
		
		public static void Find (Token Parent, Aim a, Cell start=default(Cell), Token other=default(Token)) {
			//GUISelectors.Reset();
			if (start == null && a.Trajectory != ETraj.FREE) {start = Parent.Body.Cell;}

			switch (a.Trajectory) {
				case ETraj.NEIGHBOR: Neighbor(start, Parent, a, other); break;
				case ETraj.LINE: Line(start, Parent, a, other); break;
				case ETraj.ARC: Arc(start, Parent, a, other); break;
				case ETraj.FREE: Free(Parent, a, other); break;
				case ETraj.CELLMATE: Debug.Log("Cellmate targeting no longer active."); break;
				case ETraj.PATH: Path(start, Parent,a); break;//Debug.Log("Path aim no longer active."); break;
				case ETraj.GLOBAL: Debug.Log("Global aim no longer active."); break;
				default: break;
			}
		}

		static void Neighbor (Cell start, Token Parent, Aim a, Token other) {
			CellGroup neighborCells = start.Neighbors(true);
			if (a.Purpose == EPurp.MOVE) {neighborCells.Remove(start);}
			
			TargetGroup legal = new TargetGroup();
			
			if (a.Special.Is(EType.CELL)) {
				if (a.Purpose == EPurp.CREATE) {legal.Add(neighborCells.Occupiable(other));}
				if (a.Purpose == EPurp.MOVE) {legal.Add(neighborCells.Occupiable(Parent));}
			}
			else {legal.Add(neighborCells.Occupants.Restrict(Parent, a));}
			
			legal.Legalize();
		}

		static void Free (Token Parent, Aim a, Token other) {
			TargetGroup legal = new TargetGroup();
			
			if (a.Special.Is(EType.CELL)) {
				if (a.Purpose == EPurp.CREATE) {legal.Add(Game.Board.Cells.Occupiable(other));}
        	    if (a.Purpose == EPurp.MOVE) {legal.Add(Game.Board.Cells.Occupiable(Parent));}
			}
	        else {legal.Add(TokenFactory.Tokens.Restrict(Parent, a));}
			
			legal.Legalize();
		}

		static void Path (Cell start, Token Parent, Aim a) {
			if (!a.Special.Is(EType.CELL)) {
				
				
				TargetGroup legal = new TargetGroup();

				CellGroup thisRad = start.Neighbors();
				CellGroup nextRad = new CellGroup();
				CellGroup marked = new CellGroup();

				for (int i=1; i<=a.Range; i++) {

					foreach (Cell c in thisRad) {
						legal.Add(c.Occupants.Restrict(Parent, a));
						marked.Add(c);

						if (c.Occupants.Count==0 ||
						    (c.Occupants.Count==1 && c.Contains(EPlane.SUNK))) {

							foreach (Cell d in c.Neighbors()) {
								if (!marked.Contains(d)) {nextRad.Add(d);}		
							}
						}
					}
					thisRad = nextRad;
					nextRad = new CellGroup();
				}

				legal.Legalize();
			}
			else {throw new ArgumentException("Path trajectory can only be used to target tokens.  Cell targets must use repeated neighbor trajectory.");}


		}

		static void Line (Cell start, Token Parent, Aim a, Token other) {
			List<CellGroup> star = CellStar(start, a.Range);
			
			TargetGroup legal = new TargetGroup();
			
			foreach (CellGroup line in star) {
				
				if (a.Special.Is(EType.CELL)) {
					if (a.Purpose == EPurp.MOVE) {legal.Add(LineUntilStop(line, Parent));}
					if (a.Purpose == EPurp.CREATE) {legal.Add(LineUntilStop(line, other));}
				}
				
				else {
					foreach (Cell c in LineUntilToken(line)) {
						legal.Add(c.Occupants.Restrict(Parent, a));
					}
					legal.Add(start.Occupants);
				}
			}
			legal.Legalize();
		}
		
		static List<CellGroup> CellStar (Cell start, int range) {
			List<CellGroup> star = new List<CellGroup>();
			
			for (int i=0; i<8; i++) {
				CellGroup line = new CellGroup();
				Int2 dir = Direction.FromInt(i);
				Cell last = start;
				for (int j=1; j<=range; j++) {
					Cell next;
					Int2 index = last.Index + dir;
					if (Game.Board.HasCell(index, out next)) {
						line.Add(next);
						last = next;
					}
				}
				star.Add(line);
			}
			return star;
		}
		
		static CellGroup LineUntilStop (CellGroup line, Token Parent) {
			CellGroup legal = new CellGroup();
			foreach (Cell c in line) {
				if (Parent.Body.CanEnter(c)) {legal.Add(c);}
				else {break;}
				if (c.StopToken(Parent)) {break;}
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

		static void Arc (Cell start, Token Parent, Aim a, Token other) {

			TargetGroup legal = new TargetGroup();
			CellGroup square = CellSquare (start, a.Range, a.MinRange);
			
			if (a.Special.Is(EType.CELL)) {
				if (a.Purpose == EPurp.ATTACK) {legal.Add(square);}
				if (a.Purpose == EPurp.CREATE) {legal.Add(square.Occupiable(other));}
				if (a.Purpose == EPurp.MOVE) {
					if (other == default(Token)) {legal.Add(square.Occupiable(Parent));}
					else {legal.Add(square.Occupiable(other));}
				}
			}
			else {legal.Add(square.Occupants.Restrict(Parent, a));}
			legal.Legalize();
		}
		
		static CellGroup CellSquare (Cell start, int range, int min) {
			CellGroup square = new CellGroup();
			Cell c;
			for (int x=(start.X-range); x<=(start.X+range); x++) {
				for (int y=(start.Y-range); y<=(start.Y+range); y++) {
					if (Game.Board.HasCell(x, y, out c)) {square.Add(c);}
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

		public static void FindArenMove (Token Parent, Aim a) {
			CellGroup block = ((ArenaNonSensus)Parent).Cells;

			CellGroup neighbors = new CellGroup();
			foreach (Cell c in block) {neighbors.Add(c.Neighbors());}

			foreach (Cell c in neighbors) {
				if (Parent.Body.CanEnter(c)) {
					c.Legal = true;
				}
			}
		}
	}
}
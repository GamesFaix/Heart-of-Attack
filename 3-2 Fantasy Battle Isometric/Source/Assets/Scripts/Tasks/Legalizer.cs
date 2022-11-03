using UnityEngine;
using System;
using System.Collections.Generic;

namespace HOA {
	
	public static class Legalizer {

		public static bool Find (Token actor, Aim aim, Cell center=null, Token other=null) {
			ETraj trajectory = aim.Trajectory;

			if (center == null && trajectory != ETraj.FREE) {center = actor.Body.Cell;}

			switch (trajectory) {
				case ETraj.NEIGHBOR: return Neighbor(actor, aim, center, other);
				case ETraj.LINE: return Line(actor, aim, center, other);
				case ETraj.ARC: return Arc(actor, aim, center, other);
				case ETraj.FREE: return Free(actor, aim, other);
				case ETraj.PATH: return Path(actor, aim, center);
				case ETraj.RADIAL: return Radial(actor, aim, center);
				default: return false;
			}
		}

		static bool Neighbor (Token actor, Aim aim, Cell center, Token other) {
			CellGroup neighborCells = center.Neighbors(true);

			if (aim.Purpose == EPurp.MOVE) {neighborCells.Remove(center);}
			
			TargetGroup legal = new TargetGroup();
			
			if (aim.Special.Is(ESpecial.CELL)) {
				if (aim.Purpose == EPurp.CREATE) {legal.Add(neighborCells.Occupiable(other));}
				if (aim.Purpose == EPurp.MOVE) {legal.Add(neighborCells.Occupiable(actor));}
			}
			else {legal.Add(neighborCells.Occupants.Restrict(actor, aim));}

			if (legal.Count > 0) {
				legal.Legalize();
				return true;
			}
			else {return false;}
		}

		static bool Free (Token actor, Aim aim, Token other) {
			TargetGroup legal = new TargetGroup();
			
			if (aim.Special.Is(ESpecial.CELL)) {
				if (aim.Purpose == EPurp.CREATE) {legal.Add(Game.Board.Cells.Occupiable(other));}
        	    if (aim.Purpose == EPurp.MOVE) {legal.Add(Game.Board.Cells.Occupiable(actor));}
			}
	        else {legal.Add(TokenFactory.Tokens.Restrict(actor, aim));}
			if (legal.Count > 0) {
				legal.Legalize();
				return true;
			}
			return false;
		}

		static bool Path (Token actor, Aim aim, Cell center) {
			if (!aim.Special.Is(ESpecial.CELL)) {
				TargetGroup legal = new TargetGroup();

				CellGroup thisRad = center.Neighbors();
				CellGroup nextRad = new CellGroup();
				CellGroup marked = new CellGroup();

				for (int i=1; i<=aim.Range; i++) {

					foreach (Cell c in thisRad) {
						legal.Add(c.Occupants.Restrict(actor, aim));
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
				if (legal.Count > 0) {
					legal.Legalize();
					return true;
				}
				else {return false;}
			}
			else {throw new ArgumentException("Path trajectory can only be used to target tokens.  Cell targets must use repeated neighbor trajectory.");}
		}

		static bool Line (Token actor, Aim aim, Cell center, Token other) {
			List<CellGroup> star = CellStar(center, aim.Range);
			
			TargetGroup legal = new TargetGroup();
			
			foreach (CellGroup line in star) {
				
				if (aim.Special.Is(ESpecial.CELL)) {
					if (aim.Purpose == EPurp.MOVE) {legal.Add(LineUntilStop(line, actor));}
					if (aim.Purpose == EPurp.CREATE) {legal.Add(LineUntilStop(line, other));}
				}
				
				else {
					foreach (Cell c in LineUntilToken(line)) {
						legal.Add(c.Occupants.Restrict(actor, aim));
					}
					legal.Add(center.Occupants);
				}
			}
			if (legal.Count > 0) {
				legal.Legalize();
				return true;
			}
			return false;
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

		static bool Arc (Token actor, Aim aim, Cell center, Token other) {

			TargetGroup legal = new TargetGroup();
			CellGroup square = CellSquare (center, aim.Range, aim.MinRange);
			
			if (aim.Special.Is(ESpecial.CELL)) {
				if (aim.Purpose == EPurp.ATTACK) {legal.Add(square);}
				if (aim.Purpose == EPurp.CREATE) {legal.Add(square.Occupiable(other));}
				if (aim.Purpose == EPurp.MOVE) {
					if (other == default(Token)) {legal.Add(square.Occupiable(actor));}
					else {legal.Add(square.Occupiable(other));}
				}
			}
			else {legal.Add(square.Occupants.Restrict(actor, aim));}
			if (legal.Count > 0) {
				legal.Legalize();
				return true;
			}
			return false;
		}

		static bool Radial (Token actor, Aim aim, Cell firstCell) {

			Cell center = actor.Body.Cell;
			NeighborMatrix neighbors = new NeighborMatrix(center);

			Cell nextClockwise;
			Cell nextCounter;

			TargetGroup legal = new TargetGroup();

			if (neighbors.CellClockwise(firstCell, out nextClockwise)) {
				legal.Add(nextClockwise);
			}
			if (neighbors.CellCounter(firstCell, out nextCounter)) {
				legal.Add(nextCounter);
			}
			if (legal.Count > 0) {
				legal.Legalize();
				return true;
			}
			return false;
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
	}
}
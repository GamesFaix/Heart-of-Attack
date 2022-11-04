using System;
using System.Collections.Generic;
using UnityEngine;

namespace HOA { 

	public partial class Aim {
	
		TargetSet FindNeighbor (Token actor, Cell center, Token other) {
			if (center == null) {center = actor.Body.Cell;}

            TargetSet Targets = new TargetSet();
			
			CellSet neighborCells = center.NeighborsAndSelf;
			if (Purpose == EPurp.MOVE) {neighborCells.Remove(center);}
			
			if (Filter.Contains(FilterTests.Cell)) 
            {
				if (Purpose == EPurp.CREATE) {Targets.Add(neighborCells - TargetFilter.Occupiable(other));}
				if (Purpose == EPurp.MOVE) {Targets.Add(neighborCells - TargetFilter.Occupiable(actor));}
			}
			else {Targets.Add(neighborCells.Occupants - Filter);}
			return Targets;
		}

        TargetSet FindFree(Token actor, Cell center, Token other)
        {
			//center does nothing
            TargetSet Targets = new TargetSet();

            if (Filter.Contains(FilterTests.Cell))
            {
				if (Purpose == EPurp.CREATE) {Targets.Add(Game.Board.Cells - TargetFilter.Occupiable(other));}
				if (Purpose == EPurp.MOVE) {Targets.Add(Game.Board.Cells -TargetFilter.Occupiable(actor));}
			}
			else {Targets.Add(TokenRegistry.Tokens - Filter);}
			return Targets;
		}

        TargetSet FindPath(Token actor, Cell center, Token other = null)
        {
			if (center == null) {center = actor.Body.Cell;}

            if (!Filter.Contains(FilterTests.Cell))
            {
                TargetSet Targets = new TargetSet();

                CellSet thisRad = center.Neighbors;
                CellSet nextRad = new CellSet();
                CellSet marked = new CellSet();
				
				for (int i=1; i<=Range; i++) {
					
					foreach (Cell c in thisRad) {
						Targets.Add(c.Occupants - Filter);
						marked.Add(c);
						
						if (c.Occupants.Count==0 
                            || (c.Occupants.Count==1 
                                && (c.Occupants - TargetFilter.Plane(Plane.Sunken, true)).Count > 0)) {
							
							foreach (Cell d in c.Neighbors) {
								if (!marked.Contains(d)) {nextRad.Add(d);}		
							}
						}
					}
					thisRad = nextRad;
                    nextRad = new CellSet();
				}
				return Targets;
			}
			else {throw new ArgumentException("Path trajectory can only be used to Target tokens.  Cell Targets must use repeated neighbor trajectory.");}
		}

        TargetSet FindLine(Token actor, Cell center, Token other)
        {
			if (center == null) {center = actor.Body.Cell;}

            TargetSet Targets = new TargetSet();
            List<CellSet> star = CellStar(center, Range);
            foreach (CellSet line in star)
            {
                if (Filter.Contains(FilterTests.Cell))
                {
					if (Purpose == EPurp.MOVE) {Targets.Add(LineUntilStop(line, actor));}
					if (Purpose == EPurp.CREATE) {Targets.Add(LineUntilStop(line, other));}
				}
				else {
                    CellSet cells = LineUntilStop(line, actor, true);
                    cells.Add(center);
                    Targets.Add(cells.Occupants - Filter);
				}
			}
			return Targets;
		}

		static List<CellSet> CellStar (Cell start, int range) {
			List<CellSet> star = new List<CellSet>();
			
			foreach (int2 dir in Direction.Directions) {
				CellSet line = new CellSet();
				Cell last = start;
				for (int j=1; j<=range; j++) {
					Cell next;
					index2 index;
					if (index2.Safe( (int2)last.Index + dir, out index)) {
						if (Game.Board.HasCell(index, out next)) {
							line.Add(next);
							last = next;
						}
					}
				}
				star.Add(line);
			}
			return star;
		}
		
		static CellSet LineUntilStop (CellSet line, Token Parent, bool inclusive=false) {
			CellSet legal = new CellSet();
			foreach (Cell c in line) {
				if (Parent.Body.CanEnter(c)) {legal.Add(c);}
				else {
					if (inclusive) {legal.Add(c);}
					break;
				}
				if (c.StopToken(Parent)) {break;}
			}
			return legal;
		}
		
		static CellSet LineUntilToken (CellSet line) {
			CellSet legal = new CellSet();
			foreach (Cell c in line) {
				legal.Add(c);
				if (c.Occupants.Count > 0 
                    || (c.Occupants - TargetFilter.Plane(Plane.Sunken, true)).Count == c.Occupants.Count) 
                    break;
			}
			return legal;
		}

		TargetSet FindRadial (Token actor, Cell firstCell, Token other) {
			Cell center = actor.Body.Cell;
			NeighborMatrix neighbors = new NeighborMatrix(center);
			
			Cell nextClockwise;
			Cell nextCounter;
			
			TargetSet Targets = new TargetSet();
			
			if (neighbors.CellClockwise(firstCell, out nextClockwise)) {
				Targets.Add(nextClockwise);
			}
			if (neighbors.CellCounter(firstCell, out nextCounter)) {
				Targets.Add(nextCounter);
			}
			return Targets;
		}

		TargetSet FindArc (Token actor, Cell center, Token other) {
			if (center == null) {center = actor.Body.Cell;}

			TargetSet Targets = new TargetSet();
			CellSet square = CellSquare (center, Range, MinRange);

            if (Filter.Contains(FilterTests.Cell))
            {
				if (Purpose == EPurp.ATTACK) {Targets.Add(square);}
				if (Purpose == EPurp.CREATE) {Targets.Add(square - TargetFilter.Occupiable(other));}
				if (Purpose == EPurp.MOVE) {
					if (other == default(Token)) {Targets.Add(square - TargetFilter.Occupiable(actor));}
					else {Targets.Add(square - TargetFilter.Occupiable(other));}
				}
			}
			else {Targets.Add(square.Occupants - Filter);}
			return Targets;
		}

 {
			CellSet square = new CellSet();
			Cell c;
			for (int x=(start.X-range); x<=(start.X+range); x++) {
				for (int y=(start.Y-range); y<=(start.Y+range); y++) {
					if (Game.Board.HasCell(x, y, out c)) {square.Add(c);}
				}
			}
			if (min > 0) {square = RemoveMin(square, start, min);}
			return square;
		}
		
		static CellSet RemoveMin (CellSet square, Cell start, int min) {
            CellSet ring = new CellSet();
			foreach (Cell c in square) {
				if ((Math.Abs(c.X - start.X) >= min) 
				    || (Math.Abs(c.Y - start.Y) >= min)) {
					ring.Add(c);
				}
			}
			return ring;
		}

		TargetSet FindCreateAren (Token actor, Cell center, Token other) {
			TargetSet Targets = new TargetSet();

			center = actor.Body.Cell;
			CellSet cells = NeighborsExtra(center);
			foreach (Cell c in cells) {
				if (TokenRegistry.Templates[Species.Arena].Body.CanEnter(c)) {
					Targets.Add(c);
				}
			}
			Targets.Add(cells);
			return Targets;
		}

		CellSet NeighborsExtra (Cell center) {
            CellSet cells = center.NeighborsAndSelf;
            CellSet extras = new CellSet();
			Cell extra;
			List<int2> directions = new List<int2> {Direction.Up, Direction.Left, Direction.UpLeft};

			foreach (Cell c in cells) {
				foreach (int2 dir in directions) {
					if (Game.Board.HasCell(c.Index + dir, out extra)) {
						if (!cells.Contains(extra) 
						    && !(extra is ExoCell)) {
							extras.Add(extra);
						}
					}
				}
			}
			cells.Add(extras);
			return cells;
		}

		bool ArenSquare (Token aren, Cell head) {
            CellSet square = new CellSet(head);
			Cell tail;
			List<int2> directions = new List<int2> {Direction.Down, Direction.Right, Direction.DownRight};
			foreach (int2 dir in directions) {
				if (Game.Board.HasCell(head.Index + dir, out tail)) {
					if (!(tail is ExoCell)) {square.Add(tail);}
				}
			}
			if (square.Count == 4) {
				foreach (Cell c in square) {
					if (!(aren.Body.CanEnter(c))) {return false;}
				}
				return true;
			}
			return false;
		}
	}
}
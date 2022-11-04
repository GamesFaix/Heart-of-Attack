using System;
using System.Collections.Generic;
using UnityEngine;

namespace HOA { 

	public partial class Aim {
	
		TargetGroup FindNeighbor (Token actor, Cell center, Token other) {
			if (center == null) {center = actor.Body.Cell;}

			TargetGroup targets = new TargetGroup();
			
			TargetGroup neighborCells = center.Neighbors(true);
			if (Purpose == EPurp.MOVE) {neighborCells.Remove(center);}
			
			if (Filter.Contains(FilterTests.Cell)) 
            {
				if (Purpose == EPurp.CREATE) {targets.Add(neighborCells - TargetFilter.Occupiable(other));}
				if (Purpose == EPurp.MOVE) {targets.Add(neighborCells - TargetFilter.Occupiable(actor));}
			}
			else {targets.Add(neighborCells.Occupants - Filter);}
			return targets;
		}

		TargetGroup FindFree (Token actor, Cell center, Token other) {
			//center does nothing
			TargetGroup targets = new TargetGroup();

            if (Filter.Contains(FilterTests.Cell))
            {
				if (Purpose == EPurp.CREATE) {targets.Add(Game.Board.Cells - TargetFilter.Occupiable(other));}
				if (Purpose == EPurp.MOVE) {targets.Add(Game.Board.Cells -TargetFilter.Occupiable(actor));}
			}
			else {targets.Add(TokenFactory.Tokens - Filter);}
			return targets;
		}

		TargetGroup FindPath (Token actor, Cell center, Token other=null) {
			if (center == null) {center = actor.Body.Cell;}

            if (!Filter.Contains(FilterTests.Cell))
            {
				TargetGroup targets = new TargetGroup();
				
				TargetGroup thisRad = center.Neighbors();
				TargetGroup nextRad = new TargetGroup();
				TargetGroup marked = new TargetGroup();
				
				for (int i=1; i<=Range; i++) {
					
					foreach (Cell c in thisRad) {
						targets.Add(c.Occupants - Filter);
						marked.Add(c);
						
						if (c.Occupants.Count==0 ||
						    (c.Occupants.Count==1 && c.Contains(Planes.Sunken))) {
							
							foreach (Cell d in c.Neighbors()) {
								if (!marked.Contains(d)) {nextRad.Add(d);}		
							}
						}
					}
					thisRad = nextRad;
					nextRad = new TargetGroup();
				}
				return targets;
			}
			else {throw new ArgumentException("Path trajectory can only be used to target tokens.  Cell targets must use repeated neighbor trajectory.");}
		}

		TargetGroup FindLine (Token actor, Cell center, Token other) {
			if (center == null) {center = actor.Body.Cell;}

			TargetGroup targets = new TargetGroup();
			List<TargetGroup> star = CellStar(center, Range);
			foreach (TargetGroup line in star) {
                if (Filter.Contains(FilterTests.Cell))
                {
					if (Purpose == EPurp.MOVE) {targets.Add(LineUntilStop(line, actor));}
					if (Purpose == EPurp.CREATE) {targets.Add(LineUntilStop(line, other));}
				}
				else {
                    TargetGroup cells = LineUntilStop(line, actor, true);
                    cells.Add(center);
                    targets.Add(cells.Occupants - Filter);
				}
			}
			return targets;
		}

		static List<TargetGroup> CellStar (Cell start, int range) {
			List<TargetGroup> star = new List<TargetGroup>();
			
			foreach (int2 dir in Direction.Directions) {
				TargetGroup line = new TargetGroup();
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
		
		static TargetGroup LineUntilStop (TargetGroup line, Token Parent, bool inclusive=false) {
			TargetGroup legal = new TargetGroup();
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
		
		static TargetGroup LineUntilToken (TargetGroup line) {
			TargetGroup legal = new TargetGroup();
			foreach (Cell c in line) {
				legal.Add(c);
				if (!(c.IsEmpty() || c.ContainsOnly(Planes.Sunken))) {
					break;
				}
			}
			return legal;
		}

		TargetGroup FindRadial (Token actor, Cell firstCell, Token other) {
			Cell center = actor.Body.Cell;
			NeighborMatrix neighbors = new NeighborMatrix(center);
			
			Cell nextClockwise;
			Cell nextCounter;
			
			TargetGroup targets = new TargetGroup();
			
			if (neighbors.CellClockwise(firstCell, out nextClockwise)) {
				targets.Add(nextClockwise);
			}
			if (neighbors.CellCounter(firstCell, out nextCounter)) {
				targets.Add(nextCounter);
			}
			return targets;
		}

		TargetGroup FindArc (Token actor, Cell center, Token other) {
			if (center == null) {center = actor.Body.Cell;}

			TargetGroup targets = new TargetGroup();
			TargetGroup square = CellSquare (center, Range, MinRange);

            if (Filter.Contains(FilterTests.Cell))
            {
				if (Purpose == EPurp.ATTACK) {targets.Add(square);}
				if (Purpose == EPurp.CREATE) {targets.Add(square - TargetFilter.Occupiable(other));}
				if (Purpose == EPurp.MOVE) {
					if (other == default(Token)) {targets.Add(square - TargetFilter.Occupiable(actor));}
					else {targets.Add(square - TargetFilter.Occupiable(other));}
				}
			}
			else {targets.Add(square.Occupants - Filter);}
			return targets;
		}

		static TargetGroup CellSquare (Cell start, int range, int min) {
			TargetGroup square = new TargetGroup();
			Cell c;
			for (int x=(start.X-range); x<=(start.X+range); x++) {
				for (int y=(start.Y-range); y<=(start.Y+range); y++) {
					if (Game.Board.HasCell(x, y, out c)) {square.Add(c);}
				}
			}
			if (min > 0) {square = RemoveMin(square, start, min);}
			return square;
		}
		
		static TargetGroup RemoveMin (TargetGroup square, Cell start, int min) {
			TargetGroup ring = new TargetGroup();
			foreach (Cell c in square) {
				if ((Math.Abs(c.X - start.X) >= min) 
				    || (Math.Abs(c.Y - start.Y) >= min)) {
					ring.Add(c);
				}
			}
			return ring;
		}

		TargetGroup FindCreateAren (Token actor, Cell center, Token other) {
			TargetGroup targets = new TargetGroup();

			center = actor.Body.Cell;
			TargetGroup cells = NeighborsExtra(center);
			foreach (Cell c in cells) {
				if (TokenFactory.Template(EToken.AREN).Body.CanEnter(c)) {
					targets.Add(c);
				}
			}
			targets.Add(cells);
			return targets;
		}

		TargetGroup NeighborsExtra (Cell center) {
			TargetGroup cells = center.Neighbors(true);
			TargetGroup extras = new TargetGroup();
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
			TargetGroup square = new TargetGroup(head);
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
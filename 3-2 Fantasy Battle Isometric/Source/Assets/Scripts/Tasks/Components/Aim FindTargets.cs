using System;
using System.Collections.Generic;
using UnityEngine;

namespace HOA { 

	public partial class Aim {
	
		TargetGroup FindNeighbor (Token actor, Cell center, Token other) {
			if (center == null) {center = actor.Body.Cell;}
			CellGroup cells = center.Neighbors(true);
			TargetGroup targets = (TargetGroup)(cells) + (TargetGroup)(cells.Occupants);
			return ((other == null) ? Filter(targets, actor) : Filter(targets, other));
		}

		TargetGroup FindFree (Token actor, Cell center, Token other) {
			CellGroup cells = Game.Board.Cells;
			TargetGroup targets = (TargetGroup)(cells) + (TargetGroup)(cells.Occupants);
			return (other == null ? Filter(targets, actor) : Filter(targets, other));
		}

		TargetGroup FindArc (Token actor, Cell center, Token other) {
			if (center == null) {center = actor.Body.Cell;}
			CellGroup cells = CellSquare (center, range, minRange);
			TargetGroup targets = (TargetGroup)cells + (TargetGroup)(cells.Occupants);
			return (other == null ? Filter(targets, actor) : Filter(targets, other));
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
				if ((Math.Abs(c.X - start.X) >= min) 
				    || (Math.Abs(c.Y - start.Y) >= min)) {
					ring.Add(c);
				}
			}
			return ring;
		}

		TargetGroup FindPath (Token actor, Cell center, Token other=null) {
			if (center == null) {center = actor.Body.Cell;}

			if (!(Filter == Filters.Cells)) {
				TargetGroup targets = new TargetGroup();
				
				CellGroup thisRad = center.Neighbors();
				CellGroup nextRad = new CellGroup();
				CellGroup marked = new CellGroup();
				
				for (int i=1; i<=range; i++) {
					
					foreach (Cell c in thisRad) {
						targets += Filter(c.Occupants, actor);
						marked.Add(c);
						
						if (c.Occupants.Count==0 ||
						    (c.Occupants.Count==1 && (c.Occupants/Plane.Sunken).Count > 0)) {
							
							foreach (Cell d in c.Neighbors()) {
								if (!marked.Contains(d)) {nextRad.Add(d);}		
							}
						}
					}
					thisRad = nextRad;
					nextRad = new CellGroup();
				}
				return targets;
			}
			else {throw new ArgumentException("Path trajectory can only be used to target tokens.  Cell targets must use repeated neighbor trajectory.");}
		}

		TargetGroup FindLine (Token actor, Cell center, Token other) {
			if (center == null) {center = actor.Body.Cell;}

			TargetGroup targets = new TargetGroup();
			List<CellGroup> star = CellStar(center, range);
			foreach (CellGroup line in star) {
				if (Filter == Filters.Move) {targets += (TargetGroup)(LineUntilStop(line, actor));}
				else if (Filter == Filters.Create) {targets += (TargetGroup)(LineUntilStop(line, other));}
				else {
					targets += Filter(LineUntilStop(line,actor,true).Occupants, actor);
					targets += Filter(center.Occupants, actor);
				}
			}
			return targets;
		}

		static List<CellGroup> CellStar (Cell start, int range) {
			List<CellGroup> star = new List<CellGroup>();
			
			foreach (int2 dir in Direction.Directions) {
				CellGroup line = new CellGroup();
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
		
		static CellGroup LineUntilStop (CellGroup line, Token Parent, bool inclusive=false) {
			CellGroup legal = new CellGroup();
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
		
		static CellGroup LineUntilToken (CellGroup line) {
			CellGroup legal = new CellGroup();
			foreach (Cell c in line) {
				legal.Add(c);
				if (!(c.IsEmpty() || 
				     (c.Occupants.Count == 1 && (c.Occupants/Plane.Sunken).Count > 0))) {
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



		TargetGroup FindCreateAren (Token actor, Cell center, Token other) {
			TargetGroup targets = new TargetGroup();

			center = actor.Body.Cell;
			CellGroup cells = NeighborsExtra(center);
			foreach (Cell c in cells) {
				if (TokenFactory.Template(EToken.AREN).Body.CanEnter(c)) {
					targets.Add(c);
				}
			}
			targets.Add(cells);
			return targets;
		}

		CellGroup NeighborsExtra (Cell center) {
			CellGroup cells = center.Neighbors(true);
			CellGroup extras = new CellGroup();
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
			CellGroup square = new CellGroup(head);
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
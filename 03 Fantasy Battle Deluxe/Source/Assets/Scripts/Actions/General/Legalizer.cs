using HOA.Tokens;
using HOA.Map;
using UnityEngine;

namespace HOA.Actions {
	
	public static class Legalizer {
		
		public static void Find (Token t, Aim a, Token other=default(Token)) {
			switch (a.AimType) {
				case AIMTYPE.CELLMATE: FindCellmate(t, a, other); break;
				case AIMTYPE.NEIGHBOR: FindNeighbor(t, a, other); break;
				case AIMTYPE.PATH: FindPath(t, a, other); break;
				case AIMTYPE.LINE: FindLine(t, a, other); break;
				case AIMTYPE.ARC: FindArc(t, a, other); break;
				case AIMTYPE.GLOBAL: FindGlobal(t, a, other); break;
				default: break;
			}
		}
		
		//cellmate
		static void FindCellmate (Token t, Aim a, Token other) {}
		
		//neighbor
		static void FindNeighbor (Token t, Aim a, Token other) {
			if (a.Target == TARGET.TOKEN) {NeighborTokens(t, a);}
			if (a.CTar == CTAR.CREATE) {NeighborCellCreate(t, other);}
		}
		
		static void NeighborTokens (Token actor, Aim a) {
			TokenGroup neighbors = actor.Cell.Neighbors().Occupants;
			neighbors.Add(actor.Cell.Occupants);
			TokenGroup legalTargets = neighbors.FilterTarget(a.TTar);
			foreach (Token t in legalTargets) {t.Legal = true;}
		}
		
		static void NeighborCellCreate (Token actor, Token child) {
			Cell start = actor.Cell;
			CellGroup neighbors = start.Neighbors();
			foreach (Cell c in neighbors) {
				if (child.CanEnter(c)) {
					c.Legal = true;
				}
			}
		}
		
		//path
		static void FindPath (Token t, Aim a, Token other) {
			if (a.Target == TARGET.TOKEN) {PathTokens (t,a);}
			if (a.CTar == CTAR.MOVE) {PathCellMove (t,a);}
		}
		
		static void PathTokens (Token actor, Aim a) {
			int range = a.Range;
			Cell start = actor.Cell;
			
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
							if (next.IsEmpty()) {
								nextRad.Add(next);
							}
							TokenGroup occupants = next.Occupants;
							occupants = occupants.FilterTarget(a.TTar);
							foreach (Token t in occupants) {t.Legal = true;}
						}
					}
				}
			}
		}
			
		static void PathCellMove (Token actor, Aim a) {
			int range = a.Range;
			Cell start = actor.Cell;
			
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
							if (actor.CanEnter(next)) {
								if (!legal.Contains(next)) {
									nextRad.Add(next);
									legal.Add(next);
									next.Legal = true;
								}
							}
						}
					}
				}
			}
			
		}
		
		
		//line
		static void FindLine (Token t, Aim a, Token other) {
			if (a.Target == TARGET.TOKEN) {LineTokens(t,a);}
			if (a.CTar == CTAR.MOVE) {LineCellMove(t,a);}
		}
			
		static void LineTokens (Token actor, Aim a) {
			int range = a.Range;
			Cell start = actor.Cell;
			
			for (int i=0; i<8; i++) {
				int[] dir = Direction.FromInt(i);
				Cell last = start;
				for (int j=1; j<=range; j++) {
					Cell next;
					int checkX = last.X + dir[0];
					int checkY = last.Y + dir[1];					
					
					if (Board.HasCell(checkX, checkY, out next)) {
						if (next.IsEmpty()) {
							last = next;
						}
						TokenGroup occupants = next.Occupants;
						occupants = occupants.FilterTarget(a.TTar);
						foreach (Token t in occupants) {t.Legal = true;}
					}
					else {break;}
				}
			}
		}
			
		static void LineCellMove (Token actor, Aim a) {
			int range = a.Range;
			Cell start = actor.Cell;
			CellGroup legal = new CellGroup();
			
			for (int i=0; i<8; i++) {
				int[] dir = Direction.FromInt(i);
				Cell last = start;
				for (int j=1; j<=range; j++) {
					Cell next;
					int checkX = last.X + dir[0];
					int checkY = last.Y + dir[1];					
					
					if (Board.HasCell(checkX, checkY, out next)) {
						if (actor.CanEnter(next)) {
							legal.Add(next);
							next.Legal = true;
							last = next;
						}
					}
					else {break;}
				}
			}
		}
		
		//arc
		static void FindArc (Token t, Aim a, Token other) {
			if (a.Target == TARGET.TOKEN) {ArcTokens(t,a);}
			if (a.CTar == CTAR.ATTACK) {ArcCellAttack(t,a);}
			if (a.CTar == CTAR.CREATE) {ArcCellCreate(t,a,other);}
			
		}
		static void ArcTokens (Token actor, Aim a) {
			int r = a.Range;
			Cell start = actor.Cell;
			int left = start.X-r;
			int right = start.X+r;
			int top = start.Y-r;
			int bottom = start.Y+r;
			
			for (int x=left; x<=right; x++) {
				for (int y=top; y<=bottom; y++) {
					Cell c;
					if (Board.HasCell(x, y, out c)) {
						TokenGroup occupants = c.Occupants;
						occupants = occupants.FilterTarget(a.TTar);
						foreach (Token t in occupants) {t.Legal = true;}
					}
				}
			}
		}
				
		static void ArcCellCreate (Token actor, Aim a, Token child) {
			int r = a.Range;
			
			Cell start = actor.Cell;
			int left = start.X-r;
			int right = start.X+r;
			int top = start.Y-r;
			int bottom = start.Y+r;
			
			for (int x=left; x<=right; x++) {
				for (int y=top; y<=bottom; y++) {
					Cell c;
					if (Board.HasCell(x, y, out c)) {
						if (child.CanEnter(c)) {
							c.Legal = true;
						}	
					}
				}
			}
		}
		static void ArcCellAttack (Token actor, Aim a) {
			int r = a.Range;
			Cell start = actor.Cell;
			int left = start.X-r;
			int right = start.X+r;
			int top = start.Y-r;
			int bottom = start.Y+r;
			
			for (int x=left; x<=right; x++) {
				for (int y=top; y<=bottom; y++) {
					Cell c;
					if (Board.HasCell(x, y, out c)) {
						c.Legal = true;
					}
				}
			}
		}
		
		static void FindGlobal (Token t, Aim a, Token other) {}

		/*
		public static void FilterTeammate (Token t) {
			foreach (Unit u in TurnQueue) {
				if (u.Owner() != t.Owner()) {
					u.Legalize(false);
				}
			}
		}

		public static void FilterEnemy (Token t) {
			foreach (Unit u in TurnQueue) {
				if (u.Owner() == t.Owner()) {
					u.Legalize(false);
				}
			}
		}
		*/
	}
}
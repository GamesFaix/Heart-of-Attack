using UnityEngine;

namespace HOA {
	
	public static class Legalizer {
		
		public static void Find (Token t, Aim a, Token other=default(Token)) {
			//GUISelectors.Reset();
			switch (a.AimType) {
				case AIMTYPE.CELLMATE: FindCellmate(t, a, other); break;
				case AIMTYPE.NEIGHBOR: FindNeighbor(t, a, other); break;
				case AIMTYPE.PATH: FindPath(t, a, other); break;
				case AIMTYPE.LINE: FindLine(t, a, other); break;
				case AIMTYPE.ARC: FindArc(t, a, other); break;
				case AIMTYPE.GLOBAL: FindGlobal(t, a, other); break;
				case AIMTYPE.FREE: FindFree(t,a, other); break;
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
			TokenGroup targets = actor.Cell.Neighbors().Occupants;
			targets.Add(actor.Cell.Occupants);
			targets = targets.FilterTarget(a.TTar);
			targets = Restrict(targets, actor, a);
			foreach (Token t in targets) {t.Legal = true;}
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
							if (next.IsEmpty() || next.ContainsOnly(PLANE.SUNK)) {
								nextRad.Add(next);
							}
							TokenGroup targets = next.Occupants;
							targets = targets.FilterTarget(a.TTar);
							targets = Restrict(targets, actor, a);
							foreach (Token t in targets) {t.Legal = true;}
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
									if ( ! (actor.IsSpecial(SPECIAL.TRAM) && next.Contains(SPECIAL.DEST))) {
										nextRad.Add(next);
									}


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
						if (next.IsEmpty() || next.ContainsOnly(PLANE.SUNK)) {
							last = next;
						}
						TokenGroup targets = next.Occupants;
						targets = targets.FilterTarget(a.TTar);
						targets = Restrict(targets, actor, a);
						foreach (Token t in targets) {
							//Debug.Log(t.ToString() +" is a legal target");
							t.Legal = true;}
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
							if ( ! (actor.IsSpecial(SPECIAL.TRAM) && next.Contains(SPECIAL.DEST))) {
								last = next;
							}
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
			if (a.CTar == CTAR.MOVE) {ArcCellMove(t,a);}
			
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
						TokenGroup targets = c.Occupants;
						targets = targets.FilterTarget(a.TTar);
						targets = Restrict(targets, actor, a);
						foreach (Token t in targets) {t.Legal = true;}
					}
				}
			}
		}
				
		static void ArcCellMove (Token actor, Aim a) {
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
						if (actor.CanEnter(c)) {
							c.Legal = true;
						}	
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
			int min = a.MinRange;
			Cell start = actor.Cell;
			int left = start.X-r;
			int right = start.X+r;
			int top = start.Y-r;
			int bottom = start.Y+r;
			CellGroup legalCells = new CellGroup();

			for (int x=left; x<=right; x++) {
				for (int y=top; y<=bottom; y++) {
					Cell c;
					if (Board.HasCell(x, y, out c)) {
						c.Legal = true;
						legalCells.Add(c);
						//Debug.Log("Legalizing "+c.ToString()+". Last legal cell is now "+legalCells[legalCells.Count-1]);
					}
				}
			}

			if (min > 0) {
				//Debug.Log (actor.Cell.ToString()+" "+a.ToString()+ " "+legalCells.Count);
				foreach (Cell c in legalCells) {
					//Debug.Log("checking "+c.ToString() + " "+c.X+ "/"+c.Y);
					if (
						(c.X - start.X < min) || (start.X - c.X < min)
						//(Mathf.Abs(c.X - start.X) < min) 
					//&& 
						//(Mathf.Abs(c.Y - start.Y) < min)
					    ) {
						c.Legal = false;
						legalCells.Remove(c);
					}
				}
			}
		}
		
		static void FindGlobal (Token t, Aim a, Token other) {}

		static void FindFree (Token actor, Aim a, Token other) {
			//Debug.Log("legalizer.findFree/ actor:"+actor+" / aim:"+a+" / other:"+other);
			if (a.Target == TARGET.TOKEN) {FreeTokens(actor,a);}
			else {
				if (a.CTar == CTAR.CREATE) {FreeCellCreate(actor, a, other);}




			}
		}

		static void FreeTokens (Token actor, Aim a) {
			TokenGroup targets = new TokenGroup();
			foreach (Token t in TokenFactory.Tokens) {
				targets.Add(t);
			}
			if (a.Target == TARGET.TOKEN) {targets = targets.FilterTarget(a.TTar);}
			targets = Restrict(targets, actor, a);
			foreach (Token t in targets) {
				t.Legal = true;
			}
		}

		static void FreeCellCreate (Token actor, Aim a, Token other) {
			//Debug.Log("legalizer.FreeCellCreate");
			foreach (Cell c in Board.cells) {
				if (other.CanEnter(c)) {
				//	Debug.Log("legalizing "+c);
					c.Legal = true;}
			}
		}

		static TokenGroup Restrict (TokenGroup tokens, Token actor, Aim a) {
			TokenGroup restricted = new TokenGroup();
			foreach (Token t in tokens) {restricted.Add(t);}

			if (a.TeamOnly) {
				for (int i=restricted.Count-1; i>=0; i--) {
					Token t = restricted[i];
					if (t.Owner != actor.Owner) {restricted.Remove(t);}
				}
			}
			if (a.EnemyOnly) {
				for (int i=restricted.Count-1; i>=0; i--) {
					Token t = restricted[i];
					if (t.Owner == actor.Owner) {restricted.Remove(t);}
				}
			}
			if (a.NoKings) {
				for (int i=restricted.Count-1; i>=0; i--) {
					Token t = restricted[i];
					if (t.IsSpecial(SPECIAL.KING)) {restricted.Remove(t);}
				}
			}
			if (!a.IncludeSelf) {restricted.Remove(actor);}
			return restricted;
		}
	}
}
using UnityEngine;

namespace HOA {
	
	public static class Legalizer {
		
		public static void Find (Token t, Aim a, Token other=default(Token)) {
			//GUISelectors.Reset();
			switch (a.AimType) {
				case EAim.CELLMATE: FindCellmate(t, a, other); break;
				case EAim.NEIGHBOR: FindNeighbor(t, a, other); break;
				case EAim.PATH: FindPath(t, a, other); break;
				case EAim.LINE: FindLine(t, a, other); break;
				case EAim.ARC: FindArc(t, a, other); break;
				case EAim.GLOBAL: FindGlobal(t, a, other); break;
				case EAim.FREE: FindFree(t,a, other); break;
				default: break;
			}
		}
		
		//cellmate
		static void FindCellmate (Token t, Aim a, Token other) {}

		//neighbor
		static void FindNeighbor (Token t, Aim a, Token other) {
			if (!a.TargetClass.Contains(EClass.CELL)) {NeighborTokens(t, a);}
			else {
				if (a.Purpose == EPurpose.CREATE) {NeighborCellCreate(t, other);}
			}
		}

		static void NeighborTokens (Token actor, Aim a) {
			TokenGroup targets = actor.Cell.Neighbors().Occupants;
			targets.Add(actor.Cell.Occupants);
			targets = targets.OnlyClass(a.TargetClass);
			targets = Restrict(targets, actor, a);
			foreach (Token t in targets) {t.Legalize();}
		}
		
		static void NeighborCellCreate (Token actor, Token child) {
			Cell start = actor.Cell;
			CellGroup neighbors = start.Neighbors();
			foreach (Cell c in neighbors) {
				if (child.CanEnter(c)) {
					c.Legalize();
				}
			}
		}
		
		//path
		static void FindPath (Token t, Aim a, Token other) {
			if (!a.TargetClass.Contains(EClass.CELL)) {PathTokens (t,a);}
			else {
				if (a.Purpose == EPurpose.MOVE) {PathCellMove (t,a);}
		
			}
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
							if (next.IsEmpty() || next.ContainsOnly(EPlane.SUNK)) {
								nextRad.Add(next);
							}
							TokenGroup targets = next.Occupants;
							targets = targets.OnlyClass(a.TargetClass);
							targets = Restrict(targets, actor, a);
							foreach (Token t in targets) {t.Legalize();}
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
									if ( ! (actor.IsClass(EClass.TRAM) && next.Contains(EClass.DEST))) {

										if (!Stop(actor, next)) {

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
		
		
		//line
		static void FindLine (Token t, Aim a, Token other) {
			if (!a.TargetClass.Contains(EClass.CELL)) {LineTokens(t,a);}
			else {
				if (a.Purpose == EPurpose.MOVE) {LineCellMove(t,a);}
		
			}
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
						if (next.IsEmpty() || next.ContainsOnly(EPlane.SUNK)) {
							last = next;
						}
						TokenGroup targets = next.Occupants;
						targets = targets.OnlyClass(a.TargetClass);
						targets = Restrict(targets, actor, a);
						foreach (Token t in targets) {
							//Debug.Log(t.ToString() +" is a legal target");
							t.Legalize();}
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
							next.Legalize();
							if ( ! (actor.IsClass(EClass.TRAM) && next.Contains(EClass.DEST))) {
								if (!Stop(actor, next)) {
									last = next;
								}
							}
						}
					}
					else {break;}
				}
			}
		}
		
		//arc
		static void FindArc (Token t, Aim a, Token other) {
			if (!a.TargetClass.Contains(EClass.CELL)) {ArcTokens(t,a);}
			else {
				if (a.Purpose == EPurpose.ATTACK) {ArcCellAttack(t,a);}
				if (a.Purpose == EPurpose.CREATE) {ArcCellCreate(t,a,other);}
				if (a.Purpose == EPurpose.MOVE) {ArcCellMove(t,a);}
			}
			
		}
		static void ArcTokens (Token actor, Aim a) {
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
						legalCells.Add(c);
					}
				}
			}
			Debug.Log(min);

			if (min > 0) {
				legalCells = MinRangeRemoved(legalCells, start, min);
			}

			foreach (Cell c in legalCells) {
				TokenGroup targets = c.Occupants;
				targets = targets.OnlyClass(a.TargetClass);
				targets = Restrict(targets, actor, a);
				foreach (Token t in targets) {t.Legalize();}
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
							c.Legalize();
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
							c.Legalize();
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
						legalCells.Add(c);
						//Debug.Log("Legalizing "+c.ToString()+". Last legal cell is now "+legalCells[legalCells.Count-1]);
					}
				}
			}

			if (min > 0) {
				legalCells = MinRangeRemoved(legalCells, start, min);
			}

			foreach (Cell c in legalCells) {c.Legalize();}
		}

		static CellGroup MinRangeRemoved (CellGroup cs, Cell start, int min) {
			CellGroup legal = new CellGroup();
			foreach (Cell c in cs) {
				if ((Mathf.Abs(c.X - start.X) >= min) 
				|| (Mathf.Abs(c.Y - start.Y) >= min)) {
					legal.Add(c);
				}
			}
			return legal;
		}
		
		static void FindGlobal (Token t, Aim a, Token other) {}

		static void FindFree (Token actor, Aim a, Token other) {
			//Debug.Log("legalizer.findFree/ actor:"+actor+" / aim:"+a+" / other:"+other);
			if (!a.TargetClass.Contains(EClass.CELL)) {FreeTokens(actor,a);}
			else {
				if (a.Purpose == EPurpose.CREATE) {FreeCellCreate(actor, a, other);}

				if (a.Purpose == EPurpose.MOVE) {FreeCellMove(actor, a, other);}


			}
		}

		static void FreeTokens (Token actor, Aim a) {
			TokenGroup targets = new TokenGroup();
			foreach (Token t in TokenFactory.Tokens) {
				targets.Add(t);
			}
			targets = targets.OnlyClass(a.TargetClass);
			targets = Restrict(targets, actor, a);
			foreach (Token t in targets) {
				t.Legalize();
			}
		}

		static void FreeCellCreate (Token actor, Aim a, Token other) {
			//Debug.Log("legalizer.FreeCellCreate");
			foreach (Cell c in Board.cells) {
				if (other.CanEnter(c)) {
				//	Debug.Log("legalizing "+c);
					c.Legalize();}
			}
		}
		static void FreeCellMove (Token actor, Aim a, Token other) {
			//Debug.Log("legalizer.FreeCellCreate");
			foreach (Cell c in Board.cells) {
				if (other.CanEnter(c)) {
					//	Debug.Log("legalizing "+c);
					c.Legalize();}
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
					if (t.IsClass(EClass.KING)) {restricted.Remove(t);}
				}
			}
			if (!a.IncludeSelf) {restricted.Remove(actor);}
			return restricted;
		}

		static bool Stop (Token mover, Cell c) {
			foreach (EPlane p in mover.Plane) {
				if (c.Stop(p)) {return true;}
			}
			return false;
		}
	}
}
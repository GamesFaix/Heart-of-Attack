using System;
using System.Collections.Generic;
using UnityEngine;

namespace HOA{
	public class ArenaNonSensus : Unit {
		public ArenaNonSensus(Source s, bool template=false){
			id = new ID(this, EToken.AREN, s, false, template);
			body = new BodyAren(this);
			plane = Plane.Eth;
			onDeath = EToken.NONE;

			ScaleQuad();
			NewHealth(55,3);
			NewWatch(2);
			BuildArsenal();	
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			arsenal.Add(new Task[]{
				new AMoveAren(this, 3),
				new AArenLeech (this),
				new AArenDonate (this)
			});
			arsenal.Sort();
		}

		public override string Notes () {return "";}

		public CellGroup Cells {get {return ((BodyAren)body).Cells;} }
	}
		
	public class BodyAren : Body{

		public BodyAren (Token t){
			parent = t;
		}

		CellGroup cells;
		public CellGroup Cells {get {return cells;} }

		public override TokenGroup Neighbors (bool cellMates = false) {
			TokenGroup neighbors = new TokenGroup();
			foreach (Cell c in cells) {
				neighbors.Add(c.Neighbors().Occupants);
			}
			if (!cellMates) {
				foreach (Cell c in cells) {
					TokenGroup occupants = c.Occupants;
					foreach (Token t in occupants) {
						neighbors.Remove(t);
					}
				}
			}
			return neighbors;
		}
		
		public override TokenGroup CellMates {
			get {
				TokenGroup cellMates = new TokenGroup();
				foreach (Cell c in cells) {
					cellMates.Add(c.Occupants);
				}
				cellMates.Remove(parent);
				return cellMates;
			}
		}

		//starts at bottom-left
		public override bool CanEnter (Cell newCell) {
			CellGroup block;
			if (FourBlock(newCell, out block)) {
				foreach (Cell c in block) {
					if (c.Occupied(parent.Plane.Value) && !cells.Contains(c)) {return false;}
				}
				return true;
			}
			return false;
		}

		bool FourBlock (Cell bottomLeft, out CellGroup block) {
			block = new CellGroup(bottomLeft);

			Cell c;
			if (Board.HasCell(bottomLeft.X+1, bottomLeft.Y, out c)) {block.Add(c);}
			if (Board.HasCell(bottomLeft.X, bottomLeft.Y+1, out c)) {block.Add(c);}
			if (Board.HasCell(bottomLeft.X+1, bottomLeft.Y+1, out c)) {block.Add(c);}

			if (block.Count == 4) {return true;}
			return false;
		}

		/*
		bool CanTakePlaceOf (Token t) {
			Cell cell = parent.Cell;
			Cell otherCell = t.Cell;
			Token blocker;
			
			foreach (EPlane p in Plane) {
				if (otherCell.Contains(p, out blocker)) {
					if (blocker != t) {return false;}
				}
			}
			return true;
		}
		
		public bool CanSwap (Token other) {
			if (CanTakePlaceOf(other) && other.Body.CanTakePlaceOf(parent)) {return true;}
			return false;
		}
		*/

		public override bool Enter (Cell newCell) {
			if (CanEnter(newCell)) {
				if (cell != default(Cell)) {Exit();}
				cell = newCell;
				CellGroup block;
				if (FourBlock(newCell, out block)) {
					cells = new CellGroup();
					foreach (Cell c in block) {
						c.Enter(parent);
						cells.Add(c);
					}
				}
				return true;
			}	
			if (newCell == TemplateFactory.c) {
				cell = newCell;
				return true;	
			}
			return false;
		}
		/*
		public bool Swap (Token other) {
			if (CanSwap(other)) {
				Cell oldCell = cell;
				Exit();
				cell = other.Cell;
				other.Cell.Enter(parent);
				
				other.Exit();
				other.Body.Cell = oldCell;
				oldCell.Enter(other);
				
				return true;
			}	
			return false;
		}
		
		*/
		public override void Exit () {
			foreach (Cell c in cells) {c.Exit(parent);}
		}

	}

	public class AMoveAren : Task, IMultiMove {
		
		int range;
		public int Optional () {return 1;}

		public override string Desc {get {return "Move "+Parent+" to target cell.";} }

		public AMoveAren (Unit u, int r) {
			Name = "Move";
			Weight = 1;

			Parent = u;

			range = r;
			for (int i=0; i<range; i++) {
				Aim a = new Aim(ETraj.NEIGHBOR, EType.CELL, EPurp.MOVE) ;
				AddAim(a);
				//Debug.Log(a);
			}
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			foreach (Target target in targets) {
				EffectQueue.Add(new EMove(new Source(Parent), Parent, (Cell)target));
			}
		}
		
		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, Name, p.s);
			
			DrawPrice(new Panel(p.LineBox, p.LineH, p.s));
			
			Aim actual = new Aim(ETraj.PATH, EType.CELL, EPurp.MOVE, range);
			actual.Draw(new Panel(p.LineBox, p.LineH, p.s));
			
			float descH = (p.H-(p.LineH*2))/p.H;
			//Rect descBox = new Rect(p.x2, p.y2, p.W, descH);
			
			GUI.Label(p.TallBox(descH), Desc);	
		}
	}

	public class AArenLeech : Task {
		
		int damage = 7;

		public override string Desc {get {return  "Do "+damage+" damage to all enemy cellmates. " +
				"\nGain health equal to damage successfully dealt.";} }

		public AArenLeech (Unit u) {
			Name = "Leech life";
			Weight = 3;

			Price = new Price(1,0);
			AddAim(HOA.Aim.Self());
			Parent = u;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			TokenGroup tokens = Parent.Body.CellMates;
			tokens = tokens.OnlyType(EType.UNIT);
			tokens = tokens.RemoveOwner(Parent.Owner);
			foreach (Token t in tokens) {
				EffectQueue.Add(new ELeech(new Source(Parent), (Unit)t, damage));
			}
		}
	}

	public class AArenDonate : Task {
		
		int damage = 7;

		public override string Desc {get {return  "All friendly cellmates +"+damage+" health. " +
				"\nLose health equal to health successfully given.";} }
		
		public AArenDonate (Unit u) {
			Name = "Donate life";
			Weight = 3;

			Price = new Price(1,0);
			AddAim(HOA.Aim.Self());
			Parent = u;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			TokenGroup tokens = Parent.Body.CellMates;
			tokens = tokens.OnlyType(EType.UNIT);
			tokens = tokens.OnlyOwner(Parent.Owner);
			foreach (Token t in tokens) {
				EffectQueue.Add(new EDonate(new Source(Parent), (Unit)t, damage));
			}
		}
	}
}
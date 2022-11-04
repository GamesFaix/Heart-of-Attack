using UnityEngine; 
using System.Collections.Generic;

namespace HOA { 
	
	public class BodyAren : Body {

		public BodyAren (Token parent, bool template=false) : base(parent){
			if (!template) {
                Aliases.Add(Alias.Arena(new Source(parent)));
                Aliases.Add(Alias.Arena(new Source(parent)));
                Aliases.Add(Alias.Arena(new Source(parent)));
              
				foreach (Alias a in Aliases) 
					TokenDisplay.Attach(a);
				
                EnterSpecial = (c) =>
                {
                    Aliases[0].Body.Enter(Game.Board.Cell(Cell.Index + Direction.Right), false);
                    Aliases[1].Body.Enter(Game.Board.Cell(Cell.Index + Direction.Down), false);
                    Aliases[2].Body.Enter(Game.Board.Cell(Cell.Index + Direction.DownRight), false);
                };
			}
		}

		public override TokenSet Cellmates {
			get {
				CellSet square;
                TokenSet cellMates = new TokenSet();
				if (SquareExists(Cell, out square)) {
					cellMates.Add(square.Occupants);
					cellMates.Remove(Parent);
					foreach (Alias a in Aliases) {cellMates.Remove(a);}
				}
				return cellMates;
			}
		}

		public override bool CanEnter (Cell newCell) {
			CellSet square;
			if (SquareExists(newCell, out square)) {
				foreach (Cell corner in square) {
					if (corner is ExoCell) return false;
                    TokenSet set = corner.Occupants - TargetFilter.Plane(Plane.Ethereal, true);
					if (set.Count > 0)
						foreach (Token t in set)
                            if (t.ID != Parent.ID) 
                                return false;
				}
				return true;
			}
			return false;
		}

		static bool SquareExists (Cell cell, out CellSet square) {
			square = new CellSet();

			List<index2> indexes = new List<index2> {
				cell.Index,
				cell.Index + Direction.Right,
				cell.Index + Direction.Down,
				cell.Index + Direction.DownRight
			};

			foreach (index2 index in indexes) {
				if (Game.Board.HasCell(index, out cell)) {
					if (!(cell is ExoCell)) {
						square.Add(cell);
					}
				}
			}
			return (square.Count == 4 ? true : false);
		}
	}
}

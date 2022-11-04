using UnityEngine; 
using System.Collections.Generic;

namespace HOA { 
	
	public class BodyAren : Body, IDeepCopyToken<BodyAren> {

		Alias[] aliases;

		index2 right {get {return Cell.Index + Direction.Right;} }
		index2 down {get {return Cell.Index + Direction.Down;} }
		index2 downRight {get {return Cell.Index + Direction.DownRight;} }

		public BodyAren (Token parent, bool template=false) : base(parent){
			if (!template) {
				aliases = new Alias[3];
				aliases[0] = Alias.Arena(new Source(parent));
                aliases[1] = Alias.Arena(new Source(parent));
                aliases[2] = Alias.Arena(new Source(parent));
				foreach (Alias alias in aliases) {
					TokenDisplay.Attach(alias);
				}
			}
		}

		public new BodyAren DeepCopy (Token parent) {return new BodyAren(parent);}

		public override TokenSet CellMates {
			get {
				CellSet square;
                TokenSet cellMates = new TokenSet();
				if (SquareExists(Cell, out square)) {
					cellMates.Add(square.Occupants);
					cellMates.Remove(Parent);
					foreach (Alias a in aliases) {cellMates.Remove(a);}
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

		protected override void EnterSpecial (Cell newCell) {
			aliases[0].Body.Enter(Game.Board.Cell(right));
			aliases[1].Body.Enter(Game.Board.Cell(down));
			aliases[2].Body.Enter(Game.Board.Cell(downRight));
		}
	}
}

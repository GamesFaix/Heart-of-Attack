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
				aliases[0] = new ArenaAlias(parent);
				aliases[1] = new ArenaAlias(parent);
				aliases[2] = new ArenaAlias(parent);
				foreach (Alias alias in aliases) {
					TokenDisplay.Attach(alias);
				}
			}
		}

		public new BodyAren DeepCopy (Token parent) {return new BodyAren(parent);}

		public override TokenGroup CellMates {
			get {
				CellGroup square;
				TokenGroup cellMates = new TokenGroup();
				if (SquareExists(Cell, out square)) {
					cellMates.Add(square.Occupants);
					cellMates.Remove(parent);
					foreach (Alias a in aliases) {cellMates.Remove(a);}
				}
				return cellMates;
			}
		}

		public override bool CanEnter (Cell newCell) {
			CellGroup square;
			if (SquareExists(newCell, out square)) {
				Token occupant;
				foreach (Cell corner in square) {
					if (corner is ExoCell) {return false;}
					if (corner.Contains(EPlane.ETH, out occupant)) {
						if (occupant.ID != parent.ID) {return false;}
					}
				}
				return true;
			}
			return false;
		}

		static bool SquareExists (Cell cell, out CellGroup square) {
			square = new CellGroup();

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

using UnityEngine; 

namespace HOA { 
	
	public class BodyAren : Body, IDeepCopyToken<BodyAren> {

		Alias[] aliases;

		index2 right {get {return Cell.Index + Direction.Right;} }
		index2 down {get {return Cell.Index + Direction.Down;} }
		index2 downRight {get {return Cell.Index + Direction.DownRight;} }

		public BodyAren (Token parent) : base(parent){
			aliases = new Alias[3];
			aliases[0] = new ArenaAlias(parent);
			aliases[1] = new ArenaAlias(parent);
			aliases[2] = new ArenaAlias(parent);
			foreach (Alias alias in aliases) {
				TokenDisplay.Attach(alias);
			}
		}

		public new BodyAren DeepCopy (Token parent) {return new BodyAren(parent);}

		public override bool CanEnter (Cell newCell) {
			CellGroup square;
			if (Square(newCell, out square)) {
				Token occupant;
				if (newCell.Contains(EPlane.ETH, out occupant)) {
					if (occupant.ID == parent.ID) {
						return true;
					}
					return false;
				}
				return true;
			}
			return false;
		}

		static bool Square (Cell cell, out CellGroup square) {
			square = new CellGroup(cell);
			index2 index = cell.Index;
			square.Add(Game.Board.Cell(index + Direction.Right));;
	        square.Add(Game.Board.Cell(index + Direction.Down));
    	    square.Add(Game.Board.Cell(index + Direction.DownRight));
			for (int i=square.Count-1; i>=0; i--) {
				Cell c = square[i];
				if (c is ExoCell) {square.Remove(c);}
			}
			if (square.Count == 4) {return true;}
			return false;
		}

		protected override void EnterSpecial (Cell newCell) {
			aliases[0].Body.Enter(Game.Board.Cell(right));
			aliases[1].Body.Enter(Game.Board.Cell(down));
			aliases[2].Body.Enter(Game.Board.Cell(downRight));
		}
	}
}

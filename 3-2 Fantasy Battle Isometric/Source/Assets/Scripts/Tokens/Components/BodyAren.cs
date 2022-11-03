using UnityEngine; 

namespace HOA { 
	
	public class BodyAren : Body {

		Alias[] aliases;

		Int2 right {get {return cell.Index + new Int2(1,0);} }
		Int2 down {get {return cell.Index + new Int2(0,1);} }
		Int2 downRight {get {return cell.Index + new Int2(1,1);} }

		public BodyAren (Token t) {
			parent = t;
			aliases = new Alias[3];
			aliases[0] = new ArenaNonSensusAlias(parent);
			aliases[1] = new ArenaNonSensusAlias(parent);
			aliases[2] = new ArenaNonSensusAlias(parent);
		}
		
		public override bool Enter (Cell newCell) {
			if (CanEnter(newCell)) {
				if (cell != default(Cell)) {Exit();}
				cell = newCell;
				newCell.Enter(parent);
				aliases[0].Body.Enter(Game.Board.Cell(right));
				aliases[1].Body.Enter(Game.Board.Cell(down));
				aliases[2].Body.Enter(Game.Board.Cell(downRight));

				if (parent.Display != null) {((TokenDisplay)parent.Display).MoveTo(cell);}
				return true;
			}	
			if (newCell == Game.Board.TemplateCell) {
				cell = newCell;
				return true;	
			}
			return false;
		}
	}
}

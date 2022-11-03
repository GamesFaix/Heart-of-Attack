using UnityEngine; 

namespace HOA { 
	
	public class BodyAren : Body, IDeepCopyToken<BodyAren> {

		Alias[] aliases;

		Int2 right {get {return Cell.Index + new Int2(1,0);} }
		Int2 down {get {return Cell.Index + new Int2(0,1);} }
		Int2 downRight {get {return Cell.Index + new Int2(1,1);} }

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

		protected override void EnterSpecial (Cell newCell) {
			aliases[0].Body.Enter(Game.Board.Cell(right));
			aliases[1].Body.Enter(Game.Board.Cell(down));
			aliases[2].Body.Enter(Game.Board.Cell(downRight));
		}
	}
}

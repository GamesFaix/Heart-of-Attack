namespace HOA {

	public class RMove : RCellSelect{
		public Token instance;
		public RMove (Source s, Token t, Cell c) {source = s; instance = t; cell = c;}

		public override void Grant () {
			Cell oldCell = instance.Cell;
			instance.Enter(cell);
			Cell newCell = instance.Cell;
			GameLog.Out (instance+" moved from "+oldCell+" to "+newCell+".");
			Reset();
		}
	}

	public class RSetStat : RInstanceSelect {
		public EStat stat; public int magnitude;
		public RSetStat (Source s, Token t, EStat st, int n) {source = s; instance = t; stat = st; magnitude = n;}

		public override void Grant () {
			if (instance is Unit) {
				Unit u = (Unit)instance;
				u.SetStat(source, stat, magnitude);
			}
			Reset();
		}
	}

	public class RAddStat : RInstanceSelect {
		public EStat stat; public int magnitude;
		public RAddStat (Source s, Token t, EStat st, int n) {source = s; instance = t; stat = st; magnitude = n;}

		public override void Grant () {
			if (instance is Unit) {
				Unit u = (Unit)instance;
				u.AddStat(source, stat, magnitude);
			}
			Reset();
		}
	}

	public class RGetHeart : Request {
		public Token heart;
		public RGetHeart (Source s, Token t) {source = s; heart = t;}

		public override void Grant () {
			GameLog.Out(source.Player.ToString() + " acquired the "+heart.ToString()); 
			heart.Die(source);
		}
	}
}
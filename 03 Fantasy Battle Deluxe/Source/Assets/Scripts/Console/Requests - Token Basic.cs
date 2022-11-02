using HOA.Tokens;
using HOA.Map;

public class RMove : RCellSelect{
	public Token instance;
	public RMove (Source s, Token t, Cell c) {source = s; instance = t; cell = c;}

	public override void Grant () {instance.Enter(cell);}
}

public class RSetStat : RInstanceSelect {
	public STAT stat; public int magnitude;
	public RSetStat (Source s, Token t, STAT st, int n) {source = s; instance = t; stat = st; magnitude = n;}

	public override void Grant () {
		if (instance is Unit) {
			Unit u = (Unit)instance;
			u.SetStat(source, stat, magnitude);
		}
	}
}

public class RAddStat : RInstanceSelect {
	public STAT stat; public int magnitude;
	public RAddStat (Source s, Token t, STAT st, int n) {source = s; instance = t; stat = st; magnitude = n;}

	public override void Grant () {
		if (instance is Unit) {
			Unit u = (Unit)instance;
			u.AddStat(source, stat, magnitude);
		}
	}
}
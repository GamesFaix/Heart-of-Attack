using HOA.Tokens;
using HOA.Map;

public class RCreate : RCellSelect{
	public TTYPE token;
	public RCreate (Source s, TTYPE t, Cell c) {source = s; token = t; cell = c;}
}

public class RKill : RInstanceSelect{
	public RKill (Source s, Token t) {source = s; instance = t;}
}

public class RReplace : RInstanceSelect{
	public TTYPE token;
	public RReplace (Source s, Token t, TTYPE newT) {source = s; instance = t; token = newT;}
}
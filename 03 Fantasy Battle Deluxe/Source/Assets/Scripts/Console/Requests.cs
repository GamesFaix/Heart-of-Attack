using HOA.Tokens;
using HOA.Map;
using HOA.Players;


public abstract class Request {public Source source;}
public abstract class RCellSelect : Request {public Cell cell;}
public abstract class RInstanceSelect : Request {public Token instance;}


//tokens
public class RMove : RCellSelect{
	public Token instance;
	public RMove (Source s, Token t, Cell c) {source = s; instance = t; cell = c;}
}



public class RAsheArise : RInstanceSelect{
	public TTYPE token;
	public RAsheArise (Source s, Token t, TTYPE newT) {source = s; instance = t; token = newT;}
}

public class RSetStat : RInstanceSelect {
	public STAT stat; public int magnitude;
	public RSetStat (Source s, Token t, STAT st, int n) {source = s; instance = t; stat = st; magnitude = n;}
}

public class RAddStat : RInstanceSelect {
	public STAT stat; public int magnitude;
	public RAddStat (Source s, Token t, STAT st, int n) {source = s; instance = t; stat = st; magnitude = n;}
}

public class RDamage : RInstanceSelect {
	public int magnitude;	
	public RDamage (Source s, Token t, int n) {source = s; instance = t; magnitude = n;}
}

public class RRage : RInstanceSelect {
	public int magnitude;	
	public RRage (Source s, Token t, int n) {source = s; instance = t; magnitude = n;}
}

public class RDamageDest : RInstanceSelect {
	public int magnitude;
	public RDamageDest (Source s, Token t, int n) {source = s; instance = t; magnitude = n;}
}

public class RExplosion : RCellSelect {
	public int magnitude;	
	public RExplosion (Source s, Cell c, int n) {source = s; cell = c; magnitude = n;}
}

public class RDamageFir : RInstanceSelect {
	public int magnitude;	
	public RDamageFir (Source s, Token t, int n) {source = s; instance = t; magnitude = n;}
}
public class RDamageFirMax : RInstanceSelect {
	public int magnitude;	
	public RDamageFirMax (Source s, Token t, int n) {source = s; instance = t; magnitude = n;}
}
public class RLeech : RInstanceSelect {
	public int magnitude;	
	public RLeech (Source s, Token t, int n) {source = s; instance = t; magnitude = n;}
}
public class RDonate : RInstanceSelect {
	public int magnitude;	
	public RDonate (Source s, Token t, int n) {source = s; instance = t; magnitude = n;}
}
public class RCorrode : RInstanceSelect {
	public int magnitude;	
	public RCorrode (Source s, Token t, int n) {source = s; instance = t; magnitude = n;}
}
public class RDeathSting: RInstanceSelect {
	public int magnitude;	
	public RDeathSting (Source s, Token t, int n) {source = s; instance = t; magnitude = n;}
}

public class RShock : RInstanceSelect {
	public int damage; public int stun;
	public RShock (Source s, Token t, int d, int st) {source = s; instance = t; damage = d; stun = st;}
}

public class RLaserSpin : RInstanceSelect {
	public int damage;
	public RLaserSpin (Source s, Token t, int d) {source = s; instance = t; damage = d;}
}

public class RSmasSlam : RInstanceSelect {
	public int magnitude;	
	public RSmasSlam (Source s, Token t, int n) {source = s; instance = t; magnitude = n;}
}



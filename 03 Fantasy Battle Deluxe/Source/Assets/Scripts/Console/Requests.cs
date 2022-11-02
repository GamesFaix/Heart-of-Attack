using HOA.Tokens;
using HOA.Map;
using HOA.Players;


public abstract class Request {
	public Source source;
}

public abstract class RCellSelect : Request {
	public Cell cell;	
}

public abstract class RInstanceSelect : Request {
	public Token instance;
}


//tokens
public class RMove : RCellSelect{
	public Token instance;
	public RMove (Source s, Token t, Cell c) {source = s; instance = t; cell = c;}
}

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


//queue
public class RQueueAdvance : Request {
	public RQueueAdvance (Source s) {source = s;}
}

public class RQueueShuffle : Request {
	public RQueueShuffle (Source s) {source = s;}
}

public class RQueueShift : RInstanceSelect {
	public int magnitude;
	public RQueueShift (Source s, Token t, int n) {source = s; instance = t; magnitude = n;}
}

//random
public class RRandom : Request {
	public DICE dice;
	public RRandom (Source s, DICE d) {source = s; dice = d;}
}

//roster
public class RRosterAdd : Request {
	public Player player;
	public RRosterAdd (Source s, Player p) {source = s; player = p;}
}

public class RRosterRemove : Request {
	public Player player;
	public RRosterRemove (Source s, Player p) {source = s; player = p;}
}

public class RRosterNew : Request {
	public int players;
	public RRosterNew (Source s, int n) {source = s; players = n;}
}

public class RRosterAssign : Request {
	public Player player; public Faction faction;
	public RRosterAssign (Source s, Player p, Faction f) {source = s; player = p; faction = f;}	
}

public class RRosterRandom : Request {
	public  RRosterRandom (Source s) {source = s;}
}

public class RCapture : Request {
	public Player captor; public Player captive;
	public RCapture (Source s, Player p1, Player p2) {source = s; captor = p1; captive = p2;}
}

public class RSetOwner : RInstanceSelect {
	public Player player;
	public RSetOwner (Source s, Token t, Player p) {source = s; instance = t; player = p;}
}

//game
public class RStart : Request {
	public int boardSize;
	public RStart (Source s, int n) {source = s; boardSize = n;}
}	
		
public class RQuit : Request {
	public RQuit (Source s) {source = s;}
}

//network
public class RNetworkHost : Request {
	public RNetworkHost (Source s) {source = s;}
}

public class RNetworkJoin : Request {
	public string hostIP;
	public RNetworkJoin (Source s, string ip) {source = s; hostIP = ip;}
}


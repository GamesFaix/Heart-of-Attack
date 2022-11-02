public class RStart : Request {
	public int boardSize;
	public RStart (Source s, int n) {source = s; boardSize = n;}
}	

public class RQuit : Request {
	public RQuit (Source s) {source = s;}
}

public class RNetworkHost : Request {
	public RNetworkHost (Source s) {source = s;}
}

public class RNetworkJoin : Request {
	public string hostIP;
	public RNetworkJoin (Source s, string ip) {source = s; hostIP = ip;}
}

public class RRandom : Request {
	public DICE dice;
	public RRandom (Source s, DICE d) {source = s; dice = d;}
}
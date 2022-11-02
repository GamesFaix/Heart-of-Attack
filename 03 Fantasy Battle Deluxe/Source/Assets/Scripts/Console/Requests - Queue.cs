using HOA.Tokens;

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
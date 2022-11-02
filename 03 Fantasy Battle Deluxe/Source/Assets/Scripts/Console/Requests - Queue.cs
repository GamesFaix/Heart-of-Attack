using HOA.Tokens;

public class RQueueAdvance : Request {
	public RQueueAdvance (Source s) {source = s;}

	public override void Grant () {TurnQueue.Advance();}
}

public class RQueueShuffle : Request {
	public RQueueShuffle (Source s) {source = s;}

	public override void Grant () {TurnQueue.Shuffle(source, false);}
}

public class RQueueShift : RInstanceSelect {
	public int magnitude;
	public RQueueShift (Source s, Token t, int n) {source = s; instance = t; magnitude = n;}

	public override void Grant () {
		if (instance is Unit) {
			if (magnitude > 0) {TurnQueue.MoveUp( (Unit)instance, magnitude);}
			if (magnitude < 0) {TurnQueue.MoveDown( (Unit)instance, 0-magnitude); }
		}	
	}
}
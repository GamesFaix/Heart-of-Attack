using HOA.Map;
using HOA.Tokens;
using HOA.Players;

public class RStart : Request {
	public int boardSize;
	public RStart (Source s, int n) {source = s; boardSize = n;}

	public override void Grant () {
		if (Roster.Count() > 2) {
			TokenFactory.Reset();
			GameLog.Reset();
			TurnQueue.Reset();
			Board.New(boardSize);	
			GUIBoard.ZoomOut();
			foreach (Player p in Roster.Players()){
				Cell cell = Board.RandomCell;
				TokenFactory.Add(p.King, new Source(p), cell, false);
			}
			TurnQueue.Shuffle(new Source(),false);
			TurnQueue.Initialize();
			GameLog.Out("New game ready.");
			GUIMaster.Toggle();
		}
		else {GameLog.Debug("Console: Cannot start game with less than 2 players.");}
	}
}	

public class RQuit : Request {
	public RQuit (Source s) {source = s;}

	public override void Grant () {
		TurnQueue.Reset();
		GameLog.Reset();
		TokenFactory.Reset();
		Board.Reset();
		Roster.Reset();
		GUIMaster.Toggle();
	}
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

	public override void Grant () {DiceCoin.Throw(source, dice);}
}
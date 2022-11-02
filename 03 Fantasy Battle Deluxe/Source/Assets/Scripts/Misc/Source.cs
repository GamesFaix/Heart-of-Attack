using HOA.Players;
using HOA.Tokens;

public class Source {
	Player player;
	Token token;
	
	public Source () {
		player = default(Player);
		token = default(Token);
	}
	
	public Source (Player p) {
		player = p;
		token = default(Token);
	}
	
	public Source (Token t) {
		player = t.Owner();
		token = t;
	}
	
	public Player Player () {return player;}
	public Token Token () {return token;}
	
	public override string ToString() {
		if (token != default(Token)) {return token.ToString();}
		if (player != default(Player)) {return player.ToString();}
		return "ERROR";
	}
	
	public static Source ActivePlayer () {
		return new Source(Referee.ActivePlayer());
	}
			
	public static Source ActiveUnit () {
		return new Source((Token)TurnQueue.Top);
	}
	
}

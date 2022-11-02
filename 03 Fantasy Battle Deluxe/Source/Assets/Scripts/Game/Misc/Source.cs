
namespace HOA {

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
			player = t.Owner;
			token = t;
		}
		
		public Player Player {get {return player;} }
		public Token Token {get {return token;} }
		
		public override string ToString() {
			if (token != default(Token)) {return token.ToString();}
			if (player != default(Player)) {return player.ToString();}
			return "ERROR";
		}
		
		public static Source ActivePlayer {
			get {return new Source(Referee.ActivePlayer);}
		}
				
		public static Source ActiveUnit {
			get {return new Source((Token)TurnQueue.Top);}
		}
		
	}
}
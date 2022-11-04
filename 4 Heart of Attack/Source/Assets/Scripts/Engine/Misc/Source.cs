
namespace HOA {

	public class Source {
		Player player;
		Token token;
		EffectSeq sequence;
		
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

		public Source (Token t, EffectSeq e) {
			player = t.Owner;
			token = t;
			sequence = e;
		}

		public Player Player {get {return player;} }
		public Token Token {get {return token;} }
		public EffectSeq Sequence {get {return sequence;} }

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

		public static Source Neutral {
			get {return new Source(Roster.Neutral);} 
		}
		
	}
}
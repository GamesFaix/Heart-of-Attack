namespace HOA {

	public static class Referee {
		static Player active = Roster.Neutral;
		
		public static Player ActivePlayer {
			get {return active;}
			set {active = value;}
		}
	}
}
namespace HOA.Players {

	public static class Referee {
		
		
		static Player active = Roster.Neutral();
		
		public static Player ActivePlayer () {
			return active;
		}
		
		public static void SetActive (Player p) {
			active = p;		
		}
		
	}
}
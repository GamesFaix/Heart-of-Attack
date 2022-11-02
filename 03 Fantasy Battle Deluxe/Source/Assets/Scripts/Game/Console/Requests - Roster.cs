﻿namespace HOA {
	public class RRosterAdd : Request {
		public Player player;
		public RRosterAdd (Source s, Player p) {source = s; player = p;}

		public override void Grant () {Roster.Add(player);}
	}

	public class RRosterRemove : Request {
		public Player player;
		public RRosterRemove (Source s, Player p) {source = s; player = p;}

		public override void Grant () {Roster.Remove(player);}
	}

	public class RRosterNew : Request {
		public int players;
		public RRosterNew (Source s, int n) {source = s; players = n;}

		public override void Grant () {Roster.New(players);}
	}

	public class RRosterAssign : Request {
		public Player player; public Faction faction;
		public RRosterAssign (Source s, Player p, Faction f) {source = s; player = p; faction = f;}	

		public override void Grant () {Roster.AssignFaction(player, faction);}
	}

	public class RRosterRandom : Request {
		public RRosterRandom (Source s) {source = s;}

		public override void Grant () {Roster.ForceRandomFactions();}
	}

	public class RCapture : Request {
		public Player captor; public Player captive;
		public RCapture (Source s, Player p1, Player p2) {source = s; captor = p1; captive = p2;}

		public override void Grant () {captor.Capture(captive);}
	}
}
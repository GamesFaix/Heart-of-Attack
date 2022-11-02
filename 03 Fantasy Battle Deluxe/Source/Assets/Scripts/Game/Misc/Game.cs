using UnityEngine;

namespace HOA {
	public static class Game {
		public static void Start (int boardSize) {
			if (Roster.Count() > 2) {
				TokenFactory.Reset();
				GameLog.Reset();
				TurnQueue.Reset();
				//Board.New(boardSize);
				Map.Map3();

				GUIBoard.ZoomOut();
				SpawnKings();
				TurnQueue.Shuffle(new Source(),false);
				TurnQueue.Initialize();
				GameLog.Out("New game ready.");
				GUIMaster.Toggle();
			}
			else {GameLog.Debug("Console: Cannot start game with less than 2 players.");}
		}

		static void SpawnKings () {

			foreach (Player p in Roster.Players()) {
				Cell cell;
				Token temp = TemplateFactory.Template(p.King);

				if (Board.RandomLegalCell(temp, out cell)) {
					TokenFactory.Add(p.King, new Source(p), cell, false);
				}
				else {
					Debug.Log("Cannot spawn "+temp+". No legal cells.");
				}

			}
		}


		public static void Quit () {
			TurnQueue.Reset();
			GameLog.Reset();
			TokenFactory.Reset();
			Board.Reset();
			Roster.Reset();
			GUIMaster.Toggle();
		}
	}
}
using UnityEngine;
using System.Collections.Generic;

namespace HOA {
	public static class Game {
		public static Board Board {get; private set;}

		public static void Start (Size2 zoneCount) {
			if (Roster.Count() > 1) {
				TokenFactory.Reset();
				GameLog.Reset();
				TurnQueue.Reset();
				Board = Board.Random(zoneCount);
	//			Board = Board.Random(zoneCount);

				EffectQueue.Add(new EShuffle(new Source()));
				EffectQueue.Add(new EInitialize(new Source()));

				GameLog.Out("New game ready.");
				GUIMaster.Toggle();
			}
			else {GameLog.Debug("Console: Cannot start game with less than 2 players.");}
		}

		public static void ClearLegal () {
			Board.ClearLegal();
			TokenFactory.ClearLegal();
		}

		public static void Quit () {
			Board.Destroy();
			TurnQueue.Reset();
			GameLog.Reset();
			TokenFactory.Reset();
			Roster.Reset();
			GUIMaster.Toggle();
		}
	}
}
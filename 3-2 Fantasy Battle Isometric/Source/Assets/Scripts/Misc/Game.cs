using UnityEngine;
using System.Collections.Generic;

namespace HOA {
	public static class Game {
		public static void Start () {
			if (Roster.Count() > 1) {
				TokenFactory.Reset();
				GameLog.Reset();
				TurnQueue.Reset();
				//Map.Map3();
				MapFactory.Build();

				EffectQueue.Add(new EShuffle(new Source()));
				EffectQueue.Add(new EInitialize(new Source()));

				GameLog.Out("New game ready.");
				GUIMaster.Toggle();
			}
			else {GameLog.Debug("Console: Cannot start game with less than 2 players.");}
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
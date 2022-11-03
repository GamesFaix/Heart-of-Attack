using UnityEngine;
using System.Collections.Generic;

namespace HOA {
	public static class Game {
		public static void Start (int boardSize) {
			if (Roster.Count() > 2) {
				TokenFactory.Reset();
				GameLog.Reset();
				TurnQueue.Reset();
				//Board.New(boardSize);
				Map.Map3();
				CellObjectFactory.Generate();
				GUIBoard.ZoomOut();
				SpawnKings();
				EffectQueue.Add(new EShuffle(new Source()));
				EffectQueue.Add(new EInitialize(new Source()));

				GameLog.Out("New game ready.");
				GUIMaster.Toggle();
			}
			else {GameLog.Debug("Console: Cannot start game with less than 2 players.");}
		}

		static void SpawnKings () {
			EffectGroup heroSpawn = new EffectGroup();

			foreach (Player p in Roster.Players()) {
				Cell cell;
				Token temp = TemplateFactory.Template(p.King);

			
				if (Board.RandomLegalCell(temp, out cell)) {
					heroSpawn.Add(new ECreate (new Source(p), p.King, cell));
//					TokenFactory.Add(p.King, new Source(p), cell, false);
				}
				else {
					Debug.Log("Cannot spawn "+temp+". No legal cells.");
				}
			
			}
			EffectQueue.Add(heroSpawn);
		}


		public static void Quit () {
			TurnQueue.Reset();
			GameLog.Reset();
			TokenFactory.Reset();
			Board.Reset();
			Roster.Reset();
			GUIMaster.Toggle();
			CellObjectFactory.Reset();
		}
	}
}
using UnityEngine;
using System.Collections.Generic;

namespace HOA {
	public static class Game {

		public static bool Active {get; private set;}
		public static bool ActivePending {get; private set;} 
		public static Map Map {get; set;}
		public static Board Board {get; set;}

		public static void Start () {
			Active = false;
			if (Roster.Count() > 1) {
				Mixer.Mute(true);
				TokenFactory.Reset();
				GameLog.Reset();
				TurnQueue.Reset();
				GUILobbyMap.Assign();
				EffectQueue.Add(Effect.Shuffle(new Source()));
				EffectQueue.Add(Effect.Initialize(new Source()));

				ActivePending = true;
			}
			else {GameLog.Debug("Console: Cannot start game with less than 2 players.");}
		}

		public static void Activate () {
			ActivePending = false;
			Active = true;
			Mixer.Mute(false);
			Core.Music.mute = false;
			GUIMaster.Toggle();

		}

		public static void ClearLegal () {
			Board.ClearLegal();
			TokenFactory.ClearLegal();
		}

		public static void Quit () {
			Core.Music.mute = true;
			Mixer.Mute(true);
			Board.Destroy();
			TurnQueue.Reset();
			GameLog.Reset();
			TokenFactory.Reset();
			Roster.Reset();
			GUIMaster.Toggle();
			Active = false;
			ActivePending = false;
		}
	}
}
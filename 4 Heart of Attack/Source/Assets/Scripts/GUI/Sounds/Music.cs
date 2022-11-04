using UnityEngine;
using System.Collections.Generic;

namespace HOA.Sounds {

	public static class Music {

        public static Dictionary<Factions, AudioClip> Themes { get; private set; }

        static Music() { Core.Load += Load; }


        public static void Load (object sender, LoadEventArgs args) 
        {
			Themes = new Dictionary<Factions, AudioClip>();
			AddTheme(Factions.Gearp, "Gearp");
			AddTheme(Factions.Republic, "Republic");
			AddTheme(Factions.Torridale, "Torridale");
			AddTheme(Factions.Grove, "Grove");
			AddTheme(Factions.Chrono, "Chrono");
			AddTheme(Factions.Psycho, "Psycho");
			AddTheme(Factions.Psilent, "Psilent");
			AddTheme(Factions.Voidoid, "Voidoid");
            TurnQueue.TurnChangeEvent += TurnChangeSubscribe;
		}

		static void AddTheme(Factions faction, string fileName) {
            Themes.Add(faction, Resources.Load("Audio/Music/" + fileName) as AudioClip);
		}

        public static void PlayTheme(Player player)
        {

            Core.Music.clip = Themes[player.Faction.Factions];
            Core.Music.Play();

        }

        public static void PlayTheme(Token token)
        {
            PlayTheme(token.Owner);
        }

        public static void TurnChangeSubscribe(object sender, TurnChangeEventArgs args) 
        { 
            if (args.LastUnit.Owner != args.NewUnit.Owner)
            {
                PlayTheme(args.NewUnit);
            }
        }
	}
}
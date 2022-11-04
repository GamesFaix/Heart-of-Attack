#define DEBUG

using System;
using System.Collections.Generic;
using HOA.Tokens;
using HOA.Abilities;

namespace HOA.Content
{
   
	
    public static class Tokens
    {
        public static Dictionary<Species, Token> templates { get; private set; }
        public static Dictionary<Species, string> names { get; private set; }
        public static Dictionary<AbilityRank, byte> rankLimits { get; private set; }

        static int speciesCount { get { return Enum.GetValues(typeof(Species)).Length; } }

        public static void Load() {
            LoadNames();
            ArsenalRankLimits();
            TokenBuilders.Load();
            CreateTemplates();
        }

        static void LoadNames()
        {
            names = new Dictionary<Species, string>(100)
            {
                {Species.None, "[Token]"},
                {Species.Antenna, "Antenna"},
                {Species.Aperture, "Aperture"},
                {Species.Arena, "Arena Non Sensus"},
                {Species.Ashes, "Ashes"},
                {Species.Beesassin, "Beesassin"},
                {Species.BlackWinnow, "Black Winnow"},
                {Species.BloodHeart, "Blood Heart of Attack"},
                {Species.BombingRange, "Bombing Range"},
                {Species.BrassHeart, "Brass Heart of Attack"},
                {Species.Carapace, "Carapace Invader"},
                {Species.Conflagragon, "Conflagragon"},
                {Species.Corpse, "Corpse"},
                {Species.Cottage, "Cottage"},
                {Species.Curse, "Curse"},
                {Species.Decimatrix, "Decimatrix"},
                {Species.Demolitia, "Demolitia"},
                {Species.DreamReaver, "Dream Reaver"},
                {Species.Exhaust, "Exhaust Vent"},
                {Species.FirHeart, "Fir Heart of Attack"},
                {Species.Gap, "Gap"},
                {Species.Gargoliath, "Gargoliath"},
                {Species.Gatecreeper, "Gatecreeper"},
                {Species.GlassHeart, "Glass Heart of Attack"},
                {Species.Grizzly, "Grizzly Elder"},
                {Species.Hill, "Hill"},
                {Species.House, "House"},
                {Species.Ice, "Ice"},
                {Species.Kabutomachine, "Kabutomachine"},
                {Species.Katandroid, "Katandroid"},
                {Species.Lava, "Lava"},
                {Species.Lichenthrope, "Lichenthrope"},
                {Species.ManTrap, "Martian Man Trap"},
                {Species.Mawth, "M.A.W.T.H."},
                {Species.MeinSchutz, "Mein Schutz"},
                {Species.Metaterrainean, "Metaterrainean"},
                {Species.Mine, "Mine"},
                {Species.Monolith, "Monolith"},
                {Species.Mountain, "Mountain"},
                {Species.Mycolonist, "Mycolonist"},
                {Species.Necro, "Necrochancellor"},
                {Species.OldThreeHands, "Old Three Hands"},
                {Species.Panopticannon, "Panopticannon"},
                {Species.Piecemaker, "Piecemaker"},
                {Species.Priest, "Priest of Naja"},
                {Species.PrismGuard, "Prism Guard"},
                {Species.Pylon, "Pylon"},
                {Species.Pyramid, "Pyramid"},
                {Species.Quicksand, "Quicksand"},
                {Species.Rambuchet, "Battering Rambuchet"},
                {Species.Rampart, "Rampart"},
                {Species.Recyclops, "Recyclops"},
                {Species.Reprospector, "Reprospector"},
                {Species.RevolvingTom, "Revolving Tom"},
                {Species.Rock, "Rock"},
                {Species.Rook, "Rook"},
                {Species.Sand, "Sand"},
                {Species.SiliconHeart, "Silicon Heart of Attack"},
                {Species.SilkHeart, "Silk Heat of Attack"},
                {Species.Smashbuckler, "Smashbuckler"},
                {Species.SteelHeart, "Steel Heart of Attack"},
                {Species.StoneHeart, "Stone Heart of Attack"},
                {Species.TalonedScout, "Taloned Scout"},
                {Species.Temple, "Temple"},
                {Species.TimeSink, "Time Sink"},
                {Species.TimeWell, "Time Spring"},
                {Species.Tree, "Tree"},
                {Species.Tree2, "Tree"},
                {Species.Tree3, "Tree"},
                {Species.Tree4, "Tree"},
                {Species.Ultratherium, "Ultratherium"},
                {Species.Water, "Water"},
                {Species.Web, "Web"}
            };
#if DEBUG
            Log.Start("Species names loaded: " + names.Keys.Count);
#endif
        }
        
        static void CreateTemplates()
        {
            templates = new Dictionary<Species, Token>();

            for (short i = 0; i < speciesCount; i++)
            {
                Token template = TokenBuilders.builders[(Species)i](Source.Force);
                templates.Add((Species)i, template);
            }
#if DEBUG
            Log.Start("Species templates created: " + templates.Keys.Count);
#endif
        }

        static void ArsenalRankLimits()
        {
            rankLimits = new Dictionary<AbilityRank, byte>(6)
            {
                {AbilityRank.Move, 1},
                {AbilityRank.Focus, 1},
                {AbilityRank.Attack, 1},
                {AbilityRank.Special, 3},
                {AbilityRank.Create, 3},
                {AbilityRank.None, 5}
            };
            Log.Start("Arsenal rank limits set.");
        }
	}
}
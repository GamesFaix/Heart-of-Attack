//#define DEBUG

using System;
using System.Collections.Generic;
using UnityEngine;
using HOA.Tokens;

namespace HOA.Storage.Textures
{ 
    /// <summary>
    /// Loads and stores all Token thumbnail textures.
    /// </summary>
    public static class Thumbnails 
	{
        public static Dictionary<Species, Texture2D> Archive { get; private set; }

#if DEBUG
        private static List<string> missingNames;
        private static int missing;
        private static int total;
#endif

        /// <summary>
        /// Loads all textures from Resources folder.
        /// </summary>
        public static void Load()
        {
            Archive = new Dictionary<Species, Texture2D>();
#if DEBUG
            missingNames = new List<string>();
            missing = 0;
            total = 0;
#endif
            Add(Species.Antenna, "Antenna");
            Add(Species.Aperture, "Aperture");
            Add(Species.Arena, "Arena");
            Add(Species.Ashes, "Ashes");
            Add(Species.Beesassin, "Beesassin");
            Add(Species.BlackWinnow, "BlackWinnow");
            Add(Species.BloodHeart, "BloodHeart");
            Add(Species.BombingRange, "BombingRange");
            Add(Species.BrassHeart, "BrassHeart");
            Add(Species.Carapace, "Carapace");
            Add(Species.Conflagragon, "Conflagragon");
            Add(Species.Corpse, "Corpse");
            Add(Species.Cottage, "Cottage");
            Add(Species.Curse, "Curse");
            Add(Species.Decimatrix, "Decimatrix");
            Add(Species.Demolitia, "Demolitia");
            Add(Species.DreamReaver, "DreamReaver");
            Add(Species.Exhaust, "Exhaust");
            Add(Species.FirHeart, "FirHeart");
            Add(Species.Gap, "Gap");
            Add(Species.Gargoliath, "Gargoliath");
            Add(Species.Gatecreeper, "Gatecreeper");
            Add(Species.GlassHeart, "GlassHeart");
            Add(Species.Grizzly, "Grizzly");
            Add(Species.Hill, "Hill");
            Add(Species.House, "House");
            Add(Species.Ice, "Ice");
            Add(Species.Kabutomachine, "Kabutomachine");
            Add(Species.Katandroid, "Katandroid");
            Add(Species.Lava, "Lava");
            Add(Species.Lichenthrope, "Lichenthrope");
            Add(Species.ManTrap, "ManTrap");
            Add(Species.Mawth, "Mawth");
            Add(Species.MeinSchutz, "MeinSchutz");
            Add(Species.Metaterrainean, "Metaterrainean");
            Add(Species.Mine, "Mine");
            Add(Species.Monolith, "Monolith");
            Add(Species.Mountain, "Mountain");
            Add(Species.Mycolonist, "Mycolonist");
            Add(Species.Necro, "Necro");
            Add(Species.OldThreeHands, "OldThreeHands");
            Add(Species.Panopticannon, "Panopticannon");
            Add(Species.Piecemaker, "Piecemaker");
            Add(Species.Priest, "Priest");
            Add(Species.PrismGuard, "PrismGuard");
            Add(Species.Pylon, "Pylon");
            Add(Species.Pyramid, "Pyramid");
            Add(Species.Rambuchet, "Rambuchet");
            Add(Species.Rampart, "Rampart");
            Add(Species.Recyclops, "Recyclops");
            Add(Species.Reprospector, "Reprospector");
            Add(Species.RevolvingTom, "RevolvingTom");
            Add(Species.Rock, "Rock");
            Add(Species.Rook, "Rook");
            Add(Species.SiliconHeart, "SiliconHeart");
            Add(Species.SilkHeart, "SilkHeart");
            Add(Species.SteelHeart, "SteelHeart");
            Add(Species.Smashbuckler, "Smashbuckler");
            Add(Species.StoneHeart, "StoneHeart");
            Add(Species.TalonedScout, "TalonedScout");
            Add(Species.Temple, "Temple");
            Add(Species.TimeSink, "TimeSink");
            Add(Species.TimeWell, "TimeWell");
            Add(Species.Tree, "Tree");
            Add(Species.Tree2, "Tree2");
            Add(Species.Tree3, "Tree3");
            Add(Species.Tree4, "Tree4");
            Add(Species.Ultratherium, "Ultratherium");
            Add(Species.Water, "Water");
            Add(Species.Web, "Web");
            Add(Species.None, "");
#if DEBUG
            string debug =  "Thumbnails loaded.  ("+(total-missing)+" of "+total+")";
            if (missing > 0)
            {
                debug += " Missing: ";
                foreach (string name in missingNames)
                    debug += name + ", ";
            }
            Debug.Log(debug);
#endif
        }
        static void Add(Species species, string fileName)
        {
            Archive.Add(species, Resources.Load("Images/Thumbnails/" + fileName) as Texture2D);

#if DEBUG
            if (species != Species.None)
            {
                total++;
                if (Archive[species] == null)
                {
                    missingNames.Add(fileName);
                    missing++;
                }
            }
#endif
        }
	}
}

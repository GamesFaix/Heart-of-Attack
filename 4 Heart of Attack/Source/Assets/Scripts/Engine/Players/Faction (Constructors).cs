using UnityEngine;
using System.Collections.Generic;

namespace HOA {

    public partial class Faction
    {
        public static Faction Gearp()
        {
            Faction f = new Faction(Factions.Gearp, "G.E.A.R.P.", 
                new Color(0, 0, 1, 1), Color.white);
            List<Species> list = new List<Species>
            {
                HOA.Species.Kabutomachine, 
                HOA.Species.Mawth, 
                HOA.Species.Carapace, 
                HOA.Species.Katandroid, 
                HOA.Species.SilkHeart
            };
            f.Species.Add(list);
            return f;
        }

        public static Faction Republic()
        {
            Faction f = new Faction(Factions.Republic, "New Republic", 
                new Color(0, 0.2f, 0, 1), new Color(0.8f, 0.8f, 0.8f, 1));
            List<Species> list = new List<Species>
            {
                HOA.Species.Decimatrix, 
                HOA.Species.Panopticannon, 
                HOA.Species.MeinSchutz, 
                HOA.Species.Demolitia, 
                HOA.Species.Mine, 
                HOA.Species.SteelHeart
            };
            f.Species.Add(list);
            return f;
        }

        public static Faction Torridale()
        {
            Faction f = new Faction(Factions.Torridale, "Torridale",
                 new Color(0.5f, 0.5f, 0.5f ,1), new Color(0.6f, 0.1f, 0.1f, 1));
			List<Species> list = new List<Species>
            {
                HOA.Species.Gargoliath, 
                HOA.Species.Rambuchet, 
                HOA.Species.Conflagragon, 
                HOA.Species.Ashes, 
                HOA.Species.Smashbuckler, 
                HOA.Species.Rook, 
                HOA.Species.StoneHeart
            };
            f.Species.Add(list);
            return f;
        }

        public static Faction Grove()
        {
            Faction f = new Faction(Factions.Grove, "Forgotten Grove", 
                new Color(0.7f, 0.5f, 0, 1), new Color(0, 0.2f, 0, 1));
            List<Species> list = new List<Species>
            {
                HOA.Species.Ultratherium, 
                HOA.Species.Metaterrainean, 
                HOA.Species.TalonedScout, 
                HOA.Species.Grizzly, 
                HOA.Species.FirHeart
            };
            f.Species.Add(list);
            return f;
        }

        public static Faction Chrono()
        {
            Faction f = new Faction(Factions.Chrono, "Chrononistas", 
                new Color(1, 0.8f, 0, 1), Color.magenta);
            List<Species> list = new List<Species>
            {
              HOA.Species.OldThreeHands, 
              HOA.Species.Reprospector, 
              HOA.Species.Piecemaker, 
              HOA.Species.RevolvingTom, 
              HOA.Species.Aperture, 
              HOA.Species.BrassHeart
            };
            f.Species.Add(list);
            return f;
        }

        public static Faction Psycho()
        {
            Faction f = new Faction(Factions.Psycho, "Psycho Tropics", 
                new Color(0.3f, 0, 0.5f, 1), Color.green);
            List<Species> list = new List<Species>
            {
                HOA.Species.BlackWinnow, 
                HOA.Species.ManTrap, 
                HOA.Species.Mycolonist, 
                HOA.Species.Beesassin, 
                HOA.Species.Lichenthrope, 
                HOA.Species.Web, 
                HOA.Species.SilkHeart
            };
            f.Species.Add(list);
            return f;
        }

        public static Faction Psilent()
        {
            Faction f = new Faction(Factions.Psilent, "The Psilent", 
                new Color(0.4f, 0.8f, 1, 1), new Color(1, 0.8f, 0, 1));
            List<Species> list = new List<Species>
            {
                HOA.Species.DreamReaver, 
                HOA.Species.Priest, 
                HOA.Species.Arena, 
                HOA.Species.PrismGuard, 
                HOA.Species.GlassHeart
            };
            f.Species.Add(list);
            return f;
        }
        public static Faction Voidoid()
        {
            Faction f = new Faction(Factions.Voidoid, "Voidoids",
                new Color(0.6f, 0.1f, 0.1f, 1), Color.black);
            List<Species> list = new List<Species>
            {
                HOA.Species.Monolith, 
                HOA.Species.Gatecreeper, 
                HOA.Species.Necro, 
                HOA.Species.Recyclops, 
                HOA.Species.BloodHeart
            };
            f.Species.Add(list);
            return f;
        }

        public static Faction Neutral()
        {
            Faction f = new Faction(Factions.Neutral, "Neutral", Color.white, Color.grey, false);

            List<Species> list = new List<Species>
            {
                HOA.Species.Mountain, 
                HOA.Species.Hill, 
                HOA.Species.Rock, 
				HOA.Species.Tree, 
                HOA.Species.Tree2, 
                HOA.Species.Tree3, 
                HOA.Species.Tree4, 
				HOA.Species.Water, 
                HOA.Species.Ice, 
                HOA.Species.Lava, 
				HOA.Species.Corpse, 
                HOA.Species.Curse,
				HOA.Species.Pyramid, 
                HOA.Species.Temple, 
                HOA.Species.House, 
                HOA.Species.Cottage, 
                HOA.Species.Rampart,
				HOA.Species.BombingRange, 
                HOA.Species.TimeSink, 
                HOA.Species.TimeWell,
				HOA.Species.Pylon, 
                HOA.Species.Gap, 
                HOA.Species.Antenna, 
                HOA.Species.Exhaust
            };
            f.Species.Add(list);
            return f;
        }
    }
}
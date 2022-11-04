using System;
using System.Collections.Generic;
using HOA.To;
using UnityEngine;

namespace HOA 
{ 
    /// <summary>
    /// Contains Species, Colors, Name, and other settings for Factions.
    /// </summary>
    public class Faction 
	{
#region Properties

        /// <summary>
        /// Faction name.
        /// </summary>
        public string Name { get; private set; }
        
        /// <summary>
        /// Species belonging to Faction.
        /// </summary>
        public Species[] Species { get; private set; }
        
        /// <summary>
        /// Species of Faction's King.
        /// </summary>
        public Species King
        {
            get
            {
                Log.Debug("Not implemented.");
                return To.Species.None;
            }
        }
        /// <summary>
        /// Species of Faction's Heart.
        /// </summary>
        public Species Heart 
        {
            get
            {
                Log.Debug("Not implemented.");
                return To.Species.None;
            }
        }
        
        /// <summary>
        /// Faction Colors, must be Color[2]{primary, secondary}
        /// </summary>
        public Color[] Colors { get; private set; }
        /// <summary>
        /// Enum value corresponding to Faction.
        /// </summary>
        public FactionEnum FactionEnum { get; private set; }

#endregion

        private Faction(FactionEnum factionEnum, string name, Color[] colors, Species[] species)
        {
            if (colors.Length != 2)
                throw new ArgumentOutOfRangeException("Must create faction with exactly 2 colors.");
            
            FactionEnum = factionEnum;
            Name = name;
            Colors = colors;
            Species = species;
        }

        /// <summary>
        /// Returns Faction name.
        /// </summary>
        /// <returns></returns>
        public override string ToString() { return Name; }

#region Constructors

        /// <summary>
        /// Create Gearp faction
        /// </summary>
        /// <returns></returns>
        public static Faction Gearp()
        {
            Color[] colors = new Color[2] { new Color(0, 0, 1, 1), Color.white };
            Species[] species = new Species[5]
            {
                To.Species.Kabutomachine, 
                To.Species.Mawth, 
                To.Species.Carapace, 
                To.Species.Katandroid, 
                To.Species.SiliconHeart
            };
            return new Faction(FactionEnum.Gearp, "G.E.A.R.P.", colors, species);
        }

        /// <summary>
        /// Create New Republic faction
        /// </summary>
        /// <returns></returns>
        public static Faction Republic()
        {
            Color[] colors = new Color[2] { new Color(0, 0.2f, 0, 1), new Color(0.8f, 0.8f, 0.8f, 1) };
            Species[] species = new Species[6]
            {
                To.Species.Decimatrix, 
                To.Species.Panopticannon, 
                To.Species.MeinSchutz, 
                To.Species.Demolitia, 
                To.Species.Mine, 
                To.Species.SteelHeart
            };
            return new Faction(FactionEnum.Republic, "New Republic", colors, species);
        }

        /// <summary>
        /// Create Torridale faction
        /// </summary>
        /// <returns></returns>
        public static Faction Torridale()
        {
            Color[] colors = new Color[2] { new Color(0.5f, 0.5f, 0.5f, 1), new Color(0.6f, 0.1f, 0.1f, 1) };
            Species[] species = new Species[7]
            {
                To.Species.Gargoliath, 
                To.Species.Rambuchet, 
                To.Species.Conflagragon, 
                To.Species.Ashes, 
                To.Species.Smashbuckler, 
                To.Species.Rook, 
                To.Species.StoneHeart
            };
            return new Faction(FactionEnum.Torridale, "Torridale", colors, species);
        }

        /// <summary>
        /// Create Forgotten Grove faction
        /// </summary>
        /// <returns></returns>
        public static Faction Grove()
        {
            Color[] colors = new Color[2] { new Color(0.7f, 0.5f, 0, 1), new Color(0, 0.2f, 0, 1) };
            Species[] species = new Species[5]
            {
                To.Species.Ultratherium, 
                To.Species.Metaterrainean, 
                To.Species.TalonedScout, 
                To.Species.Grizzly, 
                To.Species.FirHeart
            };
            return new Faction(FactionEnum.Grove, "Forgotten Grove", colors, species);
        }

        /// <summary>
        /// Create Chrononistas faction
        /// </summary>
        /// <returns></returns>
        public static Faction Chrono()
        {
            Color[] colors = new Color[2] { new Color(1, 0.8f, 0, 1), Color.magenta };
            Species[] species = new Species[6]
            {
              To.Species.OldThreeHands, 
              To.Species.Reprospector, 
              To.Species.Piecemaker, 
              To.Species.RevolvingTom, 
              To.Species.Aperture, 
              To.Species.BrassHeart
            };
            return new Faction(FactionEnum.Chrono, "Chrononistas", colors, species);
        }

        /// <summary>
        /// Create Psycho Tropics faction
        /// </summary>
        /// <returns></returns>
        public static Faction Psycho()
        {
            Color[] colors = new Color[2] { new Color(0.3f, 0, 0.5f, 1), Color.green };
            Species[] species = new Species[7]
            {
                To.Species.BlackWinnow, 
                To.Species.ManTrap, 
                To.Species.Mycolonist, 
                To.Species.Beesassin, 
                To.Species.Lichenthrope, 
                To.Species.Web, 
                To.Species.SilkHeart
            };
            return new Faction(FactionEnum.Psycho, "Psycho Tropics", colors, species);
        }

        /// <summary>
        /// Create The Psilent faction
        /// </summary>
        /// <returns></returns>
        public static Faction Psilent()
        {
            Color[] colors = new Color[2] { new Color(0.4f, 0.8f, 1, 1), new Color(1, 0.8f, 0, 1) };
            Species[] species = new Species[5]
            {
                To.Species.DreamReaver, 
                To.Species.Priest, 
                To.Species.Arena, 
                To.Species.PrismGuard, 
                To.Species.GlassHeart
            };
            return new Faction(FactionEnum.Psilent, "The Psilent", colors, species);
        }

        /// <summary>
        /// Create Voidoids faction
        /// </summary>
        /// <returns></returns>
        public static Faction Voidoid()
        {
            Color[] colors = new Color[2] { new Color(0.6f, 0.1f, 0.1f, 1), Color.black };
            Species[] species = new Species[5]
            {
                To.Species.Monolith, 
                To.Species.Gatecreeper, 
                To.Species.Necro, 
                To.Species.Recyclops, 
                To.Species.BloodHeart
            };
            return new Faction(FactionEnum.Voidoid, "Voidoids", colors, species);
        }

       
#endregion

    }

}

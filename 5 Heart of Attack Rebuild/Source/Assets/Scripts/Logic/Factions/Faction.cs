using System;
using System.Collections.Generic;
using HOA.Tokens;
using UnityEngine;
using Species = HOA.Tokens.Species;

namespace HOA 
{ 
    /// <summary> Contains Species, Colors, Name, and other settings for Factions. </summary>
    public class Faction 
	{
        #region Properties

        /// <summary> Faction name.  </summary>
        public string name { get; private set; }
        
        /// <summary> Species belonging to Faction. </summary>
        public Species[] species { get; private set; }
        
        /// <summary> Species of Faction's King. </summary>
        public Species king
        {
            get
            {
                Log.Debug("Not implemented.");
                return Species.None;
            }
        }
        /// <summary> Species of Faction's Heart. </summary>
        public Species heart 
        {
            get
            {
                Log.Debug("Not implemented.");
                return Species.None;
            }
        }
        
        /// <summary> Faction Colors, must be Color[2]{primary, secondary} </summary>
        public Color[] colors { get; private set; }
        /// <summary> Enum value corresponding to Faction. </summary>
        public FactionEnum factionEnum { get; private set; }

        #endregion

        private Faction(FactionEnum factionEnum, string name, Color[] colors, Species[] species)
        {
            if (colors.Length != 2)
                throw new ArgumentOutOfRangeException("Must create faction with exactly 2 colors.");
            
            this.factionEnum = factionEnum;
            this.name = name;
            this.colors = colors;
            this.species = species;
        }

        /// <summary> Returns Faction name. </summary>
        public override string ToString() { return name; }

        #region Constructors

        /// <summary> Create Gearp faction </summary>
        public static Faction Gearp()
        {
            Color[] colors = new Color[2] { new Color(0, 0, 1, 1), Color.white };
            Species[] species = new Species[5]
            {
                Species.Kabutomachine, 
                Species.Mawth, 
                Species.Carapace, 
                Species.Katandroid, 
                Species.SiliconHeart
            };
            return new Faction(FactionEnum.Gearp, "G.E.A.R.P.", colors, species);
        }

        /// <summary> Create New Republic faction </summary>
        public static Faction Republic()
        {
            Color[] colors = new Color[2] { new Color(0, 0.2f, 0, 1), new Color(0.8f, 0.8f, 0.8f, 1) };
            Species[] species = new Species[6]
            {
                Species.Decimatrix, 
                Species.Panopticannon, 
                Species.MeinSchutz, 
                Species.Demolitia, 
                Species.Mine, 
                Species.SteelHeart
            };
            return new Faction(FactionEnum.Republic, "New Republic", colors, species);
        }

        /// <summary>  Create Torridale faction </summary>
        public static Faction Torridale()
        {
            Color[] colors = new Color[2] { new Color(0.5f, 0.5f, 0.5f, 1), new Color(0.6f, 0.1f, 0.1f, 1) };
            Species[] species = new Species[7]
            {
                Species.Gargoliath, 
                Species.Rambuchet, 
                Species.Conflagragon, 
                Species.Ashes, 
                Species.Smashbuckler, 
                Species.Rook, 
                Species.StoneHeart
            };
            return new Faction(FactionEnum.Torridale, "Torridale", colors, species);
        }

        /// <summary> Create Forgotten Grove faction </summary>
        public static Faction Grove()
        {
            Color[] colors = new Color[2] { new Color(0.7f, 0.5f, 0, 1), new Color(0, 0.2f, 0, 1) };
            Species[] species = new Species[5]
            {
                Species.Ultratherium, 
                Species.Metaterrainean, 
                Species.TalonedScout, 
                Species.Grizzly, 
                Species.FirHeart
            };
            return new Faction(FactionEnum.Grove, "Forgotten Grove", colors, species);
        }

        /// <summary> Create Chrononistas faction </summary>
        public static Faction Chrono()
        {
            Color[] colors = new Color[2] { new Color(1, 0.8f, 0, 1), Color.magenta };
            Species[] species = new Species[6]
            {
              Species.OldThreeHands, 
              Species.Reprospector, 
              Species.Piecemaker, 
              Species.RevolvingTom, 
              Species.Aperture, 
              Species.BrassHeart
            };
            return new Faction(FactionEnum.Chrono, "Chrononistas", colors, species);
        }

        /// <summary>  Create Psycho Tropics faction </summary>
        public static Faction Psycho()
        {
            Color[] colors = new Color[2] { new Color(0.3f, 0, 0.5f, 1), Color.green };
            Species[] species = new Species[7]
            {
                Species.BlackWinnow, 
                Species.ManTrap, 
                Species.Mycolonist, 
                Species.Beesassin, 
                Species.Lichenthrope, 
                Species.Web, 
                Species.SilkHeart
            };
            return new Faction(FactionEnum.Psycho, "Psycho Tropics", colors, species);
        }

        /// <summary> Create The Psilent faction </summary>
        public static Faction Psilent()
        {
            Color[] colors = new Color[2] { new Color(0.4f, 0.8f, 1, 1), new Color(1, 0.8f, 0, 1) };
            Species[] species = new Species[5]
            {
                Species.DreamReaver, 
                Species.Priest, 
                Species.Arena, 
                Species.PrismGuard, 
                Species.GlassHeart
            };
            return new Faction(FactionEnum.Psilent, "The Psilent", colors, species);
        }

        /// <summary> Create Voidoids faction </summary>
        public static Faction Voidoid()
        {
            Color[] colors = new Color[2] { new Color(0.6f, 0.1f, 0.1f, 1), Color.black };
            Species[] species = new Species[5]
            {
                Species.Monolith, 
                Species.Gatecreeper, 
                Species.Necro, 
                Species.Recyclops, 
                Species.BloodHeart
            };
            return new Faction(FactionEnum.Voidoid, "Voidoids", colors, species);
        }

       
        #endregion

    }

}

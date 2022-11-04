using UnityEngine;
using System.Collections.Generic;
using System;

namespace HOA.Textures 
{

	public static class TokenThumbnails 
    {
      
		static Dictionary<Species, Texture2D> thumbnails;
		
		public static void Load(object sender, LoadEventArgs args) 
        {
			thumbnails = new Dictionary<Species, Texture2D>();

			Add(Species.Katandroid, "KATA"); Add(Species.Carapace, "CARA"); Add(Species.Mawth, "MAWT"); Add(Species.Kabutomachine, "KABU");
			Add(Species.Demolitia, "DEMO"); Add(Species.MeinSchutz, "MEIN"); Add(Species.Mine, "MINE"); Add(Species.Panopticannon, "PANO"); Add(Species.Decimatrix, "DECI");
			Add(Species.Rook, "ROOK"); Add(Species.Smashbuckler, "SMAS"); Add(Species.Conflagragon, "CONF"); Add(Species.Ashes, "ASHE"); Add(Species.Rambuchet, "BATT"); Add(Species.Gargoliath, "GARG");
			Add(Species.Grizzly, "GRIZ"); Add(Species.TalonedScout, "TALO"); Add(Species.Metaterrainean, "META"); Add(Species.Ultratherium, "ULTR");
			Add(Species.RevolvingTom, "REVO"); Add(Species.Piecemaker, "PIEC"); Add(Species.Aperture, "APER"); Add(Species.Reprospector, "REPR"); Add(Species.OldThreeHands, "OLDT");
			Add(Species.Lichenthrope, "LICH"); Add(Species.Beesassin, "BEES"); Add(Species.Mycolonist, "MYCO"); Add(Species.ManTrap, "MART"); Add(Species.BlackWinnow, "BLAC"); Add(Species.Web, "WEBB");
			Add(Species.PrismGuard, "PRIS"); Add(Species.Arena, "AREN"); Add(Species.Priest, "PRIE"); Add(Species.DreamReaver, "DREA");
			Add(Species.Recyclops, "RECY"); Add(Species.Necro, "NECR"); Add(Species.Gatecreeper, "MOUT"); Add(Species.Monolith, "MONO");

			Add(Species.SiliconHeart, "HSIL"); Add(Species.SteelHeart, "HSTE"); Add(Species.StoneHeart, "HSTO"); Add(Species.FirHeart, "HFIR"); 
			Add(Species.BrassHeart, "HBRA"); Add(Species.SilkHeart, "HSLK"); Add(Species.GlassHeart, "HGLA"); Add(Species.BloodHeart, "HBLO");

			Add(Species.Mountain, "Mountain"); Add(Species.Hill, "HILL"); Add(Species.Water, "Water"); Add(Species.Ice, "ICE"); Add(Species.Lava, "LAVA");
			Add(Species.Rock, "ROCK"); Add(Species.Tree, "Tree"); Add(Species.Corpse, "Corpse");

			Add(Species.Antenna, "ANTE"); Add(Species.Pylon, "PYLO"); Add(Species.Exhaust, "EXHA"); Add(Species.Gap, "HOLE");
			Add(Species.Tree2, "Tree2"); Add(Species.Tree3, "Tree3"); Add(Species.Tree4, "Tree4");
			Add(Species.Rampart, "RAMP"); Add(Species.Cottage, "COTT"); Add(Species.House, "HOUS"); Add(Species.Temple, "TEMP");
			Add(Species.Pyramid, "Pyramid"); Add(Species.TimeSink, "TSNK"); Add(Species.TimeWell, "TWEL");
			Add(Species.BombingRange, "BombingRange"); Add(Species.Curse, "CURS");

			thumbnails.Add(Species.None, default(Texture2D));
		}
		static void Add (Species species, string fileName) 
        {
            Texture2D thumb = Resources.Load("Images/Sprites/" + fileName) as Texture2D;
            thumbnails.Add(species, thumb);
        }

        public static Texture2D BySpecies (Species species)
        {
            if (thumbnails.ContainsKey(species))
                return thumbnails[species];
            else
                throw new IndexOutOfRangeException();
        }
	}
}
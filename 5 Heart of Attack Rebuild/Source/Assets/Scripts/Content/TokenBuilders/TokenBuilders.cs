#define DEBUG

using System;
using System.Collections.Generic;
using HOA.Tokens;

namespace HOA.Content 
{
    public delegate Token TokenBuilder(object source);
	
    public static partial class TokenBuilders 
    {
        public static Dictionary<Species, TokenBuilder> builders;

        public static void Load()
        {
            builders = new Dictionary<Species, TokenBuilder>(100)
            {
                {Species.None, (c) => { return null; } },
                {Species.Antenna, Antenna},
                {Species.Aperture, Aperture},
                {Species.Arena, ArenaNonSensus},
                {Species.Ashes, Ashes},
                {Species.Beesassin, Beesassin},
                {Species.BlackWinnow, BlackWinnow},
                {Species.BloodHeart, BloodHeart},
                {Species.BombingRange, BombingRange},
                {Species.BrassHeart, Brass},
                {Species.Carapace, CarapaceInvader},
                {Species.Conflagragon, Conflagragon},
                {Species.Corpse, Corpse},
                {Species.Cottage, Cottage},
                {Species.Curse, Curse},
                {Species.Decimatrix, Decimatrix},
                {Species.Demolitia, Demolitia},
                {Species.DreamReaver, DreamReaver},
                {Species.Exhaust, Exhaust},
                {Species.FirHeart, Fir},
                {Species.Gap, Gap},
                {Species.Gargoliath, Gargoliath},
                {Species.Gatecreeper, Gatecreeper},
                {Species.GlassHeart, Glass},
                {Species.Grizzly, GrizzlyElder},
                {Species.Hill, Hill},
                {Species.House, House},
                {Species.Ice, Ice},
                {Species.Kabutomachine, Kabutomachine},
                {Species.Katandroid, Katandroid},
                {Species.Lava, Lava},
                {Species.Lichenthrope, Lichenthrope},
                {Species.ManTrap, MartianManTrap},
                {Species.Mawth, Mawth},
                {Species.MeinSchutz, MeinSchutz},
                {Species.Metaterrainean, Metaterrainean},
                {Species.Mine, Mine},
                {Species.Monolith, Monolith},
                {Species.Mountain, Mountain},
                {Species.Mycolonist, Mycolonist},
                {Species.Necro, Necrochancellor},
                {Species.OldThreeHands, OldThreeHands},
                {Species.Panopticannon, Panopticannon},
                {Species.Piecemaker, Piecemaker},
                {Species.Priest, PriestOfNaja},
                {Species.PrismGuard, PrismGuard},
                {Species.Pylon, Pylon},
                {Species.Pyramid, Pyramid},
                {Species.Quicksand, Quicksand},
                {Species.Rambuchet, BatteringRambuchet},
                {Species.Rampart, Rampart},
                {Species.Recyclops, Recyclops},
                {Species.Reprospector, Reprospector},
                {Species.RevolvingTom, RevolvingTom},
                {Species.Rock, Rock},
                {Species.Rook, Rook},
                {Species.Sand, Sand},
                {Species.SiliconHeart, Silicon},
                {Species.SilkHeart, Silk},
                {Species.Smashbuckler, Smashbuckler},
                {Species.SteelHeart, SteelHeart},
                {Species.StoneHeart, Stone},
                {Species.TalonedScout, TalonedScout},
                {Species.Temple, Temple},
                {Species.TimeSink, TimeSink},
                {Species.TimeWell, TimeWell},
                {Species.Tree, Tree},
                {Species.Tree2, Tree2},
                {Species.Tree3, Tree3},
                {Species.Tree4, Tree4},
                {Species.Ultratherium, Ultratherium},
                {Species.Water, Water},
                {Species.Web, Web}
            };

#if DEBUG
            Log.Start("Species constructors loaded: " + builders.Keys.Count);
#endif
        }

	}
}
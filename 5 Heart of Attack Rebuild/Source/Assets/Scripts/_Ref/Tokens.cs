#define DEBUG

using System;
using System.Collections.Generic;
using HOA.Tokens;

namespace HOA.Content
{
    public delegate Token TokenBuilder(object source);
	
    public static class Tokens
    {
        public static Dictionary<Species, Token> templates { get; private set; }
        public static Dictionary<Species, string> names { get; private set; }
        public static Dictionary<Species, TokenBuilder> builders { get; private set; }

        static int speciesCount { get { return Enum.GetValues(typeof(Species)).Length; } }

        public static void Load() {
            LoadNames();
            LoadConstructors();
            CreateTemplates(); 
        }

        static void LoadNames()
        {
            names = new Dictionary<Species, string>();
            names.Add(Species.None, "[Token]");
            names.Add(Species.Antenna, "Antenna");
            names.Add(Species.Aperture, "Aperture");
            names.Add(Species.Arena, "Arena Non Sensus");
            names.Add(Species.Ashes, "Ashes");
            names.Add(Species.Beesassin, "Beesassin");
            names.Add(Species.BlackWinnow, "Black Winnow");
            names.Add(Species.BloodHeart, "Blood Heart of Attack");
            names.Add(Species.BombingRange, "Bombing Range");
            names.Add(Species.BrassHeart, "Brass Heart of Attack");
            names.Add(Species.Carapace, "Carapace Invader");
            names.Add(Species.Conflagragon, "Conflagragon");
            names.Add(Species.Corpse, "Corpse");
            names.Add(Species.Cottage, "Cottage");
            names.Add(Species.Curse, "Curse");
            names.Add(Species.Decimatrix, "Decimatrix");
            names.Add(Species.Demolitia, "Demolitia");
            names.Add(Species.DreamReaver, "Dream Reaver");
            names.Add(Species.Exhaust, "Exhaust Vent");
            names.Add(Species.FirHeart, "Fir Heart of Attack");
            names.Add(Species.Gap, "Gap");
            names.Add(Species.Gargoliath, "Gargoliath");
            names.Add(Species.Gatecreeper, "Gatecreeper");
            names.Add(Species.GlassHeart, "Glass Heart of Attack");
            names.Add(Species.Grizzly, "Grizzly Elder");
            names.Add(Species.Hill, "Hill");
            names.Add(Species.House, "House");
            names.Add(Species.Ice, "Ice");
            names.Add(Species.Kabutomachine, "Kabutomachine");
            names.Add(Species.Katandroid, "Katandroid");
            names.Add(Species.Lava, "Lava");
            names.Add(Species.Lichenthrope, "Lichenthrope");
            names.Add(Species.ManTrap, "Martian Man Trap");
            names.Add(Species.Mawth, "M.A.W.T.H.");
            names.Add(Species.MeinSchutz, "Mein Schutz");
            names.Add(Species.Metaterrainean, "Metaterrainean");
            names.Add(Species.Mine, "Mine");
            names.Add(Species.Monolith, "Monolith");
            names.Add(Species.Mountain, "Mountain");
            names.Add(Species.Mycolonist, "Mycolonist");
            names.Add(Species.Necro, "Necrochancellor");
            names.Add(Species.OldThreeHands, "Old Three Hands");
            names.Add(Species.Panopticannon, "Panopticannon");
            names.Add(Species.Piecemaker, "Piecemaker");
            names.Add(Species.Priest, "Priest of Naja");
            names.Add(Species.PrismGuard, "Prism Guard");
            names.Add(Species.Pylon, "Pylon");
            names.Add(Species.Pyramid, "Pyramid");
            names.Add(Species.Quicksand, "Quicksand");
            names.Add(Species.Rambuchet, "Battering Rambuchet");
            names.Add(Species.Rampart, "Rampart");
            names.Add(Species.Recyclops, "Recyclops");
            names.Add(Species.Reprospector, "Reprospector");
            names.Add(Species.RevolvingTom, "Revolving Tom");
            names.Add(Species.Rock, "Rock");
            names.Add(Species.Rook, "Rook");
            names.Add(Species.Sand, "Sand");
            names.Add(Species.SiliconHeart, "Silicon Heart of Attack");
            names.Add(Species.SilkHeart, "Silk Heat of Attack");
            names.Add(Species.Smashbuckler, "Smashbuckler");
            names.Add(Species.SteelHeart, "Steel Heart of Attack");
            names.Add(Species.StoneHeart, "Stone Heart of Attack");
            names.Add(Species.TalonedScout, "Taloned Scout");
            names.Add(Species.Temple, "Temple");
            names.Add(Species.TimeSink, "Time Sink");
            names.Add(Species.TimeWell, "Time Spring");
            names.Add(Species.Tree, "Tree");
            names.Add(Species.Tree2, "Tree");
            names.Add(Species.Tree3, "Tree");
            names.Add(Species.Tree4, "Tree");
            names.Add(Species.Ultratherium, "Ultratherium");
            names.Add(Species.Water, "Water");
            names.Add(Species.Web, "Web");
#if DEBUG
            Log.Start("Species names loaded: " + names.Keys.Count);
#endif
        }
        
        static void LoadConstructors()
        {
            builders = new Dictionary<Species, TokenBuilder>(100)
            {
                {Species.None, (c) => { return null; } },
                {Species.Antenna, Obstacle.Antenna},
                {Species.Aperture, Obstacle.Aperture},
                {Species.Arena, Unit.ArenaNonSensus},
                {Species.Ashes, Unit.Ashes},
                {Species.Beesassin, Unit.Beesassin},
                {Species.BlackWinnow, Unit.BlackWinnow},
                {Species.BloodHeart, Obstacle.BloodHeart},
                {Species.BombingRange, Obstacle.BombingRange},
                {Species.BrassHeart, Obstacle.Brass},
                {Species.Carapace, Unit.CarapaceInvader},
                {Species.Conflagragon, Unit.Conflagragon},
                {Species.Corpse, Obstacle.Corpse},
                {Species.Cottage, Obstacle.Cottage},
                {Species.Curse, Obstacle.Curse},
                {Species.Decimatrix, Unit.Decimatrix},
                {Species.Demolitia, Unit.Demolitia},
                {Species.DreamReaver, Unit.DreamReaver},
                {Species.Exhaust, Terrain.Exhaust},
                {Species.FirHeart, Obstacle.Fir},
                {Species.Gap, Terrain.Gap},
                {Species.Gargoliath, Unit.Gargoliath},
                {Species.Gatecreeper, Unit.Gatecreeper},
                {Species.GlassHeart, Obstacle.Glass},
                {Species.Grizzly, Unit.GrizzlyElder},
                {Species.Hill, Terrain.Hill},
                {Species.House, Obstacle.House},
                {Species.Ice, Terrain.Ice},
                {Species.Kabutomachine, Unit.Kabutomachine},
                {Species.Katandroid, Unit.Katandroid},
                {Species.Lava, Terrain.Lava},
                {Species.Lichenthrope, Unit.Lichenthrope},
                {Species.ManTrap, Unit.MartianManTrap},
                {Species.Mawth, Unit.Mawth},
                {Species.MeinSchutz, Unit.MeinSchutz},
                {Species.Metaterrainean, Unit.Metaterrainean},
                {Species.Mine, Obstacle.Mine},
                {Species.Monolith, Unit.Monolith},
                {Species.Mountain, Terrain.Mountain},
                {Species.Mycolonist, Unit.Mycolonist},
                {Species.Necro, Unit.Necrochancellor},
                {Species.OldThreeHands, Unit.OldThreeHands},
                {Species.Panopticannon, Unit.Panopticannon},
                {Species.Piecemaker, Unit.Piecemaker},
                {Species.Priest, Unit.PriestOfNaja},
                {Species.PrismGuard, Unit.PrismGuard},
                {Species.Pylon, Obstacle.Pylon},
                {Species.Pyramid, Obstacle.Pyramid},
                {Species.Quicksand, Terrain.Quicksand},
                {Species.Rambuchet, Unit.BatteringRambuchet},
                {Species.Rampart, Obstacle.Rampart},
                {Species.Recyclops, Unit.Recyclops},
                {Species.Reprospector, Unit.Reprospector},
                {Species.RevolvingTom, Unit.RevolvingTom},
                {Species.Rock, Obstacle.Rock},
                {Species.Rook, Unit.Rook},
                {Species.Sand, Terrain.Sand},
                {Species.SiliconHeart, Obstacle.Silicon},
                {Species.SilkHeart, Obstacle.Silk},
                {Species.Smashbuckler, Unit.Smashbuckler},
                {Species.SteelHeart, Obstacle.SteelHeart},
                {Species.StoneHeart, Obstacle.Stone},
                {Species.TalonedScout, Unit.TalonedScout},
                {Species.Temple, Obstacle.Temple},
                {Species.TimeSink, Terrain.TimeSink},
                {Species.TimeWell, Terrain.TimeWell},
                {Species.Tree, Obstacle.Tree},
                {Species.Tree2, Obstacle.Tree2},
                {Species.Tree3, Obstacle.Tree3},
                {Species.Tree4, Obstacle.Tree4},
                {Species.Ultratherium, Unit.Ultratherium},
                {Species.Water, Terrain.Water},
                {Species.Web, Obstacle.Web}
            };

#if DEBUG
            Log.Start("Species constructors loaded: " + builders.Keys.Count);
#endif
        }

        static void CreateTemplates()
        {
            templates = new Dictionary<Species, Token>();

            for (short i = 0; i < speciesCount; i++)
            {
                Token template = builders[(Species)i](Source.Force);
                templates.Add((Species)i, template);
            }
#if DEBUG
            Log.Start("Species templates created: " + templates.Keys.Count);
#endif
        }
	}
}
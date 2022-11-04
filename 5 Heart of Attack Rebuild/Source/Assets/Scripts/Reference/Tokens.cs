#define DEBUG

using System;
using System.Collections.Generic;
using HOA.Tokens;

namespace HOA.Reference
{
    public delegate Token TokenConstructor(object source);
	
    public static class Tokens
    {
        public static Dictionary<Species, Token> templates { get; private set; }
        public static Dictionary<Species, string> names { get; private set; }
        public static Dictionary<Species, TokenConstructor> constructors { get; private set; }

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
            Debug.Log("Species names loaded: " + names.Keys.Count);
#endif
        }
        
        static void LoadConstructors()
        {
            constructors = new Dictionary<Species, TokenConstructor>();

            constructors.Add(Species.None, ((c) => { return null; }));
            
            constructors.Add(Species.Antenna, Obstacle.Antenna);
            constructors.Add(Species.Aperture, Obstacle.Aperture);
            constructors.Add(Species.Arena, Unit.ArenaNonSensus);
            constructors.Add(Species.Ashes, Unit.Ashes);
            constructors.Add(Species.Beesassin, Unit.Beesassin);
            constructors.Add(Species.BlackWinnow, Unit.BlackWinnow);
            constructors.Add(Species.BloodHeart, Obstacle.BloodHeart);
            constructors.Add(Species.BombingRange, Obstacle.BombingRange);
            constructors.Add(Species.BrassHeart, Obstacle.Brass);
            constructors.Add(Species.Carapace, Unit.CarapaceInvader);
            constructors.Add(Species.Conflagragon, Unit.Conflagragon);
            constructors.Add(Species.Corpse, Obstacle.Corpse);
            constructors.Add(Species.Cottage, Obstacle.Cottage);
            constructors.Add(Species.Curse, Obstacle.Curse);
            constructors.Add(Species.Decimatrix, Unit.Decimatrix);
            constructors.Add(Species.Demolitia, Unit.Demolitia);
            constructors.Add(Species.DreamReaver, Unit.DreamReaver);
            constructors.Add(Species.Exhaust, Terrain.Exhaust);
            constructors.Add(Species.FirHeart, Obstacle.Fir);
            constructors.Add(Species.Gap, Terrain.Gap);
            constructors.Add(Species.Gargoliath, Unit.Gargoliath);
            constructors.Add(Species.Gatecreeper, Unit.Gatecreeper);
            constructors.Add(Species.GlassHeart, Obstacle.Glass);
            constructors.Add(Species.Grizzly, Unit.GrizzlyElder);
            constructors.Add(Species.Hill, Terrain.Hill);
            constructors.Add(Species.House, Obstacle.House);
            constructors.Add(Species.Ice, Terrain.Ice);
            constructors.Add(Species.Kabutomachine, Unit.Kabutomachine);
            constructors.Add(Species.Katandroid, Unit.Katandroid);
            constructors.Add(Species.Lava, Terrain.Lava);
            constructors.Add(Species.Lichenthrope, Unit.Lichenthrope);
            constructors.Add(Species.ManTrap, Unit.MartianManTrap);
            constructors.Add(Species.Mawth, Unit.Mawth);
            constructors.Add(Species.MeinSchutz, Unit.MeinSchutz);
            constructors.Add(Species.Metaterrainean, Unit.Metaterrainean);
            constructors.Add(Species.Mine, Obstacle.Mine);
            constructors.Add(Species.Monolith, Unit.Monolith);
            constructors.Add(Species.Mountain, Terrain.Mountain);
            constructors.Add(Species.Mycolonist, Unit.Mycolonist);
            constructors.Add(Species.Necro, Unit.Necrochancellor);
            constructors.Add(Species.OldThreeHands, Unit.OldThreeHands);
            constructors.Add(Species.Panopticannon, Unit.Panopticannon);
            constructors.Add(Species.Piecemaker, Unit.Piecemaker);
            constructors.Add(Species.Priest, Unit.PriestOfNaja);
            constructors.Add(Species.PrismGuard, Unit.PrismGuard);
            constructors.Add(Species.Pylon, Obstacle.Pylon);
            constructors.Add(Species.Pyramid, Obstacle.Pyramid);
            constructors.Add(Species.Quicksand, Terrain.Quicksand);
            constructors.Add(Species.Rambuchet, Unit.BatteringRambuchet);
            constructors.Add(Species.Rampart, Obstacle.Rampart);
            constructors.Add(Species.Recyclops, Unit.Recyclops);
            constructors.Add(Species.Reprospector, Unit.Reprospector);
            constructors.Add(Species.RevolvingTom, Unit.RevolvingTom);
            constructors.Add(Species.Rock, Obstacle.Rock);
            constructors.Add(Species.Rook, Unit.Rook);
            constructors.Add(Species.Sand, Terrain.Sand);
            constructors.Add(Species.SiliconHeart, Obstacle.Silicon);
            constructors.Add(Species.SilkHeart, Obstacle.Silk);
            constructors.Add(Species.Smashbuckler, Unit.Smashbuckler);
            constructors.Add(Species.SteelHeart, Obstacle.SteelHeart);
            constructors.Add(Species.StoneHeart, Obstacle.Stone);
            constructors.Add(Species.TalonedScout, Unit.TalonedScout);
            constructors.Add(Species.Temple, Obstacle.Temple);
            constructors.Add(Species.TimeSink, Terrain.TimeSink);
            constructors.Add(Species.TimeWell, Terrain.TimeWell);
            constructors.Add(Species.Tree, Obstacle.Tree);
            constructors.Add(Species.Tree2, Obstacle.Tree2);
            constructors.Add(Species.Tree3, Obstacle.Tree3);
            constructors.Add(Species.Tree4, Obstacle.Tree4);
            constructors.Add(Species.Ultratherium, Unit.Ultratherium);
            constructors.Add(Species.Water, Terrain.Water);
            constructors.Add(Species.Web, Obstacle.Web);

#if DEBUG
            Debug.Log("Species constructors loaded: " + constructors.Keys.Count);
#endif
        }

        static void CreateTemplates()
        {
            templates = new Dictionary<Species, Token>();

            for (short i = 0; i < speciesCount; i++)
            {
                Token template = constructors[(Species)i](Source.Force);
                templates.Add((Species)i, template);
            }
#if DEBUG
            Debug.Log("Species templates created: " + templates.Keys.Count);
#endif
        }
	}
}
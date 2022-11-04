using System.Collections.Generic;
using System;

namespace HOA
{

    public partial class Token
    {
        public static Dictionary<Species, Func<Source, bool, Token>> Constructors { get; private set; }

        public static void Load()
        {
            Constructors = new Dictionary<Species, Func<Source, bool, Token>>();
            Constructors.Add(Species.Antenna, Obstacle.Antenna);
            Constructors.Add(Species.Aperture, Obstacle.Aperture);
            Constructors.Add(Species.Arena, Unit.ArenaNonSensus);
            Constructors.Add(Species.Ashes, Unit.Ashes);
            Constructors.Add(Species.Beesassin, Unit.Beesassin);
            Constructors.Add(Species.BlackWinnow, King.BlackWinnow);
            Constructors.Add(Species.BloodHeart, Heart.Blood);
            Constructors.Add(Species.BombingRange, Obstacle.BombingRange);
            Constructors.Add(Species.BrassHeart, Heart.Brass);
            Constructors.Add(Species.Carapace, Unit.CarapaceInvader);
            Constructors.Add(Species.Conflagragon, Unit.Conflagragon);
            Constructors.Add(Species.Corpse, Obstacle.Corpse);
            Constructors.Add(Species.Cottage, Obstacle.Cottage);
            Constructors.Add(Species.Curse, Obstacle.Curse);
            Constructors.Add(Species.Decimatrix, King.Decimatrix);
            Constructors.Add(Species.Demolitia, Unit.Demolitia);
            Constructors.Add(Species.DreamReaver, King.DreamReaver);
            Constructors.Add(Species.Exhaust, Obstacle.Exhaust);
            Constructors.Add(Species.FirHeart, Heart.Fir);
            Constructors.Add(Species.Gap, Obstacle.Gap);
            Constructors.Add(Species.Gargoliath, King.Gargoliath);
            Constructors.Add(Species.Gatecreeper, Unit.Gatecreeper);
            Constructors.Add(Species.GlassHeart, Heart.Glass);
            Constructors.Add(Species.Grizzly, Unit.GrizzlyElder);
            Constructors.Add(Species.Hill, Obstacle.Hill);
            Constructors.Add(Species.House, Obstacle.House);
            Constructors.Add(Species.Ice, Obstacle.Ice);
            Constructors.Add(Species.Kabutomachine, King.Kabutomachine);
            Constructors.Add(Species.Katandroid, Unit.Katandroid);
            Constructors.Add(Species.Lava, Obstacle.Lava);
            Constructors.Add(Species.Lichenthrope, Unit.Lichenthrope);
            Constructors.Add(Species.ManTrap, Unit.MartianManTrap);
            Constructors.Add(Species.Mawth, Unit.Mawth);
            Constructors.Add(Species.MeinSchutz, Unit.MeinSchutz);
            Constructors.Add(Species.Metaterrainean, Unit.Metaterrainean);
            Constructors.Add(Species.Mine, Obstacle.Mine);
            Constructors.Add(Species.Monolith, King.Monolith);
            Constructors.Add(Species.Mountain, Obstacle.Mountain);
            Constructors.Add(Species.Mycolonist, Unit.Mycolonist);
            Constructors.Add(Species.Necro, Unit.Necrochancellor);
            Constructors.Add(Species.None, ((s, b) => { return null; }));
            Constructors.Add(Species.OldThreeHands, King.OldThreeHands);
            Constructors.Add(Species.Panopticannon, Unit.Panopticannon);
            Constructors.Add(Species.Piecemaker, Unit.Piecemaker);
            Constructors.Add(Species.Priest, Unit.PriestOfNaja);
            Constructors.Add(Species.PrismGuard, Unit.PrismGuard);
            Constructors.Add(Species.Pylon, Obstacle.Pylon);
            Constructors.Add(Species.Pyramid, Obstacle.Pyramid);
            Constructors.Add(Species.Rambuchet, Unit.BatteringRambuchet);
            Constructors.Add(Species.Rampart, Obstacle.Rampart);
            Constructors.Add(Species.Recyclops, Unit.Recyclops);
            Constructors.Add(Species.Reprospector, Unit.Reprospector);
            Constructors.Add(Species.RevolvingTom, Unit.RevolvingTom);
            Constructors.Add(Species.Rock, Obstacle.Rock);
            Constructors.Add(Species.Rook, Unit.Rook);
            Constructors.Add(Species.SiliconHeart, Heart.Silicon);
            Constructors.Add(Species.SilkHeart, Heart.Silk);
            Constructors.Add(Species.Smashbuckler, Unit.Smashbuckler);
            Constructors.Add(Species.SteelHeart, Heart.Steel);
            Constructors.Add(Species.StoneHeart, Heart.Stone);
            Constructors.Add(Species.TalonedScout, Unit.TalonedScout);
            Constructors.Add(Species.Temple, Obstacle.Temple);
            Constructors.Add(Species.TimeSink, Obstacle.TimeSink);
            Constructors.Add(Species.TimeWell, Obstacle.TimeWell);
            Constructors.Add(Species.Tree, Obstacle.Tree);
            Constructors.Add(Species.Tree2, Obstacle.Tree2);
            Constructors.Add(Species.Tree3, Obstacle.Tree3);
            Constructors.Add(Species.Tree4, Obstacle.Tree4);
            Constructors.Add(Species.Ultratherium, King.Ultratherium);
            Constructors.Add(Species.Water, Obstacle.Water);
            Constructors.Add(Species.Web, Obstacle.Web);


        }
    }
}

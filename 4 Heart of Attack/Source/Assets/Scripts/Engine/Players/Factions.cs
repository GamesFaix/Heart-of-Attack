using UnityEngine;
using System.Collections.Generic;

namespace HOA {

	public class FGearp : Faction {
		public FGearp () {
			name = "G.E.A.R.P.";
			tokens = new List<Species> {
                Species.Kabutomachine, 
                Species.Mawth, 
                Species.Carapace, 
                Species.Katandroid, 
                Species.SilkHeart
            };
			king = Species.Kabutomachine;
			heart = Species.SilkHeart;
			color1 = new Color(0, 0, 1, 1);
			color2 = Color.white;
			playable = true;
			theme = SoundLoader.Theme(0);
		}
	}

	public class FNewRepublic : Faction {
		public FNewRepublic () {
			name = "New Republic";
			tokens =  new List<Species> {
                Species.Decimatrix, 
                Species.Panopticannon, 
                Species.MeinSchutz, 
                Species.Demolitia, 
                Species.Mine, 
                Species.SteelHeart
            };
			king = Species.Decimatrix;
			heart = Species.SteelHeart;
			color1 = new Color(0, 0.2f, 0, 1);
			color2 = new Color(0.8f, 0.8f, 0.8f, 1);
			playable = true;
			theme = SoundLoader.Theme(1);
		}
	}

	public class FTorridale : Faction {
		public FTorridale () {
			name = "Torridale";
			tokens = new List<Species> {
                Species.Gargoliath, 
                Species.Rambuchet, 
                Species.Conflagragon, 
                Species.Ashes, 
                Species.Smashbuckler, 
                Species.Rook, 
                Species.StoneHeart
            };
			king = Species.Gargoliath;
			heart = Species.StoneHeart;
			color1 = new Color(0.5f, 0.5f, 0.5f ,1);
			color2 = new Color(0.6f, 0.1f, 0.1f, 1);
			playable = true;
			theme = SoundLoader.Theme(2);
		}
	}

	public class FGrove : Faction {
		public FGrove () {
			name = "Forgotten Grove";
			tokens = new List<Species> {
                Species.Ultratherium, 
                Species.Metaterrainean, 
                Species.TalonedScout, 
                Species.Grizzly, 
                Species.FirHeart
            };
			king = Species.Ultratherium;
			heart = Species.FirHeart;
			color1 = new Color(0.7f, 0.5f, 0, 1);
			color2 = new Color(0, 0.2f, 0, 1);
			playable = true;
			theme = SoundLoader.Theme(3);
		}
	}
	public class FChrono : Faction {
		public FChrono () {
			name = "Chrononistas";
			tokens = new List<Species> {Species.OldThreeHands, Species.Reprospector, Species.Piecemaker, Species.RevolvingTom, Species.Aperture, Species.BrassHeart};
			king = Species.OldThreeHands;
			heart = Species.BrassHeart;
			color1 = new Color(1, 0.8f, 0, 1);
			color2 = Color.magenta;
			playable = true;
			theme = SoundLoader.Theme(4);
		}
	}

	public class FPsycho : Faction {
		public FPsycho () {
			name = "Psycho Tropics";
			tokens = new List<Species> {
                Species.BlackWinnow, 
                Species.ManTrap, 
                Species.Mycolonist, 
                Species.Beesassin, 
                Species.Lichenthrope, 
                Species.Web, 
                Species.SilkHeart};
			king = Species.BlackWinnow;
			heart = Species.SilkHeart;
			color1 = new Color(0.3f, 0, 0.5f, 1);
			color2 = Color.green;
			playable = true;
			theme = SoundLoader.Theme(5);
		}
	}

	public class FPsilent : Faction {
		public FPsilent () {
			name = "Psilent";
			tokens = new List<Species> {
                Species.DreamReaver, 
                Species.Priest, 
                Species.Arena, 
                Species.PrismGuard, 
                Species.GlassHeart};
			king = Species.DreamReaver;
			heart = Species.GlassHeart;
			color1 = new Color(0.4f, 0.8f, 1, 1);
			color2 = new Color(1, 0.8f, 0, 1);
			playable = true;
			theme = SoundLoader.Theme(6);
		}
	}

	public class FVoidoid : Faction {
		public FVoidoid () {
			name = "Voidoids";
			tokens = new List<Species> {
                Species.Monolith, 
                Species.Gatecreeper, 
                Species.Necro, 
                Species.Recyclops, 
                Species.BloodHeart};
			king = Species.Monolith;
			heart = Species.BloodHeart;
			color1 = new Color(0.6f, 0.1f, 0.1f, 1);
			color2 = Color.black;
			playable = true;
			theme = SoundLoader.Theme(7);
		}
	}

	public class FObstacle : Faction {
		public FObstacle () {
			name = "(Obstacles)";
			tokens = new List<Species> {
				Species.Mountain, Species.Hill, Species.Rock, 
				Species.Tree, Species.Tree2, Species.Tree3, Species.Tree4, 
				Species.Water, Species.Ice, Species.Lava, 
				Species.Corpse, Species.Curse,
				Species.Pyramid, Species.Temple, Species.House, Species.Cottage, Species.Rampart,
				Species.BombingRange, Species.TimeSink, Species.TimeWell,
				Species.Pylon, Species.Gap, Species.Antenna, Species.Exhaust
			};
			king = Species.None; 
			heart = Species.None;
			color1 = Color.white;
			color2 = Color.grey;
			playable = false;
			theme = default(AudioClip);
		}
	}
}
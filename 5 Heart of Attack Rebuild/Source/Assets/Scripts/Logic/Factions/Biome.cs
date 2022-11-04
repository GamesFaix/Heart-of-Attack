using System.Collections.Generic;
using HOA.To;

namespace HOA 
{ 
    /// <summary>
    /// Contains Species, Name, and other settings for Biome.
    /// </summary>
    public class Biome 
	{
#region Properties

        /// <summary>
        /// Biome name.
        /// </summary>
        public string Name { get; private set; }
        
        /// <summary>
        /// Obstacles found specifically in biome.  (Must add King and Heart.)
        /// </summary>
        public Species[] Species { get; private set; }
        
        /// <summary>
        /// Species of Faction's King.  (Must also be added to Set.)
        /// </summary>
        
        public BiomeEnum BiomeEnum { get; private set; }

#endregion

        private Biome(BiomeEnum biomeEnum, string name, Species[] species)
        {
            BiomeEnum = biomeEnum;
            Name = name;
            Species = species;
        }

        /// <summary>
        /// Returns Faction name.
        /// </summary>
        /// <returns></returns>
        public override string ToString() { return Name; }

        /// <summary>
        /// Create Generic biome
        /// </summary>
        /// <returns></returns>
        public static Biome Generic()
        {
            Species[] species = new Species[3]
            {
                To.Species.Rock, 
				To.Species.Tree, 
                To.Species.Corpse, 
            };
            return new Biome(BiomeEnum.Generic, "Generic", species);
        }

        /// <summary>
        /// Create Desert biome
        /// </summary>
        /// <returns></returns>
        public static Biome Desert()
        {
            Species[] species = new Species[2]
            {
                To.Species.Pyramid, 
				To.Species.Tree2 
            };
            return new Biome(BiomeEnum.Desert, "Desert", species);
        }

        /// <summary>
        /// Create Space biome
        /// </summary>
        /// <returns></returns>
        public static Biome Space()
        {
            Species[] species = new Species[3]
            {
                To.Species.Exhaust, 
				To.Species.Pylon,
                To.Species.Antenna
            };
            return new Biome(BiomeEnum.Space, "Space", species);
        }
    }
}
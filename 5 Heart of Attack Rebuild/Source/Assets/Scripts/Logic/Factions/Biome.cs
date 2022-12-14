using System.Collections.Generic;
using HOA.Tokens;
using Species = HOA.Tokens.Species;

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
        public string name { get; private set; }
        
        /// <summary>
        /// Obstacles found specifically in biome.  (Must add King and Heart.)
        /// </summary>
        public Species[] species { get; private set; }
        
        /// <summary>
        /// Species of Faction's King.  (Must also be added to Set.)
        /// </summary>
        
        public BiomeEnum biomeEnum { get; private set; }

        #endregion

        private Biome(BiomeEnum biomeEnum, string name, Species[] species)
        {
            this.biomeEnum = biomeEnum;
            this.name = name;
            this.species = species;
        }

        /// <summary>
        /// Returns Faction name.
        /// </summary>
        /// <returns></returns>
        public override string ToString() { return name; }

        /// <summary>
        /// Create Generic biome
        /// </summary>
        /// <returns></returns>
        public static Biome Generic()
        {
            Species[] species = new Species[3]
            {
                Species.Rock, 
				Species.Tree, 
                Species.Corpse, 
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
                Species.Pyramid, 
				Species.Tree2 
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
                Species.Exhaust, 
				Species.Pylon,
                Species.Antenna
            };
            return new Biome(BiomeEnum.Space, "Space", species);
        }
    }
}
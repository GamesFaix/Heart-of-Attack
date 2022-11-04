using UnityEngine;

namespace HOA 
{ 
    /// <summary>
    /// Accessor for dictionary classes in HOA.Storage.Textures namespace.
    /// </summary>
    public static class Textures 
	{
        /// <summary>
        /// Calls Load() on classes in HOA.Storage.Textures.
        /// </summary>
        public static void Load()
        {
            HOA.Storage.Textures.Backgrounds.Load();
            HOA.Storage.Textures.Icons.Load();
            HOA.Storage.Textures.Thumbnails.Load();
            HOA.Storage.Textures.GameEffects.Load();
        }

        /// <summary>
        /// Accessor for HOA.Storage.Textures.Icons
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Texture2D Icon(string name)
        {
            return HOA.Storage.Textures.Icons.Archive[name];
        }

        /// <summary>
        /// Accessor for HOA.Storage.Textures.Backgrounds
        /// </summary>
        public static Texture2D Backgrounds(string name)
        {
            return HOA.Storage.Textures.Backgrounds.Archive[name];
        }

        /// <summary>
        /// Accessor for HOA.Storage.Textures.Thumbnails
        /// </summary>
        /// <param name="species"></param>
        /// <returns></returns>
        public static Texture2D Thumbnails(HOA.To.Species species)
        {
            return HOA.Storage.Textures.Thumbnails.Archive[species];
        }

        /// <summary>
        /// Accessor for HOA.Storage.Textures.GameEffects
        /// </summary>
        /// <param name="species"></param>
        /// <returns></returns>
        public static Texture2D GameEffects(string name)
        {
            return HOA.Storage.Textures.GameEffects.Archive[name];
        }
	}

}

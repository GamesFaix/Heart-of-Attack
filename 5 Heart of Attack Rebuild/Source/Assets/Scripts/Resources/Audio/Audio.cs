using System;
using System.Collections.Generic;
using UnityEngine;

namespace HOA 
{
    /// <summary>
    /// Accessor for dictionary classes in HOA.Storage.Audio namespace.
    /// </summary>
    public static class Audio 
	{

        /// <summary>
        /// Calls Load() on classes in HOA.Storage.Audio
        /// </summary>
        public static void Load()
        {
            HOA.Storage.Audio.Music.Load();
            HOA.Storage.Audio.GameEffects.Load();
            HOA.Storage.Audio.GUIEffects.Load();
        }

        /// <summary>
        /// Accessor for HOA.Storage.Audio.Music
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public static AudioClip Music(FactionEnum f)
        {
            return HOA.Storage.Audio.Music.Archive[f];
        }

        /// <summary>
        /// Accessor for HOA.Storage.Audio.GameEffects
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public static AudioClip GameEffect(string name)
        {
            return HOA.Storage.Audio.GameEffects.Archive[name];
        }

        /// <summary>
        /// Accessor for HOA.Storage.Audio.GUIEffects
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public static AudioClip GUIEffect(string name)
        {
            return HOA.Storage.Audio.GUIEffects.Archive[name];
        }

	}

}

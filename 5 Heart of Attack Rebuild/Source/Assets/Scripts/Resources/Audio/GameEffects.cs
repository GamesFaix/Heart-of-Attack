//#define DEBUG

using System;
using System.Collections.Generic;
using UnityEngine;

namespace HOA.Storage.Audio
{
    /// <summary>
    /// Loads and stores all in-game sound effects.
    /// </summary>
    public static class GameEffects
    {
        public static Dictionary<string, AudioClip> Archive { get; private set; }

        /// <summary>
        /// Load all sounds from Resources folder.
        /// </summary>
        public static void Load()
        {
            Archive = new Dictionary<string, AudioClip>(25);

            Add(new string[22]{
            "Advance", "Birth", "Burrow", "Corrode", "Damage",
            "Death", "Destruct", "Detonate", "Explode", "Fire",
            "Fly", "GetHeart", "Laser", "Miss", "Owner", 
            "Shuffle", "StatDown", "StatUp", "Stick", "Teleport", 
            "Walk", "Waterlog"} 
            );

#if DEBUG
            int total = 0;
            int missing = 0;
            List<string> missingNames = new List<string>();

            foreach (string name in Archive.Keys)
            {
                total++;
                if (Archive[name] == null)
                {
                    missing++;
                    missingNames.Add(name);
                }
            }

            string debug = "In-game sound effects loaded. (" + (total - missing) + " of " + total + ")";
            if (missing > 0)
            {
                debug += "Missing: ";
                foreach (string name in missingNames)
                    debug += name + ", ";
            }
            Log.Start(debug);
#endif
        }
        /// <summary>
        /// Add sound to dictionary.
        /// </summary>
        /// <param name="folder">Subfolder of Resources/Audio/Effects/ containing file.</param>
        /// <param name="name">File name and dictionary key.</param>
        /// <returns>True if sound non-null.</returns>
        static bool Add(string folder, string name)
        {
            return Add(folder + "/" + name);
        }

        /// <summary>
        /// Add sound to dictionary.
        /// </summary>
        /// <param name="name">File name and dictionary key.</param>
        /// <returns>True if sound non-null.</returns>
        static bool Add(string name)
        {   
            AudioClip sound = UnityEngine.Resources.Load("Audio/Effects/" + name) as AudioClip;
            Archive.Add(name, sound);
            return (sound != null);
        }

        /// <summary>
        /// Add multiple sounds in the same folder to dictionary.
        /// </summary>
        /// <param name="folder">Folder containing files.</param>
        /// <param name="names">Array of file names.</param>
        static void Add(string folder, string[] names)
        {
            foreach (string name in names)
                Add(folder, name);
        }

        /// <summary>
        /// Add multiple sounds in the same folder to dictionary.
        /// </summary>
        /// <param name="names">Array of file names.</param>
        static void Add(string[] names)
        {
            foreach (string name in names)
                Add(name);
        }
    }
}

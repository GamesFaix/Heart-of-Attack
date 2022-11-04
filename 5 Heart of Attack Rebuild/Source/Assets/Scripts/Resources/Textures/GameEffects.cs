//#define DEBUG

using System;
using System.Collections.Generic;
using UnityEngine;

namespace HOA.Storage.Textures
{
    /// <summary>
    /// Loads and stores all in-game effect textures.
    /// </summary>
    public static class GameEffects
    {
        public static Dictionary<string, Texture2D> Archive;

        /// <summary>
        /// Loads all textures from Resources folder.
        /// </summary>
        public static void Load()
        {
            Archive = new Dictionary<string, Texture2D>(23);

            Add(new string[23] {
                "Birth", "Burrow", "Corrode", "Damage", "Death",
                "Destruct", "Detonate", "Explode", "Fire", "GetHeart", 
                "Heads", "Highlight", "Incinerate", "Laser", "Miss",
                "Owner", "StatDown", "StatUp", "Stick", "Stun", 
                "Tails", "Teleport", "Waterlog"
            });

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
            string debug = "In-game effect textures loaded.  (" + (total - missing) + " of " + total + ")";
            if (missing > 0)
            {
                debug += "Missing: ";
                foreach (string name in missingNames)
                    debug += name + ", ";
            }
            Debug.Log(debug);
#endif
        }

        /// <summary>
        /// Adds texture to dictionary.
        /// </summary>
        /// <param name="name">File name in Images/Effects/ and dictionary key.</param>
        static void Add(string name)
        {
            Archive.Add(name, Resources.Load("Images/Effects/" + name) as Texture2D);
        }

        /// <summary>
        /// Adds multiple textures to dictionary.
        /// </summary>
        /// <param name="names">Array of file names in Images/Effects/ and their dictionary keys.</param>
        static void Add(string[] names)
        {
            foreach (string name in names)
                Add(name);
        }

    }

}

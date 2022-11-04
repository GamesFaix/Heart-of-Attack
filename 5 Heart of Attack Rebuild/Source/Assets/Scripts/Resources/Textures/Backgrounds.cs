//#define DEBUG

using System;
using System.Collections.Generic;
using UnityEngine; 

namespace HOA.Storage.Textures 
{ 
    /// <summary>
    /// Loads and stores all GUI background textures.
    /// </summary>
    public static class Backgrounds 
	{
        public static Dictionary<string, Texture2D> Archive;

        /// <summary>
        /// Loads all backgrounds from Resources folder.
        /// </summary>
        public static void Load()
        {
            Archive = new Dictionary<string, Texture2D>(5);

            Add(new string[3]{"Wood", "WoodLarge", "Parchment"});
        
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
            string debug = "Backgrounds loaded.  (" + (total - missing) + " of " + total + ")";
            if (missing > 0)
            {
                debug += "Missing: ";
                foreach (string name in missingNames)
                    debug += name+", ";
            }
            Debug.Log(debug);
#endif
        }

        /// <summary>
        /// Adds texture to dictionary.
        /// </summary>
        /// <param name="name">File name in Images/Textures/Backgrounds/ and dictionary key.</param>
        static void Add(string name)
        {
            Archive.Add(name, Resources.Load("Images/Textures/Backgrounds/" + name) as Texture2D); 
        }

        /// <summary>
        /// Adds multiple textures to dictionary.
        /// </summary>
        /// <param name="names">Array of file names in Images/Textures/Backgrounds/ and their dictionary keys.</param>
        static void Add(string[] names)
        {
            foreach (string name in names)
                Add(name);
        }

	}

}

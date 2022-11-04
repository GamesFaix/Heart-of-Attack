//#define DEBUG

using System;
using System.Collections.Generic;
using UnityEngine; 

namespace HOA.Storage.Audio
{ 
    /// <summary>
    /// Loads and stores all music files.
    /// </summary>
    public static class Music 
	{
        public static Dictionary<FactionEnum, AudioClip> Archive { get; private set; }
#if DEBUG
        private static List<string> missingNames;
        private static int total;
        private static int missing;
#endif

        /// <summary>
        /// Loads music files from Resources folder.
        /// </summary>
        public static void Load()
        {
            Archive = new Dictionary<FactionEnum, AudioClip>();
#if DEBUG
            missingNames = new List<string>();
            missing = 0;
            total = 0;
#endif
            string[] fileNames = new string[9] { 
                "", "Gearp", "Republic", "Torridale", "Grove",
                "Chrono", "Psycho", "Psilent", "Voidoid"};
            for (byte i = 1; i <= 8; i++)
                Add((FactionEnum)i, fileNames[i]);

#if DEBUG
            string debug = "Music loaded. (" + (total - missing) + " of " + total + ")";
            if (missing > 0)
            {
                debug += " Missing: ";
                foreach (string names in missingNames)
                    debug += names + ", ";
            }
            Log.Start(debug);
#endif
        }

        static void Add(FactionEnum f, string fileName)
        {
            Archive.Add(f, UnityEngine.Resources.Load("Audio/Music/" + fileName) as AudioClip);
#if DEBUG
            total++;
            if (Archive[f] == null)
            {
                missing++;
                missingNames.Add(fileName);
            }
#endif
        }

	}

}

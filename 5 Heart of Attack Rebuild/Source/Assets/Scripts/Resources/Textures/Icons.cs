//#define DEBUG

using System;
using System.Collections.Generic;
using UnityEngine; 

namespace HOA.Storage.Textures 
{ 
    /// <summary>
    /// Loads and stores all icons for GUI.
    /// </summary>
    public static class Icons 
	{
        public static Dictionary<string, Texture2D> Archive { get; set; }
        
        /// <summary>
        /// Load all icons from Resources folder.
        /// </summary>
        public static void Load()
        {
            Archive = new Dictionary<string, Texture2D>(35);
            
            Add("Stats", new string[6] { 
                "Health", "Defense", "Initiative", 
                "Energy", "Focus", "Skip" }
            );

            Add("Planes", new string[4] { 
                "Sunken", "Ground", "Air", "Ethereal" }
            );

            Add("EntityClasses", new string[9] {
                "Cell", "Token", "Unit", "Obstacle", 
                "King", "Heart", "Trample", "Destructible", "Corpse" }
            );

            Add("Trajectories", new string[8] {
                "Neighbor", "Path", "Line", "Arc",
                "Free", "Global", "Self", "Radial"  }
            );

            Add("Effects", new string[4] {
                "Fire", "Explosive", "Damage", "Corrosive" }
            );

            Add("Misc", new string[3] {
                "Timer", "Sensor", "OnDeath" }
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

            string debug = "Icons loaded. (" + (total - missing) + " of " + total + ")";
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
        /// Add texture to dictionary.
        /// </summary>
        /// <param name="folder">Subfolder of Resources/Images/Icons/ containing file.</param>
        /// <param name="name">File name and dictionary key.</param>
        /// <returns>True if texture non-null.</returns>
        static bool Add(string folder, string name)
        {
            Texture2D icon = UnityEngine.Resources.Load("Images/Icons/" + folder + "/" + name) as Texture2D;
            Archive.Add(name, icon);
            return (icon != null);
        }
        /// <summary>
        /// Add multiple textures in the same folder to dictionary.
        /// </summary>
        /// <param name="folder">Folder containing files.</param>
        /// <param name="names">Array of file names.</param>
        static void Add(string folder, string[] names)
        {
            foreach (string name in names)
                Add(folder, name);
        }
    }
}

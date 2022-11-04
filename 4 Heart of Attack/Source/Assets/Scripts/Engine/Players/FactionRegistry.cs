using UnityEngine;
using System.Collections.Generic;

namespace HOA { 

    public static class FactionRegistry 
    {
        public static Dictionary<Factions, Faction> Factions { get; private set; }
        public static List<Factions> Taken { get; private set; }

        static FactionRegistry() { Core.Load += Load; }

        public static void Load(object sender, LoadEventArgs args) 
        {
            Factions = new Dictionary<Factions, Faction>();
            Factions.Add(HOA.Factions.Gearp, Faction.Gearp());
            Factions.Add(HOA.Factions.Republic, Faction.Republic());
            Factions.Add(HOA.Factions.Torridale, Faction.Torridale());
            Factions.Add(HOA.Factions.Grove, Faction.Grove());
            Factions.Add(HOA.Factions.Chrono, Faction.Chrono());
            Factions.Add(HOA.Factions.Psycho, Faction.Psycho());
            Factions.Add(HOA.Factions.Psilent, Faction.Psilent());
            Factions.Add(HOA.Factions.Voidoid, Faction.Voidoid());
            Factions.Add(HOA.Factions.Neutral, Faction.Neutral());
        }

        public static int LargestSize
        {
            get
            {
                int largest = 0;
                foreach (Faction f in Factions.Values)
                {
                    if (f.Species.Count > largest)
                        largest = f.Species.Count;
                }
                return largest;
            }
        }

        public static List<Faction> Playable
        {
            get
            {
                List<Faction> playable = new List<Faction>();
                foreach (Faction f in Factions.Values)
                    if (f.Playable) 
                        playable.Add(f);
                return playable;
            }
        }

        public static Faction Take(Factions f) 
        { 
            Taken.Add(f);
            return Factions[f];
        }
        public static void Release(Factions f) { Taken.Remove(f); }
        public static void ReleaseAll()
        {
            Taken = new List<Factions>();
        }

        public static List<Factions> Free
        {
            get
            {
                List<Factions> free = new List<Factions>();
                
                foreach (Faction f in Factions.Values) 
                    if (!Taken.Contains(f.Factions) && f.Playable) 
                        free.Add(f.Factions);
                
                return free;
            }
        }

        public static string[] FreeNames
        {
            get
            {
                string[] names = new string[Free.Count];
                for (int i = 0; i < names.Length; i++) { names[i] = Factions[Free[i]].ToString(); }
                return names;
            }
        }

        public static Factions RandomFree
        {
            get
            {
                int random = Mathf.RoundToInt(Random.Range(0, Free.Count));
                return Free[random];
            }
        }
    }
}

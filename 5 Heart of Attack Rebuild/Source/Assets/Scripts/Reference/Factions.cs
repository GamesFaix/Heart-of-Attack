using System.Collections.Generic;

namespace HOA.Reference
{
    public static class Factions
    {
        public static Dictionary<FactionEnum, Faction> factions { get; private set; }

        public static void Load()
        {
            factions = new Dictionary<FactionEnum, Faction>();
            factions.Add(HOA.FactionEnum.Gearp, Faction.Gearp());
            factions.Add(HOA.FactionEnum.Republic, Faction.Republic());
            factions.Add(HOA.FactionEnum.Torridale, Faction.Torridale());
            factions.Add(HOA.FactionEnum.Grove, Faction.Grove());
            factions.Add(HOA.FactionEnum.Chrono, Faction.Chrono());
            factions.Add(HOA.FactionEnum.Psycho, Faction.Psycho());
            factions.Add(HOA.FactionEnum.Psilent, Faction.Psilent());
            factions.Add(HOA.FactionEnum.Voidoid, Faction.Voidoid());


#if DEBUG
            int factionCount = factions.Keys.Count;
            Debug.Log(factionCount + " factions loaded:");
            foreach (Faction f in factions.Values)
            {
                string debug = "  " + f.Name + "(";
                foreach (Tokens.Species s in f.Species)
                    debug += s.ToString() + ", ";
                debug += "King: " + f.King.ToString() + ", ";
                debug += "Heart: " + f.Heart.ToString() + ")";
                Debug.Log(debug);
            }
#endif
        }

        /// <summary>
        /// Returns the number of species in the faction with the most species.
        /// </summary>
        public static int LargestSize
        {
            get
            {
                int largest = 0;
                foreach (Faction f in factions.Values)
                {
                    if (f.Species.Length > largest)
                        largest = f.Species.Length;
                }
                return largest;
            }
        }


	}
}
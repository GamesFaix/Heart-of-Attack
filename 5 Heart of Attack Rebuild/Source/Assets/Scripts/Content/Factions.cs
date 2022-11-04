using System.Collections.Generic;

namespace HOA.Content
{
    public static class Factions
    {
        public static Dictionary<FactionEnum, Faction> factions { get; private set; }

        public static void Load()
        {
            factions = new Dictionary<FactionEnum, Faction>()
            {
                {HOA.FactionEnum.Gearp, Faction.Gearp()},
                {HOA.FactionEnum.Republic, Faction.Republic()},
                {HOA.FactionEnum.Torridale, Faction.Torridale()},
                {HOA.FactionEnum.Grove, Faction.Grove()},
                {HOA.FactionEnum.Chrono, Faction.Chrono()},
                {HOA.FactionEnum.Psycho, Faction.Psycho()},
                {HOA.FactionEnum.Psilent, Faction.Psilent()},
                {HOA.FactionEnum.Voidoid, Faction.Voidoid()}
            };

#if DEBUG
            int factionCount = factions.Keys.Count;
            Log.Start(factionCount + " factions loaded:");
            foreach (Faction f in factions.Values)
            {
                string debug = "  " + f.name + "(";
                foreach (To.Species s in f.species)
                    debug += s.ToString() + ", ";
                debug += "King: " + f.king.ToString() + ", ";
                debug += "Heart: " + f.heart.ToString() + ")";
                Log.Start(debug);
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
                    if (f.species.Length > largest)
                        largest = f.species.Length;
                }
                return largest;
            }
        }


	}
}
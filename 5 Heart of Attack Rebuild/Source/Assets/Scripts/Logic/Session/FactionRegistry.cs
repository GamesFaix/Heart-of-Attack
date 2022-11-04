//#define DEBUG

using System.Collections.Generic;

namespace HOA.Sessions
{
    public class FactionRegistry : SessionComponent
    {
        /// <summary>Factions selected by players.</summary>
        public List<FactionEnum> Taken { get; private set; }

        /// <summary>Returns a list of all playable Factions that aren't Taken.</summary>
        public List<FactionEnum> Free
        {
            get
            {
                List<FactionEnum> free = new List<FactionEnum>();

                foreach (Faction f in Content.Factions.factions.Values)
                    if (!Taken.Contains(f.factionEnum))
                        free.Add(f.factionEnum);

                return free;
            }
        }

        /// <summary>Returns an array of the names of all Free factions.</summary>
        public string[] FreeNames
        {
            get
            {
                string[] names = new string[Free.Count];
                for (int i = 0; i < names.Length; i++)
                    names[i] = Content.Factions.factions[Free[i]] + "";
                return names;
            }
        }

        public FactionRegistry(Session session) : base(session)
        {
            Taken = new List<FactionEnum>();
        }

        /// <summary>Adds faction to Taken list.</summary>
        /// <param name="f">Faction to take</param>
        /// <returns>Taken faction</returns>
        public Faction Take(FactionEnum f)
        {
            Taken.Add(f);
            return Content.Factions.factions[f];
        }
        
        /// <summary>Removes faction from Taken list.</summary>
        /// <param name="f">Faction to remove</param>
        public void Release(FactionEnum f) { Taken.Remove(f); }
        
        /// <summary>Removes all Factions from Taken list.</summary>
        public void ReleaseAll() { Taken = new List<FactionEnum>(); }

    }
}

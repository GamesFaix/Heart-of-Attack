//#define DEBUG

using System.Collections.Generic;


namespace HOA.Sessions
{
    /// <summary> Collection of all players in the game. </summary>
    public class PlayerRegistry : SessionComponent
    {
        List<string> availableDefaultNames;

        /// <summary>  List of players, excluding Neutral  </summary>
        public List<Player> Players { get; private set; }

        public PlayerRegistry(Session session) : base(session)
        {
            Players = new List<Player>(Content.Players.max);
            availableDefaultNames = Content.Players.defaultNames;
        }

        #region Add/Remove/Contains

        /// <summary> Add a player  </summary>
        /// <param name="player">New player</param>
        public void Add(Player player)
        {
#if DEBUG
            string debug;
#endif
            if (Players.Count < Content.Players.max)
            {
                if (!Names.Contains(player.ToString()))
                {
                    Players.Add(player);
#if DEBUG
                    debug = player.ToString() + " added to Player Registry.";
#endif
                }
                else
                {
#if DEBUG
                    debug = "Cannot add " + player.ToString() + " to registry." +
                        "Duplicate player names illegal.";
#endif
                }
            }
            else
            {
#if DEBUG
                debug = "Cannot add " + player.ToString() + " to registry." +
                    "Registry full.";
                Log.Session(debug);
#endif
            }
        }

        /// <summary> Remove a player from registry, free its faction </summary>
        public void Remove(Player p)
        {
            if (Players.Contains(p))
            {
                session.Release(p.faction.factionEnum);
                Players.Remove(p);
            }
            else
            {
#if DEBUG
                Log.Session("Cannot remove " + p.ToString() + " from registry." +
                    " Player not found.");
#endif
            }
        }

        /// <summary> Does registry contain player? </summary>
        public bool Contains(Player p) { return Players.Contains(p); }

        #endregion

        /// <summary> Size of team with most tokens currently on board.  (Not counting Neutral.) </summary>
        public int LargestTeamSize
        {
            get
            {
                int largest = 0;
                foreach (Player p in Players)
                    if (p.Tokens.Count > largest) largest = p.Tokens.Count;
                return largest;
            }
        }

        /// <summary>  Returns a List of players' names. </summary>
        public List<string> Names
        {
            get
            {
                List<string> names = new List<string>();
                foreach (Player p in Players) 
                    names.Add(p.ToString());
                return names;
            }
        }

        /// <summary>  Calls indexer of inner list. </summary>
        public Player this[int n] { get { return Players[n]; } }

        /// <summary> Take faction from FactionRegistry and assign it to a player. </summary>
        public void AssignFaction(Player p, FactionEnum f)
        {
            p.faction = session.Take(f);
#if DEBUG
            Log.Session(f.ToString() + " assigned to " + p.ToString() + ".");
#endif
        }

        /// <summary> Randomly assign an untaken faction to each player who hasn't been assigned one. </summary>
        public void ForceRandomFactions()
        {
            foreach (Player p in Players)
                if (p.faction == default(Faction))
                    AssignFaction(p, session.factions.Random());
        }

        /// <summary> Creates max amount of players with default names and assigns random factions to all. </summary>
        public void AutoPopulate()
        {
            for (int i = 0; i < Content.Players.max; i++)
                Add(new Player(RandomDefaultName()));
            ForceRandomFactions();
        }

        string RandomDefaultName()
        {
            List<string> a = availableDefaultNames;
            if (a.Count < 1)
                a = Content.Players.defaultNames;
            string name = a.Random();
            a.Remove(name);
            return name;
        }

    }
}

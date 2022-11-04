using System;
using System.Collections.Generic;
using HOA.Tokens;

namespace HOA 
{ 
    
    public class Token : IEntity
	{
       
        Identity identity;
        public string Name { get { return identity.Name; } }
        public int Instance { get { return identity.Instance; } }
        public Species Species { get { return identity.Species; } }
        public Player Owner 
        { 
            get { return identity.Owner; }
            set { identity.Owner = value; }
        }
        
        
        protected Body body;
        protected TokenFlags Flags { get { return body.Flags; } }
        public bool destructible { get { return Flags.ContainsAny(TokenFlags.Destructible); } }
        public bool trample { get { return Flags.ContainsAny(TokenFlags.Trample); } }
        public bool corpse { get { return Flags.ContainsAny(TokenFlags.Corpse); } }
        public bool heart { get { return Flags.ContainsAny(TokenFlags.Heart); } }
        public bool king { get { return (this is Unit && (this as Unit).rank == Rank.King); } }
        public Cell Cell { get { return body.Cell; } }
        public bool CanEnter(Cell cell) { return body.CanEnter(cell); }
        public bool CanAimThru(Cell cell) { return body.CanAimThru(cell); }
        public bool CanTakePlaceOf(Token token) { return body.CanTakePlaceOf(token); }
        public List<Sensor> sensors { get { return body.Sensors; } }
        public List<Timer> timers { get { return body.Timers; } }


        public Tokens.Plane plane { get { return body.Plane; } }
        public bool Legal { get; set; }

        public WatchList watchList { get; private set; }
        

        public override string ToString() { return identity.ToString(); }

        public Token(IPlayer player, string name, Species species)
        {
            identity = new Identity(player, this, name, species);
            watchList = new WatchList(this);
        }

        public void Destroy(Abilities.IEffect effect, bool normalRemains = true)
        {
        }
	}

}

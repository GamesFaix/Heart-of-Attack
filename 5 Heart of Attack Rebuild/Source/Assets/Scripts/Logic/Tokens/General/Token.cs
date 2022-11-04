using System;
using System.Collections.Generic;
using HOA.Tokens;
using HOA.Abilities;

namespace HOA 
{ 
    
    public abstract class Token : IEntity, Abilities.IAbilityUser
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
        public Species Remains { get; set; }
        
        
        protected Body body;
        protected TokenFlags Flags { get { return body.Flags; } }
        public bool destructible { get { return Flags.ContainsAny(TokenFlags.Destructible); } }
        public bool trample { get { return Flags.ContainsAny(TokenFlags.Trample); } }
        public bool corpse { get { return Flags.ContainsAny(TokenFlags.Corpse); } }
        public bool heart { get { return Flags.ContainsAny(TokenFlags.Heart); } }
        public bool king { get { return (this is Unit && (this as Unit).rank == Tokens.Rank.King); } }

        public void SetFlags(IEffect source, TokenFlags flags, bool b)
        {
            if(b)
                Flags.Add(flags);
            else
                Flags.Remove(flags);
        }

        public Cell Cell { get { return body.Cell; } }
        public bool CanEnter(Cell cell) { return body.CanEnter(cell); }
        public void Enter(Cell cell) { body.Enter(cell); }
        public bool CanAimThru(Cell cell) { return body.CanAimThru(cell); }
        public bool CanTakePlaceOf(Token token) { return body.CanTakePlaceOf(token); }
        public void RefreshCell() { Enter(Cell); }
        public void Swap(Token other) { body.Swap(other); }
        public Set<IEntity> Neighbors { get { return body.Neighbors; } }
        public Set<IEntity> Cellmates { get { return body.Cellmates; } }
        public Set<IEntity> NeighborsAndCellmates { get { return body.NeighborsAndCellmates; } }


        public List<Sensor> sensors { get { return body.Sensors; } }
        public List<Timer> timers { get { return body.Timers; } }


        public Tokens.Plane plane { get { return body.Plane; } }
        public void SetPlane(Abilities.IEffect effect, Plane plane) 
        { 
            body.Plane = plane;
            RefreshCell();
        }

        public bool Legal { get; set; }

        public TrackList trackList { get; private set; }

        public override string ToString() { return identity.ToString(); }

        public Token(ITokenCreator creator, Species species)
        {
            identity = new Identity(creator, this, species);
            trackList = new TrackList(this);
            Remains = Species.None;
        }

        public void Destroy(Abilities.IEffect effect, bool normalRemains = true)
        {
        }

        public Token ToToken() { return this; }
        public ITokenCreator ToTokenCreator() { return Owner; }
	}

}

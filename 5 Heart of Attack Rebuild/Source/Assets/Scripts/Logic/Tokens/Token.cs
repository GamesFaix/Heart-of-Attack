using System;
using HOA.Abilities;

namespace HOA.Tokens
{

    public abstract partial class Token : IEntity
    {
        readonly Identity identity;
        protected readonly Body body;
        internal readonly TrackList trackList;

        public Species remains { get; private set; }
        public bool Legal { get; set; }
        
        internal Token(
            object source, 
            Species species,
            Plane plane,
            TokenFlags flags = TokenFlags.None,
            Species remains = Species.None)
        {
            if (!IsValidSource(source))
                throw new InvalidSourceException(
                    String.Format("{0}, {1}", source, source.GetType()));

            this.source = new Source(source);
            identity = new Identity(this.source, this, species);
            this.body = new Body(this, plane, flags);
            trackList = new TrackList(this);
            this.remains = remains;
        }

        internal Token(
            object source,
            Species species,
            Plane plane,
            Species remains)
            : this(source, species, plane, TokenFlags.None, remains)
        { }

        public void Destroy(Effects.IEffect effect, bool normalRemains = true)
        {
            Log.Debug("Not implemented.");
        }

        public override string ToString() { return identity.ToString(); }
    }

}

using HOA.Sessions;
using System;

namespace HOA.Tokens
{
    public abstract partial class Token
    {
        public string name { get { return identity.name; } }
        public int instance { get { return identity.instance; } }
        public Species species { get { return identity.species; } }
        public Player owner
        {
            get { return identity.owner; }
            set { identity.owner = value; }
        }

        class Identity
        {
            public readonly Token self;
            public readonly string name;
            public readonly Species species;
            public readonly int instance;
            public Player owner { get; set; }

            public Identity(Source source, Token self, Species species)
            {
                this.self = self;
                name = Content.Tokens.names[species];
                this.species = species;
                if (Session.Active != null)
                    instance = Session.Active.NextAvailableInstance(species);
                else //(only if template)
                    instance = -1;
                owner = source.Last<Player>();
            }

            public override string ToString() { return String.Format("{0} {1}", name, instance); }
        }
    }
}
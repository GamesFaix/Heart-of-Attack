namespace HOA { 

	public partial class Alias : Token {
	
		public Token Parent {get; protected set;}

		public override TokenID ID {get {return Parent.ID;} }
		public override Plane Plane {get {return Parent.Plane;} }
        public override Body Body { get { return Parent.Body; } }
        public override Species OnDeath {get {return Parent.OnDeath;} }
		public override Player Owner {get {return Parent.Owner;} }

		public override string ToString () {return ID.FullName+" Alias";}
		//public override string Notes () {return "Alias";}

        private Alias(Source source)
            : base (source, Species.None, "Alias")
        {
            Parent = source.Token;
        }
	}
}

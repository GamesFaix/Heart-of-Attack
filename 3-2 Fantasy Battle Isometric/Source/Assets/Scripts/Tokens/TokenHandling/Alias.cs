namespace HOA { 

	public abstract class Alias : Token {
	
		public Token Parent {get; protected set;}

		public override ID ID {get {return Parent.ID;} }
		public override Plane Plane {get {return Parent.Plane;} }
		public override Special Special {get {return Parent.Special;} }
		public override EToken OnDeath {get {return Parent.OnDeath;} }
		public override Player Owner {get {return Parent.Owner;} }

		public override string ToString () {return ID.FullName+" Alias";}
		public override string Notes () {return "Alias";}
		
	
	}
}

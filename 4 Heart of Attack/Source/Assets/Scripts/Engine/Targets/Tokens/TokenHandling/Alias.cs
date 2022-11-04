namespace HOA { 

	public abstract class Alias : Token {
	
		public Token Parent {get; protected set;}

		public override TokenID ID {get {return Parent.ID;} }
		public override Plane Plane {get {return Parent.Plane;} }
		public override TargetClass TargetClass {get {return Parent.TargetClass;} }
		public override EToken OnDeath {get {return Parent.OnDeath;} }
		public override Player Owner {get {return Parent.Owner;} }

		public override string ToString () {return ID.FullName+" Alias";}
		//public override string Notes () {return "Alias";}
		
	
	}
}

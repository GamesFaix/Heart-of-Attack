using UnityEngine; 

namespace HOA.Tokens { 

	public class Rock : Obstacle {
		public static Token Instantiate (Source source, bool template) {
			return new Rock (source, template);
		}
		Rock(Source s, bool template=false){
			ID = new ID(this, EToken.ROCK, s, false, template);
			TokenType type = this.TokenType;
			type.destructible = true;
			this.TokenType = type;
			ScaleMedium();
			Neutralize();
		}
		public override string Notes () {return "";}
	}
	
	public class Tree : Obstacle {
		public static Token Instantiate (Source source, bool template) {
			return new Tree (source, template);
		}
		Tree(Source s, bool template=false){
			ID = new ID(this, EToken.TREE, s, false, template);
			TokenType type = this.TokenType;
			type.destructible = true;
			this.TokenType = type;
			ScaleMedium();
			Neutralize();
		}
		public override string Notes () {return "";}
	}
	public class Tree2 : Obstacle {
		public static Token Instantiate (Source source, bool template) {
			return new Tree2 (source, template);
		}
		Tree2(Source s, bool template=false){
			ID = new ID(this, EToken.TREE2, s, false, template);
			TokenType type = this.TokenType;
			type.destructible = true;
			this.TokenType = type;
			ScaleMedium();
			Neutralize();
		}
		public override string Notes () {return "";}
	}	
	public class Tree3 : Obstacle {
		public static Token Instantiate (Source source, bool template) {
			return new Tree3 (source, template);
		}
		Tree3(Source s, bool template=false){
			ID = new ID(this, EToken.TREE3, s, false, template);
			TokenType type = this.TokenType;
			type.destructible = true;
			this.TokenType = type;
			ScaleMedium();
			Neutralize();
		}
		public override string Notes () {return "";}
	}	
	public class Tree4 : Obstacle {
		public static Token Instantiate (Source source, bool template) {
			return new Tree4 (source, template);
		}
		Tree4(Source s, bool template=false){
			ID = new ID(this, EToken.TREE4, s, false, template);
			TokenType type = this.TokenType;
			type.destructible = true;
			this.TokenType = type;
			ScaleMedium();
			Neutralize();
		}
		public override string Notes () {return "";}
	}
	public class House : Obstacle {
		public static Token Instantiate (Source source, bool template) {
			return new House (source, template);
		}
		House(Source s, bool template=false){
			ID = new ID(this, EToken.HOUS, s, false, template);
			TokenType type = this.TokenType;
			type.destructible = true;
			this.TokenType = type;
			ScaleMedium();
			Neutralize();
		}
		public override string Notes () {return "";}
	}
	public class Cottage : Obstacle {
		public static Token Instantiate (Source source, bool template) {
			return new Cottage (source, template);
		}
		Cottage(Source s, bool template=false){
			ID = new ID(this, EToken.COTT, s, false, template);
			TokenType type = this.TokenType;
			type.destructible = true;
			this.TokenType = type;
			ScaleMedium();
			Neutralize();
		}
		public override string Notes () {return "";}
	}
	
	public class Rampart : Obstacle {
		public static Token Instantiate (Source source, bool template) {
			return new Rampart (source, template);
		}
		Rampart(Source s, bool template=false){
			ID = new ID(this, EToken.RAMP, s, false, template);
			TokenType type = this.TokenType;
			type.destructible = true;
			this.TokenType = type;	
			ScaleMedium();
			Neutralize();
		}
		public override string Notes () {return "";}
	}
	
	public class Temple : Obstacle {
		public static Token Instantiate (Source source, bool template) {
			return new Temple (source, template);
		}
		Temple(Source s, bool template=false){
			ID = new ID(this, EToken.TEMP, s, false, template);
			TokenType type = this.TokenType;
			type.destructible = true;
			this.TokenType = type;
			ScaleMedium();
			Neutralize();
		}
		public override string Notes () {return "";}
	}
	
	public class Antenna : Obstacle {
		public static Token Instantiate (Source source, bool template) {
			return new Antenna (source, template);
		}
		Antenna(Source s, bool template=false){
			ID = new ID(this, EToken.ANTE, s, false, template);
			TokenType type = this.TokenType;
			type.destructible = true;
			this.TokenType = type;
			ScaleMedium();
			Neutralize();
		}
		public override string Notes () {return "";}
	}
	public class Corpse : Obstacle {
		public static Token Instantiate (Source source, bool template) {
			return new Corpse (source, template);
		}
		Corpse(Source s, bool template=false){
			ID = new ID(this, EToken.CORP, s, false, template);
			TokenType type = this.TokenType;
			type.destructible = true;
			this.TokenType = type;
			ScaleMedium();
			Neutralize();
		}
		public override string Notes () {return "";}
	}
}

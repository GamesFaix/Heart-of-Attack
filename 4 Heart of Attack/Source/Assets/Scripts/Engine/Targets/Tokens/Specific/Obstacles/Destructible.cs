using UnityEngine; 

namespace HOA.Tokens { 

	public class Rock : Obstacle {
		public static Token Instantiate (Source source, bool template) {
			return new Rock (source, template);
		}
		Rock(Source s, bool template=false){
			ID = new TokenID(this, EToken.ROCK, s, false, template);
            TargetClass += TargetClasses.Dest;
			ScaleMedium();
			Neutralize();
		}
		
	}
	
	public class Tree : Obstacle {
		public static Token Instantiate (Source source, bool template) {
			return new Tree (source, template);
		}
		Tree(Source s, bool template=false){
			ID = new TokenID(this, EToken.TREE, s, false, template);
            TargetClass += TargetClasses.Dest;
            ScaleMedium();
			Neutralize();
		}
		
	}
	public class Tree2 : Obstacle {
		public static Token Instantiate (Source source, bool template) {
			return new Tree2 (source, template);
		}
		Tree2(Source s, bool template=false){
			ID = new TokenID(this, EToken.TREE2, s, false, template);
            TargetClass += TargetClasses.Dest;
            ScaleMedium();
			Neutralize();
		}
		
	}	
	public class Tree3 : Obstacle {
		public static Token Instantiate (Source source, bool template) {
			return new Tree3 (source, template);
		}
		Tree3(Source s, bool template=false){
			ID = new TokenID(this, EToken.TREE3, s, false, template);
            TargetClass += TargetClasses.Dest;
            ScaleMedium();
			Neutralize();
		}
		
	}	
	public class Tree4 : Obstacle {
		public static Token Instantiate (Source source, bool template) {
			return new Tree4 (source, template);
		}
		Tree4(Source s, bool template=false){
			ID = new TokenID(this, EToken.TREE4, s, false, template);
            TargetClass += TargetClasses.Dest;
            ScaleMedium();
			Neutralize();
		}
		
	}
	public class House : Obstacle {
		public static Token Instantiate (Source source, bool template) {
			return new House (source, template);
		}
		House(Source s, bool template=false){
			ID = new TokenID(this, EToken.HOUS, s, false, template);
            TargetClass += TargetClasses.Dest;
            ScaleMedium();
			Neutralize();
		}
		
	}
	public class Cottage : Obstacle {
		public static Token Instantiate (Source source, bool template) {
			return new Cottage (source, template);
		}
		Cottage(Source s, bool template=false){
			ID = new TokenID(this, EToken.COTT, s, false, template);
            TargetClass += TargetClasses.Dest;
            ScaleMedium();
			Neutralize();
		}
		
	}
	
	public class Rampart : Obstacle {
		public static Token Instantiate (Source source, bool template) {
			return new Rampart (source, template);
		}
		Rampart(Source s, bool template=false){
			ID = new TokenID(this, EToken.RAMP, s, false, template);
            TargetClass += TargetClasses.Dest;
            ScaleMedium();
			Neutralize();
		}
		
	}
	
	public class Temple : Obstacle {
		public static Token Instantiate (Source source, bool template) {
			return new Temple (source, template);
		}
		Temple(Source s, bool template=false){
			ID = new TokenID(this, EToken.TEMP, s, false, template);
            TargetClass += TargetClasses.Dest;
            ScaleMedium();
			Neutralize();
		}
		
	}
	
	public class Antenna : Obstacle {
		public static Token Instantiate (Source source, bool template) {
			return new Antenna (source, template);
		}
		Antenna(Source s, bool template=false){
			ID = new TokenID(this, EToken.ANTE, s, false, template);
            TargetClass += TargetClasses.Dest;
            ScaleMedium();
			Neutralize();
		}
		
	}
	public class Corpse : Obstacle {
		public static Token Instantiate (Source source, bool template) {
			return new Corpse (source, template);
		}
		Corpse(Source s, bool template=false){
			ID = new TokenID(this, EToken.CORP, s, false, template);
            TargetClass += TargetClasses.Dest;
            TargetClass += TargetClasses.Corpse;
            ScaleMedium();
			Neutralize();
		}
		
	}
}

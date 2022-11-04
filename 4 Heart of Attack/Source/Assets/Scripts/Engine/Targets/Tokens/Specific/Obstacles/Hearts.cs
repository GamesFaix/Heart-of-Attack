
namespace HOA.Tokens {
	
	public class SiliconHOA : Obstacle {
		public static Token Instantiate (Source source, bool template) {
			return new SiliconHOA (source, template);
		}

		SiliconHOA(Source s, bool template=false){
			ID = new TokenID(this, EToken.HSIL, s, true, template);
			BuildHeart();
		}
		
	}
	
	public class SteelHOA : Obstacle {
		public static Token Instantiate (Source source, bool template) {
			return new SteelHOA (source, template);
		}
		SteelHOA(Source s, bool template=false){
			ID = new TokenID(this, EToken.HSTE, s, true, template);
			BuildHeart();
		}
		
	}
	
	public class StoneHOA : Obstacle {
		public static Token Instantiate (Source source, bool template) {
			return new StoneHOA (source, template);
		}
		StoneHOA(Source s, bool template=false){
			ID = new TokenID(this, EToken.HSTO, s, true, template);
			BuildHeart();
		}
		
	}
	
	public class FirHOA : Obstacle {
		public static Token Instantiate (Source source, bool template) {
			return new FirHOA (source, template);
		}
		FirHOA(Source s, bool template=false){
			ID = new TokenID(this, EToken.HFIR, s, true, template);
			BuildHeart();
		}
		
	}
		
	public class BrassHOA : Obstacle {
		public static Token Instantiate (Source source, bool template) {
			return new BrassHOA (source, template);
		}
		BrassHOA(Source s, bool template=false){
			ID = new TokenID(this, EToken.HBRA, s, true, template);
			BuildHeart();
		}
		
	}
	
	public class SilkHOA : Obstacle {
		public static Token Instantiate (Source source, bool template) {
			return new SilkHOA (source, template);
		}
		SilkHOA(Source s, bool template=false){
			ID = new TokenID(this, EToken.HSLK, s, true, template);
			BuildHeart();		
		}
		
	}
	
	public class GlassHOA : Obstacle {
		public static Token Instantiate (Source source, bool template) {
			return new GlassHOA (source, template);
		}
		GlassHOA(Source s, bool template=false){
			ID = new TokenID(this, EToken.HGLA, s, true, template);
			BuildHeart();
		}
		
	}	
	
	public class BloodHOA : Obstacle {
		public static Token Instantiate (Source source, bool template) {
			return new BloodHOA (source, template);
		}
		BloodHOA(Source s, bool template=false){
			ID = new TokenID(this, EToken.HBLO, s, true, template);
			BuildHeart();
		}
		
	}
}

namespace HOA {
	
	public class SiliconHOA : Obstacle {
		public SiliconHOA(Source s, bool template=false){
			id = new ID(this, EToken.HSIL, s, true, template);
			BuildHeart();
		}
		public override string Notes () {return "";}
	}
	
	public class SteelHOA : Obstacle {
		public SteelHOA(Source s, bool template=false){
			id = new ID(this, EToken.HSTE, s, true, template);
			BuildHeart();
		}
		public override string Notes () {return "";}
	}
	
	public class StoneHOA : Obstacle {
		public StoneHOA(Source s, bool template=false){
			id = new ID(this, EToken.HSTO, s, true, template);
			BuildHeart();
		}
		public override string Notes () {return "";}
	}
	
	public class FirHOA : Obstacle {
		public FirHOA(Source s, bool template=false){
			id = new ID(this, EToken.HFIR, s, true, template);
			BuildHeart();
		}
		public override string Notes () {return "";}
	}
		
	public class BrassHOA : Obstacle {
		public BrassHOA(Source s, bool template=false){
			id = new ID(this, EToken.HBRA, s, true, template);
			BuildHeart();
		}
		public override string Notes () {return "";}
	}
	
	public class SilkHOA : Obstacle {
		public SilkHOA(Source s, bool template=false){
			id = new ID(this, EToken.HSLK, s, true, template);
			BuildHeart();		
		}
		public override string Notes () {return "";}
	}
	
	public class GlassHOA : Obstacle {
		public GlassHOA(Source s, bool template=false){
			id = new ID(this, EToken.HGLA, s, true, template);
			BuildHeart();
		}
		public override string Notes () {return "";}
	}	
	
	public class BloodHOA : Obstacle {
		public BloodHOA(Source s, bool template=false){
			id = new ID(this, EToken.HBLO, s, true, template);
			BuildHeart();
		}
		public override string Notes () {return "";}
	}
}
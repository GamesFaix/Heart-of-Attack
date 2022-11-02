
namespace HOA {
	
	public class SiliconHOA : Obstacle {
		public SiliconHOA(Source s, bool template=false){
			NewLabel(EToken.HSIL, s, true, template);
			BuildHeart();
		}
		public override string Notes () {return "";}
	}
	
	public class SteelHOA : Obstacle {
		public SteelHOA(Source s, bool template=false){
			NewLabel(EToken.HSTE, s, true, template);
			BuildHeart();
		}
		public override string Notes () {return "";}
	}
	
	public class StoneHOA : Obstacle {
		public StoneHOA(Source s, bool template=false){
			NewLabel(EToken.HSTO, s, true, template);
			BuildHeart();
		}
		public override string Notes () {return "";}
	}
	
	public class FirHOA : Obstacle {
		public FirHOA(Source s, bool template=false){
			NewLabel(EToken.HFIR, s, true, template);
			BuildHeart();
		}
		public override string Notes () {return "";}
	}
		
	public class BrassHOA : Obstacle {
		public BrassHOA(Source s, bool template=false){
			NewLabel(EToken.HBRA, s, true, template);
			BuildHeart();
		}
		public override string Notes () {return "";}
	}
	
	public class SilkHOA : Obstacle {
		public SilkHOA(Source s, bool template=false){
			NewLabel(EToken.HSLK, s, true, template);
			BuildHeart();		
		}
		public override string Notes () {return "";}
	}
	
	public class GlassHOA : Obstacle {
		public GlassHOA(Source s, bool template=false){
			NewLabel(EToken.HGLA, s, true, template);
			BuildHeart();
		}
		public override string Notes () {return "";}
	}	
	
	public class BloodHOA : Obstacle {
		public BloodHOA(Source s, bool template=false){
			NewLabel(EToken.HBLO, s, true, template);
			BuildHeart();
		}
		public override string Notes () {return "";}
	}
}
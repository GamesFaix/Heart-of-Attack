using HOA.Map;
using HOA.Players;

namespace HOA.Tokens {
	
	public class SiliconHOA : Obstacle {
		public SiliconHOA(Source s, bool template=false){
			NewLabel(TTYPE.HSIL, s, true, template);
			BuildHeart();
		}
		public override string Notes () {return "";}
	}
	
	public class SteelHOA : Obstacle {
		public SteelHOA(Source s, bool template=false){
			NewLabel(TTYPE.HSTE, s, true, template);
			BuildHeart();
		}
		public override string Notes () {return "";}
	}
	
	public class StoneHOA : Obstacle {
		public StoneHOA(Source s, bool template=false){
			NewLabel(TTYPE.HSTO, s, true, template);
			BuildHeart();
		}
		public override string Notes () {return "";}
	}
	
	public class FirHOA : Obstacle {
		public FirHOA(Source s, bool template=false){
			NewLabel(TTYPE.HFIR, s, true, template);
			BuildHeart();
		}
		public override string Notes () {return "";}
	}
		
	public class BrassHOA : Obstacle {
		public BrassHOA(Source s, bool template=false){
			NewLabel(TTYPE.HBRA, s, true, template);
			BuildHeart();
		}
		public override string Notes () {return "";}
	}
	
	public class SilkHOA : Obstacle {
		public SilkHOA(Source s, bool template=false){
			NewLabel(TTYPE.HSLK, s, true, template);
			BuildHeart();		
		}
		public override string Notes () {return "";}
	}
	
	public class GlassHOA : Obstacle {
		public GlassHOA(Source s, bool template=false){
			NewLabel(TTYPE.HGLA, s, true, template);
			BuildHeart();
		}
		public override string Notes () {return "";}
	}	
	
	public class BloodHOA : Obstacle {
		public BloodHOA(Source s, bool template=false){
			NewLabel(TTYPE.HBLO, s, true, template);
			BuildHeart();
		}
		public override string Notes () {return "";}
	}
}
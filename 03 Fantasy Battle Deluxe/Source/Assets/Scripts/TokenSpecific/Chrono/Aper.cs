
namespace HOA{
	public class Aperture : Obstacle {
		public Aperture(Source s, bool template=false){
			NewLabel(TTYPE.APER, s, false, template);
			BuildSunken();
		}
		public override string Notes () {return "";}
	}
}
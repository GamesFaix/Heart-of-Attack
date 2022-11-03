
namespace HOA{
	public class Aperture : Obstacle {
		public Aperture(Source s, bool template=false){
			ID = new ID(this, EToken.APER, s, false, template);
			Plane = Plane.Sunk;
		}
		public override string Notes () {return "";}
	}
}

namespace HOA{
	public class Aperture : Obstacle {
		public Aperture(Source s, bool template=false){
			id = new ID(this, EToken.APER, s, false, template);
			plane = Plane.Sunk;
		}
		public override string Notes () {return "";}
	}
}
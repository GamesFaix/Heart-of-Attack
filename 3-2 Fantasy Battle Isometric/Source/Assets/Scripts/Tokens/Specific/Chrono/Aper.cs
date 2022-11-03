
namespace HOA.Tokens {

	public class Aperture : Obstacle {

		public static Token Instantiate (Source source, bool template) {
			return new Aperture (source, template);
		}

		public Aperture other;

		Aperture(Source s, bool template=false){
			ID = new ID(this, EToken.APER, s, false, template);
			Plane = Plane.Sunk;

			Body = new BodyAper(this);

		}
		public override string Notes () {return "0% Functional";}
	}
}
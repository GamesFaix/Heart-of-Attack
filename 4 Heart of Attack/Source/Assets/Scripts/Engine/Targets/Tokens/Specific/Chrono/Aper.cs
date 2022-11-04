
namespace HOA.Tokens {

	public class Aperture : Obstacle {

		public static Token Instantiate (Source source, bool template) {
			return new Aperture (source, template);
		}

		public Aperture other;

		Aperture(Source s, bool template=false){
			ID = new TokenID(this, EToken.APER, s, false, template);
			Plane = Plane.Sunken;

			Body = new BodyAper(this);
            Notes = () => { return "0% Functional"; };
		}
	}
}
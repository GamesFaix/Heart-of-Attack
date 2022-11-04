namespace HOA.Tokens {

	public class Reprospector : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new Reprospector (source, template);
		}

		Reprospector(Source s, bool template=false){
			ID = new TokenID(this, EToken.REPR, s, false, template);
			Plane = Plane.Ground;
			ScaleLarge();

			NewHealth(55);
			NewWatch(2);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Ability[] {
				Ability.Move(this, 4),
				Ability.TimeMine(this),
				Ability.TimeSlam(this),
				Ability.TimeBomb(this)
			});
			Arsenal.Sort();
		}
	}
}
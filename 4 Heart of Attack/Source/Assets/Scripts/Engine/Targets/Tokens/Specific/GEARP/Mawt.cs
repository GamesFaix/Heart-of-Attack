namespace HOA.Tokens {

	public class Mawth : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new Mawth (source, template);
		}

		Mawth (Source s, bool template=false){
			ID = new TokenID(this, EToken.MAWT, s, false, template);
			Plane = Plane.Air;
			ScaleLarge();
			NewHealth(55);
			NewWatch(3);
			BuildArsenal();
		}

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Ability[]{
				Ability.Dart(this, 4),
				Ability.LaserShot(this),
				Ability.Bombard(this)
			});
			Arsenal.Sort();
		}
	}
}
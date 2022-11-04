namespace HOA.Tokens {

	public class Rook : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new Rook (source, template);
		}

		Rook(Source s, bool template=false){
			ID = new TokenID(this, EToken.ROOK, s, false, template);
			Plane = Plane.Ground;
			OnDeath = EToken.ROCK;
			ScaleMedium();
			NewHealth(20,3);
			NewWatch(3);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Ability[]{
				Ability.Rebuild(this),
				Ability.Volley(this)
			});
			Arsenal.Sort();
		}

		
	}
}

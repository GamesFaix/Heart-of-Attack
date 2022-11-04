namespace HOA.Tokens {

	public class Conflagragon : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new Conflagragon (source, template);
		}

		Conflagragon(Source s, bool template=false){
			ID = new TokenID(this, EToken.CONF, s, false, template);
			Plane = Plane.Air;
			OnDeath = EToken.ASHE;
			ScaleMedium();
			NewHealth(30);
			NewWatch(4);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Ability[]{
				Ability.Move(this, 6),
				Ability.Maul(this),
				Ability.FireBreath(this)
			});

			Arsenal.Sort();
		}

		
	}
}
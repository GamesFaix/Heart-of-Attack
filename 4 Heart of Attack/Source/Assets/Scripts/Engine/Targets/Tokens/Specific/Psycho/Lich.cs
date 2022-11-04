namespace HOA.Tokens {

	public class Lichenthrope : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new Lichenthrope (source, template);
		}

		Lichenthrope(Source s, bool template=false){
			ID = new TokenID(this, EToken.LICH, s, false, template);
			Plane = Plane.Ground;
			OnDeath = EToken.NONE;
			ScaleSmall();
			NewHealth(15);
			NewWatch(5);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Ability[]{
				Ability.Move(this, 0),
				Ability.Feed(this),
				Ability.Evolve(this, Price.Cheap, EToken.BEES),
				Ability.Evolve(this, new Price(1,2), EToken.MYCO),
				Ability.Evolve(this, new Price(1,3), EToken.MART)
			});
			Arsenal.Sort();
		}

		
	}
}
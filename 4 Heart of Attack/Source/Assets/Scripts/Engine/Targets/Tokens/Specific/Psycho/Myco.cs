namespace HOA.Tokens {

	public class Mycolonist : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new Mycolonist (source, template);
		}

		Mycolonist(Source s, bool template=false){
			ID = new TokenID(this, EToken.MYCO, s, false, template);
			Plane = Plane.Ground;
			ScaleMedium();
			NewHealth(40);
			NewWatch(2);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Ability[]{
				Ability.Move(this, 2),
				Ability.Sporatic(this),
				Ability.Donate(this),
				Ability.Seed(this)
			});
			Arsenal.Sort();
		}

		
	}
}
namespace HOA.Tokens {
	
	public class RevolvingTom : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new RevolvingTom (source, template);
		}

		RevolvingTom(Source s, bool template=false){
			ID = new TokenID(this, EToken.REVO, s, false, template);
			Plane = Plane.Ground;
			ScaleSmall();

			NewHealth(30);
			NewWatch(4);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Ability[] {
				Ability.Move(this, 3),
				Ability.Shoot(this, 2, 8),
				Ability.Quickdraw(this)
			});
			Arsenal.Sort();
		}
	}
}
namespace HOA.Tokens {

	public class BatteringRambuchet : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new BatteringRambuchet (source, template);
		}

		BatteringRambuchet(Source s, bool template=false){
			ID = new TokenID(this, EToken.BATT, s, false, template);
			Plane = Plane.Ground;
            TargetClass += TargetClasses.Tram;

			ScaleLarge();
			NewHealth(65);
			NewWatch(1);
			BuildArsenal();
		}

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Ability[]{
				Ability.Move(this, 2),
				Ability.Strike(this, 16),
				Ability.Fling(this),
				Ability.Cocktail(this)
			});
			Arsenal.Sort();
		}

		
	}	
}
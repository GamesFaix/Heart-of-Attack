namespace HOA.Tokens {

	public class TalonedScout : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new TalonedScout (source, template);
		}

		TalonedScout(Source s, bool template=false){
			ID = new TokenID(this, EToken.TALO, s, false, template);
			Plane = Plane.Air;
			ScaleMedium();
			NewHealth(35);
			NewWatch(4);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Ability[]{
				Ability.Move(this, 6),
				Ability.Strike(this, 12),
				Ability.ArcticGust(this)
			});
			Arsenal.Sort();
		}
	}
}
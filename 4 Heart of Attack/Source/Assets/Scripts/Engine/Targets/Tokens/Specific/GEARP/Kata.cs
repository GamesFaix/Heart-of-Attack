namespace HOA.Tokens {
	
	public class Katandroid : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new Katandroid (source, template);
		}

		Katandroid(Source s, bool template=false){
			ID = new TokenID(this, EToken.KATA, s, false, template);
			Plane = Plane.Ground;
			ScaleSmall();
			NewHealth(25);
			NewWatch(5);	
			BuildArsenal();
		}

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Ability[]{
				Ability.Move(this, 4),
				Ability.Strike(this, 8),
				Ability.Sprint(this),
				Ability.LaserSpin(this)
			});
			Arsenal.Sort();
		}
	}
}
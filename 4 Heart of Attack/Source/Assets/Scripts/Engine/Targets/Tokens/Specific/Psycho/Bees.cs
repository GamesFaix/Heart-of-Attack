namespace HOA.Tokens {

	public class Beesassin : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new Beesassin (source, template);
		}

		Beesassin(Source s, bool template=false){
			ID = new TokenID(this, EToken.BEES, s, false, template);
			Plane = Plane.Air;
			ScaleSmall();
			NewHealth(25);
			NewWatch(5);
			timers.Add(Timer.Corrosion(new Source(this), this, 12));
			BuildArsenal();
		}	

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Ability[]{
				Ability.Dart(this, 5),
				Ability.Sting(this, 8),
				Ability.FatalBlow(this)
			});
			Arsenal.Sort();
		}

		
	}
}
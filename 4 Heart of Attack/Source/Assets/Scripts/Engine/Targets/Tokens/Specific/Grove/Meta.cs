namespace HOA.Tokens {

	public class Metaterrainean : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new Metaterrainean (source, template);
		}

		Metaterrainean(Source s, bool template=false){
			ID = new TokenID(this, EToken.META, s, false, template);
			Plane = Plane.Ground;
            TargetClass += TargetClasses.Tram;
			OnDeath = EToken.ROCK;

			ScaleLarge();
			NewHealth(50);
			NewWatch(1);
			BuildArsenal();
		}		

		protected override void BuildArsenal() {
			base.BuildArsenal();
			Arsenal.Add(new Ability[] {
				Ability.Move(this, 2),
				Ability.Strike(this, 20),
				Ability.Engorge(this)
			});
			Arsenal.Sort();
		}
    }
}
namespace HOA.Tokens {

	public class Gatecreeper : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new Gatecreeper (source, template);
		}

		Gatecreeper(Source s, bool template=false){
			ID = new TokenID(this, EToken.GATE, s, false, template);
			Plane = Plane.Ground;
            TargetClass += TargetClasses.Tram;
			ScaleLarge();
			NewHealth(30);
			NewWatch(4);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Ability[]{
				Ability.Burrow(this),
				Ability.Recycle(this, new Price(0,1)),
				Ability.Feast(this)
			});
			Arsenal.Sort();
		}

		
	}
}

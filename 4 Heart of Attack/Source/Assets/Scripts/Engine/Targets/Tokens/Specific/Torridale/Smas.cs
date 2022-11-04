namespace HOA.Tokens {

	public class Smashbuckler : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new Smashbuckler (source, template);
		}

		Smashbuckler(Source s, bool template=false){
			ID = new TokenID(this, EToken.SMAS, s, false, template);
			Plane = Plane.Ground;
			ScaleSmall();
			NewHealth(30);
			NewWatch(3);
			BuildArsenal();	
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Ability[]{
				Ability.Move(this, 3),
				Ability.Flail(this),
				Ability.Slam(this)
			});
			Arsenal.Sort();
		}
		
		
	}	
}
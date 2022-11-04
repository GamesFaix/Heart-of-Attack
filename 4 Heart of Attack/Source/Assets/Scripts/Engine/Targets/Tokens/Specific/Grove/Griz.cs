namespace HOA.Tokens {

	public class GrizzlyElder : Unit {

		public static Token Instantiate (Source source, bool template) {
			return new GrizzlyElder (source, template);
		}

		GrizzlyElder(Source s, bool template=false){
			ID = new TokenID(this, EToken.GRIZ, s, false, template);
			Plane = Plane.Ground;
			ScaleSmall();

			NewHealth(25);
			NewWatch(3);
			BuildArsenal();
		}	

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Ability[]{
				Ability.Move(this, 3),
				Ability.Strike(this, 9),
				Ability.Create(this, new Price(0,1), EToken.TREE),
				Ability.Sooth(this)
			});
			Arsenal.Sort();
		}

		
	}
}
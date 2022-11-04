namespace HOA.Tokens {

	public class Gargoliath : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new Gargoliath (source, template);
		}

		Gargoliath(Source s, bool template=false){
			ID = new TokenID(this, EToken.GARG, s, true, template);
			Plane = Plane.Air;
            TargetClass += TargetClasses.King;
            OnDeath = EToken.HSTO;
			ScaleJumbo();
			NewHealth(75);
			NewWatch(3);
			NewWallet(3);
			BuildArsenal();
		}		

		protected override void BuildArsenal() {
			base.BuildArsenal();
			Arsenal.Add(new Ability[]{
				Ability.Move(this, 4),
				Ability.Strike(this, 18),
				Ability.Land(this),
				Ability.Petrify(this),
				Ability.CreateRook(this),
				Ability.Create(this, Price.Cheap, EToken.SMAS),
				Ability.Create(this, new Price(1,1), EToken.CONF),
				Ability.Create(this, new Price(2,2), EToken.BATT)
			});
			Arsenal.Sort();
		}

		
	}	
}
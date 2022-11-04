namespace HOA.Tokens {

	public class BlackWinnow : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new BlackWinnow (source, template);
		}

		BlackWinnow(Source s, bool template=false){
			ID = new TokenID(this, EToken.BLAC, s, true, template);
			Plane = Plane.Ground;
            TargetClass += TargetClasses.King;
            OnDeath = EToken.HSLK;
			ScaleJumbo();
			NewHealth(75);
			NewWatch(3); 
			NewWallet(3);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal ();
			Arsenal.Add(new Ability[]{
				Ability.Move(this, 3),
				Ability.Sting(this, 15),
				Ability.CreateLich(this),
				Ability.WebShot(this)
			});
			Arsenal.Sort();
		}

		
	}
}
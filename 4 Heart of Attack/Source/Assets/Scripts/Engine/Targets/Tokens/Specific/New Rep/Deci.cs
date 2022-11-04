namespace HOA.Tokens {

	public class Decimatrix : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new Decimatrix (source, template);
		}

		Decimatrix(Source s, bool template=false){
			ID = new TokenID(this, EToken.DECI, s, true);
			Plane = Plane.Ground;
            TargetClass += TargetClasses.Tram;
            TargetClass += TargetClasses.King;
			OnDeath = EToken.HSTE;

			ScaleJumbo();
			NewHealth(85);
			NewWatch(2);
			Wallet = new WalletDEF(this, 3, 4);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Ability[]{
				Ability.Tread(this),
				Ability.Shoot(this, 3, 15),
				Ability.Pierce(this, new Price(1,1), 15),
				Ability.Mortar(this),
				//new ADeciFortify(this),
				Ability.Create(this, new Price(1,0), EToken.DEMO),
				Ability.Create(this, new Price(1,1), EToken.MEIN),
				Ability.Create(this, new Price(2,2), EToken.PANO)
			});
			Arsenal.Sort();
		}
	}
}


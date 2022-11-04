namespace HOA.Tokens {

	public class OldThreeHands : Unit {

		public static Token Instantiate (Source source, bool template) {
			return new OldThreeHands (source, template);
		}

		OldThreeHands(Source s, bool template=false){
			ID = new TokenID(this, EToken.OLDT, s, true, template);
			Plane = Plane.Ground;
			TargetClass += TargetClasses.King;
			OnDeath = EToken.HBRA;
			ScaleJumbo();

			NewHealth(85,2);
			NewWatch(2);
			Wallet = new WalletIN(this, 3);
			//NewWallet(3);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();

			Arsenal.Add(new Ability[] {
				Ability.Move(this, 2), 
				Ability.Lob(this, 3, 15),
				Ability.HourSaviour(this),
				Ability.MinuteWaltz(this),
				Ability.SecondInCommand(this),
				Ability.Create(this, Price.Cheap, EToken.REVO),
				Ability.Create(this, new Price(2,0), EToken.PIEC),
				Ability.Create(this, new Price(2,1), EToken.REPR)
			} );
			Arsenal.Sort();
		}
	}
}
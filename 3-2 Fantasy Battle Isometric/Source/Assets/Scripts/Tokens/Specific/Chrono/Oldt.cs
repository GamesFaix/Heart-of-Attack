namespace HOA.Tokens {

	public class OldThreeHands : Unit {

		public static Token Instantiate (Source source, bool template) {
			return new OldThreeHands (source, template);
		}

		OldThreeHands(Source s, bool template=false){
			ID = new ID(this, EToken.OLDT, s, true, template);
			Plane = Plane.Gnd;
			Special.Add(ESpecial.KING);
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

			Arsenal.Add(new Task[] {
				new Actions.Move(this, 2), 
				new Actions.Lob(this, 3, 15),
				new Actions.HourSaviour(this),
				new Actions.MinuteWaltz(this),
				new Actions.SecondInCommand(this),
				new Actions.Create(this, Price.Cheap, EToken.REVO),
				new Actions.Create(this, new Price(2,0), EToken.PIEC),
				new Actions.Create(this, new Price(2,1), EToken.REPR)
			} );
			Arsenal.Sort();
		}

		public override string Notes () {return "";}
	}
}
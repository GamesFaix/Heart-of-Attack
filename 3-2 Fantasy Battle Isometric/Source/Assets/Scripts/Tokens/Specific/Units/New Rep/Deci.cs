namespace HOA.Tokens {

	public class Decimatrix : King {
		public static Token Instantiate (Source source, bool template) {
			return new Decimatrix (source, template);
		}

		Decimatrix(Source s, bool template=false){
			ID = new ID(this, EToken.DECI, s, true);
			Plane = Plane.Ground;
			TokenType type = this.TokenType;
			type.trample = true;
			this.TokenType = type;
			OnDeath = EToken.HSTE;

			ScaleJumbo();
			NewHealth(85);
			NewWatch(2);
			Wallet = new WalletDEF(this, 3, 4);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Task[]{
				new Actions.Tread(this),
				new Actions.Shoot(this, 3, 15),
				new Actions.Pierce(this, new Price(1,1), 15),
				new Actions.Mortar(this),
				//new ADeciFortify(this),
				new Actions.Create(this, new Price(1,0), EToken.DEMO),
				new Actions.Create(this, new Price(1,1), EToken.MEIN),
				new Actions.Create(this, new Price(2,2), EToken.PANO)
			});
			Arsenal.Sort();
		}
		
		public override string Notes () {return "";}
	}
}


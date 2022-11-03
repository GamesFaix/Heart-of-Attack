namespace HOA{
	public class Decimatrix : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new Decimatrix (source, template);
		}

		Decimatrix(Source s, bool template=false){
			ID = new ID(this, EToken.DECI, s, true);
			Plane = Plane.Gnd;
			Special.Add(EType.TRAM);
			Special.Add(EType.KING);
			OnDeath = EToken.HSTE;

			ScaleJumbo();
			NewHealth(85);
			NewWatch(2);
			Wallet = new DEFWallet(this, 3, 4);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Task[]{
				new ADeciMove(this),
				new AShoot(this, 3, 15),
				new APanoPierce(this, new Price(1,1), 15),
				new ADeciMortar(this),
				//new ADeciFortify(this),
				new ACreate(this, new Price(1,0), EToken.DEMO),
				new ACreate(this, new Price(1,1), EToken.MEIN),
				new ACreate(this, new Price(2,2), EToken.PANO)
			});
			Arsenal.Sort();
		}
		
		public override string Notes () {return "";}
	}
}


namespace HOA.Tokens {

	public class Panopticannon : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new Panopticannon (source, template);
		}

		Panopticannon(Source s, bool template=false){
			ID = new TokenID(this, EToken.PANO, s, false, template);
			Plane = Plane.Ground;
            TargetClass += TargetClasses.Tram;
			ScaleLarge();
			NewHealth(65);
			NewWatch(1);
			Wallet = new WalletDEF(this, 2, 2);
			BuildArsenal();
		}	

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Ability[] {
				Ability.Move(this, 1),
				Ability.Cannon(this, Price.Cheap, 12),
				Ability.Pierce(this, new Price(1,2), 20),
			});
			Arsenal.Sort();
		}
		
	}	
}
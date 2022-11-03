namespace HOA.Tokens {

	public class Panopticannon : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new Panopticannon (source, template);
		}

		Panopticannon(Source s, bool template=false){
			ID = new ID(this, EToken.PANO, s, false, template);
			Plane = Plane.Gnd;
			Special.Add(ESpecial.TRAM);
			ScaleLarge();
			NewHealth(65);
			NewWatch(1);
			Wallet = new WalletDEF(this, 2, 2);
			BuildArsenal();
		}	

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Task[] {
				new Actions.Move(this, 1),
				new Actions.Cannon(this, Price.Cheap, 12),
				new Actions.Pierce(this, new Price(1,2), 20),
			});
			Arsenal.Sort();
		}
		public override string Notes () {return "";}
	}	
}
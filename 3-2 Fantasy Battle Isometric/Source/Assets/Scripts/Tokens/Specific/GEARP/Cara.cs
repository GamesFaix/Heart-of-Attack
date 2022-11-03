namespace HOA.Tokens {

	public class CarapaceInvader : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new CarapaceInvader (source, template);
		}

		CarapaceInvader(Source s, bool template=false){
			ID = new ID(this, EToken.CARA, s, false, template);
			Plane = Plane.Gnd;
			Body = new BodySensor9(this, SensorCaraShield.Instantiate);
			ScaleMedium();
			Health = new HealthDEFCap(this, 35, 2, 5);
			NewWatch(4);
			Wallet = new WalletDEF (this, 2, 3);
			BuildArsenal();
		}

		protected override void BuildArsenal() {
			base.BuildArsenal();
			Arsenal.Add(new Task[]{
				new Actions.Move(this, 3),
				new Actions.Shock(this),
				new Actions.Discharge(this)
			});
			Arsenal.Sort();
		}

		public override string Notes () {return "All non-Carapace neighboring teammates add Carapace's Defense.";}
		
		public override void Die (Source s, bool corpse=true, bool log=true) {
			((BodySensor9)Body).DestroySensors();
			base.Die(s,corpse,log);
		}	
	}

}
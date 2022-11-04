namespace HOA.Tokens {

	public class CarapaceInvader : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new CarapaceInvader (source, template);
		}

		CarapaceInvader(Source s, bool template=false){
			ID = new TokenID(this, EToken.CARA, s, false, template);
			Plane = Plane.Ground;
			Body = new BodySensor9(this, Sensor.Carapace);
			ScaleMedium();
			Health = new HealthDEFCap(this, 35, 2, 5);
			NewWatch(4);
			Wallet = new WalletDEF (this, 2, 3);
			BuildArsenal();
            Notes = () => 
            {
                return "All non-Carapace neighboring teammates add Carapace's Defense.";
            };
		}

		protected override void BuildArsenal() {
			base.BuildArsenal();
			Arsenal.Add(new Ability[]{
				Ability.Move(this, 3),
				Ability.Shock(this),
				Ability.Discharge(this)
			});
			Arsenal.Sort();
		}
		
		public override void Die (Source s, bool corpse=true, bool log=true) {
			((BodySensor9)Body).DestroySensors();
			base.Die(s,corpse,log);
		}	
	}

}
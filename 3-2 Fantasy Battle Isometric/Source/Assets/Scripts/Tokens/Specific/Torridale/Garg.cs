namespace HOA.Tokens {

	public class Gargoliath : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new Gargoliath (source, template);
		}

		Gargoliath(Source s, bool template=false){
			ID = new ID(this, EToken.GARG, s, true, template);
			Plane = Plane.Air;
			Special.Add(ESpecial.KING);
			OnDeath = EToken.HSTO;
			ScaleJumbo();
			NewHealth(75);
			NewWatch(3);
			NewWallet(3);
			BuildArsenal();
		}		

		protected override void BuildArsenal() {
			base.BuildArsenal();
			Arsenal.Add(new Task[]{
				new Actions.Move(this, 4),
				new Actions.Strike(this, 18),
				new Actions.Land(this),
				new Actions.Petrify(this),
				new Actions.CreateROOK(this),
				new Actions.Create(this, Price.Cheap, EToken.SMAS),
				new Actions.Create(this, new Price(1,1), EToken.CONF),
				new Actions.Create(this, new Price(2,2), EToken.BATT)
			});
			Arsenal.Sort();
		}

		public override string Notes () {return "";}
	}	
}
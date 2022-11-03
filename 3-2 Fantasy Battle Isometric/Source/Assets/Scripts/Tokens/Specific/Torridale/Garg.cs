namespace HOA{

	public class Gargoliath : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new Gargoliath (source, template);
		}

		Gargoliath(Source s, bool template=false){
			ID = new ID(this, EToken.GARG, s, true, template);
			Plane = Plane.Air;
			Special.Add(EType.KING);
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
				new AMovePath(this, 4),
				new AStrike(this, 18),
				new AGargLand(this),
				new AGargPetrify(this),
				new AGargRook(this),
				new ACreate(this, Price.Cheap, EToken.SMAS),
				new ACreate(this, new Price(1,1), EToken.CONF),
				new ACreate(this, new Price(2,2), EToken.BATT)
			});
			Arsenal.Sort();
		}

		public override string Notes () {return "";}
	}	
}
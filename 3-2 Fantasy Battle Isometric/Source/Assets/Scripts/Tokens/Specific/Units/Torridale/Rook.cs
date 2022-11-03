namespace HOA.Tokens {

	public class Rook : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new Rook (source, template);
		}

		Rook(Source s, bool template=false){
			ID = new ID(this, EToken.ROOK, s, false, template);
			Plane = Plane.Ground;
			OnDeath = EToken.ROCK;
			ScaleMedium();
			NewHealth(20,3);
			NewWatch(3);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Task[]{
				new Actions.Rebuild(this),
				new Actions.Volley(this)
			});
			Arsenal.Sort();
		}

		public override string Notes () {return "";}
	}
}

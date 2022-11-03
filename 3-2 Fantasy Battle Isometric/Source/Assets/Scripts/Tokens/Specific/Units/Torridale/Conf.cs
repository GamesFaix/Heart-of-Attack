namespace HOA.Tokens {

	public class Conflagragon : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new Conflagragon (source, template);
		}

		Conflagragon(Source s, bool template=false){
			ID = new ID(this, EToken.CONF, s, false, template);
			Plane = Plane.Air;
			OnDeath = EToken.ASHE;
			ScaleMedium();
			NewHealth(30);
			NewWatch(4);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Task[]{
				new Actions.Move(this, 6),
				new Actions.Maul(this),
				new Actions.Firebreathing(this)
			});

			Arsenal.Sort();
		}

		public override string Notes () {return "";}
	}
}
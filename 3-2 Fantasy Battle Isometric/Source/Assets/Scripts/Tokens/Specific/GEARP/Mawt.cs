namespace HOA.Tokens {

	public class Mawth : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new Mawth (source, template);
		}

		Mawth (Source s, bool template=false){
			ID = new ID(this, EToken.MAWT, s, false, template);
			Plane = Plane.Air;
			ScaleLarge();
			NewHealth(55);
			NewWatch(3);
			BuildArsenal();
		}

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Task[]{
				new Actions.Dart(this, 4),
				new Actions.LaserShot(this),
				new Actions.Bombard(this)
			});
			Arsenal.Sort();
		}

		public override string Notes () {return "";}
	}
}
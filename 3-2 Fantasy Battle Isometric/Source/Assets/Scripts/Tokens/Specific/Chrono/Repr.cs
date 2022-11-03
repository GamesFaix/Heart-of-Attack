namespace HOA.Tokens {

	public class Reprospector : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new Reprospector (source, template);
		}

		Reprospector(Source s, bool template=false){
			ID = new ID(this, EToken.REPR, s, false, template);
			Plane = Plane.Gnd;
			ScaleLarge();

			NewHealth(55);
			NewWatch(2);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Task[] {
				new Actions.Move(this, 4),
				new Actions.TimeMine(this),
				new Actions.TimeSlam(this),
				new Actions.TimeBomb(this)
			});
			Arsenal.Sort();
		}

		public override string Notes () {return "";}
	}
}
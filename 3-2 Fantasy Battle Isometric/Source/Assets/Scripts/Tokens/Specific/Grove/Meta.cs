namespace HOA.Tokens {

	public class Metaterrainean : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new Metaterrainean (source, template);
		}

		Metaterrainean(Source s, bool template=false){
			ID = new ID(this, EToken.META, s, false, template);
			Plane = Plane.Gnd;
			Special.Add(ESpecial.TRAM);
			OnDeath = EToken.ROCK;

			ScaleLarge();
			NewHealth(50);
			NewWatch(1);
			BuildArsenal();
		}		

		protected override void BuildArsenal() {
			base.BuildArsenal();
			Arsenal.Add(new Task[] {
				new Actions.Move(this, 2),
				new Actions.Strike(this, 20),
				new Actions.Engorge(this)
			});
			Arsenal.Sort();
		}

		public override string Notes () {return "";}
	}
}
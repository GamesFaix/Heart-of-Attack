namespace HOA.Tokens {

	public class Mycolonist : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new Mycolonist (source, template);
		}

		Mycolonist(Source s, bool template=false){
			ID = new ID(this, EToken.MYCO, s, false, template);
			Plane = Plane.Gnd;
			ScaleMedium();
			NewHealth(40);
			NewWatch(2);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Task[]{
				new Actions.Move(this, 2),
				new Actions.Sporatic(this),
				new Actions.Donate(this),
				new Actions.Seed(this)
			});
			Arsenal.Sort();
		}

		public override string Notes () {return "";}
	}
}
namespace HOA{
	public class Lichenthrope : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new Lichenthrope (source, template);
		}

		Lichenthrope(Source s, bool template=false){
			ID = new ID(this, EToken.LICH, s, false, template);
			Plane = Plane.Gnd;
			OnDeath = EToken.NONE;
			ScaleSmall();
			NewHealth(15);
			NewWatch(5);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Task[]{
				new AMovePath(this, 0),
				new ALichFeed(this),
				new AEvolve(this, Price.Cheap, EToken.BEES),
				new AEvolve(this, new Price(1,2), EToken.MYCO),
				new AEvolve(this, new Price(1,3), EToken.MART)
			});
			Arsenal.Sort();
		}

		public override string Notes () {return "";}
	}
}
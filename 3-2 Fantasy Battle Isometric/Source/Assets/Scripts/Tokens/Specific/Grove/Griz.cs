namespace HOA{
	public class GrizzlyElder : Unit {

		public static Token Instantiate (Source source, bool template) {
			return new GrizzlyElder (source, template);
		}

		GrizzlyElder(Source s, bool template=false){
			ID = new ID(this, EToken.GRIZ, s, false, template);
			Plane = Plane.Gnd;
			ScaleSmall();

			NewHealth(25);
			NewWatch(3);
			BuildArsenal();
		}	

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Task[]{
				new AMovePath(this, 3),
				new AStrike(this, 9),
				new ACreate(this, new Price(0,1), EToken.TREE),
				new AGrizHeal(this)
			});
			Arsenal.Sort();
		}

		public override string Notes () {return "";}
	}
}
namespace HOA.Tokens {

	public class GrizzlyElder : Unit {

		public static Token Instantiate (Source source, bool template) {
			return new GrizzlyElder (source, template);
		}

		GrizzlyElder(Source s, bool template=false){
			ID = new ID(this, EToken.GRIZ, s, false, template);
			Plane = Plane.Ground;
			ScaleSmall();

			NewHealth(25);
			NewWatch(3);
			BuildArsenal();
		}	

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Task[]{
				new Actions.Move(this, 3),
				new Actions.Strike(this, 9),
				new Actions.Create(this, new Price(0,1), EToken.TREE),
				new Actions.Sooth(this)
			});
			Arsenal.Sort();
		}

		public override string Notes () {return "";}
	}
}
namespace HOA.Tokens {
	
	public class RevolvingTom : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new RevolvingTom (source, template);
		}

		RevolvingTom(Source s, bool template=false){
			ID = new ID(this, EToken.REVO, s, false, template);
			Plane = Plane.Ground;
			ScaleSmall();

			NewHealth(30);
			NewWatch(4);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Task[] {
				new Actions.Move(this, 3),
				new Actions.Shoot(this, 2, 8),
				new Actions.Quickdraw(this)
			});
			Arsenal.Sort();
		}
		public override string Notes () {return "";}
	}
}
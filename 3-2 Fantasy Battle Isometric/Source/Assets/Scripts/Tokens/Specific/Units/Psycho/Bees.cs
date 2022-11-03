namespace HOA.Tokens {

	public class Beesassin : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new Beesassin (source, template);
		}

		Beesassin(Source s, bool template=false){
			ID = new ID(this, EToken.BEES, s, false, template);
			Plane = Plane.Air;
			ScaleSmall();
			NewHealth(25);
			NewWatch(5);
			timers.Add(new TCorrosion(this, this, 12));
			BuildArsenal();
		}	

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Task[]{
				new Actions.Dart(this, 5),
				new Actions.Sting(this, 8),
				new Actions.FatalBlow(this)
			});
			Arsenal.Sort();
		}

		public override string Notes () {return "";}
	}
}
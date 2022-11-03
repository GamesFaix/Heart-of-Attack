namespace HOA.Tokens {

	public class TalonedScout : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new TalonedScout (source, template);
		}

		TalonedScout(Source s, bool template=false){
			ID = new ID(this, EToken.TALO, s, false, template);
			Plane = Plane.Air;
			ScaleMedium();
			NewHealth(35);
			NewWatch(4);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Task[]{
				new Actions.Move(this, 6),
				new Actions.Strike(this, 12),
				new Actions.ArcticGust(this)
			});
			Arsenal.Sort();
		}

		public override string Notes () {return "";}
	}
}
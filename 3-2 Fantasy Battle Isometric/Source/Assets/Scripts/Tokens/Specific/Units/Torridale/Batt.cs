namespace HOA.Tokens {

	public class BatteringRambuchet : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new BatteringRambuchet (source, template);
		}

		BatteringRambuchet(Source s, bool template=false){
			ID = new ID(this, EToken.BATT, s, false, template);
			Plane = Plane.Ground;
			TokenType type = this.TokenType;
			type.trample = true;
			this.TokenType = type;
			ScaleLarge();
			NewHealth(65);
			NewWatch(1);
			BuildArsenal();
		}

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Task[]{
				new Actions.Move(this, 2),
				new Actions.Strike(this, 16),
				new Actions.Fling(this),
				new Actions.Cocktail(this)
			});
			Arsenal.Sort();
		}

		public override string Notes () {return "";}
	}	
}
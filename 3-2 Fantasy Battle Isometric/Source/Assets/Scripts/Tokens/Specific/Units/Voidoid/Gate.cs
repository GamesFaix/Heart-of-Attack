namespace HOA.Tokens {

	public class Gatecreeper : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new Gatecreeper (source, template);
		}

		Gatecreeper(Source s, bool template=false){
			ID = new ID(this, EToken.GATE, s, false, template);
			Plane = Plane.Ground;
			TokenType type = this.TokenType;
			type.trample = true;
			this.TokenType = type;
			ScaleLarge();
			NewHealth(30);
			NewWatch(4);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Task[]{
				new Actions.Burrow(this),
				new Actions.Recycle(this, new Price(0,1)),
				new Actions.Feast(this)
			});
			Arsenal.Sort();
		}

		public override string Notes () {return "";}
	}
}

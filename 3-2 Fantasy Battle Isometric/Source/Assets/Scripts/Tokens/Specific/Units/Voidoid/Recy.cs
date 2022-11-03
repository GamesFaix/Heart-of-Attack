namespace HOA.Tokens {

	public class Recyclops : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new Recyclops (source, template);
		}

		Recyclops(Source s, bool template=false){
			ID = new ID(this, EToken.RECY, s, false, template);
			Plane = Plane.Ground;
			TokenType type = this.TokenType;
			type.destructible = true;
			this.TokenType = type;
			ScaleSmall();
			NewHealth(15);
			NewWatch(4);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Task[]{
				new Actions.Move(this, 2),
				new Actions.Rage(this, 12),
				new Actions.Burst(this),
				new Actions.Cannibalize(this)
			});
			Arsenal.Sort();
		}

		public override string Notes () {return "";}
	}
}
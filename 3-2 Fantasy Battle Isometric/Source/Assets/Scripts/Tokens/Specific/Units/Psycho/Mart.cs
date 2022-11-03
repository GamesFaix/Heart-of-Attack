namespace HOA.Tokens {

	public class MartianManTrap : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new MartianManTrap (source, template);
		}

		MartianManTrap(Source s, bool template=false){
			ID = new ID(this, EToken.MART, s, false, template);
			Plane = Plane.Ground;
			TokenType type = this.TokenType;
			type.trample = true;
			this.TokenType = type;
			ScaleLarge();
			NewHealth(70);
			NewWatch(4);
			BuildArsenal();
		}	

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Remove("Focus");
			Arsenal.Add(new Task[]{
				new Actions.Creep(this),
				new Actions.Grow(this),
				new Actions.Strike(this, 12),
				new Actions.VineWhip(this)
			});
			Arsenal.Sort();
		}

		public override string Notes () {return "";}
	}
}
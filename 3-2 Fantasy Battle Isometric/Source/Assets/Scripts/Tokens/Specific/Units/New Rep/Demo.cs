namespace HOA.Tokens {

	public class Demolitia : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new Demolitia (source, template);
		}

		Demolitia(Source s, bool template=false){
			ID = new ID(this, EToken.DEMO, s, false, template);
			Plane = Plane.Ground;

			ScaleSmall();
			NewHealth(30);
			NewWatch(3);
			Wallet = new WalletDEF(this, 2, 4);
			BuildArsenal();
		}

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Task[] {
				new Actions.Move(this, 3),
				new Actions.Throw(this),
				new Actions.StickyGrenade(this)
			});
			Arsenal.Sort();
		}
		public override string Notes () {return "";}
	}
}
namespace HOA.Tokens {

	public class Kabutomachine : King {
		public static Token Instantiate (Source source, bool template) {
			return new Kabutomachine (source, template);
		}

		Kabutomachine(Source s, bool template=false){
			ID = new ID(this, EToken.KABU, s, true, template);
			Plane = Plane.Air;
			OnDeath = EToken.HSIL;

			ScaleJumbo();
			NewHealth(75);
			NewWatch(4);
			NewWallet(3);
			BuildArsenal();
		}

		protected override void BuildArsenal() {
			base.BuildArsenal();
			Arsenal.Add(new Task[]{
				new Actions.Dart(this, 5),
				new Actions.Strike(this, 16),
				new Actions.Warp(this),
				new Actions.GammaBurst(this),
				new Actions.Create(this, Price.Cheap, EToken.KATA),
				new Actions.Create(this, new Price(2,1), EToken.CARA),
				new Actions.Create(this, new Price(2,2), EToken.MAWT)
			});
			Arsenal.Sort();
		}
		public override string Notes () {return "";}
	}
}
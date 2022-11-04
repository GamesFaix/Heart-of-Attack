namespace HOA.Tokens {

	public class Kabutomachine : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new Kabutomachine (source, template);
		}

		Kabutomachine(Source s, bool template=false){
			ID = new TokenID(this, EToken.KABU, s, true, template);
			Plane = Plane.Air;
            TargetClass += TargetClasses.King;
			OnDeath = EToken.HSIL;

			ScaleJumbo();
			NewHealth(75);
			NewWatch(4);
			NewWallet(3);
			BuildArsenal();
		}

		protected override void BuildArsenal() {
			base.BuildArsenal();
			Arsenal.Add(new Ability[]{
				Ability.Dart(this, 5),
				Ability.Strike(this, 16),
				Ability.Warp(this),
				Ability.GammaBurst(this),
				Ability.Create(this, Price.Cheap, EToken.KATA),
				Ability.Create(this, new Price(2,1), EToken.CARA),
				Ability.Create(this, new Price(2,2), EToken.MAWT)
			});
			Arsenal.Sort();
		}
	}
}
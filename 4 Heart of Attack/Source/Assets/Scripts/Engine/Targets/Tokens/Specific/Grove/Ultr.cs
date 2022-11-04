namespace HOA.Tokens {

	public class Ultratherium : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new Ultratherium (source, template);
		}

		Ultratherium(Source s, bool template=false){
			ID = new TokenID(this, EToken.ULTR, s, true, template);
			Plane = Plane.Ground;
            TargetClass += TargetClasses.King;
            TargetClass += TargetClasses.Tram;
			OnDeath = EToken.HFIR;

			ScaleJumbo();
			NewHealth(80);
			NewWatch(2);
			NewWallet(3);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Ability[]{
				Ability.Move(this, 3),
				Ability.Strike(this, 16),
				Ability.ThrowTerrain(this),
				Ability.IceBlast(this),
				Ability.Create(this, Price.Cheap, EToken.GRIZ),
				Ability.Create(this, new Price(1,1), EToken.TALO),
				Ability.Animate(this)
			});
			Arsenal.Sort();
		}

		
	}	
}
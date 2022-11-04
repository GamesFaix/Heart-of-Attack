namespace HOA.Tokens {

	public class Monolith : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new Monolith (source, template);
		}

		Monolith(Source s, bool template=false){
			ID = new TokenID(this, EToken.MONO, s, true, template);
			Plane = Plane.Tall;
            TargetClass += TargetClasses.King;
			OnDeath = EToken.HBLO;
			ScaleTall();
			NewHealth(100);
			NewWatch(2);
			NewWallet(3);
			BuildArsenal();
		}	

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Ability[]{
				Ability.Move(this, 4),
				Ability.Rage(this, 20),
				Ability.DeathField(this),
				Ability.BloodAltar(this),
				Ability.Create(this, new Price(1,0), EToken.RECY),
				Ability.Recycle(this, new Price(1,0)),
				Ability.Create(this, new Price(2,1), EToken.NECR),
				Ability.CreateArc(this, new Price(1,2), EToken.GATE, 3,3)
			});
			Arsenal.Sort();
		}

		
	}
}
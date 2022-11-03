namespace HOA.Tokens {

	public class Monolith : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new Monolith (source, template);
		}

		Monolith(Source s, bool template=false){
			ID = new ID(this, EToken.MONO, s, true, template);
			Plane = Plane.Tall;
			Special.Add(ESpecial.KING);
			OnDeath = EToken.HBLO;
			ScaleTall();
			NewHealth(100);
			NewWatch(2);
			NewWallet(3);
			BuildArsenal();
		}	

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Task[]{
				new Actions.Move(this, 4),
				new Actions.Rage(this, 20),
				new Actions.DeathField(this),
				new Actions.BloodAltar(this),
				new Actions.Create(this, new Price(1,0), EToken.RECY),
				new Actions.Recycle(this, new Price(1,0)),
				new Actions.Create(this, new Price(2,1), EToken.NECR),
				new Actions.CreateArc(this, new Price(1,2), EToken.GATE, 3,3)
			});
			Arsenal.Sort();
		}

		public override string Notes () {return "";}
	}
}
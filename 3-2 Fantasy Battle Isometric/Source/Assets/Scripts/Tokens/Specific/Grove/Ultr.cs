namespace HOA.Tokens {

	public class Ultratherium : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new Ultratherium (source, template);
		}

		Ultratherium(Source s, bool template=false){
			ID = new ID(this, EToken.ULTR, s, true, template);
			Plane = Plane.Gnd;
			Special.Add(ESpecial.TRAM);
			Special.Add(ESpecial.KING);
			OnDeath = EToken.HFIR;

			ScaleJumbo();
			NewHealth(80);
			NewWatch(2);
			NewWallet(3);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Task[]{
				new Actions.Move(this, 3),
				new Actions.Strike(this, 16),
				new Actions.ThrowTerrain(this),
				new Actions.IceBlast(this),
				new Actions.Create(this, Price.Cheap, EToken.GRIZ),
				new Actions.Create(this, new Price(1,1), EToken.TALO),
				new Actions.Animate(this)
			});
			Arsenal.Sort();
		}

		public override string Notes () {return "";}
	}	
}
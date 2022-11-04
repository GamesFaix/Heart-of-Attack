namespace HOA.Tokens {

	public class Piecemaker : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new Piecemaker (source, template);
		}

		Piecemaker(Source s, bool template=false){
			ID = new TokenID(this, EToken.PIEC, s, false, template);
			Plane = Plane.Ground;
			ScaleMedium();

			NewHealth(35,3);
			NewWatch(1); 
			BuildArsenal();

		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Ability[] {
				Ability.Move(this, 4),
				Ability.Strike(this, 10),
				Ability.CreateArc(this, new Price(1,1), EToken.APER, 2),
				Ability.Repair(this)
			});
			Arsenal.Sort();
		}
    }
}

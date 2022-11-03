namespace HOA.Tokens {

	public class Piecemaker : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new Piecemaker (source, template);
		}

		Piecemaker(Source s, bool template=false){
			ID = new ID(this, EToken.PIEC, s, false, template);
			Plane = Plane.Gnd;
			ScaleMedium();

			NewHealth(35,3);
			NewWatch(1); 
			BuildArsenal();

		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Task[] {
				new Actions.Move(this, 4),
				new Actions.Strike(this, 10),
				new Actions.CreateArc(this, new Price(1,1), EToken.APER, 2),
				new Actions.Repair(this)
			});
			Arsenal.Sort();
		}

		public override string Notes () {return "";}
	}
}

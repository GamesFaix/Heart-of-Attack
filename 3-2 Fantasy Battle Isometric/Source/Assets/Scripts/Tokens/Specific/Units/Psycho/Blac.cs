namespace HOA.Tokens {

	public class BlackWinnow : King {
		public static Token Instantiate (Source source, bool template) {
			return new BlackWinnow (source, template);
		}

		BlackWinnow(Source s, bool template=false){
			ID = new ID(this, EToken.BLAC, s, true, template);
			Plane = Plane.Ground;
			OnDeath = EToken.HSLK;
			ScaleJumbo();
			NewHealth(75);
			NewWatch(3); 
			NewWallet(3);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal ();
			Arsenal.Add(new Task[]{
				new Actions.Move(this, 3),
				new Actions.Sting(this, 15),
				new Actions.CreateLICH(this),
				new Actions.WebShot(this)
			});
			Arsenal.Sort();
		}

		public override string Notes () {return "";}
	}
}
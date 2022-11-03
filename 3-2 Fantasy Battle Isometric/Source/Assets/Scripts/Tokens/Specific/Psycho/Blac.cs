namespace HOA{
	public class BlackWinnow : Unit {
		public BlackWinnow(Source s, bool template=false){
			ID = new ID(this, EToken.BLAC, s, true, template);
			Plane = Plane.Gnd;
			Special.Add(EType.KING);
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
				new AMovePath(this, 3),
				new ASting(this, 15),
				new ABlacLich(this),
				new ABlacWeb(this)
			});
			Arsenal.Sort();
		}

		public override string Notes () {return "";}
	}
}
namespace HOA{
	public class OldThreeHands : Unit {
		public OldThreeHands(Source s, bool template=false){
			ID = new ID(this, EToken.OLDT, s, true, template);
			Plane = Plane.Gnd;
			Special.Add(EType.KING);
			OnDeath = EToken.HBRA;
			ScaleJumbo();

			NewHealth(85,2);
			NewWatch(2);
			Wallet = new INWallet(this, 3);
			//NewWallet(3);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();

			Arsenal.Add(new Task[] {
				new AMovePath(this, 2), 
				new AVolley(this, 3, 15),
				new AOldtHour(this),
				new AOldMinute(this),
				new AOldtSecond(this),
				new ACreate(this, Price.Cheap, EToken.REVO),
				new ACreate(this, new Price(2,0), EToken.PIEC),
				new ACreate(this, new Price(2,1), EToken.REPR)
			} );
			Arsenal.Sort();
		}

		public override string Notes () {return "";}
	}
}
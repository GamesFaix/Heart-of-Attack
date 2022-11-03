namespace HOA{
	public class Ultratherium : Unit {
		public Ultratherium(Source s, bool template=false){
			ID = new ID(this, EToken.ULTR, s, true, template);
			Plane = Plane.Gnd;
			Special.Add(EType.TRAM);
			Special.Add(EType.KING);
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
				new AMovePath(this, 3),
				new AStrike(this, 16),
				new AUltrThrow(this),
				new AUltrBlast(this),
				new ACreate(this, Price.Cheap, EToken.GRIZ),
				new ACreate(this, new Price(1,1), EToken.TALO),
				new AUltrCreateMeta(this)
			});
			Arsenal.Sort();
		}

		public override string Notes () {return "";}
	}	
}
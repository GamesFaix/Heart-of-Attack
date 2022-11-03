namespace HOA {
	public class Kabutomachine : Unit {
	
		public Kabutomachine(Source s, bool template=false){
			ID = new ID(this, EToken.KABU, s, true, template);
			Plane = Plane.Air;
			Special.Add(EType.KING);
			OnDeath = EToken.HSIL;

			ScaleJumbo();
			NewHealth(75);
			NewWatch(4);
			NewWallet(3);
			BuildArsenal();
		}

		protected override void BuildArsenal() {
			base.BuildArsenal();
			Arsenal.Add(new Task[]{
				new AMoveLine(this, 5),
				new AStrike(this, 16),
				new AKabuTeleport(this),
				new AKabuLaser(this),
				new ACreate(this, Price.Cheap, EToken.KATA),
				new ACreate(this, new Price(2,1), EToken.CARA),
				new ACreate(this, new Price(2,2), EToken.MAWT)
			});
			Arsenal.Sort();
		}
		public override string Notes () {return "";}
	}
}
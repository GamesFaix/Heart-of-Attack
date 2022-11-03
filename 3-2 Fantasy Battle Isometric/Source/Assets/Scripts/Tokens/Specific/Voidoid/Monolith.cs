namespace HOA{
	public class Monolith : Unit {
		public Monolith(Source s, bool template=false){
			ID = new ID(this, EToken.MONO, s, true, template);
			Plane = Plane.Tall;
			Special.Add(EType.KING);
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
				new AMovePath(this, 4),
				new ARage(this, 20),
				new AMonoField(this),
				new AMonoAltar(this),
				new ACreate(this, new Price(1,0), EToken.RECY),
				new AMonoReanimate(this, new Price(1,0)),
				new ACreate(this, new Price(2,1), EToken.NECR),
				new ACreateArc(this, new Price(1,2), EToken.GATE, 3,3)
			});
			Arsenal.Sort();
		}

		public override string Notes () {return "";}
	}
}
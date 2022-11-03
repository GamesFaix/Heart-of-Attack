namespace HOA{
	public class Rook : Unit {
		public Rook(Source s, bool template=false){
			ID = new ID(this, EToken.ROOK, s, false, template);
			Plane = Plane.Gnd;
			OnDeath = EToken.ROCK;
			ScaleMedium();
			NewHealth(20,3);
			NewWatch(3);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Task[]{
				new ARookMove(this),
				new ARookVolley(this)
			});
			Arsenal.Sort();
		}

		public override string Notes () {return "";}
	}
}

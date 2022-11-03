namespace HOA{
	
	public class RevolvingTom : Unit {
		public RevolvingTom(Source s, bool template=false){
			ID = new ID(this, EToken.REVO, s, false, template);
			Plane = Plane.Gnd;
			ScaleSmall();

			NewHealth(30);
			NewWatch(4);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Task[] {
				new AMovePath(this, 3),
				new AShoot(this, 2, 8),
				new ARevoQuick(this)
			});
			Arsenal.Sort();
		}
		public override string Notes () {return "";}
	}
}
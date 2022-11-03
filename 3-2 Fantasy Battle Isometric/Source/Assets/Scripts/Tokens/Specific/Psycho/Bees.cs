namespace HOA{
	public class Beesassin : Unit {
		public Beesassin(Source s, bool template=false){
			ID = new ID(this, EToken.BEES, s, false, template);
			Plane = Plane.Air;
			ScaleSmall();
			NewHealth(25);
			NewWatch(5);
			timers.Add(new TCorrosion(this, this, 12));
			BuildArsenal();
		}	

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Task[]{
				new AMoveLine(this, 5),
				new ASting(this, 8),
				new ABeesFatalBlow(this)
			});
			Arsenal.Sort();
		}

		public override string Notes () {return "";}
	}
}
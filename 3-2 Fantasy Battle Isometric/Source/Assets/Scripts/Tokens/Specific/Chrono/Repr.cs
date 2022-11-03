namespace HOA{
	public class Reprospector : Unit {
		public Reprospector(Source s, bool template=false){
			ID = new ID(this, EToken.REPR, s, false, template);
			Plane = Plane.Gnd;
			ScaleLarge();

			NewHealth(55);
			NewWatch(2);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Task[] {
				new AMovePath(this, 4),
				new AReprMine(this),
				new AReprSlam(this),
				new AReprBomb(this)
			});
			Arsenal.Sort();
		}

		public override string Notes () {return "";}
	}
}
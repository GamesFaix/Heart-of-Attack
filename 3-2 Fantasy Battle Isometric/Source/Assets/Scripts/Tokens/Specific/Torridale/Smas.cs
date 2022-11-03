namespace HOA{
	public class Smashbuckler : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new Smashbuckler (source, template);
		}

		Smashbuckler(Source s, bool template=false){
			ID = new ID(this, EToken.SMAS, s, false, template);
			Plane = Plane.Gnd;
			ScaleSmall();
			NewHealth(30);
			NewWatch(3);
			BuildArsenal();	
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Task[]{
				new AMovePath(this, 3),
				new ASmasFlail(this),
				new ASmasSlam(this)
			});
			Arsenal.Sort();
		}
		
		public override string Notes () {return "";}
	}	
}
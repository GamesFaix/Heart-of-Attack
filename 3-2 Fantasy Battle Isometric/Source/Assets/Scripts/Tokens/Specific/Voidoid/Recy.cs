namespace HOA{
	public class Recyclops : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new Recyclops (source, template);
		}

		Recyclops(Source s, bool template=false){
			ID = new ID(this, EToken.RECY, s, false, template);
			Plane = Plane.Gnd;
			Special.Add(EType.DEST);
			Special.Add(EType.REM);
			ScaleSmall();
			NewHealth(15);
			NewWatch(4);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Task[]{
				new AMovePath(this, 2),
				new ARage(this, 12),
				new ARecyExplode(this),
				new ARecyCannibal(this)
			});
			Arsenal.Sort();
		}

		public override string Notes () {return "";}
	}
}
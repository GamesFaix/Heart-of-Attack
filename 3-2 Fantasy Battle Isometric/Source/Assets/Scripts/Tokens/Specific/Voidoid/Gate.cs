namespace HOA{
	public class Gatecreeper : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new Gatecreeper (source, template);
		}

		Gatecreeper(Source s, bool template=false){
			ID = new ID(this, EToken.GATE, s, false, template);
			Plane = Plane.Gnd;
			Special.Add(EType.TRAM);

			ScaleLarge();
			NewHealth(30);
			NewWatch(4);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Task[]{
				new AGateBurrow(this),
				new AMonoReanimate(this, new Price(0,1)),
				new AGateFeast(this)
			});
			Arsenal.Sort();
		}

		public override string Notes () {return "";}
	}
}

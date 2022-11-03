namespace HOA.Tokens {

	public class Ashes : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new Ashes (source, template);
		}

		Ashes(Source s, bool template=false){
			ID = new ID(this, EToken.ASHE, s, false, template);
			Plane = Plane.Gnd;
			Special.Add(ESpecial.DEST);
			OnDeath = EToken.NONE;
			ScaleSmall();
			NewHealth(15);
			NewWatch(5);
			BuildArsenal();
		}	
		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Remove("Focus");
			Arsenal.Add(new Actions.Arise(this));
			Arsenal.Sort();
		}

		public override string Notes () {return "";}
	}	
}
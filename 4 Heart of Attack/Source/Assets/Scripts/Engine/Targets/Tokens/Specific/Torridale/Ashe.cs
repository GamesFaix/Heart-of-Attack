namespace HOA.Tokens {

	public class Ashes : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new Ashes (source, template);
		}

		Ashes(Source s, bool template=false){
			ID = new TokenID(this, EToken.ASHE, s, false, template);
			Plane = Plane.Ground;
            TargetClass += TargetClasses.Dest;
			OnDeath = EToken.NONE;
			ScaleSmall();
			NewHealth(15);
			NewWatch(5);
			BuildArsenal();
		}	
		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Remove("Focus");
			Arsenal.Add(Ability.Arise(this));
			Arsenal.Sort();
		}

		
	}	
}
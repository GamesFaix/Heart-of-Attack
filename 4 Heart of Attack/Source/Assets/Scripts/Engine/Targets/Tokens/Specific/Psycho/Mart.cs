namespace HOA.Tokens {

	public class MartianManTrap : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new MartianManTrap (source, template);
		}

		MartianManTrap(Source s, bool template=false){
			ID = new TokenID(this, EToken.MART, s, false, template);
			Plane = Plane.Ground;
            TargetClass += TargetClasses.Tram;
            ScaleLarge();
			NewHealth(70);
			NewWatch(4);
			BuildArsenal();
		}	

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Remove("Focus");
			Arsenal.Add(new Ability[]{
				Ability.Creep(this),
				Ability.Grow(this),
				Ability.Strike(this, 12),
				Ability.VineWhip(this)
			});
			Arsenal.Sort();
		}

		
	}
}
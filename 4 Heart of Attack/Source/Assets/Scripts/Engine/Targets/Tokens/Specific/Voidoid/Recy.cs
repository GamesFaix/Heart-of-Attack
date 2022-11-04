namespace HOA.Tokens {

	public class Recyclops : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new Recyclops (source, template);
		}

		Recyclops(Source s, bool template=false){
			ID = new TokenID(this, EToken.RECY, s, false, template);
			Plane = Plane.Ground;
			TargetClass += TargetClasses.Dest;
			TargetClass += TargetClasses.Corpse;
			ScaleSmall();
			NewHealth(15);
			NewWatch(4);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Ability[]{
				Ability.Move(this, 2),
				Ability.Rage(this, 12),
				Ability.Burst(this),
				Ability.Cannibalize(this)
			});
			Arsenal.Sort();
		}

		
	}
}
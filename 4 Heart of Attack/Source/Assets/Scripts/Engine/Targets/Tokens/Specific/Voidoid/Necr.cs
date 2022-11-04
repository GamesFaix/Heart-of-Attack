using System.Collections.Generic;

namespace HOA.Tokens {

	public class Necrochancellor : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new Necrochancellor (source, template);
		}

		Necrochancellor(Source s, bool template=false){
			ID = new TokenID(this, EToken.NECR, s, false, template);
			Plane = Plane.Ethereal;
			OnDeath = EToken.NONE;
			ScaleMedium();
			NewHealth(30,5);
			NewWatch(3);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Ability[]{
				Ability.Move(this, 3),
				Ability.Defile(this),
				Ability.TouchOfDeath(this)
			});
			Arsenal.Sort();
		}

		
	}
}
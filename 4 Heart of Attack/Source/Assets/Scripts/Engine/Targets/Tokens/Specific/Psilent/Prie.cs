using System.Collections.Generic;

namespace HOA.Tokens {

	public class PriestOfNaja : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new PriestOfNaja (source, template);
		}

		PriestOfNaja(Source s, bool template=false){
			ID = new TokenID(this, EToken.PRIE, s, false, template);
			Plane = Plane.Ground;
			ScaleLarge();
			NewHealth(50,2);
			NewWatch(4);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Ability[]{
				Ability.Move(this, 4),
				Ability.Strike(this, 15),
				Ability.Shove(this)
			});
			Arsenal.Sort();
		}

		
	}
		











}
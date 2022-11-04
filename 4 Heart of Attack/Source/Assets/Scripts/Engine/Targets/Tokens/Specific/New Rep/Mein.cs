using UnityEngine;
using System.Collections.Generic;

namespace HOA.Tokens {

	public class MeinSchutz : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new MeinSchutz (source, template);
		}

		MeinSchutz(Source s, bool template=false){
			ID = new TokenID(this, EToken.MEIN, s, false, template);
			Plane = Plane.Ground;

			ScaleMedium();
			NewHealth(40);
			NewWatch(4);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Ability[]{
				Ability.Move(this, 5),
				Ability.Shoot(this, 2, 12),
				Ability.Create(this, new Price(0,1), EToken.MINE),
				Ability.Detonate(this)
			});
			Arsenal.Sort();
		}
	}
}


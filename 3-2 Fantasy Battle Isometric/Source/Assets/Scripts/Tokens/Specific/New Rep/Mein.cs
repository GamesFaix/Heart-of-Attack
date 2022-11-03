using UnityEngine;
using System.Collections.Generic;

namespace HOA.Tokens {

	public class MeinSchutz : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new MeinSchutz (source, template);
		}

		MeinSchutz(Source s, bool template=false){
			ID = new ID(this, EToken.MEIN, s, false, template);
			Plane = Plane.Gnd;

			ScaleMedium();
			NewHealth(40);
			NewWatch(4);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Task[]{
				new Actions.Move(this, 5),
				new Actions.Shoot(this, 2, 12),
				new Actions.Create(this, new Price(0,1), EToken.MINE),
				new Actions.Detonate(this)
			});
			Arsenal.Sort();
		}

		public override string Notes () {return "";}
	}
}


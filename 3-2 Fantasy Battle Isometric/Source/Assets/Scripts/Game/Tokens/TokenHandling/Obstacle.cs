using System.Collections.Generic;
using UnityEngine;

namespace HOA {

	public abstract class Obstacle : Token{

		public Obstacle () {
			body = new Body(this);
			type = new Type(EClass.OB);
			plane = Plane.Gnd;
		}

		protected void BuildHeart () {
			plane = Plane.Tall;
			type = new Type(new List<EClass> {EClass.OB, EClass.HEART});
			Neutralize();
		}

		protected void Neutralize () {
			Owner = Roster.Neutral;
		}
	}
}
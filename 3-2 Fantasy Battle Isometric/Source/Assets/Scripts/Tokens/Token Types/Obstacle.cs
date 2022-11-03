using System.Collections.Generic;
using UnityEngine;

namespace HOA {

	public abstract class Obstacle : Token{

		public Obstacle () {
			Body = new Body(this);
			Plane = Plane.Ground;
			OnDeath = EToken.NONE;
		}

		protected void Neutralize () {
			Owner = Roster.Neutral;
		}
	}
}
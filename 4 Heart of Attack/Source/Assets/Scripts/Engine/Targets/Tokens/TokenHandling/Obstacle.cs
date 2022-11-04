using System.Collections.Generic;
using UnityEngine;

namespace HOA {

	public abstract class Obstacle : Token{

		public Obstacle () {
			Body = new Body(this);
			TargetClass += TargetClasses.Ob;
			Plane = Plane.Ground;
			OnDeath = EToken.NONE;
		}

		protected void BuildHeart () {
			Plane = Plane.Tall;
            TargetClass += TargetClasses.Heart;
			Neutralize();
		}

		protected void Neutralize () {
			Owner = Roster.Neutral;
		}
	}
}
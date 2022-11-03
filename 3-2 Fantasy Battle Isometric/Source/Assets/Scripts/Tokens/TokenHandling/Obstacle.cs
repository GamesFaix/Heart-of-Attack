using System.Collections.Generic;
using UnityEngine;

namespace HOA {

	public abstract class Obstacle : Token{

		public Obstacle () {
			Body = new Body(this);
			Special = new Special (EType.TOKEN);
			Special.Add(EType.OB);
			Plane = Plane.Gnd;
			OnDeath = EToken.NONE;
		}

		protected void BuildHeart () {
			Plane = Plane.Tall;
			Special = new Special(new List<EType> {EType.OB, EType.HEART});
			Neutralize();
		}

		protected void Neutralize () {
			Owner = Roster.Neutral;
		}
	}
}
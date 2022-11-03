using System.Collections.Generic;
using UnityEngine;

namespace HOA {

	public abstract class Obstacle : Token{

		public Obstacle () {
			Body = new Body(this);
			Special = new Special (ESpecial.TOKEN);
			Special.Add(ESpecial.OB);
			Plane = Plane.Gnd;
			OnDeath = EToken.NONE;
		}

		protected void BuildHeart () {
			Plane = Plane.Tall;
			Special = new Special(new List<ESpecial> {ESpecial.OB, ESpecial.HEART});
			Neutralize();
		}

		protected void Neutralize () {
			Owner = Roster.Neutral;
		}
	}
}
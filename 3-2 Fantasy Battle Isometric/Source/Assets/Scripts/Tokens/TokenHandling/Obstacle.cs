﻿using System.Collections.Generic;
using UnityEngine;

namespace HOA {

	public abstract class Obstacle : Token{

		public Obstacle () {
			body = new Body(this);
			type = new Special(EType.OB);
			plane = Plane.Gnd;
		}

		protected void BuildHeart () {
			plane = Plane.Tall;
			type = new Special(new List<EType> {EType.OB, EType.HEART});
			Neutralize();
		}

		protected void Neutralize () {
			Owner = Roster.Neutral;
		}
	}
}
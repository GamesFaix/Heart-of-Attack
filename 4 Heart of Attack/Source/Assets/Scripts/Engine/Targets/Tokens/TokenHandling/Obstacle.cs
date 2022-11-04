using System.Collections.Generic;
using UnityEngine;

namespace HOA {

	public partial class Obstacle : Token{

		protected Obstacle (Source source, Species species, string name, bool unique=false, bool template=false) 
            : base (source, species, name, unique, template)
        {
			Body = new Body(this);
			Plane = Plane.Ground;
			OnDeath = Species.None;

		}

		protected void Neutralize () {
			Owner = Roster.Neutral;
		}
	}
}
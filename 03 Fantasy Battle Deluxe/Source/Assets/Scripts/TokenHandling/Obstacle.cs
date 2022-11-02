using System.Collections.Generic;
using UnityEngine;

namespace HOA {

	public abstract class Obstacle : Token{
		
		protected void BuildStandard () {
			sprite = new HOA.Sprite(this);
			NewBody(PLANE.GND);
		}
		
		protected void BuildTall () {
			sprite = new HOA.Sprite(this);
			NewBody(new PLANE[] {PLANE.GND, PLANE.AIR});
		}
		
		protected void BuildSunken () {
			sprite = new HOA.Sprite(this);
			NewBody(PLANE.SUNK);
		}
		
		protected void AddDest () {
			AddSpecial(SPECIAL.DEST);	
		}
		
		protected void AddRem () {
			AddSpecial(SPECIAL.DEST);
			AddSpecial(SPECIAL.REM);
		}
		
		protected void BuildHeart () {
			sprite = new HOA.Sprite(this);
			NewBody(new PLANE[]{PLANE.GND, PLANE.AIR}, SPECIAL.HOA);
			Neutralize();
		}

		protected void Neutralize () {
			Owner = Roster.Neutral;
		}
		
	}
}
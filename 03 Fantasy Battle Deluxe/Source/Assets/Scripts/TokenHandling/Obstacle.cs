using System.Collections.Generic;
using UnityEngine;
using HOA.Map;
using HOA.Tokens.Components;
using HOA.Players;

namespace HOA.Tokens {

	public abstract class Obstacle : Token{
		
		protected void BuildStandard () {
			sprite = new HOASprite(this);
			NewBody(PLANE.GND);
		}
		
		protected void BuildTall () {
			sprite = new HOASprite(this);
			NewBody(new PLANE[] {PLANE.GND, PLANE.AIR});
		}
		
		protected void BuildSunken () {
			sprite = new HOASprite(this);
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
			sprite = new HOASprite(this);
			NewBody(new PLANE[]{PLANE.GND, PLANE.AIR}, SPECIAL.HOA);
		}
		
	}
}
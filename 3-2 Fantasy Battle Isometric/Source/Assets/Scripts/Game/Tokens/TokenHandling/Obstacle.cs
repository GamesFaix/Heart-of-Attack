using System.Collections.Generic;
using UnityEngine;

namespace HOA {

	public abstract class Obstacle : Token{
		
		protected void BuildStandard () {
			//sprite = new HOA.Sprite(this);
			NewBody(EPlane.GND, EClass.OB);
		}
		
		protected void BuildTall () {
			//sprite = new HOA.Sprite(this);
			NewBody(new List<EPlane> {EPlane.GND, EPlane.AIR}, EClass.OB);
		}
		
		protected void BuildSunken () {
			//sprite = new HOA.Sprite(this);
			NewBody(EPlane.SUNK, EClass.OB);
		}
		
		protected void AddDest () {
			AddClass(EClass.DEST);	
		}
		
		protected void AddRem () {
			AddClass(EClass.DEST);
			AddClass(EClass.REM);
		}
		
		protected void BuildHeart () {
			//sprite = new HOA.Sprite(this);
			NewBody(new List<EPlane> {EPlane.GND, EPlane.AIR}, new List<EClass> {EClass.HEART, EClass.OB});
			Neutralize();
		}

		protected void Neutralize () {
			Owner = Roster.Neutral;
		}
		
	}
}
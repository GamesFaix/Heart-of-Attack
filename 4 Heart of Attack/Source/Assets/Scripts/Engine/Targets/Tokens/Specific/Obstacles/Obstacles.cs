using UnityEngine;
using System.Collections;

namespace HOA.Tokens {


	public class Hill : Obstacle {
		public static Token Instantiate (Source source, bool template) {
			return new Hill (source, template);
		}
		Hill(Source s, bool template=false){
			ID = new TokenID(this, EToken.HILL, s, false, template);
			ScaleLarge();
			Neutralize();
		}
		
	}

	public class Hole : Obstacle {
		public static Token Instantiate (Source source, bool template) {
			return new Hole (source, template);
		}
		Hole(Source s, bool template=false){
			ID = new TokenID(this, EToken.HOLE, s, false, template);
			Plane = Plane.HalfSunk;
			ScaleLarge();
			Neutralize();
		}
		
	}







}

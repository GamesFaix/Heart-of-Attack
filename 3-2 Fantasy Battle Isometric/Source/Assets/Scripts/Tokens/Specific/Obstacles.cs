using UnityEngine;
using System.Collections;

namespace HOA {

	public class Mountain : Obstacle {
		public Mountain(Source s, bool template=false){
			ID = new ID(this, EToken.MNTN, s, false, template);
			Plane = Plane.Tall;	
			ScaleTall();
			Neutralize();
		}
		public override string Notes () {return "";}
	}

	public class Hill : Obstacle {
		public Hill(Source s, bool template=false){
			ID = new ID(this, EToken.HILL, s, false, template);
			ScaleLarge();
			Neutralize();
		}
		public override string Notes () {return "";}
	}

	public class Rock : Obstacle {
		public Rock(Source s, bool template=false){
			ID = new ID(this, EToken.ROCK, s, false, template);
			Special.Add(EType.DEST);	
			ScaleMedium();
			Neutralize();
		}
		public override string Notes () {return "";}
	}

	public class Tree : Obstacle {
		public Tree(Source s, bool template=false){
			ID = new ID(this, EToken.TREE, s, false, template);
			Special.Add(EType.DEST);	
			ScaleMedium();
			Neutralize();
		}
		public override string Notes () {return "";}
	}

	public class Corpse : Obstacle {
		public Corpse(Source s, bool template=false){
			ID = new ID(this, EToken.CORP, s, false, template);
			Special.Add(EType.DEST);
			Special.Add(EType.REM);
			ScaleMedium();
			Neutralize();
		}
		public override string Notes () {return "";}
	}
}

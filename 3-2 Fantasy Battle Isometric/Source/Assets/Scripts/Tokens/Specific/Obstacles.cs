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

	public class Pyramid : Obstacle {
		public Pyramid(Source s, bool template=false){
			ID = new ID(this, EToken.PYRA, s, false, template);
			Plane = Plane.Tall;	
			ScaleTall();
			Neutralize();
		}
		public override string Notes () {return "";}
	}

	public class Pylon : Obstacle {
		public Pylon(Source s, bool template=false){
			ID = new ID(this, EToken.PYLO, s, false, template);
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

	public class Hole : Obstacle {
		public Hole(Source s, bool template=false){
			ID = new ID(this, EToken.HOLE, s, false, template);
			Plane = Plane.GndSunk;
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
	public class Tree2 : Obstacle {
		public Tree2(Source s, bool template=false){
			ID = new ID(this, EToken.TREE2, s, false, template);
			Special.Add(EType.DEST);	
			ScaleMedium();
			Neutralize();
		}
		public override string Notes () {return "";}
	}	
	public class Tree3 : Obstacle {
		public Tree3(Source s, bool template=false){
			ID = new ID(this, EToken.TREE3, s, false, template);
			Special.Add(EType.DEST);	
			ScaleMedium();
			Neutralize();
		}
		public override string Notes () {return "";}
	}	
	public class Tree4 : Obstacle {
		public Tree4(Source s, bool template=false){
			ID = new ID(this, EToken.TREE4, s, false, template);
			Special.Add(EType.DEST);	
			ScaleMedium();
			Neutralize();
		}
		public override string Notes () {return "";}
	}
	public class House : Obstacle {
		public House(Source s, bool template=false){
			ID = new ID(this, EToken.HOUS, s, false, template);
			Special.Add(EType.DEST);	
			ScaleMedium();
			Neutralize();
		}
		public override string Notes () {return "";}
	}
	public class Cottage : Obstacle {
		public Cottage(Source s, bool template=false){
			ID = new ID(this, EToken.COTT, s, false, template);
			Special.Add(EType.DEST);	
			ScaleMedium();
			Neutralize();
		}
		public override string Notes () {return "";}
	}

	public class Rampart : Obstacle {
		public Rampart(Source s, bool template=false){
			ID = new ID(this, EToken.RAMP, s, false, template);
			Special.Add(EType.DEST);	
			ScaleMedium();
			Neutralize();
		}
		public override string Notes () {return "";}
	}

	public class Temple : Obstacle {
		public Temple(Source s, bool template=false){
			ID = new ID(this, EToken.TEMP, s, false, template);
			Special.Add(EType.DEST);	
			ScaleMedium();
			Neutralize();
		}
		public override string Notes () {return "";}
	}

	public class Antenna : Obstacle {
		public Antenna(Source s, bool template=false){
			ID = new ID(this, EToken.ANTE, s, false, template);
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

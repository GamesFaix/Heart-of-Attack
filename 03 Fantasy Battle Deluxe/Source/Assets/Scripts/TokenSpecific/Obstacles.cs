using UnityEngine;
using System.Collections;
using HOA.Tokens.Components;
using HOA.Map;

namespace HOA.Tokens {

	public class Mountain : Obstacle {
		public Mountain(Source s, bool template=false){
			NewLabel(TTYPE.MNTN, s, false, template);
			BuildTall();	
			Neutralize();
		}
		public override string Notes () {return "";}
	}

	public class Hill : Obstacle {
		public Hill(Source s, bool template=false){
			NewLabel(TTYPE.HILL, s, false, template);
			BuildStandard();	
			Neutralize();
		}
		public override string Notes () {return "";}
	}

	public class Rock : Obstacle {
		public Rock(Source s, bool template=false){
			NewLabel(TTYPE.ROCK, s, false, template);
			BuildStandard();
			AddDest();	
			Neutralize();
		}
		public override string Notes () {return "";}
	}

	public class Tree : Obstacle {
		public Tree(Source s, bool template=false){
			NewLabel(TTYPE.TREE, s, false, template);
			BuildStandard();
			AddDest();	
			Neutralize();
		}
		public override string Notes () {return "";}
	}

	public class Corpse : Obstacle {
		public Corpse(Source s, bool template=false){
			NewLabel(TTYPE.CORP, s, false, template);
			BuildStandard();
			AddRem();	
			Neutralize();
		}
		public override string Notes () {return "";}
	}

	public class Water : Obstacle {
		public Water(Source s, bool template=false){
			NewLabel(TTYPE.WATR, s, false, template);
			BuildSunken();	
			Neutralize();
		}
		public override string Notes () {return "";}
	}

	public class Lava : Obstacle {
		public Lava(Source s, bool template=false){
			NewLabel(TTYPE.LAVA, s, false, template);
			BuildSunken();	
			Neutralize();
		}
		public override string Notes () {return "";}
	}
}

using UnityEngine;
using System.Collections;

namespace HOA {

	public class Mountain : Obstacle {
		public Mountain(Source s, bool template=false){
			NewLabel(EToken.MNTN, s, false, template);
			BuildTall();	
			Neutralize();
		}
		public override string Notes () {return "";}
	}

	public class Hill : Obstacle {
		public Hill(Source s, bool template=false){
			NewLabel(EToken.HILL, s, false, template);
			BuildStandard();	
			Neutralize();
		}
		public override string Notes () {return "";}
	}

	public class Rock : Obstacle {
		public Rock(Source s, bool template=false){
			NewLabel(EToken.ROCK, s, false, template);
			BuildStandard();
			AddDest();	
			Neutralize();
		}
		public override string Notes () {return "";}
	}

	public class Tree : Obstacle {
		public Tree(Source s, bool template=false){
			NewLabel(EToken.TREE, s, false, template);
			BuildStandard();
			AddDest();	
			Neutralize();
		}
		public override string Notes () {return "";}
	}

	public class Corpse : Obstacle {
		public Corpse(Source s, bool template=false){
			NewLabel(EToken.CORP, s, false, template);
			BuildStandard();
			AddRem();	
			Neutralize();
		}
		public override string Notes () {return "";}
	}
}

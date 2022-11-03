using UnityEngine; 

namespace HOA { 

	public abstract class Target {
	
		/*public virtual bool Select (Source s) {return true;}
		//	if (this is Token) {GUISelectors.Instance = (Token)this;}
		//	if (this is Cell) {GUISelectors.Cell = (Cell)this;}
		//}
*/
		public TargetDisplay Display {get; set;}

		bool legal;
		public bool Legal {
			get {return legal;} 
			set {
				legal = value;
				if (legal) {Display.Legal = true;}
				else {Display.Legal = false;}
			}
		}
	
	
	
	
	
	
	}
}

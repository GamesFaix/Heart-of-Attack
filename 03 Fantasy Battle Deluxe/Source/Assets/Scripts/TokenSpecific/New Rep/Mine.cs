using UnityEngine;

namespace HOA{
	public class Mine : Obstacle {
		public Mine(Source s, bool template=false){
			NewLabel(TTYPE.MINE, s, false, template);
			BuildSunken();
			AddDest();
		}
		public override string Notes () {return "";}
		
		public override void Die (Source s, bool corpse = false, bool log=true) {
			if (this == GUIInspector.Inspected) {GUIInspector.Inspected = default(Token);}
			TokenFactory.Remove(this);
//			Cell oldCell = Cell;
			Exit();
			if (log && !IsSpecial(SPECIAL.HOA)) {GameLog.Out(s.ToString()+" destroyed "+this+".");}
			InputBuffer.Submit(new RExplosion (new Source(this), Cell, 12));
			
		}
	}
}
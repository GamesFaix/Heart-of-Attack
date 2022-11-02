
namespace HOA{
	
	public class Web : Obstacle {
		public Web(Source s, bool template=false){
			NewLabel(TTYPE.WEBB, s, false, template);
			BuildSunken();
			AddDest();
		}
		public override string Notes () {return "";}
		
		public override bool Enter (Cell cell) {
			bool enter = body.Enter(cell);
			if (enter) {DamageCellmates();}
			return enter;
		}
		
		void DamageCellmates () {
			foreach (Token t in CellMates){
				if (t is Unit) {
					InputBuffer.Submit(new RDamage(new Source(this), (Unit)t, 12));
				}
			}
		}
	}
}

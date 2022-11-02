using HOA.Map;

namespace HOA.Tokens.Components {
	
	public abstract class Sensor {
		protected Unit parent;
		protected Cell cell;

		public Cell Cell {get {return cell;} }
		public Unit Parent {get {return parent;} }
		
		public abstract void Enter (Cell c);
		public abstract void Exit ();

		public abstract void OtherEnter (Token t);
		public abstract void OtherExit (Token t);
		
		public void Delete () {
			Exit();
			cell.RemoveSensor(this);
		}
		
	}
}
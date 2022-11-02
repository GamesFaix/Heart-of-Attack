using HOA.Map;

namespace HOA.Tokens.Components {
	
	public abstract class Sensor {
		protected Unit parent;
		protected Cell cell;

		public Cell Cell () {return cell;}
		public Unit Parent () {return parent;}
		
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